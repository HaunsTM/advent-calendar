//adventCalendarApp.config(function ($routeProvider, $locationProvider, $httpProvider) {
//    //here we will write code for implement routing 
//    $routeProvider
//        .when('/', {
//            // This is for reditect to another route
//            redirectTo: function() {
//                return '/welcome';
//            }
//        })
//        .when('/about', {
//            templateUrl: '/angularJSApp/components/about/AboutView.html',
//            controller: 'AboutController'
//        })
//        .when('/assignUsersToCalendar', {
//            templateUrl: '/angularJSApp/components/assignUsersToCalendar/AssignUsersToCalendarView.html',
//            controller: 'AssignUsersToCalendarController'
//        })
//        .when('/calendar', {
//            templateUrl: '/angularJSApp/components/calendar/CalendarView.html',
//            controller: 'CalendarController'
//        })
//        .when('/createCalendar', {
//            templateUrl: '/angularJSApp/components/createCalendar/CreateCalendarView.html',
//            controller: 'CreateCalendarController'
//        })
//        .when('/login', {
//            templateUrl: '/angularJSApp/components/login/LoginView.html',
//            controller: 'LoginController'
//        })
//        .when('/logout', {
//            templateUrl: '/angularJSApp/components/logout/LogoutView.html',
//            controller: 'LogoutController'
//        })
//        .when('/register', {
//            templateUrl: '/angularJSApp/components/register/RegisterView.html',
//            controller: 'RegisterController'
//        })
//        .when('/welcome', {
//            templateUrl: '/angularJSApp/components/welcome/WelcomeView.html',
//            controller: 'WelcomeController'
//        })
//        .otherwise({
//            // This is when any route not matched
//            templateUrl: '/angularJSApp/components/error/ErrorView.html',
//            controller: 'ErrorController'
//        });

//    $locationProvider.html5Mode(false).hashPrefix('!'); // This is for Hashbang Mode
//    $httpProvider.interceptors.push('AuthHttpResponseInterceptorFactory');
//});

adventCalendarApp.config(['$stateProvider', '$httpProvider', '$locationProvider',function($stateProvider, $httpProvider, $locationProvider) {
    //http://www.codemag.com/article/1505061
    $stateProvider
        .state('stateAbout', {
            url: '/about', //det som syns i webbläsaren
            views: {
                "containerOne": {
                    templateUrl: '/angularJSApp/components/welcome/WelcomeView.html', //var mallfilen finns
                    controller: 'WelcomeController'
                },
                "containerTwo": {
                    templateUrl: '/angularJSApp/components/about/AboutView.html',
                    controller: 'AboutController'
                }
            }
        })
        .state('stateAssignUsersToCalendar', {
            url: '/assignUsersToCalendar',
            views: {
                "containerOne": {
                    templateUrl: '/angularJSApp/components/welcome/WelcomeView.html',
                    controller: 'WelcomeController'
                },
                "containerTwo": {
                    templateUrl: '/angularJSApp/components/assignUsersToCalendar/AssignUsersToCalendarView.html',
                    controller: 'AssignUsersToCalendarController'
                }
            }
        })
        .state('stateCalendar', {
            url: '/calendar',
            views: {
                "containerOne": {
                    templateUrl: '/angularJSApp/components/welcome/WelcomeView.html',
                    controller: 'WelcomeController'
                },
                "containerTwo": {
                    templateUrl: '/angularJSApp/components/calendar/CalendarView.html',
                    controller: 'CalendarController'
                }
            }
        })
        .state('stateCreateCalendar', {
            url: '/createCalendar',
            views: {
                "containerOne": {
                    templateUrl: '/angularJSApp/components/welcome/WelcomeView.html',
                    controller: 'WelcomeController'
                },
                "containerTwo": {
                    templateUrl: '/angularJSApp/components/createCalendar/CreateCalendarView.html',
                    controller: 'CreateCalendarController'
                }
            }
        })
        .state('stateLogin', {
            url: '/login',
            views: {
                "containerOne": {
                    templateUrl: '/angularJSApp/components/welcome/WelcomeView.html',
                    controller: 'WelcomeController'
                },
                "containerTwo": {
                    templateUrl: '/angularJSApp/components/login/LoginView.html',
                    controller: 'LoginController'
                }
            }
        })
        .state('stateLogout', {
            url: '/logout',
            views: {
                "containerOne": {
                    templateUrl: '/angularJSApp/components/welcome/WelcomeView.html',
                    controller: 'WelcomeController'
                },
                "containerTwo": {
                    templateUrl: '/angularJSApp/components/logout/LogoutView.html',
                    controller: 'LogoutController'
                }
            }
        })
        .state('stateRegister', {
            url: '/register',
            views: {
                "containerOne": {
                    templateUrl: '/angularJSApp/components/welcome/WelcomeView.html',
                    controller: 'WelcomeController'
                },
                "containerTwo": {
                    templateUrl: '/angularJSApp/components/register/RegisterView.html',
                    controller: 'RegisterController'
                }
            }
        })
        .state('stateWelcome', {
            url: '/welcome',
            views: {
                "containerOne": {
                    templateUrl: '/angularJSApp/components/welcome/WelcomeView.html',
                    controller: 'WelcomeController'
                },
                "containerTwo": {
                    templateUrl: '/angularJSApp/components/welcome/WelcomeView.html',
                    controller: 'WelcomeController'
                }
            }
        })
        .state('otherwise', {
            url: '*path',
            views: {
                "containerOne": {
                    templateUrl: '/angularJSApp/components/welcome/WelcomeView.html',
                    controller: 'WelcomeController'
                },
                "containerTwo": {
                    templateUrl: '/angularJSApp/components/empty/emptyView.html',
                    controller: 'WelcomeController'
                }
            }
        });

    $locationProvider.html5Mode(true).hashPrefix('!'); // This is for Hashbang Mode
    $httpProvider.interceptors.push('AuthHttpResponseInterceptorFactory');
}]);