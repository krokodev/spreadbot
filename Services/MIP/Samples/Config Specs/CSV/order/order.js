/**
 * Orders - XML Reference Implementation
 *
 * The Order Report's input source conforms to the OrderReport XML format
 * governed by eBay's Large Merchant Services. The documentation and schema can
 * be found at the following locations:
 *
 *     http://developer.ebay.com/DevZone/merchant-data/CallRef/OrderReport.html
 *     http://developer.ebay.com/webservices/latest/merchantdataservice.xsd
 *
 * Benefits of using the OrderReport's XML format as-is:
 *
 * >> The resulting Order Report can be validated against the published schema.
 * >> The resulting Order Report can be deserialized to objects with code
 *    generation from the schema.
 * >> If a new XML element is added to the OrderReport, the element is
 *    automatically added to the resulting Order Report.
 */

 /**
 * Configures input, output, and processing options.
 */
function config() {
	CONFIG.FileFormat = 'XML';

    // The output file format
    CONFIG.OutputFileFormat = 'CSV';

    // Skip row when header is present in CSV
	CONFIG.SkipRowCount = '1';

    // Columns that need to be added in the Order output
	CONFIG.OutputColumnNames = ['orderID', 'channelID', 'merchantOrderID', 'createdDate', 'buyerID', 'buyerEmail', 'buyerName', 'buyerFirstName', 'buyerLastName', 'buyerAddressID', 
                                'buyerNameOnAddress', 'buyerPhone', 'buyerAddressLine1', 'buyerAddressLine2', 'buyerCity', 'buyerCounty', 'buyerStateOrProvince', 'buyerPostalCode', 
                                'buyerCountry', 'sellerID', 'fulfillmentType', 'programType', 'shippingType', 'shippingServiceID', 'shippingServiceName', 'shippingMethod', 'shipToAddressID', 
                                'shipToAddressName', 'shipToAddressPhone', 'shipToAddressLine1', 'shipToAddressLine2', 'shipToAddressCity', 'shipToAddressCounty', 'shipToAddressStateOrProvince', 
                                'shipToAddressPostalCode', 'shipToAddressCountry', 'expectedDeliveryDate', 'finalDestinationAddressID', 'finalDestinationAddressName', 'finalDestinationAddressPhone', 
                                'finalDestinationAddressLine1', 'finalDestinationAddressLine2', 'finalDestinationCity', 'finalDestinationCounty', 'finalDestinationStateOrProvince', 'finalDestinationPostalCode', 
                                'finalDestinationCountry', 'pickupType', 'pickupLocationID', 'pickupAddressID', 'pickupAddressName', 'pickupAddressPhone', 'pickupAddressLine1', 'pickupAddressLine2', 'pickupAddressCity',
                                'pickupAddressCounty', 'pickupAddressStateOrProvince', 'pickupAddressPostalCode', 'pickupAddressCountry', 'expectedPickupTime', 'lineItemID', 'listingChannelID',
                                'itemID', 'SKU', 'title', 'quantity', 'unitPrice', 'unitPriceCurrency', 'lineItemPriceLines', 'lineItemSumTotal', 'lineItemSumTotalCurrency', 'logisticsStatus', 
                                'giftMessage', 'giftWrap', 'lineItemNote', 'paymentProcessedBy', 'paymentID', 'paidToAccount', 'paymentTotal', 'paymentCurrency', 'paymentClearedDate', 
                                'orderPriceLines', 'orderSumTotal', 'orderSumTotalCurrency', 'orderPaymentStatus', 'orderLogisticsStatus', 'note'];

	CONFIG.RecordPath = '/BulkDataExchangeResponses/OrderReport/OrderArray/Order';
	
	LOOKUP_SERVICE.loadDelimitedFile('ChannelIDLookup.csv', ',', [0], [3]);
}

/**
 * Transforms an 'ORDER_INRECORD' to an element (or set of elements) that can
 * optionally be appended to the 'ORDER' document.
 *
 * An 'ORDER_INRECORD' is the E4X (ECMAScript for XML) representation of an
 * <Order> record.
 *     
 * This function is invoked once per <Order> record. Preceding each
 * invocation, the 'ORDER_INRECORD' is initialized to the next <Order>
 * in the underlying OrderReport. 
 * 
 * The 'ORDER_INRECORD' can then be modified and/or used to create an element,
 * or set of elements, that are appended/added to the 'ORDER' document.
 */
function transform() {
   
   // Even if there is <Order></Order> a record will be created. So putting it without a check will create empty records.
   // For an order with a single line item atleast one transaction will be there. So adding Transaction check.

   var transactions = ORDER_INRECORD.TransactionArray.Transaction;

   if (transactions.length() > 0) {

       var i = 0;
       var channelID = "";

       var site = transactions[0].TransactionSiteID.toString();
       if (typeof site !== 'undefined' && !isEmptyOrNull(site)) {
           var channelIDResult = LOOKUP_SERVICE.get('ChannelIDLookup.csv', [site]);
           if (!isEmptyOrNull(channelIDResult)) {
               channelID = channelIDResult[0];
           }
       }

       var buyerFirstName = transactions[0].Buyer.UserFirstName.toString();
       var buyerLastName = transactions[0].Buyer.UserLastName.toString();
       var buyerEmail = transactions[0].Buyer.Email.toString();

       var transactionShippingDetail = transactions[0].ShippingDetails;


       while ( i < transactions.length()) {

            // Create an empty order record at the order level
            var order = ORDERS.createOrder();
            order.orderID = ORDER_INRECORD.OrderID.toString();
            order.channelID = channelID;
            order.createdDate = ORDER_INRECORD.CreatedTime.toString();

            /* Buyer */
            order.buyerID = ORDER_INRECORD.BuyerUserID.toString();
            order.buyerEmail = buyerEmail;
            order.buyerFirstName = buyerFirstName;
            order.buyerLastName = buyerLastName;

            /* Seller */
            order.sellerID = ORDER_INRECORD.SellerUserID.toString();

            /* LogisticsPlan */
            var fulFillmentType;
            var logisticsStatus;
            if (ORDER_INRECORD.PickupMethodSelected.length() > 0) {
                fulFillmentType = "PICKUP";
            } else {
                fulFillmentType = "SHIP";
            }
            order.fulfillmentType = fulFillmentType;
            
            /* FulFillment Type : SHIP */
            if (fulFillmentType == "SHIP") {

                var shippingType;
                if (ORDER_INRECORD.IsMultiLegShipping == false) {
                    shippingType = "DIRECT";
                } else {
                    shippingType = "MULTILEG";
                }

                order.shippingType = shippingType;
                order.shippingServiceName = transactionShippingDetail.ShipmentTrackingDetails.ShippingCarrierUsed.toString();    //Assuming seller uses same carrier for multiple packets of an order
                order.shippingMethod = ORDER_INRECORD.ShippingServiceSelected.ShippingService.toString();

                order.shipToAddressID = ORDER_INRECORD.ShippingAddress.AddressID.toString();
                order.shipToAddressName = ORDER_INRECORD.ShippingAddress.Name.toString();
                order.shipToAddressPhone = ORDER_INRECORD.ShippingAddress.Phone.toString();
                order.shipToAddressLine1 = ORDER_INRECORD.ShippingAddress.Street1.toString();
                order.shipToAddressLine2 = ORDER_INRECORD.ShippingAddress.Street2.toString();
                order.shipToAddressCity = ORDER_INRECORD.ShippingAddress.CityName.toString();
                order.shipToAddressStateOrProvince = ORDER_INRECORD.ShippingAddress.StateOrProvince.toString();
                order.shipToAddressPostalCode = ORDER_INRECORD.ShippingAddress.PostalCode.toString();
                order.shipToAddressCountry = ORDER_INRECORD.ShippingAddress.Country.toString();

                order.expectedDeliveryDate = transactionShippingDetail.ShippingServiceOptions.ShippingPackageInfo.EstimatedDeliveryTimeMax.toString();

                /*Set Logistic Status */
                if (ORDER_INRECORD.ShippedTime.length() > 0) {
                    logisticsStatus = "SHIPPED";
                } else {
                    logisticsStatus = "NOT_SHIPPED";
                }
            }

            /* FulFillment Type : PICKUP */
            if (fulFillmentType == "PICKUP") {
                var pickupMethod = ORDER_INRECORD.PickupMethodSelected.PickupMethod.toString();
                if (pickupMethod.toUpperCase() == "INSTOREPICKUP") {
                    order.pickupType = "RECIPIENT";
                }
                order.pickupLocationID = ORDER_INRECORD.PickupMethodSelected.PickupStoreID.toString();
                
                /* Set Pickup Status */
                var pickupStatus = ORDER_INRECORD.PickupMethodSelected.PickupStatus.toString().toUpperCase();
                if (pickupStatus == "READYTOPICKUP") {
                    logisticsStatus = "READY_FOR_PICKUP";
                } else if (pickupStatus == "PICKEDUP") {
                    logisticsStatus = "PICKED_UP";
                } else {
                    logisticsStatus = "NOT_READY_FOR_PICKUP";
                }
            }

            /* LineItem */
            var lineItem_INRECORD = transactions[i];

            order.orderID = ORDER_INRECORD.OrderID.toString();

            order.lineItemID = lineItem_INRECORD.OrderLineItemID.toString();
            var lineItemSite = lineItem_INRECORD.Item.Site.toString();
            if (typeof lineItemSite !== 'undefined' && !isEmptyOrNull(lineItemSite)) {
                var lineItemChannelIDresult = LOOKUP_SERVICE.get('ChannelIDLookup.csv', [lineItemSite]);
                if (!isEmptyOrNull(lineItemChannelIDresult)) {
                    order.listingChannelID = lineItemChannelIDresult[0];
                }
            }
            order.itemID = lineItem_INRECORD.Item.ItemID.toString();
            if (lineItem_INRECORD.Variation.length() > 0){
		        /* MSKU Item */
		        order.SKU = lineItem_INRECORD.Variation.SKU.toString();
		    } else {
		        /* Non-MSKU Item */
		        order.SKU = lineItem_INRECORD.Item.SKU.toString();
		    }
            order.title = lineItem_INRECORD.Item.Title.toString();

            order.quantity = lineItem_INRECORD.QuantityPurchased.toString();
            var currencyCode = lineItem_INRECORD.TransactionPrice.@["currencyID"].toString();
            order.unitPrice = lineItem_INRECORD.TransactionPrice.toString();
            order.unitPriceCurrency = currencyCode.toString();

            
            /* lineItemPriceLines */
            var lineItemSumTotalAmount = 0;
            var lineItemCurrencyCode = lineItem_INRECORD.TransactionPrice.@["currencyID"].toString();
            
            var lineItemPriceLines = [];

            /* LineItem Price */
            var itemAmount = order.unitPrice * order.quantity * 1.00;
            var itemAmountFixed = itemAmount.toFixed(2);
            var itemAmountCurrency = lineItemCurrencyCode;
            lineItemSumTotalAmount = lineItemSumTotalAmount + itemAmount;
            lineItemPriceLines.push(JSON.stringify({
                "pricelineType":"ITEM",
                "amount":itemAmountFixed,
                "currency":itemAmountCurrency
            }));

            /* LineItem Shipment Price */
            var shipAmount = lineItem_INRECORD.ActualShippingCost * 1.00;
            var shipAmountFixed = shipAmount.toFixed(2);
            var shipAmountCurrency = ORDER_INRECORD.ActualShippingCost.@["currencyID"].toString();
            lineItemSumTotalAmount = lineItemSumTotalAmount + shipAmount;
            lineItemPriceLines.push(JSON.stringify({
                "pricelineType":"SHIPPING",
                "amount":shipAmountFixed,
                "currency":shipAmountCurrency
            }));

            /* LineItem Tax */
            var j = 0;
            while ( j < lineItem_INRECORD.Taxes.TaxDetails.length()) {
                
                var taxPriceLine_INRECORD = lineItem_INRECORD.Taxes.TaxDetails[j];
                var taxType = taxPriceLine_INRECORD.Imposition.toString().toUpperCase();
                var taxDescription = taxPriceLine_INRECORD.TaxDescription.toString();
                var taxAmount = taxPriceLine_INRECORD.TaxAmount * 1.00;
                var taxAmountFixed = taxAmount.toFixed(2);
                var taxAmountCurrency = taxPriceLine_INRECORD.TaxAmount.@["currencyID"].toString();

                lineItemSumTotalAmount = lineItemSumTotalAmount + taxAmount;

                if ( taxType == "SALESTAX" ) {                    
                    lineItemPriceLines.push(JSON.stringify({
                        "pricelineType":"ITEM_TAX",
                        "description":taxDescription,
                        "amount":taxAmountFixed,
                        "currency":taxAmountCurrency
                    }));
                } else if ( taxType == "WASTERECYCLINGFEE" ) {
                    lineItemPriceLines.push(JSON.stringify({
                        "pricelineType":"RECYCLING",
                        "description":taxDescription,
                        "amount":taxAmountFixed,
                        "currency":taxAmountCurrency
                    }));
                }
                j++;
            }

            order.lineItemPriceLines = ("[" + lineItemPriceLines + "]").toString();

            /* SumTotal */
            order.lineItemSumTotal = lineItemSumTotalAmount.toFixed(2);
            order.lineItemSumTotalCurrency = lineItemCurrencyCode;

            /* Payment */
            if (ORDER_INRECORD.MonetaryDetails.Payments.length() > 0) {
                order.paymentID = ORDER_INRECORD.MonetaryDetails.Payments.Payment.ReferenceID.toString();
                order.paymentTotal = ORDER_INRECORD.MonetaryDetails.Payments.Payment.PaymentAmount.toString();
                order.paymentCurrency = ORDER_INRECORD.MonetaryDetails.Payments.Payment.PaymentAmount.@["currencyID"].toString();
                
            }

            /* orderPriceLineItems */
            var orderPriceLines = [];

            /* Total Item Price */
            var totalItemPriceLineAmount = ORDER_INRECORD.Subtotal.toString();
            var totalItemPriceLineAmountCurrency = ORDER_INRECORD.Subtotal.@["currencyID"].toString(); //Copying SubTotal as Item level price for order
            orderPriceLines.push(JSON.stringify({
                "pricelineType":"ITEM",
                "amount":totalItemPriceLineAmount,
                "currency":totalItemPriceLineAmountCurrency
            }));

            /* Total Tax */
            var totalTaxPriceLineAmount = ORDER_INRECORD.ShippingDetails.SalesTax.SalesTaxAmount.toString();
            var totalTaxPriceLineAmountCurrency = ORDER_INRECORD.ShippingDetails.SalesTax.SalesTaxAmount.@["currencyID"].toString();
            orderPriceLines.push(JSON.stringify({
                "pricelineType":"ITEM_TAX",
                "amount":totalTaxPriceLineAmount,
                "currency":totalTaxPriceLineAmountCurrency
            }));

            /* Total Shipping Price */
            var totalShippingPriceLineAmount = ORDER_INRECORD.ShippingServiceSelected.ShippingServiceCost.toString();
            var totalShippingPriceLineAmountCurrency = ORDER_INRECORD.ShippingServiceSelected.ShippingServiceCost.@["currencyID"].toString();
            orderPriceLines.push(JSON.stringify({
                "pricelineType":"SHIPPING",
                "amount":totalShippingPriceLineAmount,
                "currency":totalShippingPriceLineAmountCurrency
            }));

            /* Total Discount */
            var totalDiscountPriceLineAmount = ORDER_INRECORD.AdjustmentAmount.toString();
            var totalDiscountPriceLineAmountCurrency = ORDER_INRECORD.AdjustmentAmount.@["currencyID"].toString();
            orderPriceLines.push(JSON.stringify({
                "pricelineType":"DISCOUNT",
                "amount":totalDiscountPriceLineAmount,
                "currency":totalDiscountPriceLineAmountCurrency
            }));

            order.orderPriceLines = ("[" + orderPriceLines + "]").toString();

            /* Total */
            order.orderSumTotal = ORDER_INRECORD.Total.toString();
            order.orderSumTotalCurrency = ORDER_INRECORD.Total.@["currencyID"].toString();

            /* Status */
            var eBayPaymentStatus = ORDER_INRECORD.CheckoutStatus.eBayPaymentStatus.toString().toUpperCase();
            if (eBayPaymentStatus == "NOPAYMENTFAILURE"){
	            order.orderPaymentStatus = "PAID";
	        }
	        else if (eBayPaymentStatus == "PAYMENTINPROCESS" || eBayPaymentStatus == "PAYPALPAYMENTINPROCESS" ){
	            order.orderPaymentStatus = "PENDING";
	        }
	        else {
	            order.orderPaymentStatus = "FAILED";
	        } 

            order.orderLogisticsStatus = logisticsStatus;
            order.note = ORDER_INRECORD.BuyerCheckoutMessage.toString();
            
            i++;
       }
   }
}

function isEmptyOrNull(str) {
    return (str == null || str == '');
}