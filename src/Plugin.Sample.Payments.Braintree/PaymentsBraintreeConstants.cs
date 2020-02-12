﻿// © 2016 Sitecore Corporation A/S. All rights reserved. Sitecore® is a registered trademark of Sitecore Corporation A/S.

namespace Plugin.Sample.Payments.Braintree
{
    /// <summary>
    /// The payments constants.
    /// </summary>
    public static class PaymentsBraintreeConstants
    {
        /// <summary>
        /// The get client token block name.
        /// </summary>
        public const string GetClientTokenBlock = "PaymentsBraintree.block.getclienttoken";

        /// <summary>
        /// The add federated payment block
        /// </summary>
        public const string UpdateFederatedPaymentBlock = "PaymentsBraintree.block.updatefederatedpayment";

        /// <summary>
        /// The update order after federated payment settlement block name
        /// </summary>
        public const string UpdateOrderAfterFederatedPaymentSettlementBlock = "PaymentsBraintree.block.UpdateOrderAfterFederatedPaymentSettlement";

        /// <summary>
        /// The create federated payment block
        /// </summary>
        public const string CreateFederatedPaymentBlock = "PaymentsBraintree.block.createfederatedpayment";

        /// <summary>
        /// The settle federated payment block name
        /// </summary>
        public const string SettleFederatedPaymentBlock = "PaymentsBraintree.block.SettleFederatedPayment";

        /// <summary>
        /// The void cancel order federated payment block
        /// </summary>
        public const string VoidCancelOrderFederatedPaymentBlock = "PaymentsBraintree.block.voidcancelorderfederatedpayment";

        /// <summary>
        /// The refund federated payment block
        /// </summary>
        public const string RefundFederatedPaymentBlock = "PaymentsBraintree.block.refundfederatedpayment";

        /// <summary>
        /// The registered plugin block name.
        /// </summary>
        public const string RegisteredPluginBlock = "PaymentsBraintree.block.RegisteredPlugin";

        /// <summary>
        /// Settle order sales activities block name.
        /// </summary>
        public const string SettleOrderSalesActivitiesBlock = "PaymentsBraintree.block.SettleOrderSalesActivities";
    }
}
