using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Humanizer;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using Newtonsoft.Json.Linq;

namespace mdresgen
{
    class IconThing
    {
        public void Run()
        {            
            Console.WriteLine("Downloading icon data...");

            //var nameDataPairs = GetNameDataPairs(GetSourceData()).ToList();

//            Console.WriteLine(nameDataPairs.Count);

            UpdateEnum("IconType.template.cs");
        }

        private static string GetSourceData()
        {
            var webRequest = WebRequest.CreateDefault(
                new Uri("https://materialdesignicons.com/api/package/38EF63D0-4744-11E4-B3CF-842B2B6CFE1B"));
            webRequest.UseDefaultCredentials = true;
            using (var sr = new StreamReader(webRequest.GetResponse().GetResponseStream()))
            {
                var iconData = sr.ReadToEnd();

                Console.WriteLine("Got.");

                return iconData;
            }
        }

        private static IEnumerable<Tuple<string, string>> GetNameDataPairs(string sourceData)
        {
            var jObject = JObject.Parse(sourceData);
            return jObject["icons"].Select(t => new Tuple<string, string>(
                t["name"].ToString().Underscore().Pascalize(), 
                t["data"].ToString()));
        }



        private void UpdateEnum(string sourceFile)
        {
            var sourceText = SourceText.From(new FileStream(sourceFile, FileMode.Open));
            var syntaxTree = CSharpSyntaxTree.ParseText(sourceText);

            var newSyntaxTree = CSharpSyntaxTree.ParseText("Red, Green, Blue");
            var syntaxNodes = newSyntaxTree.GetRoot().ChildNodes().ToList();            

            var enumDeclarationSyntax = syntaxTree.GetRoot().ChildNodes()
                //should be the root name space
                .Single()
                .ChildNodes().OfType<EnumDeclarationSyntax>()
                .Last();
            

            var emptyEnumDeclarationSyntax = enumDeclarationSyntax.RemoveNodes(enumDeclarationSyntax.ChildNodes().OfType<EnumMemberDeclarationSyntax>(), SyntaxRemoveOptions.KeepNoTrivia);

            var generatedEnumDeclarationSyntax = emptyEnumDeclarationSyntax.AddMembers(
                SyntaxFactory.EnumMemberDeclaration("Aston"),
                SyntaxFactory.EnumMemberDeclaration("Villa"));

            var generatedSyntaxTree = generatedEnumDeclarationSyntax.SyntaxTree;

            var rootSyntaxNode = enumDeclarationSyntax.SyntaxTree.GetRoot();
            var replaceNode = rootSyntaxNode.ReplaceNode(rootSyntaxNode.ChildNodes().Single(), generatedSyntaxTree.GetRoot());



            var modifiedSyntaxTree = syntaxTree.GetRoot().ReplaceNode(emptyEnumDeclarationSyntax, generatedEnumDeclarationSyntax);


            var nodes = emptyEnumDeclarationSyntax.ChildNodes().ToList();
            //emptyEnumDeclarationSyntax.InsertNodesAfter()



            //emptyEnumDeclarationSyntax.AddMembers(new EnumMemberDeclarationSyntax(new CSharpSyntaxNode(new GreenNode(), ))

            //enumDeclarationSyntax.ReplaceNodes(enumDeclarationSyntax.ChildNodes().OfType<EnumMemberDeclarationSyntax>(), ).ReplaceSyntax(enumDeclarationSyntax.SyntaxTree,)

            //            enumDeclarationSyntax.

            //          enumDeclarationSyntax.Re



            //EnumMemberDeclarationSyntax.P

            //var enumMemberDeclarationSyntax = enumDeclarationSyntax.ChildNodes().OfType<EnumMemberDeclarationSyntax>().Single();




        }

    }
}
