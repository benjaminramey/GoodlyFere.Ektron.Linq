#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EkConstants.cs">
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

#endregion

namespace GoodlyFere.Ektron.Linq.Model
{
    public static class EkConstants
    {
        public struct SearchProperties
        {
            #region Constants and Fields

            public const string AssetVersion = "assetversion";
            public const string Author = "cmsauthor";
            public const string ContentId = "contentid";
            public const string ContentLanguage = "contentlanguage";
            public const string ContentSubType = "contentsubtype";
            public const string ContentType = "contenttype1";
            public const string DateCreated = "datecreated";
            public const string DateModified = "datemodified";
            public const string Description = "description";
            public const string ExpiryDate = "expirydate";
            public const string ExpiryType = "expirytype";
            public const string FolderId = "folderid";
            public const string FolderIdPath = "folderidpath";
            public const string FolderName = "foldername";
            public const string FolderPath = "foldernamepath";
            public const string GoLiveDate = "golivedate";
            public const string HighlightedSummary = "hithighlightedsummary";
            public const string MapAddress = "mapaddress";
            public const string MapDate = "mapdate";
            public const string MapLatitude = "maplatitude";
            public const string MapLongitude = "maplongitude";
            public const string Path = "path";
            public const string Public = "public";
            public const string QuickLink = "quicklink";
            public const string Rank = "rank";
            public const string SiteId = "parentsiteid";
            public const string Size = "size";
            public const string Tags = "tags";
            public const string TaxonomyCategory = "taxonomycategory";
            public const string Title = "doctitle";
            public const string XmlConfigId = "xmlconfigid";

            #endregion
        }
    }
}