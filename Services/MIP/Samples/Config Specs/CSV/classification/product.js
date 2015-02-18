/**
* Classification - SKUBI CSV eBay Schema
*/

// Global variables

/*
 * columnNames and headerColumns logic is implemented to read the header column names dynamically.
 * It is ok to increase this number of columns more than 'AZ' as well.
 */
var columnNames = ['A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
                    'AA', 'AB', 'AC', 'AD', 'AE', 'AF', 'AG', 'AH', 'AI', 'AJ', 'AK', 'AL', 'AM', 'AN', 'AO', 'AP', 'AQ', 'AR', 'AS', 'AT', 'AU', 'AV', 'AW', 'AX', 'AY', 'AZ'];

var headerColumns = new Array();

var commentStr = '//';

/**
* Configures input, output, and processing options.
* This function is called only ONCE by the tranformation processor to read
* the configuration data.
*/
function config() {

    // The input file format
    CONFIG.FileFormat = 'CSV';

    CONFIG.ColumnNames = columnNames;
    
    // Skipping the Version row
    CONFIG.SkipRowCount = '0';
                             
    // The node/record "grouping" strategy used by the groupBy() function.
    CONFIG.GroupingType = 'VALUE';

    CONFIG.EbayCSVConfigSpec = 'true';    
}

/*
* This function is called by transformation processor for EACH input record.
* The function should return the primary key value for the given record
*/
function groupBy() {

    var sku = getColumnValue('SKU');
    var locale = getColumnValue('localizedFor');
    var groupId = getColumnValue('variationGroupId');

    if (!isEmptyOrNull(groupId)) {
        return groupId + "|~~|" + locale;
    }

    return sku + "|~~|" + locale;

}

/**
 * This function is used in the code to check if a variant of msku is given in the feed
 * without the parent/group information.
 */ 
function getVariantOf(){
    var sku = getColumnValue('SKU');
    var variationGroupId = getColumnValue('variationGroupId');
    var locale = getColumnValue('localizedFor');
    if (!isEmptyOrNull(sku) && !isEmptyOrNull(variationGroupId)) {
        return variationGroupId + "|~~|" + locale;
    }    
}

/**
 * This function is used in the code to check if parent/group of msku is given in the feed
 * without any variations.
 */
function getGroupId(){
    var sku = getColumnValue('SKU');
    var variationGroupId = getColumnValue('variationGroupId');
    var locale = getColumnValue('localizedFor');
    if (isEmptyOrNull(sku) && !isEmptyOrNull(variationGroupId)) {
        return variationGroupId + "|~~|" + locale;
    } 
 }


/*
* This function is called by transformation processor for EACH input record.
* The function should return the true or false to convey if the row should be skipped from processing.
* To read the headercolumn names dynamically, when this function is called first time for the header columns record,
* each header column name is added to the headerColumns array. This array is used for dynamically parsing the rest of the
* records.
*/
function isSkipRecord() {

     if (typeof String.prototype.startsWith != 'function') {
        // see below for better implementation!
        String.prototype.startsWith = function (str){
            return this.indexOf(str) == 0;
        };
     }

     if (PRODUCT_INRECORD[columnNames[0]].startsWith(commentStr) == true) {
        return true;        
     }
     else if (headerColumns.length == 0) {
        // The loop is breaked when it reaches the end of the header columns record.
        var BreakException = {};
        try {
            for each(columnName in columnNames) {
                if (typeof PRODUCT_INRECORD[columnName] !== 'undefined') {
                    headerColumns.push(PRODUCT_INRECORD[columnName].toString());
                } else {
                    throw BreakException;
                }
            }
        } catch(e) {
        }
        return true;
    }
    return false;
}

/*
* This function is called by the transformation processor for EACH group.
* The objective is to populate the PRODUCT object with the necessary information so that it will be persisted.
*/
function transform() {

    for (i = 0; i < GROUP.length; i++) {

        PRODUCT_INRECORD = GROUP[i];

        //Assuming first element of the GROUP is always the parent.
        if (i == 0) {
            transformParent();
        } else {
            transformChild();
        }
    }
}

/*
 * This is an user defined function which demonstrates how the PRODUCT object can be populated for a group parent.
 */
function transformParent() {

    var sku = getColumnValue('SKU');
    var groupId = getColumnValue('variationGroupId');
    var locale = getColumnValue('localizedFor');


    // For GROUP length 1 adding additional check of empty SKU in case a MultiSku Parent comes alone in a GROUP with no Variant products.
    if (GROUP.length == 1 && !isEmptyOrNull(sku) ) {
        
        PRODUCT.ID = sku;
        PRODUCT.SKU = sku;

    } else {
        
        PRODUCT.ID =  groupId;
        PRODUCT.SKU =  groupId;

    }

    /* Locale */
    PRODUCT.Locale = locale;

    /* Title and Subtitle */
    PRODUCT.Title = getTitle();
}


/*
* This is a user defined function which demonstrates how the PRODUCT variations can be populated for a group child.
*
*/
function transformChild() {
}