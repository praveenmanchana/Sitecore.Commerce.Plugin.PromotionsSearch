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

                var properties1 = entityView.Properties;
                properties1.Add(new ViewProperty()
                {
                    Name = "EntityId",
                    RawValue = promotion.Id
                });
                var properties2 = entityView.Properties;
                properties2.Add(new ViewProperty()
                {
                    Name = "Name",
                    RawValue = promotion.Name
                });
                var properties3 = entityView.Properties;
                properties3.Add(new ViewProperty()
                {
                    Name = "Description",
                    RawValue = promotion.Description
                });
                var properties4 = entityView.Properties;
                properties4.Add(new ViewProperty()
                {
                    Name = "Status",
                    RawValue = promotion.IsApproved(context.CommerceContext) ? "Approved" : "Draft"
                });
                var properties5 = entityView.Properties;
                properties5.Add(new ViewProperty()
                {
                    Name = "CartText",
                    RawValue = promotion.DisplayCartText
                });
                var properties6 = entityView.Properties;
                properties6.Add(new ViewProperty()
                {
                    Name = "DisplayText",
                    RawValue = promotion.DisplayText
                });
                var properties7 = entityView.Properties;
                properties7.Add(new ViewProperty()
                {
                    Name = "ValidFrom",
                    RawValue = promotion.ValidFrom
                });
                var properties8 = entityView.Properties;
                properties8.Add(new ViewProperty()
                {
                    Name = "ValidTo",
                    RawValue = promotion.ValidTo
                });
                var properties9 = entityView.Properties;
                properties9.Add(new ViewProperty()
                {
                    Name = "ArtifactStoreId",
                    RawValue = context.CommerceContext.Environment.ArtifactStoreId
                });
            });
            return Task.FromResult(arg);
        }
    }

}
