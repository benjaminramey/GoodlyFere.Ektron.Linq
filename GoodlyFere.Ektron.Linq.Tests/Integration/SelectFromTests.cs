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
        #region Constants and Fields

        private readonly IdProvider _idProvider;

        #endregion

        #region Constructors and Destructors

        public SelectFromTests()
        {
            _idProvider = new IdProvider();
        }

        #endregion

        #region Public Methods

        [Fact]
        public void SelectFrom_ContentTypeAttrObj_TranslateToIdenticalExpr()
        {
            var expectedContentTypeId = _idProvider.GetContentTypeId(ContentTypeWidget.ContentTypeName);
            var query = from w in EktronQueryFactory.Queryable<ContentTypeWidget>(_idProvider)
                        select w;

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = SearchContentProperty.ContentType == expectedContentTypeId;
            EkAssert.Equal(expectedTranslation, actualTranslation);
        }

        [Fact]
        public void SelectFrom_FolderPathAndContentTypeAttrsObj_TranslateToIdenticalExpr()
        {
            var expectedContentTypeId = _idProvider.GetContentTypeId(
                FolderPathAndContentTypeWidget.ContentTypeName);
            var expectedSubContentTypeId =
                _idProvider.GetContentTypeId(FolderPathAndContentTypeWidget.ContentSubTypeName);
            var query = from w in EktronQueryFactory.Queryable<FolderPathAndContentTypeWidget>(_idProvider)
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
            var expectedSmartFormId = _idProvider.GetSmartFormId(FolderPathAndSmartFormWidget.SmartFormName);
            var query = from w in EktronQueryFactory.Queryable<FolderPathAndSmartFormWidget>(_idProvider)
                        select w;

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = SearchContentProperty.XmlConfigId == expectedSmartFormId
                                      & SearchContentProperty.FolderPath == FolderPathAndSmartFormWidget.TestFolder;
            EkAssert.Equal(expectedTranslation, actualTranslation);
        }

        [Fact]
        public void SelectFrom_FolderPathAttrObj_TranslateToIdenticalExpr()
        {
            var query = from w in EktronQueryFactory.Queryable<FolderPathWidget>(_idProvider)
                        select w;

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = SearchContentProperty.FolderPath == FolderPathWidget.TestFolder;
            EkAssert.Equal(expectedTranslation, actualTranslation);
        }

        [Fact]
        public void SelectFrom_PlainObject_TranslatesToNull()
        {
            var query = from w in EktronQueryFactory.Queryable<Widget>(_idProvider)
                        select w;

            var translation = TestHelper.GetTranslation(query);
            Assert.Null(translation);
        }

        [Fact]
        public void SelectFrom_SmartFormAndContentTypeAndFolderPathAttrsObj_TranslateToIdenticalExpr()
        {
            var expectedContentTypeId = _idProvider.GetContentTypeId(
                SmartFormAndContentTypeAndFolderPathWidget.ContentTypeName);
            var expectedSubContentTypeId =
                _idProvider.GetContentTypeId(SmartFormAndContentTypeAndFolderPathWidget.ContentSubTypeName);
            var expectedSmartFormId =
                _idProvider.GetSmartFormId(SmartFormAndContentTypeAndFolderPathWidget.SmartFormName);
            var query = from w in EktronQueryFactory.Queryable<SmartFormAndContentTypeAndFolderPathWidget>(_idProvider)
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
            var expectedContentTypeId = _idProvider.GetContentTypeId(
                SmartFormAndContentTypeWidget.ContentTypeName);
            var expectedSubContentTypeId =
                _idProvider.GetContentTypeId(SmartFormAndContentTypeWidget.ContentSubTypeName);
            var expectedSmartFormId = _idProvider.GetSmartFormId(SmartFormAndContentTypeWidget.SmartFormName);
            var query = from w in EktronQueryFactory.Queryable<SmartFormAndContentTypeWidget>(_idProvider)
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
            var expectedSmartFormId = _idProvider.GetSmartFormId(SmartFormWidget.SmartFormName);
            var query = from w in EktronQueryFactory.Queryable<SmartFormWidget>(_idProvider)
                        select w;

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = SearchContentProperty.XmlConfigId == expectedSmartFormId;
            EkAssert.Equal(expectedTranslation, actualTranslation);
        }

        [Fact]
        public void SelectFrom_SubContentTypeAttrObj_TranslateToIdenticalExpr()
        {
            var expectedContentTypeId = _idProvider.GetContentTypeId(SubContentTypeWidget.ContentTypeName);
            var expectedSubContentTypeId = _idProvider.GetContentTypeId(SubContentTypeWidget.ContentSubTypeName);
            var query = from w in EktronQueryFactory.Queryable<SubContentTypeWidget>(_idProvider)
                        select w;

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = SearchContentProperty.ContentType == expectedContentTypeId
                                      & SearchContentProperty.ContentSubType == expectedSubContentTypeId;
            EkAssert.Equal(expectedTranslation, actualTranslation);
        }

        #endregion
    }
}