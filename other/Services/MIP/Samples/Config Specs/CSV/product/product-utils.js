function isEmptyOrNull(str) {

    return (str == null || str == '');
}

/**
* For a given ColumnName, gets the corresponding columnIndex and gets the value for that column.
*/
function getColumnValue(columnName) {

    if (!isEmptyOrNull(columnName)) {
        var columnNameIndex = headerColumns.indexOf(columnName);

        if (columnNameIndex > -1) {
            return PRODUCT_INRECORD[columnNames[columnNameIndex]];
        }
    }
    return null;
}

/**
* If Title length is greater than 80 characters trim it to 77 characters and add '...'
*/
function getTitle() {
    var fullTitle = getColumnValue('title');

    if (fullTitle != null) {
        if (fullTitle.length > 80) {
            return fullTitle.substr(0, 77) + '...';
        }
    }
    return fullTitle;
}

/**
* Sets DescriptionTemplate, ProductDescription, AdditionalInfo and any other CustomFields
*/
function setDescriptionDetails() {

    // If description template is not provided by the user, default it to description.html
    // Use the commented code when template input in the feed is supported
    /*var descriptionTemplate = getColumnValue('template');
    if (isEmptyOrNull(descriptionTemplate)) {
    descriptionTemplate = 'description.html';
    }
    var descriptionTemplatePrefix = '/store/lookup/template/';
    PRODUCT.DescriptionTemplate = descriptionTemplatePrefix + descriptionTemplate;*/
    PRODUCT.DescriptionTemplate = '/store/lookup/template/description.html';
    PRODUCT.CustomFields.ProductDescription = getColumnValue('productDescription');
    PRODUCT.CustomFields.AdditionalInfo = getColumnValue('additionalInfo');

    var customFieldsStr = getColumnValue('customFields');
    if (!isEmptyOrNull(customFieldsStr)) {
        try {
            var customFields = JSON.parse(customFieldsStr);
            for (var name in customFields) {
                PRODUCT.CustomFields[name] = customFields[name];
            }
        } catch (err) {
            LOGGER.log("---- customFields JSON parse error for " + PRODUCT.ID + " : " + err.message);
        }
    }
}


/**
* Use the category input from the feed if the type is EBAY_LEAF_CATEGORY else 
* lookup the category mapping file.
*/
function setCategory() {

    var categoryStr = getColumnValue('category');
    var categorySet = false;

    if (!isEmptyOrNull(categoryStr)) {
        try {
            var category = JSON.parse(categoryStr);
            var categoryId = category.EBAY_LEAF_CATEGORY;
            if (typeof categoryId !== 'undefined' && !isEmptyOrNull(categoryId)) {
                PRODUCT.Category = categoryId;
                categorySet = true;
            }
        } catch (err) {
            LOGGER.log("---- category JSON parse error for " + PRODUCT.ID + " : " + err.message);
        }
    }

    // If the category value is not provided or if the provided category
    // is not of type EBAY_LEAF_CATEGORY
    if (!categorySet) {
        var productId = PRODUCT.ID;
        var locale = PRODUCT.Locale;
        if (!isEmptyOrNull(productId) && !isEmptyOrNull(locale)) {
            var categoryResult = LOOKUP_SERVICE.get('/store/lookup/common/CategoryMapping.csv', [productId, locale]);
            if (!isEmptyOrNull(categoryResult)) {
            PRODUCT.Category = categoryResult[0];
            }
        }
    }
}

function setItemSpecifics() {

    var category = PRODUCT.Category;
    var locale = PRODUCT.Locale;
    var itemSpecificsStr = getColumnValue('attributes');
    if (!isEmptyOrNull(itemSpecificsStr)) {
        try {
            var itemSpecifics = JSON.parse(itemSpecificsStr);
            for (var name in itemSpecifics) {
                if (!isEmptyOrNull(name)) {
                    var value = itemSpecifics[name];
                    if (!isEmptyOrNull(category) && !isEmptyOrNull(locale)) {
                        // For each attribute name look it up in the mapping csv file.
                        // This is to map from the seller's attribute name to eBay's attribute name.
                        var attributeNameMapping = LOOKUP_SERVICE.get('/store/lookup/common/ItemSpecificsMapping.csv', [category, locale, name]);
                        if (!isEmptyOrNull(attributeNameMapping)) {
                            name = attributeNameMapping[0];
                        }
                    }
                    PRODUCT.ItemSpecifics[name] = value;
                }
            }
        } catch (err) {
            LOGGER.log("---- itemspecifics JSON parse error for " + PRODUCT.ID + " : " + err.message);
        }
    }
}

function setShippingPackageDetails() {

    PRODUCT.PackageDetails["MeasurementSystem"] = capitaliseFirstLetter(getColumnValue('measurementSystem'));
    PRODUCT.PackageDetails["WeightMajor"] = getColumnValue('weightMajor');
    PRODUCT.PackageDetails["WeightMinor"] = getColumnValue('weightMinor');
    PRODUCT.PackageDetails["Length"] = getColumnValue('length');
    PRODUCT.PackageDetails["Width"] = getColumnValue('width');
    PRODUCT.PackageDetails["Depth"] = getColumnValue('height');
    PRODUCT.PackageDetails["Type"] = getColumnValue('packageType');

}

function getPictureUrls() {

    var pictureUrlsArr = new Array();

    var conditionDisplayOrderMap = {};
    var conditionDisplayOrderNumberArray = [];

    var mainDisplayOrderMap = {};
    var mainDisplayOrderNumberArray = [];

    var conditionPictures = null;
    var pictureUrls = null;

    // ConditionPictures
    // If condition pictures exists add them first and later add pictureURLs
    var conditionPicturesStr = getColumnValue('conditionPictureURL');
    if (!isEmptyOrNull(conditionPicturesStr)) {
        try {
            conditionPictures = JSON.parse(conditionPicturesStr);
        } catch (err) {
            LOGGER.log("---- conditionPictures JSON parse error for " + PRODUCT.ID + " : " + err.message);
        }
    }

    // Picture Urls
    var pictureUrlsStr = getColumnValue('pictureURLs');
    if (!isEmptyOrNull(pictureUrlsStr)) {
        try {
            pictureUrls = JSON.parse(pictureUrlsStr);
        } catch (err) {
            LOGGER.log("---- pictureUrls JSON parse error for " + PRODUCT.ID + " : " + err.message);
        }
    }
    var BreakException = {};
    var considerDisplayOrder = true;

    try {
        var url = "";
        var displayOrder = "";
        if (conditionPictures != null) {
            for (var i = 0; i < conditionPictures.length; i++) {
                var obj = conditionPictures[i];
                url = obj.url;
                displayOrder = obj.displayOrder;

                if (!isEmptyOrNull(displayOrder)) {
                    if (displayOrder in conditionDisplayOrderMap) {
                        // Then same order has been given. Break.
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
        }

        conditionDisplayOrderNumberArray.sort(function (a, b) { return a - b });

        if (pictureUrls != null) {
            for (var i = 0; i < pictureUrls.length; i++) {
                var obj = pictureUrls[i];
                url = obj.url;
                displayOrder = obj.displayOrder;

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
        }

        mainDisplayOrderNumberArray.sort(function (a, b) { return a - b });

    } catch (e) {
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
            pictureUrlsArr[k + l] = mainDisplayOrderMap[orderKey];
        }
    } else {

        if (conditionPictures != null) {
            for (var i = 0; i < conditionPictures.length; i++) {
                pictureUrlsArr[i] = conditionPictures[i].url;
            }
        }

        var x = pictureUrlsArr.length;

        if (pictureUrls != null) {
            for (var y = x; y < (x + pictureUrls.length); y++) {
                pictureUrlsArr[y] = pictureUrls[y - x].url;
            }
        }
    }

    return pictureUrlsArr;
}

// Sets Condition :http://developer.ebay.com/DevZone/finding/CallRef/Enums/conditionIdList.html and ConditionDescription
function setConditionInfo(source) {

    var conditionString = getColumnValue('condition');

    if (!isEmptyOrNull(conditionString)) {
        conditionString = conditionString.toLowerCase();
        var conditionResult = LOOKUP_SERVICE.get('conditionLookup.csv', [conditionString]);
        if (!isEmptyOrNull(conditionResult)) {
            source.Condition = conditionResult[0];
        }
    }

    source.ConditionDescription = getColumnValue('conditionDescription');

}

function getProductIds(type) {

    var map = {};

    var productIdStr = getColumnValue('productId');

    if (!isEmptyOrNull(productIdStr)) {

        var brand = "";
        var mpn = "";
        var catalogProductIDset = false;

        try {
            var productIds = JSON.parse(productIdStr);

            for (var productIdType in productIds) {

                if (!isEmptyOrNull(productIdType)) {

                    var productIdValue = productIds[productIdType];

                    if (!isEmptyOrNull(productIdValue)
                        && isValidProductIDType(productIdType)) {

                        if (productIdType.toLowerCase() === 'brand') {
                            brand = productIdValue;
                        } else if (productIdType.toLowerCase() === 'mpn') {
                            mpn = productIdValue;
                        } else {
                            if (type == "ssku" && !catalogProductIDset) {
                                PRODUCT.CatalogProductType = productIdType;
                                PRODUCT.CatalogProductID = productIdValue;
                                catalogProductIDset = true;
                            }
                        }
                        map[productIdType] = productIdValue;
                    }
                }
            }
        } catch (err) {
            LOGGER.log("---- productIds JSON parse error for " + PRODUCT.ID + " : " + err.message);
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

function setLocation() {

    LOGGER.log("looking up location");
    var defaultLocationResult = LOOKUP_SERVICE.get('/store/lookup/common/Location.csv', ['Default']);
    if (!isEmptyOrNull(defaultLocationResult)) {
        PRODUCT.ItemPostalCode = defaultLocationResult[3];
        PRODUCT.ItemCountry = defaultLocationResult[2];
        PRODUCT.ItemLocation = defaultLocationResult[0] + ", " + defaultLocationResult[1];
    }
}

function setCompatibilities() {

    var compatibilitiesStr = getColumnValue('compatibility');

    if (!isEmptyOrNull(compatibilitiesStr)) {
        try {
            var compatibilitiesList = JSON.parse(compatibilitiesStr);

            for (var i = 0; i < compatibilitiesList.length; i++) {

                var compatibilityObj = compatibilitiesList[i];

                var compatibility = PRODUCT.createCompatibility();

                compatibility.Notes = compatibilityObj.notes;

                for (var name in compatibilityObj) {
                    if (name.toLowerCase() == "notes") {
                        compatibility.Notes = compatibilityObj[name];
                    } else {
                        compatibility[name] = compatibilityObj[name];
                    }
                }
            }
        }
        catch (err) {
            LOGGER.log("---- compatibilities JSON parse error for " + PRODUCT.ID + " : " + err.message);
        }
    }
}


function setVariationSpecifics(variation) {

    var variationSpecificsStr = getColumnValue('attributes');

    if (!isEmptyOrNull(variationSpecificsStr)) {

        try {
            var variationSpecifics = JSON.parse(variationSpecificsStr);

            var attributeWithOrderMap = {};
            var count = 0;
            var category = PRODUCT.Category;
            var locale = PRODUCT.Locale;

            for (var name in variationSpecifics) {

                if (!isEmptyOrNull(name)) {

                    if (variationVector.length > 0 && variationVector.indexOf(name) < 0) {
                        LOGGER.log("--------------- VariationVector check: " + name + " does not exist!!");
                    } else {
                        // Attribute name exists in the variation vector.
                        if (considerVariationVectorOrder) {
                            // If the boolean is set then order the attribute elements as per the order
                            attributeWithOrderMap[variationVector.indexOf(name)] = name;
                            count++;
                        } else {
                            // If the boolean is not set, just add the variation specific without considering order.
                            if (!isEmptyOrNull(category) && !isEmptyOrNull(locale)) {
                                // For each attribute name look it up in the mapping csv file. This is to map from the seller's attribute name to eBay's attribute name.
                                var attributeNameMapping = LOOKUP_SERVICE.get('/store/lookup/common/ItemSpecificsMapping.csv', [category, locale, name]);
                                if (!isEmptyOrNull(attributeNameMapping)) {
                                    attributeName = attributeNameMapping[0];
                                }
                            }
                            var value = variationSpecifics[name];
                            if (!isEmptyOrNull(value) && value.length > 0) {
                                variation.VariationSpecifics[attributeName] = value[0];
                            }
                        }
                    }
                }
            }

            // If the boolean is set then attribute elements have been ordered. Fill in the variationspecifics. 
            if (considerVariationVectorOrder) {
                for (var i = 0; i < count; i++) {
                    var name = attributeWithOrderMap[i];
                    var value = variationSpecifics[name];
                    if (!isEmptyOrNull(category) && !isEmptyOrNull(locale)) {
                        var attributeNameMapping = LOOKUP_SERVICE.get('/store/lookup/common/ItemSpecificsMapping.csv', [category, locale, name]);
                        if (!isEmptyOrNull(attributeNameMapping)) {
                            name = attributeNameMapping[0];
                        }
                    }
                    if (!isEmptyOrNull(value) && value.length > 0) {
                        variation.VariationSpecifics[name] = value[0];
                    }
                }
            }

        } catch (err) {
            LOGGER.log("---- variationSpecifics JSON parse error for " + PRODUCT.ID + " : " + variation.SKU + " : " + err.message);
        }
    }
}

function setVariationPackageDetails(variation) {

    variation.VariationPackageDetails["MeasurementSystem"] = capitaliseFirstLetter(getColumnValue('measurementSystem'));
    variation.VariationPackageDetails["WeightMajor"] = getColumnValue('weightMajor');
    variation.VariationPackageDetails["WeightMinor"] = getColumnValue('weightMinor');
    variation.VariationPackageDetails["Length"] = getColumnValue('length');
    variation.VariationPackageDetails["Width"] = getColumnValue('width');
    variation.VariationPackageDetails["Depth"] = getColumnValue('height');
    variation.VariationPackageDetails["Type"] = getColumnValue('packageType');
}

function capitaliseFirstLetter(string) {
    if (!isEmptyOrNull(string)) {
        string = string.toLowerCase();
        return string.charAt(0).toUpperCase() + string.slice(1);
    }
}

function setVariationVector() {

    var variationVectorStr = getColumnValue('variationVector');

    if (!isEmptyOrNull(variationVectorStr)) {

        try {
            var variationVectorList = JSON.parse(variationVectorStr);

            var variationNamesDisplayOrderMap = {};
            var variationNamesDisplayOrderNumberArray = [];
            var BreakException = {};

            try {
                for (var i = 0; i < variationVectorList.length; i++) {

                    var variationVectorObj = variationVectorList[i];

                    var displayOrder = variationVectorObj.displayOrder;
                    var variationName = variationVectorObj.name;

                    if (typeof displayOrder !== 'undefined' && !isEmptyOrNull(displayOrder)
                        && typeof variationName !== 'undefined' && !isEmptyOrNull(variationName)) {
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
            } catch (e) {
                considerVariationVectorOrder = false;
            }

            if (considerVariationVectorOrder) {
                variationNamesDisplayOrderNumberArray.sort(function (a, b) { return a - b });
            }

            for (var i = 0; i < variationNamesDisplayOrderNumberArray.length; i++) {
                var orderKey = variationNamesDisplayOrderNumberArray[i];
                variationVector.push(variationNamesDisplayOrderMap[orderKey]);
            }
        } catch (err) {
            LOGGER.log("---- variationVector JSON parse error for " + PRODUCT.ID + " : " + err.message);
            considerVariationVectorOrder = false;
        }
    } else {
        // If variation vector is not provided then don't consider order at all.
        considerVariationVectorOrder = false;
    }

}


