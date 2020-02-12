namespace Sitecore.Commerce.Sample.Console
{
    using System;

    using FluentAssertions;

    using Sitecore.Commerce.Core;
    using Sitecore.Commerce.Extensions;
    using Sitecore.Commerce.Plugin.Fulfillment;
    using Sitecore.Commerce.Sample.Contexts;
    using Sitecore.Commerce.ServiceProxy;

    public static class Fulfillment
    {
        private static string _cartId;
        private static string _cartLineId;
        private static readonly Sitecore.Commerce.Engine.Container ShopsContainer = new CsrSheila().Context.ShopsContainer();

        public static void RunScenarios()
        {
            using (new SampleScenarioScope("Fulfillment"))
            {
                _cartId = Guid.NewGuid().ToString("B");
                _cartLineId = Carts.AddCartLineWithVariant(_cartId);

                GetFulfillmentMethods();
                GetCartFulfillmentOptions();
                GetCartFulfillmentMethods();
                GetCartLineFulfillmentOptions();
                GetCartLineFulfillmentMethods();

                Carts.DeleteCart(_cartId);
            }
        }

        private static void GetCartFulfillmentOptions()
        {
            using (new SampleMethodScope())
            {
                var options = Proxy.Execute(ShopsContainer.GetCartFulfillmentOptions(_cartId));
                options.Should().NotBeEmpty();
            }
        }

        private static void GetCartLineFulfillmentOptions()
        {
            using (new SampleMethodScope())
            {
                var options = Proxy.Execute(ShopsContainer.GetCartLineFulfillmentOptions(_cartId, _cartLineId));
                options.Should().NotBeEmpty();
            }
        }

        private static void GetFulfillmentMethods()
        {
            using (new SampleMethodScope())
            {
                var methods = Proxy.Execute(ShopsContainer.GetFulfillmentMethods());
                methods.Should().NotBeEmpty();
            }
        }

        private static void GetCartFulfillmentMethods()
        {
            using (new SampleMethodScope())
            {
                var methods = Proxy.Execute(
                    ShopsContainer.GetCartFulfillmentMethods(
                        _cartId,
                        new PhysicalFulfillmentComponent
                        {
                            ShippingParty = new Party
                            {
                                FirstName = "first name",
                                LastName = "last name",
                                AddressName = "name",
                                Address1 = "line 1",
                                City = "city",
                                State = "Ontario",
                                StateCode = "ON",
                                Country = "Canada",
                                CountryCode = "CA",
                                ZipPostalCode = "postalcode"
                            }
                        }));

                methods.Should().NotBeEmpty();
            }
        }

        private static void GetCartLineFulfillmentMethods()
        {
            using (new SampleMethodScope())
            {
                var methods = Proxy.Execute(
                    ShopsContainer.GetCartLineFulfillmentMethods(
                        _cartId,
                        new PhysicalFulfillmentComponent
                        {
                            ShippingParty = new Party
                            {
                                FirstName = "first name",
                                LastName = "last name",
                                AddressName = "name",
                                Address1 = "line 1",
                                City = "city",
                                State = "Ontario",
                                StateCode = "ON",
                                Country = "Canada",
                                CountryCode = "CA",
                                ZipPostalCode = "postalcode"
                            },
                            LineId = _cartLineId
                        }));

                methods.Should().NotBeEmpty();
            }
        }
    }
}
