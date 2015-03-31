function startUp() {
}

function config() {
	CONFIG.FileFormat = 'XML';
	CONFIG.RecordPath = '/BulkDataExchangeResponses/OrderReport/OrderArray/Order';
	CONFIG.OutputFileFormat = "XML";
	CONFIG.OutputFileXmlTag = "<pendingOrderFulfillmentResponse/>";
	CONFIG.OutputFileXmlHeader ="<?xml version=\'1.0\' encoding=\'UTF-8\'?>";

    CONFIG.EbayConfigSpec = 'true';
	
	LOOKUP_SERVICE.loadDelimitedFile('ChannelIDLookup.csv', ',', [0], [3]);
}
 
function includeHeader()  {
} 

function transform(){
   
   var pendingOrderFulfillment = <pendingOrderFulfillment/>;
   var order = <order/>;
   
   LOGGER.log("Start of Order transformation");
   
   order.orderID = ORDER_INRECORD.OrderID.toString();
   var site = ORDER_INRECORD.TransactionArray.Transaction[0].TransactionSiteID.toString(); 
   var channelID = LOOKUP_SERVICE.get('ChannelIDLookup.csv', [site]);
   order.channelID = channelID[0];
   order.createdDate = ORDER_INRECORD.CreatedTime.toString();                                   
   
   /*Buyer */
   var buyer = <buyer/>;
	   buyer.buyerID = ORDER_INRECORD.BuyerUserID.toString();
	   buyer.firstName = ORDER_INRECORD.TransactionArray.Transaction[0].Buyer.UserFirstName.toString();
	   buyer.lastName =ORDER_INRECORD.TransactionArray.Transaction[0].Buyer.UserLastName.toString();
	   buyer.email = ORDER_INRECORD.TransactionArray.Transaction[0].Buyer.Email.toString();
   order.appendChild(buyer);
   
   /*Seller */
   var seller =<seller/>;
	seller.sellerID = ORDER_INRECORD.SellerUserID.toString();
   order.appendChild(seller);
   
   /* LogisticsPlan */
   var logisticsPlan = <logisticsPlan/>;
	   var fulFillmentType;
	   var logisticsStatus;
	   if (ORDER_INRECORD.PickupMethodSelected.length() > 0){
	   fulFillmentType = "PICKUP";
	   }
	   else{
	   fulFillmentType = "SHIP";
	   }
	   logisticsPlan.@["fulfillmentType"] = fulFillmentType;
	   
	   /* FulFillment Type : SHIP */
	   if (fulFillmentType == "SHIP") {
		  var shipping = <shipping/>;
		  var shippingType;
		  if (ORDER_INRECORD.IsMultiLegShipping == false){
			 shippingType = "DIRECT";
		  }
		  else{
			 shippingType = "MULTILEG";
		  }
		  shipping.@["shippingType"] = shippingType;
		  
		  var transactionShippingDetail = ORDER_INRECORD.TransactionArray.Transaction[0].ShippingDetails;
		  var shippingService = <shippingService/>;
			  shippingService.service = transactionShippingDetail.ShipmentTrackingDetails.ShippingCarrierUsed.toString();    //Assuming seller uses same carrier for multiple packets of an order
			  shippingService.method =  ORDER_INRECORD.ShippingServiceSelected.ShippingService.toString();
		  shipping.appendChild(shippingService);
		  
		  var shipToAddress = <shipToAddress/>;
			setAddress(shipToAddress);
		  shipping.appendChild(shipToAddress);
		  
		  shipping.expectedDeliveryDate = transactionShippingDetail.ShippingServiceOptions.ShippingPackageInfo.EstimatedDeliveryTimeMax.toString();
		  
		  /*Set Logistic Status */
		  if (ORDER_INRECORD.ShippedTime.length() >0) {
		  logisticsStatus ="SHIPPED";
		  }
		  else{
		  logisticsStatus ="NOT_SHIPPED";
		  }
		  
		  logisticsPlan.appendChild(shipping);
	   }
	   
		/* FulFillment Type : PICKUP */
	   if (fulFillmentType == "PICKUP") {
		   var pickup = <pickup/>;
		   var pickupMethod =  ORDER_INRECORD.PickupMethodSelected.PickupMethod.toString();
		   if (pickupMethod.toUpperCase() == "INSTOREPICKUP"){
		   pickup.@["pickupType"] = "RECIPIENT";
		   }
		   pickup.locationID = ORDER_INRECORD.PickupMethodSelected.PickupStoreID.toString(); 
		   
		   /*Set Pickup Status */
		   var pickupStatus = ORDER_INRECORD.PickupMethodSelected.PickupStatus.toString().toUpperCase();
		   if (pickupStatus == "READYTOPICKUP"){
		   logisticsStatus = "READY_FOR_PICKUP";
		   }
           else if(pickupStatus == "PICKEDUP"){
		   logisticsStatus = "PICKED_UP";
		   }
		   else{
		   logisticsStatus = "NOT_READY_FOR_PICKUP";
		   }
		   logisticsPlan.appendChild(pickup);
	   }
   
   order.appendChild(logisticsPlan);
    
   /*LineItem */
   var i=0;
   while ( i < ORDER_INRECORD.TransactionArray.Transaction.length() ){
       var lineItem = <lineItem/>;
	   var lineItem_InRecord = ORDER_INRECORD.TransactionArray.Transaction[i];
       lineItem.lineItemID =  lineItem_InRecord.OrderLineItemID.toString();
	   
	   var listing = <listing/>;
	     var site = lineItem_InRecord.Item.Site.toString(); 
		 var channelID = LOOKUP_SERVICE.get('ChannelIDLookup.csv', [site]);
		 listing.channelID = channelID[0];
	     listing.itemID = lineItem_InRecord.Item.ItemID.toString();
		 if (lineItem_InRecord.Variation.length() >0){
		    /* MSKU Item */
		    listing.SKU = lineItem_InRecord.Variation.SKU.toString();
		 }
		 else{
		    /* Non-MSKU Item */
		    listing.SKU = lineItem_InRecord.Item.SKU.toString();
		 }
		 listing.title = lineItem_InRecord.Item.Title.toString();
		                    
	   lineItem.appendChild(listing);
	   
	   lineItem.quantity = lineItem_InRecord.QuantityPurchased.toString();                             
	   var currencyCode = lineItem_InRecord.TransactionPrice.@["currencyID"].toString();
	   lineItem.unitPrice = lineItem_InRecord.TransactionPrice.toString();                           
	   lineItem.unitPrice.@["currencyCode"] = currencyCode.toString();
	   
	   var subTotal = <subtotal/>;
		   var sumTotalAmount =0;
		   var lineItemCurrencyCode = lineItem_InRecord.TransactionPrice.@["currencyID"].toString();
		   
		   /*LineItem Price */
		   var priceLine = <priceline/>;
			  priceLine.@["type"] = "ITEM";
			  var itemAmount = lineItem.unitPrice * lineItem.quantity * 1.00;
			  priceLine.amount = itemAmount.toFixed(2);
			  priceLine.amount.@["currencyCode"] = lineItemCurrencyCode;
			  sumTotalAmount = sumTotalAmount + itemAmount;
		   subTotal.appendChild(priceLine);
		   
		   /* LineItem Shipment Price */
		   if (lineItem_InRecord.ActualShippingCost.length() >0){
			   var shipPriceLine = <priceline/>;
			   shipPriceLine.@["type"] = "SHIPPING";
			   var shipAmount = lineItem_InRecord.ActualShippingCost * 1.00;
			   shipPriceLine.amount = shipAmount.toFixed(2);
			   shipPriceLine.amount.@["currencyCode"] = ORDER_INRECORD.ActualShippingCost.@["currencyID"].toString();
			   sumTotalAmount = sumTotalAmount + shipAmount;
			   subTotal.appendChild(shipPriceLine);
		   }
		   
		   /*LineItem Tax */
		   var j =0;
		   while ( j < lineItem_InRecord.Taxes.TaxDetails.length() ) {
		       var taxPriceLine_InRecord = lineItem_InRecord.Taxes.TaxDetails[j];
			   var taxType = taxPriceLine_InRecord.Imposition.toString().toUpperCase();
		       if(taxType == "SALESTAX"){
			   var taxPriceLine = <priceline/>;
			   var taxPriceLine_InRecord = lineItem_InRecord.Taxes.TaxDetails[j];
			   taxPriceLine.@["type"] = "ITEM_TAX";
			   taxPriceLine.description = taxPriceLine_InRecord.TaxDescription.toString();
			   var taxAmount = taxPriceLine_InRecord.TaxAmount * 1.00;
			   taxPriceLine.amount = taxAmount.toFixed(2);
			   taxPriceLine.amount.@["currencyCode"] = taxPriceLine_InRecord.TaxAmount.@["currencyID"].toString();
			   sumTotalAmount = sumTotalAmount + taxAmount;
			   subTotal.appendChild(taxPriceLine);
			   }
			   else if(taxType == "WASTERECYCLINGFEE"){
			   var taxPriceLine = <priceline/>;
			   var taxPriceLine_InRecord = lineItem_InRecord.Taxes.TaxDetails[j];
			   taxPriceLine.@["type"] = "RECYCLING";
			   taxPriceLine.description = taxPriceLine_InRecord.TaxDescription.toString();
			   var taxAmount = taxPriceLine_InRecord.TaxAmount * 1.00;
			   taxPriceLine.amount = taxAmount.toFixed(2);
			   taxPriceLine.amount.@["currencyCode"] = taxPriceLine_InRecord.TaxAmount.@["currencyID"].toString();
			   sumTotalAmount = sumTotalAmount + taxAmount;
			   subTotal.appendChild(taxPriceLine);
			   }
			   j++;
		   }
		   
		   /* SumTotal */
		   subTotal.sumtotal = sumTotalAmount.toFixed(2);
		   subTotal.sumtotal.@["currencyCode"] = lineItemCurrencyCode;
	   
	   lineItem.appendChild(subTotal);	  
	   order.appendChild(lineItem);  
	   i++;
   }
   
   
   /*Payment */
   var payment = <payment/>;
	   if (ORDER_INRECORD.MonetaryDetails.Payments.length() > 0){
	   payment.paymentID = ORDER_INRECORD.MonetaryDetails.Payments.Payment.ReferenceID.toString();
	   payment.total =  ORDER_INRECORD.MonetaryDetails.Payments.Payment.PaymentAmount.toString();
	   payment.total.@["currencyCode"] = ORDER_INRECORD.MonetaryDetails.Payments.Payment.PaymentAmount.@["currencyID"].toString();
	   }
   order.appendChild(payment);
   
   /* Total */
   var total = <total/>;
	   var totalAmount =0;
	   /* Total Item Price */
	   var totalItemPriceLine = <priceline/>;
		  totalItemPriceLine.@["type"] = "ITEM";
		  totalItemPriceLine.amount = ORDER_INRECORD.Subtotal.toString();
		  totalItemPriceLine.amount.@["currencyCode"] = ORDER_INRECORD.Subtotal.@["currencyID"].toString();   //Copying SubTotal as Item level price for order
		  
	   total.appendChild(totalItemPriceLine);
	   
	   /* Total Tax */
	   var totalTaxPriceLine = <priceline/>;
	      totalTaxPriceLine.@["type"] ="ITEM_TAX";
		  totalTaxPriceLine.amount = ORDER_INRECORD.ShippingDetails.SalesTax.SalesTaxAmount.toString();
		  totalTaxPriceLine.amount.@["currencyCode"] = ORDER_INRECORD.ShippingDetails.SalesTax.SalesTaxAmount.@["currencyID"].toString();
	  total.appendChild(totalTaxPriceLine);
	   
	   /*Total Shipping Price */
	   if(ORDER_INRECORD.ShippingServiceSelected.ShippingServiceCost.length() > 0){
	   var totalShippingPriceLine = <priceline/>;
	      totalShippingPriceLine.@["type"] ="SHIPPING";
		  totalShippingPriceLine.amount = ORDER_INRECORD.ShippingServiceSelected.ShippingServiceCost.toString();
		  totalShippingPriceLine.amount.@["currencyCode"] = ORDER_INRECORD.ShippingServiceSelected.ShippingServiceCost.@["currencyID"].toString();
	  total.appendChild(totalShippingPriceLine);
	   }
	   
	   /* Total Discount */
	   var totalDiscountPriceLine = <priceline/>;
	      totalDiscountPriceLine.@["type"] ="DISCOUNT";
		  totalDiscountPriceLine.amount = ORDER_INRECORD.AdjustmentAmount.toString();
		  totalDiscountPriceLine.amount.@["currencyCode"] = ORDER_INRECORD.AdjustmentAmount.@["currencyID"].toString();
	  total.appendChild(totalDiscountPriceLine);
	   
		/* Total : SumTotal */  
	   total.sumtotal = ORDER_INRECORD.Total.toString();
	   total.sumtotal.@["currencyCode"] = ORDER_INRECORD.Total.@["currencyID"].toString();
	   
	order.appendChild(total);
   
   
   /*Status */
   var status = <status/>;
       var eBayPaymentStatus = ORDER_INRECORD.CheckoutStatus.eBayPaymentStatus.toString().toUpperCase();
	   if (eBayPaymentStatus == "NOPAYMENTFAILURE"){
	    status.paymentStatus = "PAID";
	   }
	   else if (eBayPaymentStatus == "PAYMENTINPROCESS" || eBayPaymentStatus == "PAYPALPAYMENTINPROCESS" ){
	    status.paymentStatus = "PENDING";
	   }
	   else{
	    status.paymentStatus = "FAILED";
	   } 
	   
	   status.logisticsStatus = logisticsStatus;                                            
   order.appendChild(status);
   
   /* Note */
   order.note = ORDER_INRECORD.BuyerCheckoutMessage.toString();
   
   
   pendingOrderFulfillment.appendChild(order);
   ORDER.appendChild(pendingOrderFulfillment);
   
   LOGGER.log("End of Order transformation");
}

function setAddress(element) {
   element.addressID = ORDER_INRECORD.ShippingAddress.AddressID.toString();
   element.name = ORDER_INRECORD.ShippingAddress.Name.toString();
   element.phone = ORDER_INRECORD.ShippingAddress.Phone.toString();
   element.addressLine1 = ORDER_INRECORD.ShippingAddress.Street1.toString();
   element.addressLine2 = ORDER_INRECORD.ShippingAddress.Street2.toString();
   element.city = ORDER_INRECORD.ShippingAddress.CityName.toString();
   element.stateOrProvince =  ORDER_INRECORD.ShippingAddress.StateOrProvince.toString();
   element.postalCode = ORDER_INRECORD.ShippingAddress.PostalCode.toString();
   element.country = ORDER_INRECORD.ShippingAddress.Country.toString();
   
}