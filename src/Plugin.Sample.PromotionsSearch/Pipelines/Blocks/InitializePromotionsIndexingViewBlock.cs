using System;
using System.Linq;
using System.Threading.Tasks;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.EntityViews;
using Sitecore.Commerce.Plugin.Promotions;
using Sitecore.Commerce.Plugin.Search;
using Sitecore.Framework.Conditions;
using Sitecore.Framework.Pipelines;

namespace Plugin.Sample.PromotionsSearch.Pipelines.Blocks
{
    public class InitializePromotionsIndexingViewBlock : PipelineBlock<EntityView, EntityView, CommercePipelineExecutionContext>
    {
        public override Task<EntityView> Run(
            EntityView arg,
            CommercePipelineExecutionContext context)
        {

            Condition.Requires(arg).IsNotNull(Name + ": argument cannot be null.");

            var indexMinionArgument = context.CommerceContext.GetObjects<SearchIndexMinionArgument>().FirstOrDefault();

            if (string.IsNullOrEmpty(indexMinionArgument?.Policy?.Name))
                return Task.FromResult(arg);
            var entities = indexMinionArgument.Entities;
            var source = entities?.OfType<Promotion>().ToList();

            if (source == null || !source.Any())
                return Task.FromResult(arg);

            var searchViewNames = context.GetPolicy<KnownSearchViewsPolicy>();

            source.ForEach(promotion =>
            {
                var entityView = arg.ChildViews.Cast<EntityView>().FirstOrDefault(
                    v =>
                    {
                        if (v.EntityId.Equals(promotion.Id, StringComparison.OrdinalIgnoreCase))
                            return v.Name.Equals(searchViewNames.Document, StringComparison.OrdinalIgnoreCase);
                        return false;
                    });

                if (entityView == null)
                {
                    entityView = new EntityView()
                    {
                        Name = context.GetPolicy<KnownSearchViewsPolicy>().Document,
                        EntityId = promotion.Id
                    };
                    arg.ChildViews.Add(entityView);
                }

                entityView.Properties.Add(new ViewProperty()
                {
                    Name = "Name",
                    RawValue = promotion.Name
                });
                entityView.Properties.Add(new ViewProperty()
                {
                    Name = "Description",
                    RawValue = promotion.Description
                });
                entityView.Properties.Add(new ViewProperty()
                {
                    Name = "Status",
                    RawValue = promotion.IsApproved(context.CommerceContext) ? "Approved" : "Draft"
                });
                entityView.Properties.Add(new ViewProperty()
                {
                    Name = "CartText",
                    RawValue = promotion.DisplayCartText
                });
                entityView.Properties.Add(new ViewProperty()
                {
                    Name = "DisplayText",
                    RawValue = promotion.DisplayText
                });
                entityView.Properties.Add(new ViewProperty()
                {
                    Name = "ValidFrom",
                    RawValue = promotion.ValidFrom
                });
                entityView.Properties.Add(new ViewProperty()
                {
                    Name = "ValidTo",
                    RawValue = promotion.ValidTo
                });
                entityView.Properties.Add(new ViewProperty()
                {
                    Name = "ArtifactStoreId",
                    RawValue = context.CommerceContext.Environment.ArtifactStoreId
                });
            });
            return Task.FromResult(arg);
        }
    }

}
