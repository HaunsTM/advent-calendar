adventCalendarApp.config(function ($routeProvider, $locationProvider) {
    //here we will write code for implement routing 
    $routeProvider
        .when('/', {
            // This is for reditect to another route
            redirectTo: function() {
                return '/welcome';
            }
        })
        .when('/about', {
            templateUrl: '/angularJSApp/components/about/aboutView.html',
            controller: 'aboutController'
        })
        .when('/assignUsersToCalendar', {
            templateUrl: '/angularJSApp/components/assignUsersToCalendar/assignUsersToCalendarView.html',
            controller: 'assignUsersToCalendarController'
        })
        .when('/calendar', {
            templateUrl: '/angularJSApp/components/calendar/calendarView.html',
            controller: 'calendarController'
        })
        .when('/createCalendar', {
            templateUrl: '/angularJSApp/components/createCalendar/createCalendarView.html',
            controller: 'createCalendarController'
        })
        .when('/login', {
            templateUrl: '/angularJSApp/components/login/loginView.html',
            controller: 'loginController'
        })
        .when('/logout', {
            templateUrl: '/angularJSApp/components/logout/logoutView.html',
            controller: 'logoutController'
        })
        .when('/register', {
            templateUrl: '/angularJSApp/components/register/registerView.html',
            controller: 'registerController'
        })
        .when('/welcome', {
            templateUrl: '/angularJSApp/components/welcome/welcomeView.html',
            controller: 'welcomeController'
        })
        .otherwise({
            // This is when any route not matched
            templateUrl: '/angularJSApp/components/error/errorView.html',
            controller: 'errorController'
        });
 
    $locationProvider.html5Mode(false).hashPrefix('!'); // This is for Hashbang Mode
})