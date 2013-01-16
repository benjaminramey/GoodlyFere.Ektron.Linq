#region Usings

using System;
using System.Linq;
using Ektron.Cms.Search;
using GoodlyFere.Ektron.Linq.Generation.Transformation.ModelVisitors;
using GoodlyFere.Ektron.Linq.Generation.Translation.ExpressionVisitors;
using GoodlyFere.Ektron.Linq.Generation.Translation.ModelVisitors;
using GoodlyFere.Ektron.Linq.Interfaces;
using Remotion.Linq;

#endregion

namespace GoodlyFere.Ektron.Linq.Generation
{
    public class CriteriaGenerator
    {
        #region Public Methods

        public AdvancedSearchCriteria Generate(QueryModel model, IEktronIdProvider idProvider)
        {
            Properties = PropertyCollectingVisitor.Collect();

            TransformationVisitor.Transform(model);
            AdvancedSearchCriteria criteria = TranslationVisitor.Translate(model, idProvider);

            return criteria;
        }

        #endregion
    }
}