﻿"use strict";
app.config(['$stateProvider', '$httpProvider', '$locationProvider', function ($stateProvider, $httpProvider, $locationProvider) {
    //http://www.codemag.com/article/1505061
    $stateProvider
        .state('stateAbout', {
            url: '/about', //det som syns i webbläsaren
            views: {
                "containerOne": {
                    templateUrl: '/angularJSApp/components/welcome/welcomeView.html', //var mallfilen finns
                    controller: 'welcomeController'
                },
                "containerTwo": {
                    templateUrl: '/angularJSApp/components/about/aboutView.html',
                    controller: 'aboutController'
                }
            }
        })
        .state('stateAssignUsersToCalendar', {
            url: '/assignUsersToCalendar',
            views: {
                "containerOne": {
                    templateUrl: '/angularJSApp/components/welcome/welcomeView.html',
                    controller: 'welcomeController'
                },
                "containerTwo": {
                    templateUrl: '/angularJSApp/components/assignUsersToCalendar/assignUsersToCalendarView.html',
                    controller: 'assignUsersToCalendarController'
                }
            }
        })
        .state('stateCalendar', {
            url: '/calendar',
            views: {
                "containerCalendar": {
                    templateUrl: '/angularJSApp/components/calendar/calendarView.html',
                    controller: 'calendarController'
                }
            }
        })
        .state('stateCreateCalendar', {
            url: '/createCalendar',
            views: {
                "containerOne": {
                    templateUrl: '/angularJSApp/components/welcome/welcomeView.html',
                    controller: 'welcomeController'
                },
                "containerTwo": {
                    templateUrl: '/angularJSApp/components/createCalendar/createCalendarView.html',
                    controller: 'createCalendarController'
                }
            }
        })
        .state('stateLogin', {
            url: '/login',
            views: {
                "containerOne": {
                    templateUrl: '/angularJSApp/components/welcome/welcomeView.html',
                    controller: 'welcomeController'
                },
                "containerTwo": {
                    templateUrl: '/angularJSApp/components/login/loginView.html',
                    controller: 'loginController'
                }
            }
        })
        .state('stateRegisterUserAdministrator', {
            url: '/registerUserAdministrator',
            views: {
                "containerOne": {
                    templateUrl: '/angularJSApp/components/welcome/welcomeView.html',
                    controller: 'welcomeController'
                },
                "containerTwo": {
                    templateUrl: '/angularJSApp/components/registerUserAdministrator/registerUserAdministratorView.html',
                    controller: 'registerUserAdministratorController'
                }
            }
        })
        .state('stateUploadSlotsToCalendar', {
            url: '/uploadSlotsToCalendar',
            views: {
                "containerOne": {
                    templateUrl: '/angularJSApp/components/welcome/welcomeView.html',
                    controller: 'welcomeController'
                },
                "containerTwo": {
                    templateUrl: '/angularJSApp/components/uploadSlotsToCalendar/uploadSlotsToCalendarView.html',
                    controller: 'uploadSlotsToCalendarController'
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
                    templateUrl: '/angularJSApp/components/welcome/WelcomeViewHelp.html',
                    controller: 'welcomeController'
                },
                "containerTwo": {
                    templateUrl: '/angularJSApp/components/welcome/WelcomeView.html',
                    controller: 'welcomeController'
                }
            }
        });

    $locationProvider.html5Mode(true).hashPrefix('!'); // This is for Hashbang Mode
    $httpProvider.interceptors.push('authHttpResponseInterceptorFactory');
}]);