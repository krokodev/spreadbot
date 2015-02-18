/*
 * Below are the predefined variables in the calling context.
 * 		AVAILABILITY_INRECORD - Input record data 
 *      AVAILABILITY - Transformed location record
 */


/*
 * This function is called by MIP mapping processor 
 * before processing the each file
 */
 
 function startUp() {
    LOGGER.log("Processing file " + MIP_CONTEXT.FileName);
 }


 function config() { 
 	LOGGER.log("Location Config");
    CONFIG.FileFormat = 'XML';
	CONFIG.RecordPath = '/inventoryRequest/inventory';
 }
 
  function getPrimaryKey() {
    LOGGER.log("Fetch Availability Feed Key");
	LOGGER.log(AVAILABILITY_INRECORD.SKU.toString());
	return AVAILABILITY_INRECORD.SKU.toString();
}

function transform(){
    LOGGER.log("Start of Availability Feed Transformation");
	AVAILABILITY.SKU = AVAILABILITY_INRECORD.SKU.toString();
	AVAILABILITY.TotalShipToHomeQuantity = AVAILABILITY_INRECORD.totalShipToHomeQuantity.toString();
	
	var locations = AVAILABILITY_INRECORD.location;
	for each (var LOC_INRECORD in locations){
	   var locationID = LOC_INRECORD.locationID.toString();
	   var location = AVAILABILITY.createLocation(locationID);
	
	   location.LocationID = locationID;
	   location.TotalQuantityAtLocation = LOC_INRECORD.totalQuantityAtLocation.toString();
	   
	   var fulfillmentMethods = LOC_INRECORD.fulfillmentMethod;
	   for each (var FULFILLMENT_INRECORD in fulfillmentMethods){
	       var fulfillmentType = FULFILLMENT_INRECORD.fulfillmentType.toString();
	       var fulfillmentMethod = location.createFulFillmentMethod(fulfillmentType);
		   fulfillmentMethod.FulfillmentType = fulfillmentType;
		   fulfillmentMethod.Quantity = FULFILLMENT_INRECORD.quantity;
		   fulfillmentMethod.Available = FULFILLMENT_INRECORD.available.toString();
		   if (fulfillmentType == "PICKUP_IN_STORE" && fulfillmentMethod.Available == ""){
		       if (FULFILLMENT_INRECORD.quantity >0 ) {
			      fulfillmentMethod.Available = "IN_STOCK";
			   }
			   else{
			      fulfillmentMethod.Available = "OUT_OF_STOCK";
			   }
			}
		   
		   fulfillmentMethod.FulfillmentTime = FULFILLMENT_INRECORD.fulfillmentTime.toString();
		   
		}
	 }
}