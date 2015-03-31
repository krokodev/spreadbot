function isEmptyOrNull(str) {

    return (str == null || str == '');
}

/**
 * If Title length is greater than 80 characters trim it to 77 characters and add '...'
 */
function setTitle(element) {

    PRODUCT.Title = element.title.toString();
}
