#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringMethodsTests.cs">
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