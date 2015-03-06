/**
* Returns an array of scripts required to process the input file.
* Scripts are loaded in the order in which they are provided.
* 
* The following libraries are automatically loaded for your convenience:
*
* > Lo-Dash (http://lodash.com/docs)
* > Underscore.string (https://github.com/epeli/underscore.string)
* > Moment.js (http://momentjs.com/)
*
* @return {Array.<string>} Array of script filenames
*/
function scripts() {
    return ['product.js', 'product_utils.js'];
}