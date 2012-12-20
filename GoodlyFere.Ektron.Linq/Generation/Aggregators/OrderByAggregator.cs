using System.Collections.Generic;
using Ektron.Cms.Search;

namespace GoodlyFere.Ektron.Linq.Generation.Aggregators
{
    internal class OrderByAggregator
    {
        #region Constants and Fields

        private readonly List<OrderData> _orderData;

        #endregion

        #region Constructors and Destructors

        public OrderByAggregator()
        {
            _orderData = new List<OrderData>();
        }

        #endregion

        #region Public Methods

        public void Add(OrderData data)
        {
            _orderData.Add(data);
        }

        public List<OrderData> GetOrderByData()
        {
            return _orderData;
        }

        #endregion
    }
}