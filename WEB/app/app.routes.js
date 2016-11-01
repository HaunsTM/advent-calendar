"use strict";
adventCalendarApp.config(['$stateProvider', '$httpProvider', '$locationProvider', function ($stateProvider, $httpProvider, $locationProvider) {
    //http://www.codemag.com/article/1505061
    $stateProvider
        .state('stateAbout', {
            url: '/about', //det som syns i webbläsaren
            views: {
                "containerOne": {
                    templateUrl: '/app/components/welcome/welcomeView.html', //var mallfilen finns
                    controller: 'welcomeController'
                },
                "containerTwo": {
                    templateUrl: '/app/components/about/aboutView.html',
                    controller: 'aboutController'
                }
            }
        })
        .state('stateAssignUsersToCalendar', {
            url: '/assignUsersToCalendar',
            views: {
                "containerOne": {
                    templateUrl: '/app/components/welcome/welcomeView.html',
                    controller: 'welcomeController'
                },
                "containerTwo": {
                    templateUrl: '/app/components/assignUsersToCalendar/assignUsersToCalendarView.html',
                    controller: 'assignUsersToCalendarController'
                }
            }
        })
        .state('stateCalendar', {
            url: '/calendar',
            views: {
                "containerThree": {
                    templateUrl: '/app/components/calendar/calendarView.html',
                    controller: 'calendarController'
                }
            }
        })
        .state('stateCreateCalendar', {
            url: '/createCalendar',
            views: {
                "containerOne": {
                    templateUrl: '/app/components/welcome/welcomeView.html',
                    controller: 'welcomeController'
                },
                "containerTwo": {
                    templateUrl: '/app/components/createCalendar/createCalendarView.html',
                    controller: 'createCalendarController'
                }
            }
        })
        .state('stateLogin', {
            url: '/login',
            views: {
                "containerOne": {
                    templateUrl: '/app/components/welcome/welcomeView.html',
                    controller: 'welcomeController'
                },
                "containerTwo": {
                    templateUrl: '/app/components/login/loginView.html',
                    controller: 'loginController'
                }
            }
        })
        .state('stateLogout', {
            url: '/logout',
            views: {
                "containerOne": {
                    templateUrl: '/app/components/welcome/welcomeView.html',
                    controller: 'welcomeController'
                },
                "containerTwo": {
                    templateUrl: '/app/components/logout/logoutView.html',
                    controller: 'logoutController'
                }
            }
        })
        .state('stateRegisterUserAdministrator', {
            url: '/registerUserAdministrator',
            views: {
                "containerOne": {
                    templateUrl: '/app/components/welcome/welcomeView.html',
                    controller: 'welcomeController'
                },
                "containerTwo": {
                    templateUrl: '/app/components/registerUserAdministrator/registerUserAdministratorView.html',
                    controller: 'registerUserAdministratorController'
                }
            }
        })
        .state('stateUploadSlotsToCalendar', {
            url: '/uploadSlotsToCalendar',
            views: {
                "containerOne": {
                    templateUrl: '/app/components/welcome/welcomeView.html',
                    controller: 'welcomeController'
                },
                "containerTwo": {
                    templateUrl: '/app/components/uploadSlotsToCalendar/uploadSlotsToCalendarView.html',
                    controller: 'uploadSlotsToCalendarController'
                }
            }
        })
        .state('stateWelcome', {
            url: '/welcome',
            views: {
                "containerOne": {
                    templateUrl: '/app/components/welcome/welcomeView.html',
                    controller: 'welcomeController'
                },
                "containerTwo": {
                    templateUrl: '/app/components/welcome/welcomeView.html',
                    controller: 'welcomeController'
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
                    templateUrl: '/app/components/welcome/WelcomeView.html',
                    controller: 'welcomeController'
                },
                "containerTwo": {
                    templateUrl: '/app/components/empty/emptyView.html',
                    controller: 'emptyController'
                }
            }
        });
    $locationProvider.html5Mode(true).hashPrefix('!'); // This is for Hashbang Mode
    $httpProvider.interceptors.push('authHttpResponseInterceptorFactory');
}]);