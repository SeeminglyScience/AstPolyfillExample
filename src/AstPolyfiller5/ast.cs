namespace System.Management.Automation.Language
{
    /// <summary>
    /// An AST representing a syntax element chainable with '&amp;&amp;' or '||'.
    /// </summary>
    public abstract class ChainableAst : PipelineBaseAst
    {
        /// <summary>
        /// Initializes a new instance of the new chainable AST with the given extent.
        /// </summary>
        /// <param name="extent">The script extent of the AST.</param>
        protected ChainableAst(IScriptExtent extent) : base(extent)
        {
        }
    }

    /// <summary>
    /// A command-oriented flow-controlled pipeline chain.
    /// E.g. <c>npm build &amp;&amp; npm test</c> or <c>Get-Content -Raw ./file.txt || "default"</c>.
    /// </summary>
    public abstract class PipelineChainAst : ChainableAst
    {
        /// <summary>
        /// Initializes a new instance of the new statement chain AST from two statements and an operator.
        /// </summary>
        /// <param name="extent">The extent of the chained statement.</param>
        /// <param name="lhsChain">The pipeline or pipeline chain to the left of the operator.</param>
        /// <param name="rhsPipeline">The pipeline to the right of the operator.</param>
        /// <param name="chainOperator">The operator used.</param>
        /// <param name="background">True when this chain has been invoked with the background operator, false otherwise.</param>
        public PipelineChainAst(
            IScriptExtent extent,
            ChainableAst lhsChain,
            PipelineAst rhsPipeline,
            TokenKind chainOperator,
            bool background = false)
            : base(extent)
        {
        }

        /// <summary>
        /// Gets the left hand pipeline in the chain.
        /// </summary>
        public ChainableAst LhsPipelineChain => default;

        /// <summary>
        /// Gets the right hand pipeline in the chain.
        /// </summary>
        public PipelineAst RhsPipeline => default;

        /// <summary>
        /// Gets the chaining operator used.
        /// </summary>
        public TokenKind Operator => default;

        /// <summary>
        /// Gets a flag that indicates whether this chain has been invoked with the background operator.
        /// </summary>
        public bool Background => default;

        /// <summary>
        /// Create a copy of this Ast.
        /// </summary>
        /// <returns>
        /// A fresh copy of this PipelineChainAst instance.
        /// </returns>
        public override Ast Copy() => null;
    }

    /// <summary>
    /// The ast representing a ternary expression, e.g. <c>$a ? 1 : 2</c>.
    /// </summary>
    public abstract class TernaryExpressionAst : ExpressionAst
    {
        /// <summary>
        /// Initializes a new instance of the a ternary expression.
        /// </summary>
        /// <param name="extent">The extent of the expression.</param>
        /// <param name="condition">The condition operand.</param>
        /// <param name="ifTrue">The if clause.</param>
        /// <param name="ifFalse">The else clause.</param>
        public TernaryExpressionAst(IScriptExtent extent, ExpressionAst condition, ExpressionAst ifTrue, ExpressionAst ifFalse)
            : base(extent)
        {
        }

        /// <summary>
        /// Gets the ast for the condition of the ternary expression. The property is never null.
        /// </summary>
        public ExpressionAst Condition => default;

        /// <summary>
        /// Gets the ast for the if-operand of the ternary expression. The property is never null.
        /// </summary>
        public ExpressionAst IfTrue => default;

        /// <summary>
        /// Gets the ast for the else-operand of the ternary expression. The property is never null.
        /// </summary>
        public ExpressionAst IfFalse => default;

        /// <summary>
        /// Copy the TernaryExpressionAst instance.
        /// </summary>
        /// <return>
        /// Retirns a copy of the ast.
        /// </return>
        public override Ast Copy() => default;
    }
}
