/*
 * Below are the predefined variables in the calling context.
 * 		LOCATION_INRECORD - Input record data 
 *      LOCATION - Transformed location record
 */

/*
 * This function is called by MIP mapping processor 
 * before processing the each file
 */
 var commentStr ='//';
 
 function startUp() {
    LOGGER.log("Processing file " + MIP_CONTEXT.FileName);
 }

 function config() {
	LOGGER.log("Location Config");
	CONFIG.FileType = 'LOCATION';
    CONFIG.FileFormat = 'CSV';
	CONFIG.CommentString ="//";
	
	CONFIG.ColumnNames = ['column1','column2','column3','column4','column5','column6','column7','column8','column9','column10','column11','column12','column13','column14','column15','column16','column17','column18'];
	CONFIG.SkipRowCount = '0';
 }
 
 function getPrimaryKey() {
    LOGGER.log("Fetch Location Feed Key");
	LOGGER.log(LOCATION_INRECORD.locationID.toString());
	var locationId = isEmptyOrNull(LOCATION_INRECORD.locationID) ? null : LOCATION_INRECORD.locationID.toString();
	return locationId;
}

/*
 * This function is called by MIP mapping processor for each Location record.
 */
function transform(){
    LOGGER.log("Start of Location Feed Transformation");
    LOCATION.LocationID = isEmptyOrNull(LOCATION_INRECORD.locationID) ? null : LOCATION_INRECORD.locationID ;
    LOCATION.Name = isEmptyOrNull(LOCATION_INRECORD.name) ? null : LOCATION_INRECORD.name;
    LOCATION.Address1 = isEmptyOrNull(LOCATION_INRECORD.address1) ? null : LOCATION_INRECORD.address1; 
    LOCATION.Address2 = isEmptyOrNull(LOCATION_INRECORD.address2) ? null : LOCATION_INRECORD.address2;
    LOCATION.City = isEmptyOrNull(LOCATION_INRECORD.city) ? null : LOCATION_INRECORD.city;
	LOCATION.Region = isEmptyOrNull(LOCATION_INRECORD.region) ? null : LOCATION_INRECORD.region;
    LOCATION.PostalCode = isEmptyOrNull(LOCATION_INRECORD.postalCode) ? null : LOCATION_INRECORD.postalCode;
    LOCATION.Country = isEmptyOrNull(LOCATION_INRECORD.country) ? null : LOCATION_INRECORD.country;
    LOCATION.Latitude = isEmptyOrNull(LOCATION_INRECORD.latitude) ? null : LOCATION_INRECORD.latitude;
    LOCATION.Longitude = isEmptyOrNull(LOCATION_INRECORD.longitude) ? null : LOCATION_INRECORD.longitude;
	LOCATION.UtcOffset = isEmptyOrNull(LOCATION_INRECORD.utcOffset) ? null : LOCATION_INRECORD.utcOffset;
	LOCATION.Phone = isEmptyOrNull(LOCATION_INRECORD.phone) ? null : LOCATION_INRECORD.phone;
	LOCATION.url = isEmptyOrNull(LOCATION_INRECORD.url) ? null : LOCATION_INRECORD.url;
	LOCATION.PickupInstructions = isEmptyOrNull(LOCATION_INRECORD.pickupInstructions) ? null : LOCATION_INRECORD.pickupInstructions;
	LOCATION.Status = isEmptyOrNull(LOCATION_INRECORD.status) ? null : LOCATION_INRECORD.status;
	LOCATION.Hours = isEmptyOrNull(LOCATION_INRECORD.hours) ? null : LOCATION_INRECORD.hours;
	LOCATION.SpecialHours = isEmptyOrNull(LOCATION_INRECORD.specialHours) ? null : LOCATION_INRECORD.specialHours;
}

function isEmptyOrNull(str) {
    return (str == null || str.toString() == '' || str.toString() == 'null' || str.toString() == 'undefined' || str.toString() == undefined);
}

function shutDown() {
	LOGGER.log("Finished Processing " + MIP_CONTEXT.FileName);
}


