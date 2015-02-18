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

    return fullTitle;
}
