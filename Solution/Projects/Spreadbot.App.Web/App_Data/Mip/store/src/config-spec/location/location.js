/*
 * Below are the predefined variables in the calling context.
 * 		LOCATION_INRECORD - Input record data 
 *      LOCATION - Transformed location record
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
	CONFIG.RecordPath = '/locationRequest/location';
 }
 
 function getPrimaryKey() {
    LOGGER.log("Fetch Location Feed Key");
	LOGGER.log(LOCATION_INRECORD.locationID.toString());
	return LOCATION_INRECORD.locationID.toString();
}

/*
 * This function is called by MIP mapping processor for each inventory record.
 */
function transform(){
    LOGGER.log("Start of Location Feed Transformation");
    LOCATION.LocationID = LOCATION_INRECORD.locationID.toString();
    LOCATION.Name = LOCATION_INRECORD.name.toString();;
    LOCATION.Address1 = LOCATION_INRECORD.address1.toString();
    LOCATION.Address2 = LOCATION_INRECORD.address2.toString();
    LOCATION.City = LOCATION_INRECORD.city.toString();
	LOCATION.Region = LOCATION_INRECORD.region.toString();
    LOCATION.PostalCode = LOCATION_INRECORD.postalCode.toString();
    LOCATION.Country = LOCATION_INRECORD.country.toString();
    LOCATION.Latitude = LOCATION_INRECORD.latitude.toString();
    LOCATION.Longitude = LOCATION_INRECORD.longitude.toString();
	LOCATION.UtcOffset = LOCATION_INRECORD.utcOffset.toString();
	LOCATION.Phone = LOCATION_INRECORD.phone.toString();
	LOCATION.url = LOCATION_INRECORD.url;
	LOCATION.PickupInstructions = LOCATION_INRECORD.pickupInstructions.toString();
	LOCATION.Status = LOCATION_INRECORD.status;
	
	/* Transform Hours */
	var hours = '{ ';
	var days = LOCATION_INRECORD.hours.children();
	for each(var day in days) {
	    hours += '"' + day.dayOfWeek.toString() + '"';
		hours += ' : [';
		var intervals = day.interval;
		for each(var interval in intervals) {
		hours += '{"Open":"' + interval.open.toString() + '" , "Close":"' + interval.close.toString() + '"}';
		hours += ',';
		}
		hours += '],';
	}
	hours += '}';
	LOCATION.Hours = hours;
	
	/* Transform Special Hours */
	var specialHours = '{ ';
	var days = LOCATION_INRECORD.specialHours.children();
	for each(var day in days) {
	    specialHours += '"' + day.date.toString() + '"';
		specialHours += ' : [';
		var intervals = day.interval;
		for each(var interval in intervals) {
		specialHours += '{"Open":"' + interval.open.toString() + '" , "Close":"' + interval.close.toString() + '"}';
		specialHours += ',';
		}
		specialHours += '],';
	}
	specialHours += '}';
	LOCATION.SpecialHours = specialHours;
	
	
   
    /* Reference Hours format */
    /*   
    LOCATION.Hours = 		  '{ ' +
    				'"1" : [{"Open":"09:00:00" , "Close":"12:00:00"}, {"Open":"01:00:00" , "Close":"22:30:00"}],' +
    				'"2" : [{"Open":"09:00:00" , "Close":"22:30:00"}],' +
    				'"3" : [{"Open":"09:00:00" , "Close":"22:30:00"}],' +
    				'"4" : [{"Open":"09:00:00" , "Close":"22:30:00"}],' +
    				'"5" : [{"Open":"09:00:00" , "Close":"22:30:00"}],' +
    				'"6" : [{"Open":"09:00:00" , "Close":"22:30:00"}],' +
    				'"7" : [{"Open":"09:00:00" , "Close":"22:30:00"}],' +
    				'}';
					
	 */
	
}

/*
 * This function is called by MIP mapping processor 
 * after processing the each file
 *
 */
function shutDown() {
	LOGGER.log("Finished Processing " + MIP_CONTEXT.FileName);
}


