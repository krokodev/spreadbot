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
    CONFIG.FileType = 'AVAILABILITY';
	CONFIG.FileFormat = 'CSV';
	//CONFIG.ColumnNames = ['SKU','LocationID', 'FulfillmentType', 'Quantity', 'Available', 'FulfillmentTime'];
	CONFIG.ColumnNames = ['column1','column2','column3','column4','column5','column6'];
	CONFIG.SkipRowCount = '0';
	CONFIG.CommentString = "//";
	
 }
 
  function loadSku() {
    LOGGER.log("Fetch Availability Feed Key");
	LOGGER.log(AVAILABILITY_INRECORD.SKU.toString());
	return AVAILABILITY_INRECORD.SKU.toString();
}


  function transform(){
     
	LOGGER.log("Start of Availability Feed Transformation");
	
	for (i=0; i < GROUP.length; i++)
	{
	  AVAILABILITY_INRECORD = GROUP[i];
	  if ( i == 0)
	  {
	    transformParent();
	  }
	  else{
	  transformChild();
	  }
	   
	}
   }
   
   
   function transformParent(){
     LOGGER.log("Start of Availability Feed Parent Row Transformation");
     AVAILABILITY.SKU = isEmptyOrNull(AVAILABILITY_INRECORD.SKU) ? null : AVAILABILITY_INRECORD.SKU;
     AVAILABILITY.TotalShipToHomeQuantity = isEmptyOrNull(AVAILABILITY_INRECORD.Quantity) ? null : AVAILABILITY_INRECORD.Quantity;
   }
   
   
   function transformChild(){
    LOGGER.log("Start of Availability Feed Child Row Transformation");
	var locationID = isEmptyOrNull(AVAILABILITY_INRECORD.LocationID) ? null : AVAILABILITY_INRECORD.LocationID;
	var location = AVAILABILITY.createLocation(locationID);
	
	location.LocationID = locationID;
	if (!isEmptyOrNull(AVAILABILITY_INRECORD.FulfillmentType)){
	   var fulfillmentType = isEmptyOrNull(AVAILABILITY_INRECORD.FulfillmentType) ? null : AVAILABILITY_INRECORD.FulfillmentType;
	   var fulfillmentMethod = location.createFulFillmentMethod(fulfillmentType);
	   fulfillmentMethod.FulfillmentType = fulfillmentType;
	   fulfillmentMethod.Quantity = isEmptyOrNull(AVAILABILITY_INRECORD.Quantity) ? null : AVAILABILITY_INRECORD.Quantity;
	   fulfillmentMethod.Available = isEmptyOrNull(AVAILABILITY_INRECORD.Available) ? null : AVAILABILITY_INRECORD.Available;
	   if(fulfillmentType == "PICKUP_IN_STORE" && isEmptyOrNull(fulfillmentMethod.Available)) {
	      if (!isEmptyOrNull(AVAILABILITY_INRECORD.Quantity) && AVAILABILITY_INRECORD.Quantity >0){
		    fulfillmentMethod.Available = "IN_STOCK"; 
		  }
		  else {
		     fulfillmentMethod.Available = "OUT_OF_STOCK";
		  }
	    }
	  fulfillmentMethod.FulfillmentTime = isEmptyOrNull(AVAILABILITY_INRECORD.FulfillmentTime) ? null : AVAILABILITY_INRECORD.FulfillmentTime;
	}
  }
  
   function isEmptyOrNull( str )
{
    return (str == null || str.toString() == '' || str.toString() == 'null' || str.toString() == 'undefined' || str.toString() == undefined);
}
   
  

   
   

