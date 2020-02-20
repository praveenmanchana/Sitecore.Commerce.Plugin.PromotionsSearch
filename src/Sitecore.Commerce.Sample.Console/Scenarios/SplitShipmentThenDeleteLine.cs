namespace Sitecore.Commerce.Sample.Scenarios
{
    using System;
    using System.Diagnostics;
    using System.Linq;

    using EntityViews;
    using Extensions;

    using Sitecore.Commerce.Sample.Console;

    public static class SplitShipmentThenDeleteLine
    {
        public static string ScenarioName = "SplitShipmentThenDeleteLine";

        public static string Run(ShopperContext context)
        {

            using (new SampleBuyScenarioScope())
            {
                try
                {
                    var container = context.ShopsContainer();

                    var orderId = BuySplitShipment.Run(context);
                    if (!string.IsNullOrEmpty(orderId))
                    {
                        OrdersUX.HoldOrder(orderId);
                    }

                    var order = Orders.GetOrder(container, orderId);
                    if (order.Totals.GrandTotal.Amount != 180.40M)
                    {
                        ConsoleExtensions.WriteColoredLine(
                            ConsoleColor.Red,
                            $"GrandTotal Incorrect - Expecting:{180.40M} Actual:{order.Totals.GrandTotal.Amount}");
                    }

                    var lineToDelete = order.Lines.First();
                    var action = new EntityView
                        { Action = "DeleteLineItem", EntityId = orderId, ItemId = lineToDelete.Id };
                    container.DoAction(action).GetValue();
                    return orderId;
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
