adventCalendarApp.config(function($routeProvider, $locationProvider) {
    //here we will write code for implement routing 
    $routeProvider
        .when('/', {
            // This is for reditect to another route
            redirectTo: function() {
                return '/welcome';
            }
        })
        .when('/about', {
            templateUrl: '/angularJSApp/components/about/AboutView.html',
            controller: 'AboutController'
        })
        .when('/assignUsersToCalendar', {
            templateUrl: '/angularJSApp/components/assignUsersToCalendar/AssignUsersToCalendarView.html',
            controller: 'AssignUsersToCalendarController'
        })
        .when('/calendar', {
            templateUrl: '/angularJSApp/components/calendar/CalendarView.html',
            controller: 'CalendarController'
        })
        .when('/createCalendar', {
            templateUrl: '/angularJSApp/components/createCalendar/CreateCalendarView.html',
            controller: 'CreateCalendarController'
        })
        .when('/login', {
            templateUrl: '/angularJSApp/components/login/LoginView.html',
            controller: 'LoginController'
        })
        .when('/logout', {
            templateUrl: '/angularJSApp/components/logout/LogoutView.html',
            controller: 'LogoutController'
        })
        .when('/register', {
            templateUrl: '/angularJSApp/components/register/RegisterView.html',
            controller: 'RegisterController'
        })
        .when('/welcome', {
            templateUrl: '/angularJSApp/components/welcome/WelcomeView.html',
            controller: 'WelcomeController'
        })
        .otherwise({
            // This is when any route not matched
            templateUrl: '/angularJSApp/components/error/ErrorView.html',
            controller: 'ErrorController'
        });

    $locationProvider.html5Mode(false).hashPrefix('!'); // This is for Hashbang Mode
});