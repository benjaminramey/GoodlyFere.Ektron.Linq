#region Usings

using System.Linq;
using System;
using Ektron.Cms.Search;
using GoodlyFere.Ektron.Linq.Tests.Model;
using GoodlyFere.Ektron.Linq.Tests.TestImplementations;
using Xunit;

#endregion

namespace GoodlyFere.Ektron.Linq.Tests.Integration
{
    public class SelectFromTests
    {
        #region Public Methods

        [Fact]
        public void SelectFrom_ContentTypeAttrObj_TranslateToIdenticalExpr()
        {
            var expectedContentTypeId = new IdProvider().GetContentTypeId(ContentTypeWidget.ContentTypeName);
            var query = from w in EktronQueryFactory.Queryable<ContentTypeWidget>()
                        select w;

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = SearchContentProperty.ContentType == expectedContentTypeId;
            EkAssert.Equal(expectedTranslation, actualTranslation);
        }

        [Fact]
        public void SelectFrom_FolderPathAndContentTypeAttrsObj_TranslateToIdenticalExpr()
        {
            var expectedContentTypeId = new IdProvider().GetContentTypeId(
                FolderPathAndContentTypeWidget.ContentTypeName);
            var expectedSubContentTypeId =
                new IdProvider().GetContentTypeId(FolderPathAndContentTypeWidget.ContentSubTypeName);
            var query = from w in EktronQueryFactory.Queryable<FolderPathAndContentTypeWidget>()
                        select w;

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = SearchContentProperty.FolderPath == FolderPathAndContentTypeWidget.TestFolder
                                      & (SearchContentProperty.ContentType == expectedContentTypeId
                                         & SearchContentProperty.ContentSubType == expectedSubContentTypeId);
            EkAssert.Equal(expectedTranslation, actualTranslation);
        }

        [Fact]
        public void SelectFrom_FolderPathAndSmartFormAttrsObj_TranslateToIdenticalExpr()
        {
            var expectedSmartFormId = new IdProvider().GetSmartFormId(FolderPathAndSmartFormWidget.SmartFormName);
            var query = from w in EktronQueryFactory.Queryable<FolderPathAndSmartFormWidget>()
                        select w;

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = SearchContentProperty.XmlConfigId == expectedSmartFormId
                                      & SearchContentProperty.FolderPath == FolderPathAndSmartFormWidget.TestFolder;
            EkAssert.Equal(expectedTranslation, actualTranslation);
        }

        [Fact]
        public void SelectFrom_FolderPathAttrObj_TranslateToIdenticalExpr()
        {
            var query = from w in EktronQueryFactory.Queryable<FolderPathWidget>()
                        select w;

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = SearchContentProperty.FolderPath == FolderPathWidget.TestFolder;
            EkAssert.Equal(expectedTranslation, actualTranslation);
        }

        [Fact]
        public void SelectFrom_PlainObject_TranslatesToNull()
        {
            var query = from w in EktronQueryFactory.Queryable<Widget>()
                        select w;

            var translation = TestHelper.GetTranslation(query);
            Assert.Null(translation);
        }

        [Fact]
        public void SelectFrom_SmartFormAndContentTypeAndFolderPathAttrsObj_TranslateToIdenticalExpr()
        {
            var expectedContentTypeId = new IdProvider().GetContentTypeId(
                SmartFormAndContentTypeAndFolderPathWidget.ContentTypeName);
            var expectedSubContentTypeId =
                new IdProvider().GetContentTypeId(SmartFormAndContentTypeAndFolderPathWidget.ContentSubTypeName);
            var expectedSmartFormId =
                new IdProvider().GetSmartFormId(SmartFormAndContentTypeAndFolderPathWidget.SmartFormName);
            var query = from w in EktronQueryFactory.Queryable<SmartFormAndContentTypeAndFolderPathWidget>()
                        select w;

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = (SearchContentProperty.XmlConfigId == expectedSmartFormId
                                       &
                                       SearchContentProperty.FolderPath
                                       == SmartFormAndContentTypeAndFolderPathWidget.TestFolder)
                                      & (SearchContentProperty.ContentType == expectedContentTypeId
                                         & SearchContentProperty.ContentSubType == expectedSubContentTypeId);
            EkAssert.Equal(expectedTranslation, actualTranslation);
        }

        [Fact]
        public void SelectFrom_SmartFormAndContentTypeAttrsObj_TranslateToIdenticalExpr()
        {
            var expectedContentTypeId = new IdProvider().GetContentTypeId(
                SmartFormAndContentTypeWidget.ContentTypeName);
            var expectedSubContentTypeId =
                new IdProvider().GetContentTypeId(SmartFormAndContentTypeWidget.ContentSubTypeName);
            var expectedSmartFormId = new IdProvider().GetSmartFormId(SmartFormAndContentTypeWidget.SmartFormName);
            var query = from w in EktronQueryFactory.Queryable<SmartFormAndContentTypeWidget>()
                        select w;

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = SearchContentProperty.XmlConfigId == expectedSmartFormId
                                      & (SearchContentProperty.ContentType == expectedContentTypeId
                                         & SearchContentProperty.ContentSubType == expectedSubContentTypeId);
            EkAssert.Equal(expectedTranslation, actualTranslation);
        }

        [Fact]
        public void SelectFrom_SmartFormAttrObj_TranslateToIdenticalExpr()
        {
            var expectedSmartFormId = new IdProvider().GetSmartFormId(SmartFormWidget.SmartFormName);
            var query = from w in EktronQueryFactory.Queryable<SmartFormWidget>()
                        select w;

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = SearchContentProperty.XmlConfigId == expectedSmartFormId;
            EkAssert.Equal(expectedTranslation, actualTranslation);
        }

        [Fact]
        public void SelectFrom_SubContentTypeAttrObj_TranslateToIdenticalExpr()
        {
            var expectedContentTypeId = new IdProvider().GetContentTypeId(SubContentTypeWidget.ContentTypeName);
            var expectedSubContentTypeId = new IdProvider().GetContentTypeId(SubContentTypeWidget.ContentSubTypeName);
            var query = from w in EktronQueryFactory.Queryable<SubContentTypeWidget>()
                        select w;

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = SearchContentProperty.ContentType == expectedContentTypeId
                                      & SearchContentProperty.ContentSubType == expectedSubContentTypeId;
            EkAssert.Equal(expectedTranslation, actualTranslation);
        }

        #endregion
    }
}