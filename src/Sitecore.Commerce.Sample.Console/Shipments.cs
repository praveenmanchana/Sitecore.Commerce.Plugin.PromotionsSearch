namespace Sitecore.Commerce.Sample.Console
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Sitecore.Commerce.Extensions;
    using Sitecore.Commerce.Plugin.Fulfillment;
    using Sitecore.Commerce.Sample.Contexts;
    using Sitecore.Commerce.ServiceProxy;

    public static class Shipments
    {
        private static string _shipmentId;

        private static readonly Sitecore.Commerce.Engine.Container ShopsContainer = new CsrSheila().Context.ShopsContainer();

        public static void RunScenarios()
        {
            using (new SampleScenarioScope("Shipments"))
            {
                GetShipments();
                GetShipment();
            }
        }

        private static void GetShipments()
        {
            using (new SampleMethodScope())
            {
                var result = Proxy.Execute(ShopsContainer.Shipments);
                var shipments = result as IList<Shipment> ?? result.ToList();
                shipments.Should().NotBeNull();
                shipments.Should().NotBeEmpty();
                _shipmentId = shipments.FirstOrDefault()?.Id;
            }
        }

        private static void GetShipment()
        {
            using (new SampleMethodScope())
            {
                var result = Proxy.GetValue(
                    ShopsContainer.Shipments.ByKey(_shipmentId).Expand("Lines($expand=CartLineComponents)"));
                result.Should().NotBeNull();
                result.OrderId.Should().NotBeNullOrEmpty();
                result.Lines.Should().NotBeNullOrEmpty();
                result.ShipParty.Should().NotBeNull();
                result.Charge.Should().NotBeNull();
                result.Charge.Amount.Should().NotBe(0);
            }
        }
    }
}
