﻿#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Ektron.Cms.Search;
using GoodlyFere.Ektron.Linq.Extensions;
using GoodlyFere.Ektron.Linq.Helpers;
using GoodlyFere.Ektron.Linq.Model.Attributes;

#endregion

namespace GoodlyFere.Ektron.Linq.Search
{
    internal class ResultsConverter<T>
    {
        #region Methods

        internal virtual IEnumerable<T> GetMany(
            List<SearchResultData> searchResultData, AdvancedSearchCriteria criteria)
        {
            return searchResultData.Select(GetModelObject);
        }

        internal virtual T GetSingle(List<SearchResultData> searchResultData, AdvancedSearchCriteria criteria)
        {
            if (searchResultData.Count == 0)
            {
                return default(T);
            }

            return GetModelObject(searchResultData.FirstOrDefault());
        }

        protected virtual object ConvertSearchResultProperty(PropertyInfo propertyInfo, object searchResultValue)
        {
            Type propType = propertyInfo.PropertyType;
            if (propType == typeof(DateTime) && searchResultValue is long)
            {
                return new DateTime((long)searchResultValue);
            }

            if (searchResultValue == DBNull.Value)
            {
                return null;
            }

            return searchResultValue;
        }

        protected virtual T CreateModelObjectInstance()
        {
            try
            {
                return Activator.CreateInstance<T>();
            }
            catch (MissingMethodException mme)
            {
                throw new Exception(
                    string.Format(
                        "Cannot create an instance of {0}.  You must query an object of type IEnumerable<T>.",
                        typeof(T).FullName),
                    mme);
            }
        }

        protected virtual T GetModelObject(SearchResultData searchResultData)
        {
            T model = CreateModelObjectInstance();
            foreach (var propertyInfo in typeof(T).GetProperties())
            {
                var attr = propertyInfo.GetCustomAttribute<EktronPropertyAttribute>();
                if (attr == null)
                {
                    continue;
                }

                string propertyName = PropertyExpressionHelper.GetPropertyName(attr);
                if (!string.IsNullOrWhiteSpace(propertyName))
                {
                    SetPropertyValue(searchResultData, propertyInfo, model, propertyName);
                }
            }

            return model;
        }

        protected virtual void SetPropertyValue(
            SearchResultData searchResultData, PropertyInfo propertyInfo, object model, string propertyName)
        {
            if (!searchResultData.HasProperty(propertyName))
            {
                string msg = string.Format("SearchResultData does not contain '{0}' property.", propertyName);
                throw new Exception(msg);
            }

            object value = ConvertSearchResultProperty(propertyInfo, searchResultData[propertyName]);
            propertyInfo.SetValue(model, value, null);
        }

        #endregion
    }
}