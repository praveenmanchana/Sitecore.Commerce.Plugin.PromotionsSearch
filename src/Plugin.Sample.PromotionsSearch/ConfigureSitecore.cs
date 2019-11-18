using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Plugin.Sample.PromotionsSearch.Pipelines.Blocks;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.Plugin.Search;
using Sitecore.Framework.Configuration;
using Sitecore.Framework.Pipelines.Definitions.Extensions;

namespace Plugin.Sample.PromotionsSearch
{
    public class ConfigureSitecore : IConfigureSitecore
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.RegisterAllPipelineBlocks(assembly);

            services.Sitecore().Pipelines(p => p
                .ConfigurePipeline<IIncrementalIndexMinionPipeline>(c => c
                    .Add<InitializePromotionsIndexingViewBlock>().Before<ProcessItemsToAddOrUpdateBlock>())

                .ConfigurePipeline<IFullIndexMinionPipeline>(c => c
                    .Add<InitializePromotionsIndexingViewBlock>().After<ProcessItemsToAddOrUpdateBlock>()
                ));
        }
    }
}
