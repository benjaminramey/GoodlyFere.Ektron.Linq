#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EktronQueryFactory.cs">
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
using Ektron.Cms;
using Ektron.Cms.Search;
using GoodlyFere.Ektron.Linq.IdProviders;
using GoodlyFere.Ektron.Linq.Interfaces;
using Remotion.Linq;
using Remotion.Linq.Parsing.Structure;

#endregion

namespace GoodlyFere.Ektron.Linq
{
    public class EktronQueryFactory
    {
        #region Public Methods

        /// <summary>
        ///     Creates an <see cref="EktronQueryable" /> for a domain object
        ///     using the default <see cref="AppSettingsIdProvider" /> id provider.
        /// </summary>
        /// <typeparam name="T">Domain object to wrap in an IQueryable.</typeparam>
        /// <returns>
        ///     <see cref="EktronQueryable<T>"/>
        /// </returns>
        public static EktronQueryable<T> Queryable<T>()
        {
            return Queryable<T>(null, null);
        }

        /// <summary>
        ///     Creates an <see cref="EktronQueryable" /> for a domain object
        ///     with the provided <see cref="IEktronIdProvider" />.
        /// </summary>
        /// <typeparam name="T">Domain object to wrap in an IQueryable.</typeparam>
        /// <param name="idProvider">
        ///     Implementation of <see cref="IEktronIdProvider" /> to use when searching.
        /// </param>
        /// <returns>
        ///     <see cref="EktronQueryable<T>"/>
        /// </returns>
        public static EktronQueryable<T> Queryable<T>(IEktronIdProvider idProvider)
        {
            return Queryable<T>(idProvider, null);
        }

        /// <summary>
        ///     Creates an <see cref="EktronQueryable" /> for a domain object
        ///     with the provided <see cref="IEktronIdProvider" /> and <see cref="ISearchManager" />.
        /// </summary>
        /// <typeparam name="T">Domain object to wrap in an IQueryable.</typeparam>
        /// <param name="idProvider">
        ///     Implementation of <see cref="IEktronIdProvider" /> to use when searching.
        /// </param>
        /// <param name="searchManager">
        ///     Implementation of ISearchManager, usually obtained via <code>ObjectFactory.GetSearchManager()</code>.
        /// </param>
        /// <returns>
        ///     <see cref="EktronQueryable<T>"/>
        /// </returns>
        public static EktronQueryable<T> Queryable<T>(IEktronIdProvider idProvider, ISearchManager searchManager)
        {
            return new EktronQueryable<T>(CreateQueryParser(), CreateExecutor(idProvider, searchManager));
        }

        #endregion

        #region Methods

        private static IQueryExecutor CreateExecutor(IEktronIdProvider idProvider, ISearchManager searchManager)
        {
            idProvider = idProvider ?? new AppSettingsIdProvider();
            searchManager = searchManager ?? ObjectFactory.GetSearchManager();

            return new EktronQueryExecutor(idProvider, searchManager);
        }

        private static IQueryParser CreateQueryParser()
        {
            return QueryParser.CreateDefault();
        }

        #endregion
    }
}