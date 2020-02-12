﻿namespace Sitecore.Commerce.Sample.Console
{
    using System;

    using FluentAssertions;

    using Sitecore.Commerce.Extensions;
    using Sitecore.Commerce.Sample.Contexts;

    public static class Payments
    {
        private static string _cartId;
        private static readonly Sitecore.Commerce.Engine.Container ShopsContainer = new AnonymousCustomerJeff().Context.ShopsContainer();

        public static void RunScenarios()
        {
            using (new SampleScenarioScope("Payments"))
            {
                _cartId = Guid.NewGuid().ToString("B");
                Carts.AddCartLineWithVariant(_cartId);

                GetCartPaymentOptions();
                GetCartPaymentMethods();
            }
        }

        private static void GetCartPaymentOptions()
        {
            using (new SampleMethodScope())
            {
                var options = ShopsContainer.GetCartPaymentOptions(_cartId).Execute();
                options.Should().NotBeEmpty();
            }
        }

        private static void GetCartPaymentMethods()
        {
            using (new SampleMethodScope())
            {
                var method = ShopsContainer.GetCartPaymentMethods(_cartId, "Federated").Execute();
                method.Should().NotBeNull();
            }
        }
    }
}
