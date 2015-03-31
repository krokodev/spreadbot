function isEmptyOrNull(str) {

    return (str == null || str == '');
}


function setItemSpecifics(element) {

    var itemattributes = element.attribute;
	for each(var itemattribute in itemattributes) {
        var attributeName = itemattribute.@["name"].toString();
        var attributeValue = itemattribute.toString();
     
        if (!isEmptyOrNull(attributeName)) {
            if (attributeName in PRODUCT.ItemSpecifics) {
                var existingValue = PRODUCT.ItemSpecifics[ attributeName ];
                if (existingValue instanceof Array) {
                    existingValue.push(attributeValue);
                    PRODUCT.ItemSpecifics[ attributeName ] = existingValue;
                } else {
                    var valueArray = new Array();
                    valueArray.push(existingValue);
                    valueArray.push(attributeValue);
                    PRODUCT.ItemSpecifics[ attributeName ] = valueArray;
                }
            } else {
                PRODUCT.ItemSpecifics[ attributeName ] = attributeValue;
            }
        }                                                                                                            
    }
}

/**
 * First loop through the categories provided in the feed and take the first one with 
 * type "eBayLeafCategory". If it is not found, then lookup the category mapping file.
 */
function setCategory(productId, locale) {

    var categories = INFO_INRECORD.category;
    if (categories.length() > 0) {
        // Mimicking the break operation since javascript does not have inherent Break 
        var BreakException = {};
        try {
            for each (var category in categories) {
                var categoryType = category.@["categoryType"].toString();
                if (isValidCategoryType(categoryType)) {
                    if (categoryType == "EBAY_LEAF_CATEGORY" && !isEmptyOrNull(category.toString())) {
                        PRODUCT.Category = category.toString();
                        throw BreakException;
                    }
                }
            }
            // If it reaches here then could not find a eBayLeafCategory in the user input
            if (!isEmptyOrNull(productId) && !isEmptyOrNull(locale)) {
                var categoryResult = LOOKUP_SERVICE.get('/store/lookup/common/CategoryMapping.csv', [productId, locale] );
                if (!isEmptyOrNull(categoryResult)) {
                    PRODUCT.Category = categoryResult[0];
                }
            }
        } catch(e) {
        }
    } else {

        if (!isEmptyOrNull(productId) && !isEmptyOrNull(locale)) {
            var categoryResult = LOOKUP_SERVICE.get('/store/lookup/common/CategoryMapping.csv', [productId, locale] );
            if (!isEmptyOrNull(categoryResult)) {         
                PRODUCT.Category = categoryResult[0];
            }
        }
    }
}


function isValidCategoryType(categoryType) {

    var validCategoryTypes = ["EBAY_LEAF_CATEGORY", "EBAY_META", "SELLER_CATEGORY", "GOOGLE_CATEGORY"];
    
    if (validCategoryTypes.indexOf(categoryType) > -1) {
        return true;
    }

    return false;
}
