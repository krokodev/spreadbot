var columnNames = ['c0','c1','c2','c3','c4','c5','c6','c7','c8','c9','c10','c11','c12','c13',
					'c14','c15','c16','c17','c18','c19','c20','c21','c22','c23','c24','c25',
					'c26','c27','c28','c29','c30'];

var distributionColumnNames = new Array();
var commentStr = '//';

function config() {
	CONFIG.FileFormat = 'CSV';
	CONFIG.ColumnNames = columnNames;
	CONFIG.SkipRowCount = '0';
}

/*
	1. if it's comments row, we skip and return false
	2. if it's header row, we populate header names into distributionColumnNames, and return false,
	3. otherwise, we return true.
*/
function isSkipRecord(){
	if (typeof String.prototype.startsWith != 'function') {
	  // see below for better implementation!
	  String.prototype.startsWith = function (str){
		return this.indexOf(str) == 0;
	  };
	}

	if(DISTRIBUTION_INRECORD[columnNames[0]].startsWith(commentStr) == true){
		LOGGER.log('is comment ');
		return true;
		
	}
	else if(distributionColumnNames.length == 0){
	
		for each(columnName in columnNames){
			LOGGER.log('column is ' + columnName);
			var columnValue = typeof DISTRIBUTION_INRECORD[columnName];
			LOGGER.log('columnValue ' + DISTRIBUTION_INRECORD[columnName]);
			if(columnValue === 'undefined'){
				LOGGER.log('is end of header ');
				return true;
			}
			else {
				LOGGER.log(DISTRIBUTION_INRECORD[columnName].toString());
				distributionColumnNames.push(DISTRIBUTION_INRECORD[columnName].toString());
			}
		}
	} else{
		return false;
	}
}

function getColumnValue(columnName) {
	if (!isEmptyOrNull(columnName)){
		var columnIndex = distributionColumnNames.indexOf(columnName);
		if (columnIndex > -1){
			return DISTRIBUTION_INRECORD[columnNames[columnIndex]];
		}
	}
    return null;
}

function isEmptyOrNull(str) {
    return (str == null || str == '');
}

function transform() {
	DISTRIBUTION.ChannelID = getColumnValue('channelID');
//	LOGGER.log('ChannelID ' + DISTRIBUTION.ChannelID);
	
	DISTRIBUTION.SKU = getColumnValue('SKU');
//	LOGGER.log('SKU ' + DISTRIBUTION.SKU);
	
	DISTRIBUTION.ActionCode = getColumnValue('actionCode');
//	LOGGER.log('ActionCode ' + DISTRIBUTION.ActionCode);
	
	DISTRIBUTION.ShippingPolicyName = getColumnValue('shippingPolicyName');
//	LOGGER.log('ShippingPolicyName ' + DISTRIBUTION.ShippingPolicyName);
	
	setShippingCostOverrides();
	
	DISTRIBUTION.MaxQuantityPerBuyer = getColumnValue('maxQuantityPerBuyer');
//	LOGGER.log('MaxQuantityPerBuyer ' + DISTRIBUTION.MaxQuantityPerBuyer);
	
	DISTRIBUTION.PaymentPolicyName = getColumnValue('paymentPolicyName');
//	LOGGER.log('PaymentPolicyName ' + DISTRIBUTION.PaymentPolicyName);
	
	DISTRIBUTION.ReturnPolicyName = getColumnValue('returnPolicyName');
//	LOGGER.log('ReturnPolicyName ' + DISTRIBUTION.ReturnPolicyName);
	
	DISTRIBUTION.ListPrice = getColumnPrice('listPrice');
//	LOGGER.log('ListPrice ' + DISTRIBUTION.ListPrice);
	
	DISTRIBUTION.OriginalRetailPrice = getColumnPrice('strikeThroughPrice');
//	LOGGER.log('OriginalRetailPrice ' + DISTRIBUTION.OriginalRetailPrice);
	
	DISTRIBUTION.MinimumAdvertisedPriceExposure = getColumnValue('minimumAdvertisedPriceHandling');
//	LOGGER.log('MinimumAdvertisedPriceExposure ' + DISTRIBUTION.MinimumAdvertisedPriceExposure);
			 
	DISTRIBUTION.MinimumAdvertisedPrice = getColumnPrice('minimumAdvertisedPrice');				 
//	LOGGER.log('MinimumAdvertisedPrice ' + DISTRIBUTION.MinimumAdvertisedPrice);
	
	DISTRIBUTION.SoldOnEbay = getColumnValue('soldOnEbay');
//	LOGGER.log('SoldOnEbay ' + DISTRIBUTION.SoldOnEbay);
	
	DISTRIBUTION.SoldOffEbay = getColumnValue('soldOffEbay');
//	LOGGER.log('SoldOffEbay ' + DISTRIBUTION.SoldOffEbay);
	
	DISTRIBUTION.StoreCategory1 = getColumnValue('storeCategory1Name');
//	LOGGER.log('StoreCategory1 ' + DISTRIBUTION.StoreCategory1);
	
	DISTRIBUTION.StoreCategory2 = getColumnValue('storeCategory2Name');
//	LOGGER.log('StoreCategory2 ' + DISTRIBUTION.StoreCategory2);
	
	DISTRIBUTION.eBayNowEligible = getColumnValue('eBayNowEligible');
//	LOGGER.log('eBayNowEligible ' + DISTRIBUTION.eBayNowEligible);
	
	DISTRIBUTION.EligibleForPickupInStore = getColumnValue('pickupInStoreEligible');
//	LOGGER.log('EligibleForPickupInStore ' + DISTRIBUTION.EligibleForPickupInStore);
	
	DISTRIBUTION.ApplyTax = getColumnValue('applyTax');
//	LOGGER.log('ApplyTax ' + DISTRIBUTION.ApplyTax);
	
	DISTRIBUTION.VATPercentage =  getColumnValue('VATPercent');
//	LOGGER.log('VATPercentage ' + DISTRIBUTION.VATPercentage);
	
	DISTRIBUTION.TaxCategory = getColumnValue('taxCategory');
//	LOGGER.log('TaxCategory ' + DISTRIBUTION.TaxCategory);
}

function getColumnPrice(columnName){
	var price = getColumnValue(columnName);
//	LOGGER.log(columnName + price);

	if (!isEmptyOrNull(price)){
		var JSONPriceObj = parseJSON(price);
		return JSONPriceObj["value"];
	}
	return null;
}

function formatShippingCostOverrideSVT(svcType){
	if (!isEmptyOrNull(svcType)) {
        return (svcType + "Shipping");
    }
	
	return null;
}

function setShippingCostOverrides(){
	var shippingCostOverrides = getColumnValue('shippingCostOverrides');
//	LOGGER.log('shippingCostOverrides type ' + typeof(shippingCostOverrides));
	
	if(!isEmptyOrNull(shippingCostOverrides)){
		var JSONObjList = parseJSON(shippingCostOverrides);
//		LOGGER.log('json obj type ' + typeof(JSONObjList));
		for each(JSONObj in JSONObjList){
			var svcType = JSONObj["svcType"];
			var serviceType = formatShippingCostOverrideSVT(svcType);
			if(!isEmptyOrNull(JSONObj["cost"]))
				var shippingCost = JSONObj["cost"]["value"];
			if(!isEmptyOrNull(JSONObj["addnlCost"]))
				var additionalCost = JSONObj["addnlCost"]["value"];
			if(!isEmptyOrNull(JSONObj["surcharge"]))
				var surchargeCost = JSONObj["surcharge"]["value"];
			var priority = JSONObj["priority"];
			
			if (!isEmptyOrNull(shippingCost) && !isEmptyOrNull(serviceType)) {
                var shippingCostStringName = "Shipping Service #" + priority + " - Cost";
                DISTRIBUTION.PolicyOverride[serviceType][shippingCostStringName] = shippingCost;
            }
            if (!isEmptyOrNull(additionalCost) && !isEmptyOrNull(serviceType)) {
                var additionalCostStringName = "Shipping Service #" + priority + " - Cost Per Additional Item";
                DISTRIBUTION.PolicyOverride[serviceType][additionalCostStringName] = additionalCost;
            }
            if (!isEmptyOrNull(surchargeCost) && !isEmptyOrNull(serviceType)) {
                var surchargeCostStringName = "Shipping Service #" + priority + " - Surcharge Cost";
                DISTRIBUTION.PolicyOverride[serviceType][surchargeCostStringName] = surchargeCost;
            }
		}
	}
}

function parseJSON(string){
	try{
		var JSONObjList = JSON.parse(string);
	}catch(e){
		LOGGER.log('invalid JSON string ' + string);
		return null;
	}
	return JSONObjList;
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

function loadSku() {
//  LOGGER.log('primarykey: ' + getColumnValue('SKU') + "|~~|" + getColumnValue('channelID'));
    return getColumnValue('SKU') + "|~~|" + getColumnValue('channelID');
}
