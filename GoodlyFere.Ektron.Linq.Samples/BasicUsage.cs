#region Usings

using System;
using System.Linq;
using Ektron.Cms.Search.Expressions;
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

            [EktronProperty(EkConstants.SearchProperties.ContentId,
                EktronExpressionType = typeof(IntegerPropertyExpression))]
            public long Id { get; set; }

            public string Name { get; set; }
            
            #endregion
        }
    }
}