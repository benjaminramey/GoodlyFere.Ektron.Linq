#region Usings

using System;
using System.Linq;
using Ektron.Cms.Search.Expressions;
using EktronExpression = Ektron.Cms.Search.Expressions.Expression;

#endregion

namespace GoodlyFere.Ektron.Linq.Generation.Translation.ExpressionVisitors
{
    public class QueryBuildResult
    {
        #region Public Properties

        public EktronExpression Expression { get; set; }
        public PropertyExpression[] PropertiesUsed { get; set; }

        #endregion
    }
}