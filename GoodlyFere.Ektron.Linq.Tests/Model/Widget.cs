#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Widget.cs">
// LINQ to Ektron Search, a LINQ interface to the Ektron AdvancedSearchCriteria search engine
// 
// Copyright (C) 2013 Benjamin Ramey
// 
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 2.1 of the License, or (at your option) any later version.
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// Lesser General Public License for more details.
// 
// You should have received a copy of the GNU Lesser General Public
// License along with this library; if not, write to the Free Software
// Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301  USA
// 
// http://www.gnu.org/licenses/lgpl-2.1-standalone.html
// 
// You can contact me at ben.ramey@gmail.com.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#endregion

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

        [EktronProperty(EkConstants.SearchProperties.ContentId, EktronExpressionType = typeof(IntegerPropertyExpression)
            )]
        public long Id { get; set; }

        public string Name { get; set; }

        #endregion
    }

    public class ModelWidget
    {
        #region Public Properties

        [EktronProperty(EkConstants.SearchProperties.Title)]
        public string ContentTitle { get; set; }

        [EktronProperty(EkConstants.SearchProperties.DateCreated,
            EktronExpressionType = typeof(DatePropertyExpression))]
        public DateTime DateCreated { get; set; }

        [EktronProperty(EkConstants.SearchProperties.DateModified,
            EktronExpressionType = typeof(DatePropertyExpression))]
        public DateTime DateModified { get; set; }

        [EktronProperty(EkConstants.SearchProperties.FolderId,
            EktronExpressionType = typeof(IntegerPropertyExpression))]
        public long FolderId { get; set; }

        [EktronProperty(EkConstants.SearchProperties.FolderPath)]
        public string FolderPath { get; set; }

        [EktronProperty(EkConstants.SearchProperties.ContentId,
            EktronExpressionType = typeof(IntegerPropertyExpression))]
        public object Id { get; set; }

        [EktronProperty(EkConstants.SearchProperties.Path)]
        public string Path { get; set; }

        [EktronProperty(EkConstants.SearchProperties.QuickLink)]
        public string QuickLink { get; set; }

        #endregion
    }

    public class NumberTestWidget : Widget
    {
        #region Public Properties

        [EktronProperty("Number", EktronExpressionType = typeof(IntegerPropertyExpression))]
        public int Number { get; set; }

        #endregion
    }

    public class CastTestWidget : Widget
    {
        #region Public Properties

        public object NoTypeObjectId { get; set; }

        [EktronProperty("StringTypeObjectId", EktronExpressionType = typeof(StringPropertyExpression))]
        public object StringTypeObjectId { get; set; }

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