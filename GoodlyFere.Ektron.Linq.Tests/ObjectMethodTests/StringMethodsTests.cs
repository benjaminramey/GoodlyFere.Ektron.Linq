#region Usings

using System;
using System.Linq;
using Ektron.Cms.Search.Expressions;
using GoodlyFere.Ektron.Linq.Tests.Model;
using GoodlyFere.Ektron.Linq.Tests.Tools;
using Xunit;

#endregion

namespace GoodlyFere.Ektron.Linq.Tests.ObjectMethodTests
{
    public class StringMethodsTests
    {
        #region Public Methods

        [Fact]
        public void Contains()
        {
            string expectedName = "bob";
            var query = from w in EktronQueryFactory.Queryable<Widget>()
                        where w.Name.Contains(expectedName)
                        select w;

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = new StringPropertyExpression("Name").Contains("bob", WordForms.Inflections);

            EkAssert.Equal(expectedTranslation, actualTranslation);
        }

        [Fact]
        public void IsNullOrEmpty()
        {
            var query = from w in EktronQueryFactory.Queryable<Widget>()
                        where string.IsNullOrEmpty(w.Name)
                        select w;

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = new StringPropertyExpression("Name") == string.Empty;

            EkAssert.Equal(expectedTranslation, actualTranslation);
        }

        #endregion
    }
}