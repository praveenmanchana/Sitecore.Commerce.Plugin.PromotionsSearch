namespace Sitecore.Commerce.Sample.Scenarios
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;

    using Extensions;
    using FluentAssertions;

    using Sitecore.Commerce.Core;
    using Sitecore.Commerce.Plugin.Carts;
    using Sitecore.Commerce.Plugin.Fulfillment;
    using Sitecore.Commerce.Plugin.Payments;
    using Sitecore.Commerce.Sample.Console;
    using Sitecore.Commerce.ServiceProxy;

    public static class Buy1ItemMultipleQuantity
    {
        public static string ScenarioName = "Buy1ItemMultipleQuantity";

        public static string Run(ShopperContext context)
        {
            using (new SampleBuyScenarioScope())
            {
                try
                {
                    var container = context.ShopsContainer();

                    var cartId = Carts.GenerateCartId();

                    // Add Item
                    Proxy.DoCommand(container.AddCartLine(cartId, "Habitat_Master|6042134|56042134", 3));

                    // Add Cart Level Physical Fulfillment
                    var commandResponse = Proxy.DoCommand(
                        container.SetCartFulfillment(
                            cartId,
                            context.Components.OfType<PhysicalFulfillmentComponent>().First()));
                    var totals = commandResponse.Models.OfType<Totals>().First();
                    totals.AdjustmentsTotal.Amount.Should().Be(18M);
                    totals.GrandTotal.Amount.Should().Be(197.97M);

                    // Add a Payment
                    var paymentComponent = context.Components.OfType<FederatedPaymentComponent>().First();
                    paymentComponent.Amount = Money.CreateMoney(totals.GrandTotal.Amount - totals.PaymentsTotal.Amount);
                    commandResponse = Proxy.DoCommand(container.AddFederatedPayment(cartId, paymentComponent));
                    totals = commandResponse.Models.OfType<Totals>().First();
                    totals.PaymentsTotal.Amount.Should().Be(197.97M);

                    var order = Orders.CreateAndValidateOrder(container, cartId, context);
                    order.Status.Should().NotBe("Problem");
                    order.Totals.GrandTotal.Amount.Should().Be(197.97M);
                    
                    return order.Id;
                }
                catch (Exception ex)
                {
                    ConsoleExtensions.WriteColoredLine(
                        ConsoleColor.Red,
                        $"Exception in Scenario {ScenarioName} (${ex.Message}) : Stack={ex.StackTrace}");
                    return null;
                }
            }
        }
    }
}
