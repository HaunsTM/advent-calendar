/*I AngularJS finns interceptors, en typ av filter som fångar upp in- och utgående data. 

Med hjälp av interceptors kan man till exempel bygga generell loggning i applikationen.  I vårt fall måste token skickas med varje anrop för att servern ska ge åtkomst till skyddad data. Med en http interceptor kan man läsa upp token från local storage och lägga till auth headern för alla serveranrop.

http://www.webdeveasy.com/interceptors-in-angularjs-and-useful-examples/
Interceptorer:

function request(answer) {}

    Den här metoden anropas innan $http skickar request till backend - man kan modifiera konfigurationen. Funktionen tar in ett konfigurationsobjekt som parameter och returnerar ett konfigurationsobjekt eller ett Promise. Retur av ett felaktigt konfigurationsobjekt (eller tillbakavisat Promise) kommer leda till att $http-anropet misslyckas!

function requestError(answer) {}
    Ibland kan en Request av någon anledning inte skickas eller så tillbakavisas den av interceptorn. Request-error-interceptorn fångar upp Requests som har avbrutits. Funktionen kan användas till att återställa en Request samt (ibland) ogöra inställningar som gjordes före requesten (t ex ladda indikatorer, lägga till knappar, fält osv.).
    
function response(answer) {}

    Den här metonden anropas direkt efter att $http har tagit emot respone från backend, det är alltså möjligt att modifiera response samt utföra andra åtgärder. Funktionen tar ett response-objekt som inparameter och returnerar det eller ett Promise. Response-objektet inkluderar Request-konfiguration, Headers, Status samt data från backend. Om returnerad Response (eller Promise) är ogiltigt misslyckas $http-anropet. 

function responseError(rejection) {}
    Ibland misslyckas anropen mot backend, andra gånger förvägras de av någon anledning (request-interceptor eller response-interceptor). I de fallen kan Response-error-interceptor komma till vår hjälp och återställa anropet mot backend.
*/
//http://sverigesbastamedarbetare.se/Home/Article/24-angularjs-autentisering-behorighet
"use strict";
app.factory('authHttpResponseInterceptorFactory', ['$q', '$location', '$injector', function ($q, $location, $injector) {

    var interceptor = {
        //request: request,
        //requestError: requestError,
        response: response,
        responseError: responseError
    };
    /*
    function request(answer) {
        if (answer.status === 401) {
            console.log("Response 401");
        }
        return answer || $q.when(answer);
    }
    function requestError(answer) {
        if (answer.status === 401) {
            console.log("Response 401");
        }
        return answer || $q.when(answer);
    }*/
    function response(answer) {
        if (answer.status === 401) {
            console.log("Response 401");
        }
        return answer || $q.when(answer);
    }
    function responseError(rejection) {
        //om servern returnerar att vi inte är behöriga att anropa resursen, gå till inloggningssidan
        if (rejection.status === 401) {
            $injector.get('$state').go('stateLogin', { returnUrl: $location.path() });
        }
        return $q.reject(rejection);
    }

    return interceptor;
}]);