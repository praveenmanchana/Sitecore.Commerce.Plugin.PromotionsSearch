namespace Sitecore.Commerce.Sample.Console
{
    using System.Collections.ObjectModel;
    using System.Linq;

    using FluentAssertions;

    using Sitecore.Commerce.EntityViews;
    using Sitecore.Commerce.Extensions;
    using Sitecore.Commerce.Sample.Contexts;
    using Sitecore.Commerce.ServiceProxy;

    public static class Search
    {
        private static readonly Sitecore.Commerce.Engine.Container ShopsContainer = new CsrSheila().Context.ShopsContainer();

        public static void RunScenarios()
        {
            using (new SampleScenarioScope("Search"))
            {
                var view = Sitecore.Commerce.ServiceProxy.Proxy.GetEntityView(
                    ShopsContainer,
                    string.Empty,
                    "Indexes",
                    string.Empty,
                    string.Empty);
                view.Should().NotBeNull();
                view.ChildViews.Should().NotBeNull();
                view.ChildViews.Should().NotBeEmpty();
                foreach (var entityView in view.ChildViews.OfType<EntityView>())
                {
                    view = new EntityView
                    {
                        Name = "Index",
                        DisplayName = "Index",
                        EntityId = entityView.EntityId,
                        Action = "DeleteSearchIndex"
                    };
                    var result = Sitecore.Commerce.ServiceProxy.Proxy.DoCommand(ShopsContainer.DoAction(view));
                    result.Messages.Should().NotContainErrors();
                }

                // run minions
                var policies =
                    new Collection<CommerceOps.Sitecore.Commerce.Core.Policy>
                    {
                        new CommerceOps.Sitecore.Commerce.Core.RunMinionPolicy { RunChildren = false }
                    };
                var minionResult = Proxy.GetValue(
                    new MinionRunner().Context.MinionsContainer()
                        .RunMinion(
                            "Sitecore.Commerce.Plugin.Search.FullIndexMinion, Sitecore.Commerce.Plugin.Search",
                            "AdventureWorksMinions",
                            policies));
                minionResult.Messages.Should().NotContainErrors();

                minionResult = Proxy.GetValue(
                    new MinionRunner().Context.MinionsContainer()
                        .RunMinion(
                            "Sitecore.Commerce.Plugin.Search.FullIndexMinion, Sitecore.Commerce.Plugin.Search",
                            "HabitatMinions",
                            policies));
                minionResult.Messages.Should().NotContainErrors();
            }
        }
    }
}

