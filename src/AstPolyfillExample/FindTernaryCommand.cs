using System.Collections.Generic;
using System.Management.Automation;
using System.Management.Automation.Language;

namespace AstPolyfillExample
{
    [Cmdlet(VerbsCommon.Find, "Ternary")]
    public sealed class FindTernaryCommand : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        [ValidateNotNull]
        public ScriptBlock ScriptBlock { get; set; }

        protected override void BeginProcessing()
        {
            var visitor = new MyAstVisitor();
            ScriptBlock.Ast.Visit(visitor);
            WriteObject(visitor.Found, enumerateCollection: true);
        }

        private class MyAstVisitor : AstVisitor, IAstPostVisitHandler
        {
            public readonly List<TernaryExpressionAst> Found = new List<TernaryExpressionAst>();

            public void PostVisit(Ast ast)
            {
                if (ast is TernaryExpressionAst ternary)
                {
                    Found.Add(ternary);
                }
            }
        }
    }
}
