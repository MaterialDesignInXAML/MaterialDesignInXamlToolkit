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
        private readonly IEnumerable<Tuple<string, string>> _nameDataPairs;

        public IconDataFactorySyntaxRewriter(IEnumerable<Tuple<string, string>> nameDataPairs, bool visitIntoStructuredTrivia = false) : base(visitIntoStructuredTrivia)
        {
            if (nameDataPairs == null) throw new ArgumentNullException(nameof(nameDataPairs));

            _nameDataPairs = nameDataPairs;
        }

        public override SyntaxNode VisitInitializerExpression(InitializerExpressionSyntax node)
        {
            if (node.Kind() != SyntaxKind.CollectionInitializerExpression)
                return node;

            var initialiserExpressions = GetInitializerItems(_nameDataPairs);
            var complexElementInitializerExpression = SyntaxFactory.InitializerExpression(SyntaxKind.ComplexElementInitializerExpression, initialiserExpressions);

            return complexElementInitializerExpression;
        }

        private SeparatedSyntaxList<ExpressionSyntax> GetInitializerItems(
            IEnumerable<Tuple<string, string>> nameDataPairs)
        {
            var initializerExpressionSyntaxList = nameDataPairs.Select(pair =>
            {
                //create a member access expression            
                var memberAccessExpressionSyntax =
                    SyntaxFactory.MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression,
                        SyntaxFactory.IdentifierName("PackIconKind"),
                        SyntaxFactory.IdentifierName((string) pair.Item1));

                //create a string literal expression
                var literalExpressionSyntax = SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression,
                    SyntaxFactory.Literal((string) pair.Item2));

                var separatedSyntaxList = SyntaxFactory.SeparatedList<ExpressionSyntax>(new ExpressionSyntax[] { memberAccessExpressionSyntax, literalExpressionSyntax });
                var initializerExpressionSyntax = SyntaxFactory.InitializerExpression(SyntaxKind.ComplexElementInitializerExpression, separatedSyntaxList);
                return (ExpressionSyntax)initializerExpressionSyntax;
            });

            var result = SyntaxFactory.SeparatedList(initializerExpressionSyntaxList);

            return result;
        }
    }
}