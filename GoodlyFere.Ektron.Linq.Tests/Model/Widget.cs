#region Usings

using System;
using System.Linq;
using Ektron.Cms.Search.Expressions;
using GoodlyFere.Ektron.Linq.Model;
using GoodlyFere.Ektron.Linq.Model.Attributes;

#endregion

namespace GoodlyFere.Ektron.Linq.Tests.Model
{
    public class Widget
    {
        #region Public Properties

        [EktronProperty(EkConstants.SearchProperties.ContentId, EktronExpressionType = typeof(IntegerPropertyExpression))]
        public long Id { get; set; }

        public object ObjectId { get; set; }
        
        public string Name { get; set; }

        [EktronProperty("Number", EktronExpressionType = typeof(IntegerPropertyExpression))]
        public int Number { get; set; }

        #endregion
    }

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
}