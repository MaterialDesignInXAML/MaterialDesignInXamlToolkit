using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace mdresgen
{
    internal class IconDataFactorySyntaxRewriter : CSharpSyntaxRewriter
    {
        private readonly IEnumerable<Icon> _icons;

        public IconDataFactorySyntaxRewriter(IEnumerable<Icon> icons, bool visitIntoStructuredTrivia = false) : base(visitIntoStructuredTrivia)
        {
            _icons = icons ?? throw new ArgumentNullException(nameof(icons));
        }

        public override SyntaxNode VisitInitializerExpression(InitializerExpressionSyntax node)
        {
            if (node.Kind() != SyntaxKind.CollectionInitializerExpression)
                return node;

            var initializerExpressions = GetInitializerItems(_icons);
            var complexElementInitializerExpression = SyntaxFactory.InitializerExpression(SyntaxKind.ComplexElementInitializerExpression, initializerExpressions);

            return complexElementInitializerExpression;
        }

        private static SeparatedSyntaxList<ExpressionSyntax> GetInitializerItems(
            IEnumerable<Icon> icons)
        {
            var comma = SyntaxFactory.Token(SyntaxKind.CommaToken);
            var trailingTriviaList = SyntaxTriviaList.Create(SyntaxFactory.ElasticCarriageReturnLineFeed);
            var separator = SyntaxFactory.Identifier(SyntaxTriviaList.Empty, comma.Text, trailingTriviaList);

            var initializerExpressionSyntaxList = icons.Select(icon =>
            {
                //Indent
                var indent = SyntaxTriviaList.Create(SyntaxFactory.Whitespace("            "));

                //create a member access expression
                var memberAccessExpressionSyntax =
                    SyntaxFactory.MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression,
                        SyntaxFactory.IdentifierName("PackIconKind"),
                        SyntaxFactory.IdentifierName(icon.Name));

                //create a string literal expression
                var literalExpressionSyntax = SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression,
                    SyntaxFactory.Literal(icon.Data));

                var separatedSyntaxList = SyntaxFactory.SeparatedList(new ExpressionSyntax[] { memberAccessExpressionSyntax, literalExpressionSyntax });
                var initializerExpressionSyntax = SyntaxFactory.InitializerExpression(SyntaxKind.ComplexElementInitializerExpression, separatedSyntaxList)
                    .WithLeadingTrivia(indent);
                return (ExpressionSyntax)initializerExpressionSyntax;
            }).ToList();

            var result = SyntaxFactory.SeparatedList(initializerExpressionSyntaxList, Enumerable.Repeat(separator, initializerExpressionSyntaxList.Count - 1));

            return result;
        }
    }
}