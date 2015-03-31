/**
 * OrderFulfillment - SKUBI XML eBay Schema
 */

/**
 * Configures input, output, and processing options.
 * This function is called only ONCE by the tranformation processor to read
 * the configuration data.
 */
function config() {

    // The input file format
	CONFIG.FileFormat = 'XML';

    /*
     * The XPath expression to select the nodes/records
     * to be evaluated by transform().
     */
    CONFIG.RecordPath = '/orderFulfillmentRequest/orderFulfillment';
}

function transform() {

    ORDERFULFILLMENT.OrderID = ORDERFULFILLMENT_INRECORD.orderID.toString();
    ORDERFULFILLMENT.ChannelID = ORDERFULFILLMENT_INRECORD.channelID.toString();
    ORDERFULFILLMENT.MerchantOrderID = ORDERFULFILLMENT_INRECORD.merchantOrderID.toString();

    if (ORDERFULFILLMENT_INRECORD.fulfillmentInfo.length() > 0) {

        var fulfillmentInfo = ORDERFULFILLMENT_INRECORD.fulfillmentInfo;
        ORDERFULFILLMENT.Date = fulfillmentInfo.date.toString();
        var logisticsStatus = fulfillmentInfo.logisticsStatus.toString().toUpperCase();

        if (logisticsStatus == 'SHIPPED') {
            ORDERFULFILLMENT.ShippingCarrier = fulfillmentInfo.shipping.shipmentTracking.shippingCarrier.toString();
            ORDERFULFILLMENT.TrackingNumber = fulfillmentInfo.shipping.shipmentTracking.trackingNumber.toString();
            ORDERFULFILLMENT.LogisticsStatus = logisticsStatus;
        } else if (logisticsStatus == 'READY_FOR_PICKUP' || logisticsStatus == 'PICKED_UP') {
            ORDERFULFILLMENT.PickupCode = fulfillmentInfo.pickup.pickupCode.toString();
            ORDERFULFILLMENT.PickupNote = fulfillmentInfo.pickup.pickupNote.toString();
            ORDERFULFILLMENT.LogisticsStatus = logisticsStatus;
        }

    } else if (ORDERFULFILLMENT_INRECORD.lineItem.length() > 0) {

        var lineItemRecords = ORDERFULFILLMENT_INRECORD.lineItem;

        for each(var lineItemRecord in lineItemRecords) {
            var lineItemID = lineItemRecord.lineItemID.toString();
            var lineItem = ORDERFULFILLMENT.createLineItem(lineItemID);
            lineItem.LineItemID = lineItemID;
            lineItem.ChannelID = lineItemRecord.listing.channelID.toString();
            lineItem.ItemID = lineItemRecord.listing.itemID.toString();
            lineItem.SKU = lineItemRecord.listing.SKU.toString();
            lineItem.Title = lineItemRecord.listing.title.toString();
            var fulfillmentInfo = lineItemRecord.lineItemFulfillmentInfo;
            if (fulfillmentInfo.length() > 0) {
                lineItem.Date = fulfillmentInfo.date.toString();
                var logisticsStatus = fulfillmentInfo.logisticsStatus.toString().toUpperCase();
                if (logisticsStatus === 'SHIPPED') {
                    
                    var shipmentTrackingObjects = fulfillmentInfo.shipping.shipmentTracking;
                    if (shipmentTrackingObjects.length() > 0) {
                        lineItem.ShippingCarrier = shipmentTrackingObjects[0].shippingCarrier.toString();
                        lineItem.TrackingNumber = shipmentTrackingObjects[0].trackingNumber.toString();

                        if (shipmentTrackingObjects.length() > 1) {
                            var trackingDetails = new Array();
                            for (var i = 1; i < shipmentTrackingObjects.length(); i++) {
                                var shipmentTrackingObject = shipmentTrackingObjects[i];
                                var trackingMap = {};
                                trackingMap['ShippingCarrier'] = shipmentTrackingObject.shippingCarrier.toString();
                                trackingMap['TrackingNumber'] = shipmentTrackingObject.trackingNumber.toString();
                                trackingMap['Quantity'] = shipmentTrackingObject.quantity.toString();
                                trackingDetails.push(trackingMap);
                            }
                            lineItem.TrackingDetails = trackingDetails;
                        } else {
                            lineItem.TrackingDetails = null;
                        }
                    }
                    
                    lineItem.LogisticsStatus = logisticsStatus;
                                  
                } else if (logisticsStatus === 'READY_FOR_PICKUP' || logisticsStatus === 'PICKED_UP') {
                    /* To be supported in future */
                    lineItem.LogisticsStatus = logisticsStatus;
                }
            }
        }
    }
}