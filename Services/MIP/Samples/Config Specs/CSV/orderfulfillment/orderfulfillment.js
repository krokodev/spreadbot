var columnNames = ['c0','c1','c2','c3','c4','c5','c6','c7','c8','c9','c10','c11','c12','c13',
					'c14','c15','c16','c17','c18','c19','c20','c21','c22','c23','c24','c25',
					'c26','c27','c28','c29','c30'];

var orderFulfillmentColumnNames = new Array();
var commentStr = '//';

function config() {
	CONFIG.FileFormat = 'CSV';
	CONFIG.ColumnNames = columnNames;
	CONFIG.SkipRowCount = '0';
}

/*
	1. if it's comments row, we skip and return false
	2. if it's header row, we populate header names into orderFulfillmentColumnNames, and return false,
	3. otherwise, we return true.
*/
function isSkipRecord(){
	if (typeof String.prototype.startsWith != 'function') {
	  // see below for better implementation!
	  String.prototype.startsWith = function (str){
		return this.indexOf(str) == 0;
	  };
	}

	if(ORDERFULFILLMENT_INRECORD[columnNames[0]].startsWith(commentStr) == true){
		LOGGER.log('is comment ');
		return true;
		
	}
	else if(orderFulfillmentColumnNames.length == 0){
	
		for each(columnName in columnNames){
//			LOGGER.log('column is ' + columnName);
			var columnValue = typeof ORDERFULFILLMENT_INRECORD[columnName];
//			LOGGER.log('columnValue ' + ORDERFULFILLMENT_INRECORD[columnName]);
			if(columnValue === 'undefined'){
//				LOGGER.log('is end of header ');
				return true;
			}
			else {
//				LOGGER.log(ORDERFULFILLMENT_INRECORD[columnName].toString());
				orderFulfillmentColumnNames.push(ORDERFULFILLMENT_INRECORD[columnName].toString());
			}
		}
	} else{
		return false;
	}
}

function getColumnValue(columnName) {
	if (!isEmptyOrNull(columnName)){
		var columnIndex = orderFulfillmentColumnNames.indexOf(columnName);
		if (columnIndex > -1){
			return ORDERFULFILLMENT_INRECORD[columnNames[columnIndex]];
		}
	}
    return null;
}

function isEmptyOrNull(str) {
    return (str == null || str == '');
}

function transform() {

    ORDERFULFILLMENT.OrderID = getColumnValue('orderID');
    ORDERFULFILLMENT.ChannelID = getColumnValue('channelID');
    ORDERFULFILLMENT.MerchantOrderID = getColumnValue('merchantOrderID');

	// populate orderFulfillment infor and lineItem Infor
	var orderLogisticsStatus = getColumnValue('orderLogisticsStatus');
//	LOGGER.log('orderLogisticsStatus ' + orderLogisticsStatus);
	var lineItemID = getColumnValue('lineItemID');
//	LOGGER.log('lineItemID ' + lineItemID);
	
	if(!isEmptyOrNull(orderLogisticsStatus)){
		ORDERFULFILLMENT.LogisticsStatus = orderLogisticsStatus;
        ORDERFULFILLMENT.Date = getColumnValue('orderDate');
		
        if (orderLogisticsStatus.toUpperCase() == 'SHIPPED') {
			try{
				var orderShipmentJSONObjs = JSON.parse(getColumnValue('orderShipmentTracking'));
			}catch(e){
				LOGGER.log("for orderId of " + ORDERFULFILLMENT.OrderID);
				LOGGER.log("orderShipmentTracking is not in valid JSON format: " + getColumnValue('orderShipmentTracking'));
				return null;
			}
            ORDERFULFILLMENT.ShippingCarrier = orderShipmentJSONObjs[0].shippingCarrier;
//			LOGGER.log('ShippingCarrier ' + ORDERFULFILLMENT.ShippingCarrier);
            ORDERFULFILLMENT.TrackingNumber = orderShipmentJSONObjs[0].trackingNumber;
//			LOGGER.log('orderShippingCarrier ' + ORDERFULFILLMENT.TrackingNumber);
		} else if (orderLogisticsStatus.toUpperCase() == 'READY_FOR_PICKUP' || orderLogisticsStatus.toUpperCase() == 'PICKED_UP') {
            ORDERFULFILLMENT.PickupCode = getColumnValue('orderPickupCode');
            ORDERFULFILLMENT.PickupNote = getColumnValue('orderPickupNote');
        }
    } else if (!isEmptyOrNull(lineItemID)) {
		var lineItem = ORDERFULFILLMENT.createLineItem(lineItemID);
		lineItem.LineItemID = lineItemID;
		lineItem.ChannelID = getColumnValue('channelID');
		lineItem.ItemID = getColumnValue('itemID');
		lineItem.SKU = getColumnValue('SKU');
		lineItem.Title = getColumnValue('title');
		
		// populate lineItem logistics
		var lineItemLogisticsStatus = getColumnValue('logisticsStatus').toUpperCase();
		if(lineItemLogisticsStatus == 'SHIPPED'){
			lineItem.LogisticsStatus = lineItemLogisticsStatus;
			lineItem.Date = getColumnValue('date');		
			var trackingDetails = new Array();
			
			try{
				var shipmentTrackingJSONObjs = JSON.parse(getColumnValue('shipmentTracking'));
			}catch(e){
				LOGGER.log("shipmentTracking has invalid JSON format: " + getColumnValue('shipmentTracking'));
				return null;
			}
			if (shipmentTrackingJSONObjs.length > 0){
				 lineItem.ShippingCarrier = shipmentTrackingJSONObjs[0].shippingCarrier.toString();
                 lineItem.TrackingNumber = shipmentTrackingJSONObjs[0].trackingNumber.toString();
				 
				 if(shipmentTrackingJSONObjs.length > 1){
					for (var i=1; i<shipmentTrackingJSONObjs.length; i++){
						
						var trackingMap = {};
						trackingMap['ShippingCarrier'] = shipmentTrackingJSONObjs[i].shippingCarrier;
//						LOGGER.log('ItemShippingCarrier ' + trackingMap['ShippingCarrier']);
						
						trackingMap['TrackingNumber'] = shipmentTrackingJSONObjs[i].trackingNumber;
//						LOGGER.log('ItemTrackingNumber ' + trackingMap['TrackingNumber']);
						
						trackingMap['Quantity'] = shipmentTrackingJSONObjs[i].quantity;
//						LOGGER.log('ItemQuantity ' + trackingMap['Quantity']);
						
						trackingDetails.push(trackingMap);
					}
					lineItem.TrackingDetails = trackingDetails;
				 }else {
                    lineItem.TrackingDetails = null;
                 }
			}
		} else if(lineItemLogisticsStatus == 'READY_FOR_PICKUP' || lineItemLogisticsStatus == 'PICKED_UP'){
			// To be supported in future
            lineItem.LogisticsStatus = lineItemLogisticsStatus;
		}
    }
}

function capitaliseFirstLetter(string) {
    if (!isEmptyOrNull(string)) {
        string = string.toLowerCase();
        return string.charAt(0).toUpperCase() + string.slice(1);
    }
}

function isEmptyOrNull(str) {
	return (str == null || str == '');
}