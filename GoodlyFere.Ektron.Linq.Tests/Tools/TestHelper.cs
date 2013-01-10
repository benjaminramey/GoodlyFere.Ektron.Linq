#region Usings

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Ektron.Cms.Search;
using GoodlyFere.Ektron.Linq.Generation;
using GoodlyFere.Ektron.Linq.Helpers;
using GoodlyFere.Ektron.Linq.Model.Attributes;
using GoodlyFere.Ektron.Linq.Tests.TestImplementations;
using Remotion.Linq;
using Remotion.Linq.Parsing.Structure;

#endregion

namespace GoodlyFere.Ektron.Linq.Tests.Tools
{
    internal class TestHelper
    {
        #region Methods

        internal static DataRow GetDummyDataRow<T>(
            string stringValue = "", long dateTimeValue = 0L, int integerValue = 0)
        {
            var dummyObj = Activator.CreateInstance<T>();
            var dataTableMock = new DataTable();
            var dummyObjectProps = typeof(T).GetProperties();

            foreach (var prop in dummyObjectProps)
            {
                var columnType = prop.PropertyType;
                if (columnType == typeof(DateTime))
                {
                    columnType = typeof(long);
                }

                var attribute =
                    prop.GetCustomAttributes(typeof(EktronPropertyAttribute), true)
                        .Cast<EktronPropertyAttribute>()
                        .FirstOrDefault();
                string columnName = attribute == null ? prop.Name : PropertyExpressionHelper.GetPropertyName(attribute);

                dataTableMock.Columns.Add(columnName, columnType);
            }

            var dataRowMock = dataTableMock.NewRow();
            foreach (var prop in dummyObjectProps)
            {
                var attribute =
                    prop.GetCustomAttributes(typeof(EktronPropertyAttribute), true)
                        .Cast<EktronPropertyAttribute>()
                        .FirstOrDefault();
                string columnName = attribute == null ? prop.Name : PropertyExpressionHelper.GetPropertyName(attribute);

                var columnType = prop.PropertyType;
                if (columnType == typeof(DateTime))
                {
                    dataRowMock[columnName] = ((DateTime)prop.GetValue(dummyObj, null)).Ticks;
                }
                else
                {
                    dataRowMock[columnName] = prop.GetValue(dummyObj, null);
                }
            }

            //dataRowMock["RandomDateTime"] = dateTimeValue;
            //dataRowMock["RandomString"] = stringValue;
            //dataRowMock["RandomString2"] = stringValue;
            //dataRowMock["RandomNonEktronProp"] = stringValue;
            //dataRowMock["RandomInteger"] = integerValue;

            //string propName =
            //    PropertyExpressionHelper.GetSmartFormPropertyExpression(
            //        "RandomSmartFormProperty", typeof(Ek.StringPropertyExpression)).Name;
            //dataRowMock[propName] = stringValue;
            //propName =
            //    PropertyExpressionHelper.GetCustomPropertyExpression(
            //        "RandomCustomProperty", typeof(Ek.StringPropertyExpression)).Name;
            //dataRowMock[propName] = stringValue;

            return dataRowMock;
        }

        internal static QueryModel GetQueryModel<T>(IQueryable<T> query)
        {
            return QueryParser.CreateDefault().GetParsedQuery(query.Expression);
        }

        internal static SearchResponseData GetResponseData<T>(int howManyResults)
        {
            var dataRowMock = GetDummyDataRow<T>();
            var results = new List<SearchResultData>();

            for (int i = 0; i < howManyResults; i++)
            {
                results.Add(new SearchResultData(dataRowMock));
            }

            return new SearchResponseData { Results = results };
        }

        internal static global::Ektron.Cms.Search.Expressions.Expression GetTranslation<T>(IQueryable<T> query)
        {
            var queryModel = GetQueryModel(query);
            return SearchQueryModelVisitor.Translate(queryModel, new IdProvider()).ExpressionTree;
        }

        #endregion
    }
}