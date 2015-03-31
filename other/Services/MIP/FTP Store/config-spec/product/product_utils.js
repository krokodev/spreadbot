function isEmptyOrNull(str) {

    return (str == null || str == '');
}

/**
 * If Title length is greater than 80 characters trim it to 77 characters and add '...'
 */
function setTitleAndSubTitle(element) {

    var fullTitle = element.title.toString();
    var subTitle = element.subtitle.toString();

    if (fullTitle.length > 80) {
        fullTitle = fullTitle.substr(0, 77) + '...';
    }

    PRODUCT.Title = fullTitle;
    PRODUCT.Subtitle = subTitle;
}

function setItemSpecifics(element) {

    var itemattributes = element.attribute;
	for each(var itemattribute in itemattributes) {
        var attributeName = itemattribute.@["name"].toString();
        var attributeValue = itemattribute.toString();
        var category = PRODUCT.Category;
        var locale = PRODUCT.Locale;
        if (!isEmptyOrNull(attributeName)) {
            if (!isEmptyOrNull(category) && !isEmptyOrNull(locale)) {
                // For each attribute name look it up in the mapping csv file. This is to map from the seller's attribute name to eBay's attribute name.
                var attributeNameMapping = LOOKUP_SERVICE.get('/store/lookup/common/ItemSpecificsMapping.csv', [category, locale, attributeName]);
                if (!isEmptyOrNull(attributeNameMapping)) {
                    // If the mapping exists for this attribute name, we replace the locale variable "attributeName" value with the mapped value.
                    attributeName = attributeNameMapping[0];
                }
            }

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

function setVariationSpecifics(variation, INFO_INRECORD) {
	
	var attributes = INFO_INRECORD.attribute;
    var attributeWithOrderMap = {};
    var count = 0;
    var category = PRODUCT.Category;
    var locale = PRODUCT.Locale;

    // For each variation attribute, check if it is one of the VariationVector names given with the PVG parent else ignore it.
	for each(var attribute in attributes) {

        var attributeName = attribute.@["name"].toString();
        var attributeValue = attribute.toString();  
        if (!isEmptyOrNull(attributeName)) {
            if (variationVector.length > 0 && variationVector.indexOf(attributeName) < 0) {
                LOGGER.log("------------------------- VariationVector check: " + attributeName + " does not exist!!");
            } else {
                // Attribute name exists in the variation vector.
                if(considerVariationVectorOrder) {
                    // If the boolean is set then order the attribute elements as per the order

                    attributeWithOrderMap[variationVector.indexOf(attributeName)] = attribute;
                    count++;
                } else {
                    // If the boolean is not set, just add the variation specific without considering order.
                    if (!isEmptyOrNull(category) && !isEmptyOrNull(locale)) {
                        // For each attribute name look it up in the mapping csv file. This is to map from the seller's attribute name to eBay's attribute name.
                        var attributeNameMapping = LOOKUP_SERVICE.get('/store/lookup/common/ItemSpecificsMapping.csv', [category, locale, attributeName]);
                        if (!isEmptyOrNull(attributeNameMapping)) {
                            // If the mapping exists for this attribute name, we replace the locale variable "attributeName" value with the mapped value.
                            attributeName = attributeNameMapping[0];
                        }
                    }
                    variation.VariationSpecifics[attributeName] = attributeValue;
                }
            }
        }                                                                                             
    }

    // If the boolean is set then attribute elements have been ordered. Fill in the variationspecifics.
    if (considerVariationVectorOrder) {
        for (var i = 0; i < count; i++) {
            var mapAttribute = attributeWithOrderMap[i];
            if (typeof mapAttribute !== 'undefined') {
                var name = mapAttribute.@["name"].toString();
                var value = mapAttribute.toString();

                if (!isEmptyOrNull(category)) {
                    var attributeNameMapping = LOOKUP_SERVICE.get('/store/lookup/common/ItemSpecificsMapping.csv', [category, locale, name]);
                    if (!isEmptyOrNull(attributeNameMapping)) {
                        // If the mapping exists for this attribute name, we replace the locale variable "name" value with the mapped value.
                        name = attributeNameMapping[0];
                    }
                }
                variation.VariationSpecifics[name] = value;
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
	
function setVariationPackageDetails(variation, INFO_INRECORD){
   
    var shippingDetails = INFO_INRECORD.shippingDetails;

    if (shippingDetails.length() > 0) {
        var measurementSystem = shippingDetails.@["measurementSystem"].toString();
        variation.VariationPackageDetails["MeasurementSystem"] = capitaliseFirstLetter(measurementSystem);
        variation.VariationPackageDetails["WeightMajor"] = shippingDetails.weightMajor.toString();
        variation.VariationPackageDetails["WeightMinor"] = shippingDetails.weightMinor.toString();
        variation.VariationPackageDetails["Length"] = shippingDetails.length.toString();
        variation.VariationPackageDetails["Width"] = shippingDetails.width.toString();
        variation.VariationPackageDetails["Depth"] = shippingDetails.height.toString();
        variation.VariationPackageDetails["Type"] = shippingDetails.packageType.toString();
    }
}

/**
 * source is either PRODUCT or variation depending on where the method is called from
 * 
 */
function setConditionInfo(element, source) {

    var conditionInfo = INFO_INRECORD.conditionInfo;

    if (conditionInfo.length() > 0) {
        var conditionString = conditionInfo.condition.toString();
        if (!isEmptyOrNull(conditionString)) {
            conditionString = conditionString.toLowerCase();
            var conditionResult = LOOKUP_SERVICE.get('conditionLookup.csv', [conditionString]);
            if (!isEmptyOrNull(conditionResult)) {
                    source.Condition = conditionResult[0];
            }
        }
        source.ConditionDescription = conditionInfo.conditionDescription.toString();
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

/**
 * Sets DescriptionTemplate, ProductDescription, AdditionalInfo and any other CustomFields
 */
function setDescriptionDetails(element) {
    
    // Comment it out when template input in the feed is supported
    /*var descriptionTemplate = element.description.template.toString();
    if (isEmptyOrNull(descriptionTemplate)) {
        descriptionTemplate = 'description.html';
    }*/
    PRODUCT.DescriptionTemplate = '/store/lookup/template/description.html';
    PRODUCT.CustomFields.ProductDescription = element.description.productDescription.toString();
    PRODUCT.CustomFields.AdditionalInfo = element.description.additionalInfo.toString();
    var customFields = element.description.customField;
    for each(var customfield in customFields) {
        var name = customfield.@["name"].toString();
        if (!isEmptyOrNull(name)) {
            PRODUCT.CustomFields[name] = customfield.toString();
        }
    }
}

function setShippingPackageDetails(element) {

    var shippingDetails = element.shippingDetails;

    if (shippingDetails.length() > 0) {
        var measurementSystem = shippingDetails.@["measurementSystem"].toString();
        PRODUCT.PackageDetails["MeasurementSystem"] = capitaliseFirstLetter(measurementSystem);
        PRODUCT.PackageDetails["WeightMajor"] = shippingDetails.weightMajor.toString();
        PRODUCT.PackageDetails["WeightMinor"] = shippingDetails.weightMinor.toString();
        PRODUCT.PackageDetails["Length"] = shippingDetails.length.toString();
        PRODUCT.PackageDetails["Width"] = shippingDetails.width.toString();
        PRODUCT.PackageDetails["Depth"] = shippingDetails.height.toString();
        PRODUCT.PackageDetails["Type"] = shippingDetails.packageType.toString();
    }
}

function getProductIds(element, type) {

    var map = {};
    var catalogProductIDset = false;
	var productIDTypes = element.productID;
    var brand = "";
    var mpn = "";

    for each (var productIDType in productIDTypes){
        var productIDTypeName =  productIDType.@["productIDType"].toString();
        var productIDTypeValue = productIDType.toString();
        if (!isEmptyOrNull(productIDTypeName) 
                && !isEmptyOrNull(productIDTypeValue)
                && isValidProductIDType(productIDTypeName)) {
            if (productIDTypeName.toLowerCase() === 'brand') {
                brand = productIDTypeValue;
            } else if (productIDTypeName.toLowerCase() === 'mpn') {
                mpn = productIDTypeValue;
            } else {
                if (type == "ssku" && !catalogProductIDset) {
                    PRODUCT.CatalogProductType = productIDTypeName;
                    PRODUCT.CatalogProductID = productIDTypeValue;
                    catalogProductIDset = true;
                }
            }
            map[productIDTypeName] = productIDTypeValue;
        }
    }

    if (!isEmptyOrNull(brand) 
            && !isEmptyOrNull(mpn) 
            && type == "ssku" 
            && !catalogProductIDset) {
        PRODUCT.CatalogProductType = "BrandMPN";
        var brandMPNArray = new Array();
        brandMPNArray.push(brand, mpn);
        PRODUCT.CatalogProductID = brandMPNArray;
        catalogProductIDset = true;
    }

    return JSON.stringify(map);
}

function isValidProductIDType(productIDTypeName) {

    var validProductIDTypes = ["UPC", "ISBN", "EAN", "GTIN", "BRAND", "MPN", "EPID", "ASIN"];
    
    if (validProductIDTypes.indexOf(productIDTypeName) > -1) {
        return true;
    }

    return false;
}



/**
 * Location.csv file is loaded with data provided by the user while setting up the preferences on web.
 */
function setLocation() {

    var defaultLocationResult = LOOKUP_SERVICE.get('/store/lookup/common/Location.csv', [ 'Default' ] );
    if (!isEmptyOrNull(defaultLocationResult)) {
        PRODUCT.ItemPostalCode = defaultLocationResult[3];
        PRODUCT.ItemCountry = defaultLocationResult[2];
        PRODUCT.ItemLocation = defaultLocationResult[0] + ", " + defaultLocationResult[1];
    }
}

function getPictureUrls(element) {

    var pictureUrlsArr = new Array();
    
    var conditionDisplayOrderMap = {};
    var conditionDisplayOrderNumberArray = [];

    var mainDisplayOrderMap = {};
    var mainDisplayOrderNumberArray = [];
    
    // ConditionPictures
    //If Condition Pictures exists add them first and later add PictureUrls. 
    var conditionPictures = element.conditionInfo.pictureURL;

    // Picture Urls
    var pictureUrls = element.pictureURL;

    var BreakException = {};
    var considerDisplayOrder = true;

    try {
    
    var url = "";
    var displayOrder = "";

    for (var i = 0; i < conditionPictures.length(); i++) {
        url = conditionPictures[i];

        displayOrder = url.@["displayOrder"].toString();
        
        if (!isEmptyOrNull(displayOrder)) {
            if (displayOrder in conditionDisplayOrderMap) {
                // Then same order has been given again. Break.
                throw BreakException;
            } else {
               conditionDisplayOrderMap[parseInt(displayOrder)] = url;
               conditionDisplayOrderNumberArray.push(parseInt(displayOrder));
            }
        } else {
            // DisplayOrder has not been given. Break.
            throw BreakException;
        }
    }

    conditionDisplayOrderNumberArray.sort(function(a, b) {return a-b});

    for (var i = 0; i < pictureUrls.length(); i++) {
        url = pictureUrls[i];

        displayOrder = url.@["displayOrder"].toString();
        
        if (!isEmptyOrNull(displayOrder)) {
            if (displayOrder in mainDisplayOrderMap) {
                // Then same order has been given again. Break.
                throw BreakException;
            } else {
               mainDisplayOrderMap[parseInt(displayOrder)] = url;
               mainDisplayOrderNumberArray.push(parseInt(displayOrder));
            }
        } else {
            // DisplayOrder has not been given. Break.
            throw BreakException;
        }
    }

    mainDisplayOrderNumberArray.sort(function(a, b) {return a-b});

    } catch(e) {
        considerDisplayOrder = false;
    }

    if (considerDisplayOrder) {

        var orderKey = 0;

        for (var j = 0; j < conditionDisplayOrderNumberArray.length; j++) {
            orderKey = conditionDisplayOrderNumberArray[j];
            pictureUrlsArr[j] = conditionDisplayOrderMap[orderKey];
        }

        var k = pictureUrlsArr.length;

        for (var l = 0; l < mainDisplayOrderNumberArray.length; l++) {
            orderKey = mainDisplayOrderNumberArray[l];
            pictureUrlsArr[k+l] = mainDisplayOrderMap[orderKey];
        }

    } else {
        
        for (var i = 0; i < conditionPictures.length(); i++) {
            pictureUrlsArr[i] = conditionPictures[i];
        }

        var x = pictureUrlsArr.length;

        for (var y=x; y<(x+pictureUrls.length()); y++) {
            pictureUrlsArr[y] = pictureUrls[y-x];
        }

    }

    return pictureUrlsArr;
}

function setVariationVector(element) {

    var variationNames = element.variationVector.name;

    if (variationNames.length() > 0) {
            
        var variationNamesDisplayOrderMap = {};
        var variationNamesDisplayOrderNumberArray = [];
        var BreakException = {};
        try {
            for each(var variationName in variationNames) {
                var displayOrder = variationName.@["displayOrder"].toString();
                if (!isEmptyOrNull(displayOrder)) {
                    if (displayOrder in variationNamesDisplayOrderMap) {
                        // Then same order has been given again. Break.
                        throw BreakException;
                     } else {
                        variationNamesDisplayOrderMap[parseInt(displayOrder)] = variationName.toString();
                        variationNamesDisplayOrderNumberArray.push(parseInt(displayOrder));
                     }
                } else {
                     throw BreakException;
                }    
            }
        } catch(e) {
            considerVariationVectorOrder = false;
        }

        if (considerVariationVectorOrder) {
            variationNamesDisplayOrderNumberArray.sort(function(a, b) {return a-b});
            for (var i = 0; i < variationNamesDisplayOrderNumberArray.length; i++) {
                var orderKey = variationNamesDisplayOrderNumberArray[i];
                variationVector.push(variationNamesDisplayOrderMap[orderKey]);
            }
        } else {
            for each(var variationName in variationNames) {
                variationVector.push(variationName.toString());
            }
        }
    } else {
        // If variation vector is not provided then don't consider order at all.
        considerVariationVectorOrder = false;
    }
}

function setCompatibilities(element) {

    var compatibilities = "";

    compatibilities = element.compatibility;

    for (var i = 0; i < compatibilities.length(); i++) {

        var inrecordCompatibility = compatibilities[i];
        
        var compatibility = PRODUCT.createCompatibility();

        compatibility.Notes = inrecordCompatibility.notes.toString();
        var values = inrecordCompatibility.value;

        for (var j = 0; j < values.length(); j++) {
            
            var valueName = values[j].@["name"].toString();
            if (!isEmptyOrNull(valueName)) {
                compatibility[valueName] = values[j].toString();            
            }
        }
    }
}