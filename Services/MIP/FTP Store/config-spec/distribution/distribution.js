function config() {
	CONFIG.FileFormat = 'XML';
    CONFIG.RecordPath = '/distributionRequest/distribution';
}
function transform() {
	LOGGER.setLevel("ERROR");

	DISTRIBUTION.ChannelID = DISTRIBUTION_INRECORD.channelID.toString();
	//DISTRIBUTION.CategoryID = DISTRIBUTION_INRECORD.categoryID.toString();
	DISTRIBUTION.SKU = DISTRIBUTION_INRECORD.SKU.toString();
	DISTRIBUTION.ActionCode = DISTRIBUTION_INRECORD.actionCode.toString();
	DISTRIBUTION.ShippingPolicyName = DISTRIBUTION_INRECORD.listingDetails.shippingPolicyName.toString();
	setShippingCostOverrides();
	DISTRIBUTION.MaxQuantityPerBuyer = DISTRIBUTION_INRECORD.listingDetails.maxQuantityPerBuyer.toString();
	DISTRIBUTION.PaymentPolicyName = DISTRIBUTION_INRECORD.listingDetails.paymentPolicyName.toString();
	DISTRIBUTION.ReturnPolicyName = DISTRIBUTION_INRECORD.listingDetails.returnPolicyName.toString();
	DISTRIBUTION.ListPrice = DISTRIBUTION_INRECORD.listingDetails.pricingDetails.listPrice.toString();
	DISTRIBUTION.OriginalRetailPrice = DISTRIBUTION_INRECORD.listingDetails.pricingDetails.strikeThroughPrice.toString();	
	DISTRIBUTION.MinimumAdvertisedPriceExposure = DISTRIBUTION_INRECORD.listingDetails.pricingDetails.minimumAdvertisedPriceHandling.toString();
	DISTRIBUTION.MinimumAdvertisedPrice = DISTRIBUTION_INRECORD.listingDetails.pricingDetails.minimumAdvertisedPrice.toString();	
	DISTRIBUTION.SoldOnEbay = DISTRIBUTION_INRECORD.listingDetails.pricingDetails.soldOnEbay.toString();	
	DISTRIBUTION.SoldOffEbay = DISTRIBUTION_INRECORD.listingDetails.pricingDetails.soldOffEbay.toString();	
	DISTRIBUTION.StoreCategory1 = DISTRIBUTION_INRECORD.listingDetails.storeCategory1Name.toString();
	DISTRIBUTION.StoreCategory2 = DISTRIBUTION_INRECORD.listingDetails.storeCategory2Name.toString();
	DISTRIBUTION.eBayNowEligible = DISTRIBUTION_INRECORD.listingDetails.eBayNowEligible.toString();
	DISTRIBUTION.EligibleForPickupInStore = DISTRIBUTION_INRECORD.listingDetails.pickupInStoreEligible.toString();
	DISTRIBUTION.ApplyTax = DISTRIBUTION_INRECORD.listingDetails.applyTax.toString();
	DISTRIBUTION.VATPercentage =  DISTRIBUTION_INRECORD.listingDetails.VATPercent.toString();
	DISTRIBUTION.TaxCategory = DISTRIBUTION_INRECORD.listingDetails.taxCategory.toString();
}
function setShippingCostOverrides() {
    var shippingCostOverrides = DISTRIBUTION_INRECORD.listingDetails.shippingCostOverrides;
    if (shippingCostOverrides.length() > 0) {
        for (var i = 0; i < shippingCostOverrides.length(); i++ ) {
            var shippingCostOverride = shippingCostOverrides[i];
            var shippingServiceType = capitaliseFirstLetter(shippingCostOverride.shippingServiceType);
            var serviceType = "";
            if (!isEmptyOrNull(shippingServiceType)) {
                serviceType = shippingServiceType + "Shipping";
            }
            var shippingCost = shippingCostOverride.shippingCost.toString();
            var additionalCost = shippingCostOverride.additionalCost.toString();
            var surchargeCost = shippingCostOverride.surcharge.toString();
            var priority = shippingCostOverride.priority.toString();

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
    return DISTRIBUTION_INRECORD.SKU.toString() + "|~~|" + DISTRIBUTION_INRECORD.channelID.toString();
}
