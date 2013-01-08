#region Usings

using System;
using System.Linq;
using Ektron.Cms;
using Ektron.Cms.Common;
using Ektron.Cms.Search;
using Ektron.Cms.Search.Expressions;

#endregion

namespace GoodlyFere.Ektron.Linq.Tests.Ektron
{
    internal class SS2010TransformVisitor : ExpressionVisitor
    {
        #region Constants and Fields

        private readonly SearchCriteria criteria;

        private readonly SearchSettingsData searchSettingsData;

        #endregion

        #region Constructors and Destructors

        public SS2010TransformVisitor(SearchCriteria criteria)
        {
            Tree = null;
            this.criteria = criteria;
            IRequestInfoProvider requestInfoProvider = ObjectFactory.GetRequestInfoProvider();
            EkRequestInformation requestInformation = requestInfoProvider.GetRequestInformation();
            ISearchSettings searchSettings = ObjectFactory.GetSearchSettings(requestInformation);
            searchSettingsData = searchSettings.GetItem();
        }

        #endregion

        #region Properties

        internal Expression Tree { get; set; }

        #endregion

        #region Public Methods

        public void Transform(Expression expression)
        {
            Expression expression1 = null;
            if (criteria.Scope != null && criteria.Scope.Count > 0)
            {
                foreach (ScopeExpression scope in criteria.Scope)
                {
                    if (expression1 != null)
                    {
                        expression1 = expression1 | scope;
                    }
                    else
                    {
                        expression1 = scope;
                    }
                }
                expression = expression1.And(expression);
            }
            Visit(expression);
        }

        public override void Visit(Expression expression)
        {
            expression.Accept(this);
        }

        public override void Visit(AndExpression expression)
        {
            expression.Left.Accept(this);
            Expression tree = Tree;
            expression.Right.Accept(this);
            Expression tree1 = Tree;
            Tree = new AndExpression(tree, tree1);
        }

        public override void Visit(OrExpression expression)
        {
            expression.Left.Accept(this);
            Expression tree = Tree;
            expression.Right.Accept(this);
            Expression tree1 = Tree;
            Tree = new OrExpression(tree, tree1);
        }

        public override void Visit(PropertyExpression expression)
        {
            expression.Accept(this);
        }

        public override void Visit(DecimalPropertyExpression expression)
        {
            Tree = new DecimalPropertyExpression(expression.Name);
        }

        public override void Visit(IntegerPropertyExpression expression)
        {
            Tree = new IntegerPropertyExpression(expression.Name);
        }

        public override void Visit(BooleanPropertyExpression expression)
        {
            Tree = new BooleanPropertyExpression(expression.Name);
        }

        public override void Visit(DatePropertyExpression expression)
        {
            Tree = new DatePropertyExpression(expression.Name);
        }

        public override void Visit(StringPropertyExpression expression)
        {
            Tree = new StringPropertyExpression(expression.Name);
        }

        public override void Visit(IntegerValueExpression expression)
        {
            Tree = new IntegerValueExpression(expression.Value);
        }

        public override void Visit(StringValueExpression expression)
        {
            Tree = new StringValueExpression(expression.Value);
        }

        public override void Visit(DecimalValueExpression expression)
        {
            Tree = new DecimalValueExpression(expression.Value);
        }

        public override void Visit(BooleanValueExpression expression)
        {
            Tree = new BooleanValueExpression(expression.Value);
        }

        public override void Visit(DateValueExpression expression)
        {
            Tree = new DateValueExpression(expression.Value);
        }

        public override void Visit(EqualsExpression expression)
        {
            expression.Left.Accept(this);
            Expression tree = Tree;
            expression.Right.Accept(this);
            Expression tree1 = Tree;
            Tree = new EqualsExpression(tree, tree1);
        }

        public override void Visit(NotEqualsExpression expression)
        {
            expression.Left.Accept(this);
            Expression tree = Tree;
            expression.Right.Accept(this);
            Expression tree1 = Tree;
            Tree = new NotEqualsExpression(tree, tree1);
        }

        public override void Visit(LessThanExpression expression)
        {
            expression.Left.Accept(this);
            Expression tree = Tree;
            expression.Right.Accept(this);
            Expression tree1 = Tree;
            Tree = new LessThanExpression(tree, tree1);
        }

        public override void Visit(GreaterThanExpression expression)
        {
            expression.Left.Accept(this);
            Expression tree = Tree;
            expression.Right.Accept(this);
            Expression tree1 = Tree;
            Tree = new GreaterThanExpression(tree, tree1);
        }

        public override void Visit(NotExpression expression)
        {
            expression.Expression.Accept(this);
            Expression tree = Tree;
            Tree = new NotExpression(tree);
        }

        public override void Visit(GreaterThanOrEqualsExpression expression)
        {
            expression.Left.Accept(this);
            Expression tree = Tree;
            expression.Right.Accept(this);
            Expression tree1 = Tree;
            Tree = new GreaterThanOrEqualsExpression(tree, tree1);
        }

        public override void Visit(LessThanOrEqualsExpression expression)
        {
            expression.Left.Accept(this);
            Expression tree = Tree;
            expression.Right.Accept(this);
            Expression tree1 = Tree;
            Tree = new LessThanOrEqualsExpression(tree, tree1);
        }

        public override void Visit(IsNullExpression expression)
        {
            expression.Property.Accept(this);
            PropertyExpression tree = Tree as PropertyExpression;
            Tree = new IsNullExpression(tree);
        }

        public override void Visit(IsNotNullExpression expression)
        {
            expression.Property.Accept(this);
            PropertyExpression tree = Tree as PropertyExpression;
            Tree = new IsNotNullExpression(tree);
        }

        public override void Visit(KeywordExpression expression)
        {
            PropertyExpression tree = null;
            if (expression.Property != null)
            {
                Visit(expression.Property);
                tree = Tree as PropertyExpression;
            }
            Visit(expression.Phrase);
            StringValueExpression stringValueExpression = (StringValueExpression)Tree;
            Tree = new KeywordExpression(tree, stringValueExpression.Value);
        }

        public override void Visit(ContainsExpression expression)
        {
            PropertyExpression tree = null;
            if (expression.Property != null)
            {
                Visit(expression.Property);
                tree = Tree as PropertyExpression;
            }
            Visit(expression.Phrase);
            ValueExpression<string> inflectionalStringValueExpression = (ValueExpression<string>)Tree;
            WordForms forms = expression.Forms;
            switch (forms)
            {
                case WordForms.Exact:
                    {
                        Tree = new ContainsExpression(tree, inflectionalStringValueExpression);
                        return;
                    }
                case WordForms.Inflections:
                    {
                        inflectionalStringValueExpression =
                            new InflectionalStringValueExpression(inflectionalStringValueExpression.Value);
                        Tree = new ContainsExpression(tree, inflectionalStringValueExpression);
                        return;
                    }
                case WordForms.Synonyms:
                    {
                        inflectionalStringValueExpression =
                            new ThesaurusStringValueExpression(inflectionalStringValueExpression.Value);
                        Tree = new ContainsExpression(tree, inflectionalStringValueExpression);
                        return;
                    }
                default:
                    {
                        Tree = new ContainsExpression(tree, inflectionalStringValueExpression);
                        return;
                    }
            }
        }

        public override void Visit(ScopeExpression expression)
        {
            StringPropertyExpression stringPropertyExpression = new StringPropertyExpression("scope");
            Tree = new EqualsExpression(stringPropertyExpression, expression.Value);
        }

        public override void Visit(DefaultScopeExpression expression)
        {
            StringPropertyExpression stringPropertyExpression = new StringPropertyExpression("scope");
            Tree = new EqualsExpression(stringPropertyExpression, "testscope");
        }

        public override void Visit(QuotedStringValueExpression expression)
        {
            Tree = new QuotedStringValueExpression(expression.Value);
        }

        #endregion
    }
}