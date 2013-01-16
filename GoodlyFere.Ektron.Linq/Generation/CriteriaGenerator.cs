#region Usings

using System;
using System.Linq;
using Ektron.Cms.Search;
using GoodlyFere.Ektron.Linq.Generation.Transformation.ModelVisitors;
using GoodlyFere.Ektron.Linq.Generation.Translation.ModelVisitors;
using GoodlyFere.Ektron.Linq.Interfaces;
using Remotion.Linq;

#endregion

namespace GoodlyFere.Ektron.Linq.Generation
{
    public static class CriteriaGenerator
    {
        #region Public Methods

        public static AdvancedSearchCriteria Generate(QueryModel model, IEktronIdProvider idProvider)
        {
            TransformationVisitor.Transform(model);
            AdvancedSearchCriteria criteria = TranslationVisitor.Translate(model, idProvider);

            return criteria;
        }

        #endregion
    }
}