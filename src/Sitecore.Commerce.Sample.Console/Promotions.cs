namespace Sitecore.Commerce.Sample.Console
{
    using System;
    using System.Linq;

    using FluentAssertions;

    using Sitecore.Commerce.Core;
    using Sitecore.Commerce.Extensions;
    using Sitecore.Commerce.Plugin.Promotions;
    using Sitecore.Commerce.Sample.Contexts;
    using Sitecore.Commerce.ServiceProxy;

    public static class Promotions
    {
        private static Sitecore.Commerce.Engine.Container ShopsContainer = new AnonymousCustomerJeff().Context.ShopsContainer();
        private static readonly Sitecore.Commerce.Engine.Container AuthoringContainer = new AnonymousCustomerJeff(EnvironmentConstants.AdventureWorksAuthoring)
            .Context.AuthoringContainer();

        public static void RunScenarios()
        {
            using (new SampleScenarioScope("Promotions"))
            {
                GetPromotionBook();
                GetBookAssociatedCatalogs();

                GetPromotion("AdventureWorksPromotionBook-CartFreeShippingPromotion");
            }
        }

        public static Promotion GetPromotion(string promotionFriendlyId)
        {
            using (new SampleMethodScope())
            {
                var result = Proxy.GetValue(AuthoringContainer.Promotions.ByKey(promotionFriendlyId).Expand("Components"));
                result.Should().NotBeNull();
                result.Components.Should().NotBeEmpty();
                result.Components.OfType<ApprovalComponent>().Any().Should().BeTrue();

                return result;
            }
        }

        public static PromotionBook GetPromotionBook(string bookName = "")
        {
            using (new SampleMethodScope())
            {
                if (string.IsNullOrEmpty(bookName))
                {
                    bookName = "AdventureWorksPromotionBook";
                }

                var result = Proxy.GetValue(
                    ShopsContainer.PromotionBooks.ByKey(bookName).Expand("Components"));
                result.Should().NotBeNull();

                return result;
            }
        }

        private static void GetBookAssociatedCatalogs()
        {
            using (new SampleMethodScope())
            {
                var result = Proxy.Execute(
                    ShopsContainer.GetPromotionBookAssociatedCatalogs("AdventureWorksPromotionBook"));
                result.Should().NotBeNull();
            }
        }
    }
}
