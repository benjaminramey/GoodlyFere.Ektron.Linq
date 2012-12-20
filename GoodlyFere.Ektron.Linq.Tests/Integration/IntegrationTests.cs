#region Usings

using System;
using System.Linq;
using Ektron.Cms.Search;
using GoodlyFere.Ektron.Linq.Model.Attributes;
using GoodlyFere.Ektron.Linq.Tests.Model;
using Xunit;
using Ek = Ektron.Cms.Search.Expressions;

#endregion

namespace GoodlyFere.Ektron.Linq.Tests.Integration
{
    [SmartForm(SmartFormName)]
    internal class SmartFormWidget : Widget
    {
        #region Constants and Fields

        public const string SmartFormName = "Widget";

        #endregion
    }

    [ContentType(ContentTypeName)]
    internal class ContentTypeWidget : Widget
    {
        #region Constants and Fields

        public const string ContentTypeName = "Widget";

        #endregion
    }

    [ContentType(ContentTypeName, ContentSubTypeName = ContentSubTypeName)]
    internal class SubContentTypeWidget : Widget
    {
        #region Constants and Fields

        public const string ContentSubTypeName = "WidgetSubType";
        public const string ContentTypeName = "Widget";

        #endregion
    }

    [FolderPath(TestFolder)]
    internal class FolderPathWidget : Widget
    {
        #region Constants and Fields

        public const string TestFolder = "Folders/Test";

        #endregion
    }

    [FolderPath(TestFolder)]
    [SmartForm(SmartFormName)]
    internal class FolderPathAndSmartFormWidget : Widget
    {
        #region Constants and Fields

        public const string SmartFormName = "Widget";
        public const string TestFolder = "Folders/Test";

        #endregion
    }

    [FolderPath(TestFolder)]
    [ContentType(ContentTypeName, ContentSubTypeName = ContentSubTypeName)]
    internal class FolderPathAndContentTypeWidget : Widget
    {
        #region Constants and Fields

        public const string ContentSubTypeName = "WidgetSubType";
        public const string ContentTypeName = "Widget";
        public const string TestFolder = "Folders/Test";

        #endregion
    }

    [SmartForm(SmartFormName)]
    [ContentType(ContentTypeName, ContentSubTypeName = ContentSubTypeName)]
    internal class SmartFormAndContentTypeWidget : Widget
    {
        #region Constants and Fields

        public const string ContentSubTypeName = "WidgetSubType";
        public const string ContentTypeName = "Widget";
        public const string SmartFormName = "Widget";

        #endregion
    }

    [SmartForm(SmartFormName)]
    [ContentType(ContentTypeName, ContentSubTypeName = ContentSubTypeName)]
    [FolderPath(TestFolder)]
    internal class SmartFormAndContentTypeAndFolderPathWidget : Widget
    {
        #region Constants and Fields

        public const string ContentSubTypeName = "WidgetSubType";
        public const string ContentTypeName = "Widget";
        public const string SmartFormName = "Widget";
        public const string TestFolder = "Folders/Test";

        #endregion
    }

    public class SelectFromWhereTests
    {
        #region Public Methods

        [Fact]
        public void SelectFromWhere_3Clauses()
        {
            string expectedName = "anything";
            long expectedIdHigh = 10L;
            long expectedIdLow = 1L;
            var query = from w in EktronQueryFactory.Queryable<Widget>()
                        where w.Name == expectedName && w.Id < expectedIdHigh && w.Id > expectedIdLow
                        select w;

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = (new Ek.StringPropertyExpression("Name") == expectedName
                                       & SearchContentProperty.Id < expectedIdHigh)
                                      & SearchContentProperty.Id > expectedIdLow;
            EkAssert.Equal(expectedTranslation, actualTranslation);
        }

        [Fact]
        public void SelectFromWhere_4Clauses()
        {
            string expectedName = "anything";
            string expectedName2 = "nothing";
            long expectedIdHigh = 10L;
            long expectedIdLow = 1L;
            var query = from w in EktronQueryFactory.Queryable<Widget>()
                        where w.Name == expectedName
                              && w.Id < expectedIdHigh
                              && w.Id > expectedIdLow
                              && w.Name == expectedName2
                        select w;

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = new Ek.StringPropertyExpression("Name") == expectedName
                                      & SearchContentProperty.Id < expectedIdHigh
                                      & SearchContentProperty.Id > expectedIdLow
                                      & new Ek.StringPropertyExpression("Name") == expectedName2;
            EkAssert.Equal(expectedTranslation, actualTranslation);
        }

        [Fact]
        public void SelectFromWhere_4ClausesGrouped()
        {
            string expectedName = "anything";
            string expectedName2 = "nothing";
            long expectedIdHigh = 10L;
            long expectedIdLow = 1L;
            var query = from w in EktronQueryFactory.Queryable<Widget>()
                        where (w.Name == expectedName
                               && w.Id < expectedIdHigh)
                              && (w.Id > expectedIdLow
                                  && w.Name == expectedName2)
                        select w;

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = (new Ek.StringPropertyExpression("Name") == expectedName
                                       & SearchContentProperty.Id < expectedIdHigh)
                                      & (SearchContentProperty.Id > expectedIdLow
                                         & new Ek.StringPropertyExpression("Name") == expectedName2);
            EkAssert.Equal(expectedTranslation, actualTranslation);
        }

        [Fact]
        public void SelectFromWhere_BasicProperty()
        {
            long expectedValue = 1L;
            var query = from w in EktronQueryFactory.Queryable<Widget>()
                        where w.Id == expectedValue
                        select w;

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = SearchContentProperty.Id == expectedValue;
            EkAssert.Equal(expectedTranslation, actualTranslation);
        }

        [Fact]
        public void SelectFromWhere_Equals()
        {
            long expectedValue = 1L;
            var query = from w in EktronQueryFactory.Queryable<Widget>()
                        where w.Id == expectedValue
                        select w;

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = SearchContentProperty.Id == expectedValue;
            EkAssert.Equal(expectedTranslation, actualTranslation);
        }

        [Fact]
        public void SelectFromWhere_EqualsReverse()
        {
            long expectedValue = 1L;
            var query = from w in EktronQueryFactory.Queryable<Widget>()
                        where expectedValue == w.Id
                        select w;

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = SearchContentProperty.Id == expectedValue;
            EkAssert.Equal(expectedTranslation, actualTranslation);
        }

        [Fact]
        public void SelectFromWhere_GreaterThan()
        {
            long expectedValue = 1L;
            var query = from w in EktronQueryFactory.Queryable<Widget>()
                        where w.Id > expectedValue
                        select w;

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = SearchContentProperty.Id > expectedValue;
            EkAssert.Equal(expectedTranslation, actualTranslation);
        }

        [Fact]
        public void SelectFromWhere_GreaterThanOrEqual()
        {
            long expectedValue = 1L;
            var query = from w in EktronQueryFactory.Queryable<Widget>()
                        where w.Id >= expectedValue
                        select w;

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = SearchContentProperty.Id >= expectedValue;
            EkAssert.Equal(expectedTranslation, actualTranslation);
        }

        [Fact]
        public void SelectFromWhere_GreaterThanOrEqualReverse()
        {
            long expectedValue = 1L;
            var query = from w in EktronQueryFactory.Queryable<Widget>()
                        where expectedValue <= w.Id
                        select w;

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = SearchContentProperty.Id >= expectedValue;
            EkAssert.Equal(expectedTranslation, actualTranslation);
        }

        [Fact]
        public void SelectFromWhere_GreaterThanReverse()
        {
            long expectedValue = 1L;
            var query = from w in EktronQueryFactory.Queryable<Widget>()
                        where expectedValue < w.Id
                        select w;

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = SearchContentProperty.Id > expectedValue;
            EkAssert.Equal(expectedTranslation, actualTranslation);
        }

        [Fact]
        public void SelectFromWhere_LessThan()
        {
            long expectedValue = 1L;
            var query = from w in EktronQueryFactory.Queryable<Widget>()
                        where w.Id < expectedValue
                        select w;

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = SearchContentProperty.Id < expectedValue;
            EkAssert.Equal(expectedTranslation, actualTranslation);
        }

        [Fact]
        public void SelectFromWhere_LessThanOrEqual()
        {
            long expectedValue = 1L;
            var query = from w in EktronQueryFactory.Queryable<Widget>()
                        where w.Id <= expectedValue
                        select w;

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = SearchContentProperty.Id <= expectedValue;
            EkAssert.Equal(expectedTranslation, actualTranslation);
        }

        [Fact]
        public void SelectFromWhere_LessThanOrEqualReverse()
        {
            long expectedValue = 1L;
            var query = from w in EktronQueryFactory.Queryable<Widget>()
                        where expectedValue >= w.Id
                        select w;

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = SearchContentProperty.Id <= expectedValue;
            EkAssert.Equal(expectedTranslation, actualTranslation);
        }

        [Fact]
        public void SelectFromWhere_LessThanReverse()
        {
            long expectedValue = 1L;
            var query = from w in EktronQueryFactory.Queryable<Widget>()
                        where expectedValue > w.Id
                        select w;

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = SearchContentProperty.Id < expectedValue;
            EkAssert.Equal(expectedTranslation, actualTranslation);
        }

        [Fact]
        public void SelectFromWhere_LogicalAnd()
        {
            string expectedName = "anything";
            long expectedId = 1L;
            var query = from w in EktronQueryFactory.Queryable<Widget>()
                        where w.Name == expectedName && w.Id == expectedId
                        select w;

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = new Ek.StringPropertyExpression("Name") == expectedName
                                      & SearchContentProperty.Id == expectedId;
            EkAssert.Equal(expectedTranslation, actualTranslation);
        }

        [Fact]
        public void SelectFromWhere_LogicalOr()
        {
            string expectedName = "anything";
            long expectedId = 1L;
            var query = from w in EktronQueryFactory.Queryable<Widget>()
                        where w.Name == expectedName || w.Id == expectedId
                        select w;

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = new Ek.StringPropertyExpression("Name") == expectedName
                                      | SearchContentProperty.Id == expectedId;
            EkAssert.Equal(expectedTranslation, actualTranslation);
        }

        [Fact]
        public void SelectFromWhere_NotEquals()
        {
            long expectedValue = 1L;
            var query = from w in EktronQueryFactory.Queryable<Widget>()
                        where w.Id != expectedValue
                        select w;

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = SearchContentProperty.Id != expectedValue;
            EkAssert.Equal(expectedTranslation, actualTranslation);
        }

        [Fact]
        public void SelectFromWhere_NotEqualsReverse()
        {
            long expectedValue = 1L;
            var query = from w in EktronQueryFactory.Queryable<Widget>()
                        where expectedValue != w.Id
                        select w;

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = SearchContentProperty.Id != expectedValue;
            EkAssert.Equal(expectedTranslation, actualTranslation);
        }

        [Fact]
        public void SelectFromWhere_UndecoratedProperty()
        {
            string expectedValue = "anything";
            var query = from w in EktronQueryFactory.Queryable<Widget>()
                        where w.Name == expectedValue
                        select w;

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = new Ek.StringPropertyExpression("Name") == expectedValue;
            EkAssert.Equal(expectedTranslation, actualTranslation);
        }

        [Fact]
        public void SelectFromWhere_StringContains()
        {
            string expectedValue = "anything";
            var query = from w in EktronQueryFactory.Queryable<Widget>()
                        where w.Name.Contains(expectedValue)
                        select w;

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = new Ek.StringPropertyExpression("Name").Contains(expectedValue, Ek.WordForms.Inflections);
            EkAssert.Equal(expectedTranslation, actualTranslation);
        }



        #endregion
    }
}