namespace Sitecore.Commerce.Sample.Console
{
    using FluentAssertions;

    using Sitecore.Commerce.Extensions;
    using Sitecore.Commerce.Sample.Contexts;
    using Sitecore.Commerce.ServiceProxy;

    public static class Plugins
    {
        private static readonly CommerceOps.Sitecore.Commerce.Engine.Container OpsContainer = new DevOpAndre().Context.OpsContainer();

        public static void RunScenarios()
        {
            using (new SampleScenarioScope("Plugins"))
            {
                RunningPlugins();
            }
        }

        private static void RunningPlugins()
        {
            using (new SampleMethodScope())
            {
                var result = Proxy.Execute(OpsContainer.RunningPlugins());
                result.Should().NotBeNull();
                result.Should().NotBeEmpty();
            }
        }
    }
}
