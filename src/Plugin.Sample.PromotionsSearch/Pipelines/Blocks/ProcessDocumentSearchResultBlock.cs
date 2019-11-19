using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.EntityViews;
using Sitecore.Commerce.Plugin.Search;
using Sitecore.Framework.Conditions;
using Sitecore.Framework.Pipelines;

namespace Plugin.Sample.PromotionsSearch.Pipelines.Blocks
{
    public class ProcessDocumentSearchResultBlock : PipelineBlock<EntityView, EntityView, CommercePipelineExecutionContext>
    {
        public override Task<EntityView> Run(EntityView arg, CommercePipelineExecutionContext context)
        {
            Condition.Requires(arg).IsNotNull(Name + ": argument can not be null.");

            var source = context.CommerceContext.GetObjects<List<Document>>().FirstOrDefault();
            if (source == null || !source.Any() || !arg.HasPolicy<SearchScopePolicy>() || !arg.GetPolicy<SearchScopePolicy>().ResultDetailsTags.Any(t => t.Name.Equals("PromotionsTable", StringComparison.OrdinalIgnoreCase)))
                return Task.FromResult(arg);

            foreach (var document in source)
            {
                var docId = document["EntityId".ToLowerInvariant()].ToString();
                var child = arg.ChildViews.OfType<EntityView>().FirstOrDefault(c => c.EntityId.Equals(docId, StringComparison.OrdinalIgnoreCase));

                if (child != null)
                {
                    if (!docId.StartsWith("Entity-Promotion-"))
                    {
                        arg.ChildViews.Remove(child);
                    }
                    else
                    {
                        var viewProperty1 = child.Properties.FirstOrDefault(p => p.Name.Equals("EntityId", StringComparison.OrdinalIgnoreCase));
                        if (viewProperty1 != null)
                        {
                            viewProperty1.UiType = string.Empty;
                            viewProperty1.IsHidden = true;
                        }
                        var viewProperty2 = child.Properties.FirstOrDefault(p => p.Name.Equals("Name", StringComparison.OrdinalIgnoreCase));
                        if (viewProperty2 != null)
                            viewProperty2.UiType = "EntityLink";

                        child.Properties.Remove(viewProperty2);
                        child.Properties.Insert(0, viewProperty2);

                        child.EntityId = docId;
                        child.ItemId = docId;
                    }
                }
            }
            return Task.FromResult(arg);
        }
    }
}
