#region Usings

using System.Linq;
using System;
using System.Text;
using Ektron.Cms.Search;
using Ektron.Cms.Search.Expressions;

#endregion

namespace GoodlyFere.Ektron.Linq.Tests.Tools.Visitors
{
    internal class TestExpressionVisitor : ExpressionVisitor
    {
        #region Constants and Fields

        private readonly StringBuilder _expressionString;

        #endregion

        #region Constructors and Destructors

        public TestExpressionVisitor()
        {
            _expressionString = new StringBuilder();
        }

        #endregion

        #region Public Methods

        public override string ToString()
        {
            return _expressionString.ToString();
        }

        public override void Visit(Expression expression)
        {
            expression.Accept(this);
        }

        public override void Visit(AndExpression expression)
        {
            _expressionString.Append(" (");
            Visit(expression.Left);
            _expressionString.Append(" && ");
            Visit(expression.Right);
            _expressionString.Append(") ");
        }

        public override void Visit(OrExpression expression)
        {
            _expressionString.Append(" (");
            Visit(expression.Left);
            _expressionString.Append(" || ");
            Visit(expression.Right);
            _expressionString.Append(") ");
        }

        public override void Visit(PropertyExpression expression)
        {
            _expressionString.AppendFormat(" PE'{0}' ", expression.Name);
        }

        public override void Visit(StringPropertyExpression expression)
        {
            _expressionString.AppendFormat(" STRIPE'{0}' ", expression.Name);
        }

        public override void Visit(IntegerPropertyExpression expression)
        {
            _expressionString.AppendFormat(" INTEPE'{0}' ", expression.Name);
        }

        public override void Visit(DecimalPropertyExpression expression)
        {
            _expressionString.AppendFormat(" DECIPE'{0}' ", expression.Name);
        }

        public override void Visit(BooleanPropertyExpression expression)
        {
            _expressionString.AppendFormat(" BOOLPE'{0}' ", expression.Name);
        }

        public override void Visit(DatePropertyExpression expression)
        {
            _expressionString.AppendFormat(" DATEPE'{0}' ", expression.Name);
        }

        public override void Visit(IntegerValueExpression expression)
        {
            _expressionString.AppendFormat(" INTEVE'{0}' ", expression.Value.ToString());
        }

        public override void Visit(StringValueExpression expression)
        {
            _expressionString.AppendFormat(" STRIVE'{0}' ", expression.Value);
        }

        public override void Visit(QuotedStringValueExpression expression)
        {
            _expressionString.AppendFormat(" QUOSVE'{0}' ", expression.Value);
        }

        public override void Visit(DecimalValueExpression expression)
        {
            _expressionString.AppendFormat(" DECIVE'{0}' ", expression.Value.ToString());
        }

        public override void Visit(BooleanValueExpression expression)
        {
            _expressionString.AppendFormat(" BOOLVE'{0}' ", expression.Value.ToString());
        }

        public override void Visit(DateValueExpression expression)
        {
            _expressionString.AppendFormat(" DATEVE'{0}' ", expression.Value.ToString());
        }

        public override void Visit(EqualsExpression expression)
        {
            _expressionString.Append(" (");
            Visit(expression.Left);
            _expressionString.Append(" == ");
            Visit(expression.Right);
            _expressionString.Append(") ");
        }

        public override void Visit(NotEqualsExpression expression)
        {
            _expressionString.Append(" (");
            Visit(expression.Left);
            _expressionString.Append(" != ");
            Visit(expression.Right);
            _expressionString.Append(") ");
        }

        public override void Visit(LessThanExpression expression)
        {
            _expressionString.Append(" (");
            Visit(expression.Left);
            _expressionString.Append(" < ");
            Visit(expression.Right);
            _expressionString.Append(") ");
        }

        public override void Visit(GreaterThanExpression expression)
        {
            _expressionString.Append(" (");
            Visit(expression.Left);
            _expressionString.Append(" > ");
            Visit(expression.Right);
            _expressionString.Append(") ");
        }

        public override void Visit(NotExpression expression)
        {
            _expressionString.Append(" !(");
            Visit(expression.Expression);
            _expressionString.Append(") ");
        }

        public override void Visit(GreaterThanOrEqualsExpression expression)
        {
            _expressionString.Append(" (");
            Visit(expression.Left);
            _expressionString.Append(" >= ");
            Visit(expression.Right);
            _expressionString.Append(") ");
        }

        public override void Visit(LessThanOrEqualsExpression expression)
        {
            _expressionString.Append(" (");
            Visit(expression.Left);
            _expressionString.Append(" <= ");
            Visit(expression.Right);
            _expressionString.Append(") ");
        }

        public override void Visit(IsNullExpression expression)
        {
            _expressionString.Append(" ISNULL(");
            Visit(expression.Property);
            _expressionString.Append(") ");
        }

        public override void Visit(IsNotNullExpression expression)
        {
            _expressionString.Append(" ISNOTNULL(");
            Visit(expression.Property);
            _expressionString.Append(") ");
        }

        public override void Visit(KeywordExpression expression)
        {
            _expressionString.Append(" KEYWORD(PROP=");
            Visit(expression.Property);
            _expressionString.Append(", PHRASE=");
            Visit(expression.Phrase);
            _expressionString.Append(")");
        }

        public override void Visit(ContainsExpression expression)
        {
            _expressionString.Append(" CONTAINS(PROP=");
            Visit(expression.Property);
            _expressionString.Append(", PHRASE=");
            Visit(expression.Phrase);
            _expressionString.AppendFormat(", FORMS={0})", expression.Forms.ToString());
        }

        public override void Visit(ScopeExpression expression)
        {
            _expressionString.Append(" SCOPE(");
            _expressionString.Append(expression.Value);
            _expressionString.Append(")");
        }

        public override void Visit(DefaultScopeExpression expression)
        {
            _expressionString.Append(" DEFAULTSCOPE(");
            _expressionString.Append(expression.Value);
            _expressionString.Append(")");
        }

        #endregion
    }
}