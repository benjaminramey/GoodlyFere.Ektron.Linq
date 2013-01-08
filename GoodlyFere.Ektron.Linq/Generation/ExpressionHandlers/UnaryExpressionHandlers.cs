#region Usings

using System;
using System.Linq;
using System.Linq.Expressions;
using GoodlyFere.Ektron.Linq.Exceptions;
using GoodlyFere.Ektron.Linq.Extensions;
using GoodlyFere.Ektron.Linq.Generation.Mappings.PropertyMaps;
using GoodlyFere.Ektron.Linq.Model.Attributes;
using Ek = Ektron.Cms.Search.Expressions;

#endregion

namespace GoodlyFere.Ektron.Linq.Generation.ExpressionHandlers
{
    internal class UnaryExpressionHandlers
    {
        #region Public Methods

        public static Ek.Expression HandleConvert(UnaryExpression unaryExpression, Ek.Expression operand)
        {
            if (unaryExpression.NodeType != ExpressionType.Convert)
            {
                throw new ArgumentOutOfRangeException(
                    "unaryExpression", "unaryExpression must be of NodeType ExpressionType.Convert");
            }

            if (operand is Ek.PropertyExpression && unaryExpression.Operand is MemberExpression)
            {
                MemberExpression memberExpr = (MemberExpression)unaryExpression.Operand;
                EktronPropertyAttribute ekProp = memberExpr.Member
                                                           .GetCustomAttribute<EktronPropertyAttribute>();
                TypeToPropertyMap map = new TypeToPropertyMap();
                var factoryMethod = map.FirstOrDefault(unaryExpression.Type);
                Ek.PropertyExpression castedExpr = factoryMethod.Invoke(operand as Ek.PropertyExpression);

                if (ekProp == null || ekProp.EktronExpressionType == null)
                {
                    // if the property has no EktronExpressionType set, then we assume
                    // this is a valid cast (user knows best?) and just get the expression
                    // builder for this type or the default one
                    return castedExpr;
                }

                // if the property does have the type set, and the cast doesn't
                // work for that type, then we throw an exception
                if (castedExpr.GetType() != operand.GetType() && unaryExpression.Type != typeof(object))
                {
                    string msg = string.Format(
                        "You said {0} is a {1}, but are trying to cast it to {2}.",
                        memberExpr.Member.Name,
                        ekProp.EktronExpressionType.Name,
                        unaryExpression.Type.Name);
                    throw new InvalidQueryException(msg);
                }

                return operand;
            }

            return operand;
        }

        #endregion
    }
}