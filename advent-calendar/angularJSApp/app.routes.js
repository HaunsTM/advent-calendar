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
            /* $stateProvider har till skillnad från $routeProvider inget inbyggt stöd för 
               otherwise-funktionalitet (när vi försöker navigera till något som inte är 
               definierat i vår stateprovider). Som work-around kan man då använda 'catch 
               all syntax' på URL ("*path"), en specialsyntax för  */
            url: '*path',
            views: {
                "containerOne": {
                    templateUrl: '/angularJSApp/components/welcome/WelcomeView.html',
                    controller: 'WelcomeController'
                },
                "containerTwo": {
                    templateUrl: '/angularJSApp/components/empty/emptyView.html',
                    controller: 'EmptyController'
                }
            }
        });

    $locationProvider.html5Mode(true).hashPrefix('!'); // This is for Hashbang Mode
    $httpProvider.interceptors.push('AuthHttpResponseInterceptorFactory');
}]);