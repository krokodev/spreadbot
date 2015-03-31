/**
 * ItemSpecifics Mapping - SKUBI XML eBay Schema
 */
 
/*
 * Global variables
 * VariationVector to hold the variant attributes (Attributes on which variations vary eg. Size, Color)
 */
 var variationVector = new Array();

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
	CONFIG.RecordPath = '/productRequest/product;/productRequest/productVariationGroup';
    
    // The node/record "grouping" strategy used by the groupBy() function.
    CONFIG.GroupingType = 'VALUE';

    CONFIG.EbayConfigSpec = 'true';

    LOOKUP_SERVICE.loadDelimitedFile('/store/lookup/common/CategoryMapping.csv', ',', [0,1], [4]);
}

function isSkipRecord() {
    return false;
}

function groupBy() {

    if(PRODUCT_INRECORD.groupID.length() > 0 ) {
	  return PRODUCT_INRECORD.groupID.toString();
	}
	else if (PRODUCT_INRECORD.SKU.attribute("variantOf").length() > 0) {
	  return PRODUCT_INRECORD.SKU.attribute("variantOf").toString();
	}
	else {
	  return PRODUCT_INRECORD.SKU.toString();
	}
}

function getVariantOf(){
    if (PRODUCT_INRECORD.SKU.attribute("variantOf").length() > 0) {
	  return PRODUCT_INRECORD.SKU.attribute("variantOf").toString();
	}
 }


 function getGroupId(){
    if (PRODUCT_INRECORD.groupID.length() > 0) {
	  return PRODUCT_INRECORD.groupID.toString();
	}
 }
 
 
/**
 * This function is called on each GROUP to regroup by locale.
 * ELEMENTS_BY_LOCALE_MAP : Key is the locale string, value is a list.
 * For Single SKU, the list length is 1 with one ProductInformation element.
 * For MSKU, the list length is > 1 with first element being GroupInformation and
 * rest being ProductInformation elements (of variants).
 */
function groupByLocale() {

    ELEMENTS_BY_LOCALE_MAP = {};

    // For GROUP length 1 adding additional check of Product path in case a PVG comes alone in a GROUP with no Variant products.
    if (GROUP.length == 1 && GROUP[0].SKU.length() > 0) {

        for (var i = 0; i < GROUP.length; i++) {
        
            PRODUCT_INRECORD_X = GROUP[i];

            var sku = PRODUCT_INRECORD_X.SKU;

            var prodInfo = PRODUCT_INRECORD_X.productInformation;

            for (var j = 0; j < prodInfo.length(); j++) {
                var mapKey = prodInfo[j].@["localizedFor"].toString();

                // Adding SKU as child to each ProductInformation element.
                prodInfo[j].SKU = sku;

                var tempList = new Array();
                tempList[tempList.length] = prodInfo[j];
                ELEMENTS_BY_LOCALE_MAP[mapKey] = tempList;
            }
        }

    } else {

        PRODUCT_INRECORD_PVG = GROUP[0];

        var groupId = PRODUCT_INRECORD_PVG.groupID;

        for (var i = 0; i < GROUP.length; i++) {

            PRODUCT_INRECORD_X = GROUP[i];

            if (i == 0) {
            // Then it is a ProductVariationGroup

                var groupInfo = PRODUCT_INRECORD_X.groupInformation;

                for (var j = 0; j < groupInfo.length(); j++) {

                    var mapKey = groupInfo[j].@["localizedFor"].toString();
                  
                    // Adding GroupId as child to each GroupInformation element.
                    groupInfo[j].GroupID = groupId;

                    var tempList = new Array();
                    tempList[tempList.length] = groupInfo[j];                 
                    ELEMENTS_BY_LOCALE_MAP[mapKey] = tempList;
                }

            } else {
            // Then it is a Product Child
         
                var sku = PRODUCT_INRECORD_X.SKU.toString();

                var prodInfo = PRODUCT_INRECORD_X.productInformation;

                for (var k = 0; k < prodInfo.length(); k++) {

                    var mapKey = prodInfo[k].@["localizedFor"].toString();
                    var mapValue = ELEMENTS_BY_LOCALE_MAP[mapKey];

                    // Adding the child sku only if the parent is defined.
                    if (mapValue !== undefined) {

                        // Adding SKU as child to each ProductInformation element.
                        prodInfo[k].SKU = sku;

                        mapValue[mapValue.length] = prodInfo[k];
                        ELEMENTS_BY_LOCALE_MAP[mapKey] = mapValue;
                    }
                }
            }
        }
    }
}

/**
 * This function is called on each element of the ELEMENTS_BY_LOCALE_MAP.
 * key is the locale string and value is the list of elements specific to that locale.
 * INFO_INRECORD is ProductInformation for SSKU and MSKU Variant
 * INFO_INRECORD is GroupInformation for MSKU parent
 */

function transformByLocale(key, value) {

    var locale = key;
    // Making this global because it has to be accessible in transformParentByLocale function.
    elements = value;

    for (i=0;i<elements.length;i++) {
        INFO_INRECORD = elements[i]; 
        
        if (i == 0) {
            transformParentByLocale(locale);
        } else {
            transformChildByLocale(INFO_INRECORD);
        }        
    }

    // Emptying the variationVector and resetting the boolean after each Locale Group is processed.
    variationVector = [];

}

/*
 * This is an user defined function which demonstrates how the PRODUCT object can be populated for a group parent.
 * If GROUP length is 1 then it is non-multisku product. Else it is the Parent of the multisku product.
 */
function transformParentByLocale(locale) {

    // For GROUP length 1 adding additional check of SKU path in case a PVG comes alone in a GROUP with no Variant products.
    if (GROUP.length == 1 && INFO_INRECORD.SKU.length() > 0) {
        
        var sku = INFO_INRECORD.SKU.toString();

        PRODUCT.ID = sku;
        PRODUCT.SKU = sku;

        /* Locale */
        PRODUCT.Locale = locale; 

        /* Category */
        setCategory(sku, locale);

        /* ItemSpecifics */
	    setItemSpecifics(INFO_INRECORD);

    } else {
	
        var groupId = INFO_INRECORD.groupID.toString();

        PRODUCT.ID =  groupId;
        PRODUCT.SKU =  groupId;
	
        /* Locale */
	    PRODUCT.Locale = locale;

	    /* Category */
	    setCategory(groupId, locale);
	
	    /* ItemSpecifics */
	    setItemSpecifics(INFO_INRECORD.sharedProductInformation);

        /*Apart from the Product level ItemSpecifics like Brand, Style etc, the ItemSpecifics need to include
        the VariationVector specifics as well like Size, Color etc.*/

        CHILD_INFO_INRECORD = elements[1];

        // Variation Vector
        // Getting the VariationVector names first eg.Color
        var variationNames = INFO_INRECORD.variationVector.name;
        if (variationNames.length() > 0) {
            for each(var variationName in variationNames) {
                variationVector.push(variationName.toString());
            }
        }

        // From the first variant getting the values of the VariationVector Specifics eg. Blue
        var attributes = CHILD_INFO_INRECORD.attribute;
        for each(var attribute in attributes) {
            var attributeName = attribute.@["name"].toString();
            var attributeValue = attribute.toString();
            if (!isEmptyOrNull(attributeName)) {
                if (variationVector.length > 0 && variationVector.indexOf(attributeName) < 0) {
                    LOGGER.log("------------------------- VariationVector check: " + attributeName + " does not exist!!");
                } else {
                    PRODUCT.ItemSpecifics[attributeName] = attributeValue;
                }
            }
        }
    }
}

/*
 * This is a user defined function which demonstrates how the PRODUCT variations can be populated for a group child.
 * 
 */
function transformChildByLocale(INFO_INRECORD) {

}