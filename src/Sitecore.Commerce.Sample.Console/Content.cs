
namespace Sitecore.Commerce.Sample.Console
{
    using Contexts;

    using Sitecore.Commerce.ServiceProxy;
    using CommerceOps.Sitecore.Commerce.Engine;

    using Sitecore.Commerce.Extensions;

    public static class Content
    {
        public static void RunScenarios()
        {
            using (new SampleScenarioScope("Content"))
            {
                var devOp = new DevOpAndre();
                var container = devOp.Context.OpsContainer();

                EnsureSyncDefaultContentPaths(container, "AdventureWorksAuthoring", devOp.Context.Shop);
                EnsureSyncDefaultContentPaths(container, "HabitatAuthoring", devOp.Context.Shop);
            }
        }

        private static void EnsureSyncDefaultContentPaths(Container container, string environmentName, string shopName)
        {
            using (new SampleMethodScope())
            {
                var result = Proxy.GetValue(container.EnsureSyncDefaultContentPaths(environmentName, shopName));
                result.WaitUntilCompletion();
            }
        }
    }
}
