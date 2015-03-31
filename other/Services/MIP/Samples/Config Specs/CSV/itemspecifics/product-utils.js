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
    var itemSpecificsStr = getColumnValue('attributes');
    if (!isEmptyOrNull(itemSpecificsStr)) {
        try {
            var itemSpecifics = JSON.parse(itemSpecificsStr);
            for (var name in itemSpecifics) {
                if (!isEmptyOrNull(name)) {
                    PRODUCT.ItemSpecifics[name] = itemSpecifics[name];
                }
            }
        } catch (err) {
            LOGGER.log("---- itemspecifics JSON parse error for " + PRODUCT.ID + " : " + err.message);
        }
    }
}
