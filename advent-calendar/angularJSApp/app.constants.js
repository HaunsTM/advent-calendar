app.constant('constants', [function () {
	/**http://www.advancesharp.com/blog/1194/angularjs-constant-and-enum-with-example */
    debugger;
	var constants = {
		APPLICATION_USER_ROLE: ApplicationUserRoles
	};
	
	function ApplicationUserRoles() {
		var roles= {
	        SUPER_ADMINISTRATOR: 'SUPER_ADMINISTRATOR',
	        USER_ADMINISTRATOR: 'USER_ADMINISTRATOR',
	        ADMINISTRATOR: 'ADMINISTRATOR',
	        STANDARD_USER: 'STANDARD_USER',
			NO_ROLE: 'NO_ROLE'
		}
	    return roles;
	}

	return constants;
}]);