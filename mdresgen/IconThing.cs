using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Humanizer;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using Newtonsoft.Json.Linq;

namespace mdresgen
{
    static class IconThing
    {
        private static HttpClient Client { get; } = new();

        public static async Task RunAsync()
        {
            Console.WriteLine("Downloading icon data...");

            var nameDataPairs = GetIcons(await GetSourceDataAsync()).ToList();
            Console.WriteLine("Items: " + nameDataPairs.Count);

            //var nameDataPairs = GetNameDataPairs("TEST").ToList();

            Console.WriteLine("Updating enum...");
            var newEnumSource = UpdateEnum("PackIconKind.template.cs", nameDataPairs);
            Write(newEnumSource, Path.Combine("MaterialDesignThemes.Wpf", "PackIconKind.cs"));

            Console.WriteLine("Updating data factory...");
            var newDataFactorySource = UpdateDataFactory("PackIconDataFactory.template.cs", nameDataPairs);
            Write(newDataFactorySource, Path.Combine("MaterialDesignThemes.Wpf", "PackIconDataFactory.cs"));

            Console.WriteLine("done");
        }

        private static void Write(string content, string filePath)
        {
            for(string? currentDirectory = Path.GetFullPath(".");
                !string.IsNullOrEmpty(Path.GetDirectoryName(currentDirectory));
                currentDirectory = Path.GetDirectoryName(currentDirectory))
            {
                string path = Path.Combine(currentDirectory!, filePath);
                if (File.Exists(path))
                {
                    File.WriteAllText(path, content);
                    return;
                }
            }
            Console.WriteLine($"WARNING: Failed to find '{filePath}'");
        }

        private static async Task<string> GetSourceDataAsync()
        {
            return await Client.GetStringAsync(
                    "https://materialdesignicons.com/api/package/38EF63D0-4744-11E4-B3CF-842B2B6CFE1B");
        }

        private static IEnumerable<Icon> GetIcons(string sourceData)
        {
            var jObject = JObject.Parse(sourceData);
            var icons = new[] { new Icon("None", string.Empty, new List<string>()) } //Add None value always to Enum at first place
                .Concat(
                   jObject["icons"]?.Select(t => new Icon(
                   t["name"]?.ToString().Underscore().Pascalize() ?? throw new Exception("Failed to find name"),
                   t["data"]?.ToString() ?? throw new Exception("Failed to find data"),
                   t["aliases"]?.ToObject<IEnumerable<string>>()?.Select(x => x.Underscore().Pascalize()).ToList()
                        ?? throw new Exception("Failed to find aliases"))) ?? throw new Exception("failed to find icons")
                );

            var iconsByName = new Dictionary<string, Icon>(StringComparer.OrdinalIgnoreCase);
            foreach(Icon icon in icons)
            {
                if (iconsByName.ContainsKey(icon.Name))
                {
                    Console.WriteLine($"Ignoring duplicate icon '{icon.Name}'");
                }
                else
                {
                    iconsByName.Add(icon.Name, icon);
                }
            }

            //Clean up aliases to avoid naming collisions
            var seenAliases = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (var icon in iconsByName.Values)
            {
                seenAliases.Add(icon.Name);
            }
            foreach (Icon icon in iconsByName.Values)
            {
                for (int i = icon.Aliases.Count - 1; i >= 0; i--)
                {
                    string alias = icon.Aliases[i];
                    if (iconsByName.ContainsKey(alias) || !IsValidIdentifier(alias) || seenAliases.Add(alias) == false)
                    {
                        icon.Aliases.RemoveAt(i);
                    }
                }
            }

            return iconsByName.Values.OrderBy(x => x.Name);

            bool IsValidIdentifier(string identifier)
            {
                return identifier?.Length > 0 && (char.IsLetter(identifier[0]) || identifier[0] == '_');
            }
        }

        private static string UpdateDataFactory(string sourceFile, IEnumerable<Icon> icons)
        {
            var sourceText = SourceText.From(new FileStream(sourceFile, FileMode.Open));
            var syntaxTree = CSharpSyntaxTree.ParseText(sourceText);

            var mySyntaxRewriter = new IconDataFactorySyntaxRewriter(icons);
            var node = mySyntaxRewriter.Visit(syntaxTree.GetRoot());

            return node.ToString();
        }

        private static string UpdateEnum(string sourceFile, IEnumerable<Icon> icons)
        {
            var sourceText = SourceText.From(new FileStream(sourceFile, FileMode.Open));
            var syntaxTree = CSharpSyntaxTree.ParseText(sourceText);

            var rootNode = syntaxTree.GetRoot();
            var namespaceDeclarationNode = rootNode.ChildNodes().Single();
            var enumDeclarationSyntaxNode = namespaceDeclarationNode.ChildNodes().OfType<EnumDeclarationSyntax>().Single();

            var emptyEnumDeclarationSyntaxNode = enumDeclarationSyntaxNode.RemoveNodes(enumDeclarationSyntaxNode.ChildNodes().OfType<EnumMemberDeclarationSyntax>(), SyntaxRemoveOptions.KeepDirectives)
                ?? throw new Exception("Removed all nodes");

            var leadingTriviaList = SyntaxTriviaList.Create(SyntaxFactory.Whitespace("        "));
            var enumMemberDeclarationSyntaxs = icons.SelectMany(GetEnumMemberDeclarations).ToArray();
            var generatedEnumDeclarationSyntax = emptyEnumDeclarationSyntaxNode.AddMembers(enumMemberDeclarationSyntaxs);

            generatedEnumDeclarationSyntax = AddLineFeedsToCommas(generatedEnumDeclarationSyntax);

            var generatedNamespaceDeclarationSyntaxNode = namespaceDeclarationNode.ReplaceNode(enumDeclarationSyntaxNode, generatedEnumDeclarationSyntax);
            var generatedRootNode = rootNode.ReplaceNode(namespaceDeclarationNode, generatedNamespaceDeclarationSyntaxNode);

            return generatedRootNode.ToFullString();

            IEnumerable<EnumMemberDeclarationSyntax> GetEnumMemberDeclarations(Icon icon)
            {
                var emptyAttributes = new SyntaxList<AttributeListSyntax>();
                SyntaxToken iconIdentifierToken = SyntaxFactory.Identifier(leadingTriviaList, icon.Name, SyntaxTriviaList.Empty);

                yield return SyntaxFactory.EnumMemberDeclaration(iconIdentifierToken);
                foreach (string alias in icon.Aliases.OrderBy(x => x))
                {
                    yield return SyntaxFactory.EnumMemberDeclaration(emptyAttributes,
                        SyntaxFactory.Identifier(leadingTriviaList, alias, SyntaxTriviaList.Empty),
                        SyntaxFactory.EqualsValueClause(SyntaxFactory.IdentifierName(icon.Name)));
                }
            }
        }

        private static EnumDeclarationSyntax AddLineFeedsToCommas(EnumDeclarationSyntax enumDeclarationSyntax)
        {
            var none = new SyntaxToken();
            var trailingTriviaList = SyntaxTriviaList.Create(SyntaxFactory.ElasticCarriageReturnLineFeed);

            Func<EnumDeclarationSyntax, SyntaxToken> next = enumSyntax => enumSyntax.ChildNodesAndTokens()
                .Where(nodeOrToken => nodeOrToken.IsToken)
                .Select(nodeOrToken => nodeOrToken.AsToken())
                .FirstOrDefault(
                    token =>
                        token.Value?.Equals(",") == true &&
                        (!token.HasTrailingTrivia || !token.TrailingTrivia.Any(SyntaxKind.EndOfLineTrivia)));

            SyntaxToken current;
            while ((current = next(enumDeclarationSyntax)) != none)
            {
                enumDeclarationSyntax = enumDeclarationSyntax.ReplaceToken(current,
                    SyntaxFactory.Identifier(SyntaxTriviaList.Empty, ",", trailingTriviaList)
                    );
            }

            return enumDeclarationSyntax;
        }
    }
}
