#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BasicUsage.cs">
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
using GoodlyFere.Ektron.Linq.Model;
using GoodlyFere.Ektron.Linq.Model.Attributes;

#endregion

namespace GoodlyFere.Ektron.Linq.Samples
{
    internal class BasicUsage
    {
        #region Methods

        private static void Main(string[] args)
        {
            var widgets = EktronQueryFactory.Queryable<Widget>(new AppSettingsIdProvider());

            var query = from w in widgets
                        where w.Id > 10
                        select w;

            Widget[] itemWidgets = query.ToArray();
            foreach (var widget in itemWidgets)
            {
                Console.WriteLine("Widget found: {0}", widget.Id);
            }

            Console.ReadKey();
        }

        #endregion

        public class Widget
        {
            #region Public Properties

            [EktronIntegerProperty(EkConstants.SearchProperties.ContentId)]
            public long Id { get; set; }

            public string Name { get; set; }

            #endregion
        }
    }
}