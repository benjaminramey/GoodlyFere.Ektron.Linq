#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ektron.Cms.Search;
using Ektron.Cms.Search.Expressions;
using Ektron.Cms.Search.SS2010.Expressions;

#endregion

namespace GoodlyFere.Ektron.Linq.Tests.Ektron
{
    public class SS2010ExpressionVisitor : ExpressionVisitor
    {
        #region Constants and Fields

        private readonly StringBuilder sb;

        #endregion

        #region Constructors and Destructors

        public SS2010ExpressionVisitor()
        {
            sb = new StringBuilder();
        }

        #endregion

        #region Public Methods

        public override string ToString()
        {
            return sb.ToString();
        }

        public override void Visit(Expression expression)
        {
            expression.Accept(this);
        }

        public override void Visit(AndExpression expression)
        {
            sb.Append("(");
            expression.Left.Accept(this);
            sb.Append(" AND ");
            expression.Right.Accept(this);
            sb.Append(")");
        }

        public override void Visit(OrExpression expression)
        {
            sb.Append("(");
            expression.Left.Accept(this);
            sb.Append(" OR ");
            expression.Right.Accept(this);
            sb.Append(")");
        }

        public override void Visit(PropertyExpression expression)
        {
            expression.Accept(this);
        }

        public override void Visit(DecimalPropertyExpression expression)
        {
            sb.Append(expression.Name);
        }

        public override void Visit(IntegerPropertyExpression expression)
        {
            sb.Append(expression.Name);
        }

        public override void Visit(BooleanPropertyExpression expression)
        {
            sb.Append(expression.Name);
        }

        public override void Visit(DatePropertyExpression expression)
        {
            sb.Append(expression.Name);
        }

        public override void Visit(StringPropertyExpression expression)
        {
            sb.Append(expression.Name);
        }

        public override void Visit(IntegerValueExpression expression)
        {
            long value = expression.Value;
            sb.Append(value.ToString());
        }

        public override void Visit(StringValueExpression expression)
        {
            sb.Append("'").Append(expression.Value.Replace("'", "''")).Append("'");
        }

        public override void Visit(DecimalValueExpression expression)
        {
            double value = expression.Value;
            sb.Append(value.ToString());
        }

        public override void Visit(BooleanValueExpression expression)
        {
            string str;
            StringBuilder stringBuilder = sb;
            if (expression.Value)
            {
                str = "1";
            }
            else
            {
                str = "0";
            }
            stringBuilder.Append(str);
        }

        public override void Visit(DateValueExpression expression)
        {
            DateTime value = expression.Value;
            long fileTime = value.ToFileTime();
            sb.Append(fileTime.ToString());
        }

        public override void Visit(EqualsExpression expression)
        {
            sb.Append("(");
            sb.Append("\"");
            expression.Left.Accept(this);
            sb.Append("\"");
            sb.Append(" = ");
            expression.Right.Accept(this);
            sb.Append(")");
        }

        public override void Visit(NotEqualsExpression expression)
        {
            sb.Append("(");
            sb.Append("\"");
            expression.Left.Accept(this);
            sb.Append("\"");
            sb.Append(" != ");
            expression.Right.Accept(this);
            sb.Append(")");
        }

        public override void Visit(LessThanExpression expression)
        {
            sb.Append("(");
            sb.Append("\"");
            expression.Left.Accept(this);
            sb.Append("\"");
            sb.Append(" < ");
            expression.Right.Accept(this);
            sb.Append(")");
        }

        public override void Visit(GreaterThanExpression expression)
        {
            sb.Append("(");
            sb.Append("\"");
            expression.Left.Accept(this);
            sb.Append("\"");
            sb.Append(" > ");
            expression.Right.Accept(this);
            sb.Append(")");
        }

        public override void Visit(NotExpression expression)
        {
            sb.Append("NOT ");
            expression.Expression.Accept(this);
        }

        public override void Visit(GreaterThanOrEqualsExpression expression)
        {
            sb.Append("(");
            sb.Append("\"");
            expression.Left.Accept(this);
            sb.Append("\"");
            sb.Append(" >= ");
            expression.Right.Accept(this);
            sb.Append(")");
        }

        public override void Visit(LessThanOrEqualsExpression expression)
        {
            sb.Append("(");
            sb.Append("\"");
            expression.Left.Accept(this);
            sb.Append("\"");
            sb.Append(" <= ");
            expression.Right.Accept(this);
            sb.Append(")");
        }

        public override void Visit(IsNullExpression expression)
        {
            sb.Append("(");
            sb.Append("\"");
            expression.Property.Accept(this);
            sb.Append("\"");
            sb.Append(" IS NULL)");
        }

        public override void Visit(IsNotNullExpression expression)
        {
            sb.Append("(");
            sb.Append("\"");
            expression.Property.Accept(this);
            sb.Append("\"");
            sb.Append(" IS NOT NULL)");
        }

        public override void Visit(KeywordExpression expression)
        {
            sb.Append("FREETEXT(");
            if (expression.Property != null)
            {
                sb.Append("\"");
                Visit(expression.Property);
                sb.Append("\"");
                sb.Append(",");
            }
            Visit(expression.Phrase);
            sb.Append(")");
        }

        public override void Visit(ContainsExpression expression)
        {
            sb.Append("CONTAINS(");
            if (expression.Property != null)
            {
                sb.Append("\"");
                Visit(expression.Property);
                sb.Append("\"");
                sb.Append(",");
            }
            Visit(expression.Phrase);
            sb.Append(")");
        }

        public override void Visit(ScopeExpression expression)
        {
            throw new NotImplementedException("This expression is not supported on Search Server 2010");
        }

        public override void Visit(DefaultScopeExpression expression)
        {
            throw new NotImplementedException("This expression is not supported on Search Server 2010");
        }

        public override void Visit(QuotedStringValueExpression expression)
        {
            sb.Append("'\"").Append(expression.Value.Replace("'", "''").Replace("\"", "\"\"")).Append("\"'");
        }

        #endregion

        #region Methods

        internal void Visit(ThesaurusStringValueExpression expression)
        {
            sb.Append("'FORMSOF(THESAURUS,").Append(expression.Value.Replace("'", "''")).Append(")'");
        }

        internal void Visit(InflectionalStringValueExpression expression)
        {
            sb.Append("'FORMSOF(INFLECTIONAL,").Append(expression.Value.Replace("'", "''")).Append(")'");
        }

        internal void Visit(SelectExpression expression)
        {
            sb.Append("SELECT ");
            IEnumerator<PropertyExpression> enumerator = expression.Properties.GetEnumerator();
            using (enumerator)
            {
                if (enumerator.MoveNext())
                {
                    sb.Append("\"");
                    enumerator.Current.Accept(this);
                    sb.Append("\"");
                    while (enumerator.MoveNext())
                    {
                        sb.Append(",");
                        sb.Append("\"");
                        enumerator.Current.Accept(this);
                        sb.Append("\"");
                    }
                }
            }
            if (expression.FromExpression != null)
            {
                expression.FromExpression.Accept(this);
            }
            if (expression.WhereExpression != null)
            {
                expression.WhereExpression.Accept(this);
            }
            if (expression.OrderByExpression != null)
            {
                expression.OrderByExpression.Accept(this);
            }
        }

        internal void Visit(WhereExpression expression)
        {
            sb.Append(" WHERE ");
            expression.Tree.Accept(this);
        }

        internal void Visit(FromExpression expression)
        {
            sb.Append(" FROM Scope()");
        }

        internal void Visit(OrderByExpression expression)
        {
            string str;
            string str1;
            IEnumerator<OrderData> enumerator = expression.Orders.GetEnumerator();
            using (enumerator)
            {
                if (enumerator.MoveNext())
                {
                    sb.Append(" ORDER BY ");
                    sb.Append("\"");
                    enumerator.Current.Property.Accept(this);
                    sb.Append("\"");
                    StringBuilder stringBuilder = sb;
                    if (enumerator.Current.Direction == OrderDirection.Ascending)
                    {
                        str = " ASC";
                    }
                    else
                    {
                        str = " DESC";
                    }
                    stringBuilder.Append(str);
                    while (enumerator.MoveNext())
                    {
                        sb.Append(",");
                        sb.Append("\"");
                        enumerator.Current.Property.Accept(this);
                        sb.Append("\"");
                        StringBuilder stringBuilder1 = sb;
                        if (enumerator.Current.Direction == OrderDirection.Ascending)
                        {
                            str1 = " ASC";
                        }
                        else
                        {
                            str1 = " DESC";
                        }
                        stringBuilder1.Append(str1);
                    }
                }
            }
        }

        #endregion
    }
}