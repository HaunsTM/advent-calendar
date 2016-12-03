/*
    Vad är ett Directive?
    Ett Directive är en markör på ett DOM-element (ett attribut, ett element-namn, en kommentar eller en CSS-klass) som berättar för AngularJS HTML-kompilator att en viss DOM ska ha ett visst beteende.
        
    Namngivning av directive utgår från camelCase: positionSvgElementsRelatively, när ett direktiv ska användas i html använder man snake-case: position-svg-elements-relatively*

    Anprop
    Anrop av ett directive kan ske på fyra olika sätt:
        Element name (E):   <w3-test-directive></w3-test-directive>
        Attribute (A):      <div w3-test-directive></div>
        Class (C):          <div class="w3-test-directive"></div>
        Comment (M):        <!-- directive: w3-test-directive -->

    Restriktioner:
        EACM

*/

/*
    Prototyp:
        app.directive('helloWorld', function() {
            return {
                restrict: 'AE',
                replace: 'true',
                template: '<h3>Hello World!!</h3>'
            };
        });
    I ovanstående kod skapar vi ett nytt direktiv med funktionen app.directive(). Det första argumentet till funktionen är namnet på direktivet. Det andra argumentet är ett definitionsobjekt (om direktivet ska ha beroende till externa pbjekt/services som t ex $rootScope, $http, eller $compile kan dessa injiceras här.)

*/

/*
    restrict – 
        Talar om hur direktivet får lov att användas i html (ett direktiv kan som tidigare sagts anropas på fyra olika sätt . I exemplet har begränsningar satts till attribut resp. element

*/

/*    
    template – 
        Det här anger vilken HTML som ska produceras och länkas när direktivet länkas och kompileras av Angular. HTML behöver inte vara en simpel sträng, utan kan vara något avsevärt mera komplicerat som även refererar till andra direktiv  Templaten kan vara väordentligt komplex och även involvera andra direktiv, uttryck ({{ }}), o s v. I de flesta fallen kommer man att vilja använda sig av en templateUrl istället för template. Det bästa är att placera all HTML i en separat fil och därefter peka templateURL mot den.
*/

/*
    replace – (deprecated)
        Exempel: När direktivet har kompilerats kan exempelvis det här, <hello-world></hello-world>, bytas ut mot det här: <h3>Hello World!!</h3>.
        * true - Det här värdet anger att den skapade templaten ska ersätta HTML-elementet på vilket direktivet är placerat.
        * false - Det här värdet anger att den skapade templaten ska ersätta HTML-innehållet i elementet på vilket direktivet är placerat.

        <script>
            angular.module('scopes', [])
                  .controller('Ctrl', function($scope) {
                        $scope.title = "hello";

                  })
                  .directive('myDir', function() {
                    return {
                      restrict: 'E',
                      replace: true,
                      template: '<div>{{title}}</div>'
                    };
              });
        </script>

        <div ng-controller="Ctrl">
            <my-dir>
                <h3>some existing content</h3>
            </my-dir>
        </div>

        replace: 
            
            true:

                <div ng-controller="Ctrl" class="ng-scope">
                    <div class="ng-binding">hello</div>
                </div>

            false:

                <div ng-controller="Ctrl" class="ng-scope">
                    <my-dir>
                        <div class="ng-binding">hello</div>
                    </my-dir>
                </div>
    https://www.sitepoint.com/practical-guide-angularjs-directives/
*/
"use strict";
app.directive('positionSlots', ['$window', '$timeout', function ($window, $timeout) {

    var initialCalendarArea = function (svgElement) {
        var initRect = svgElement.getBoundingClientRect(); // get the bounding rectangle
        return {
            width: initRect.width,
            height: initRect.height
        }
    };

    var scale = function (oldWidth, newWidth, oldHeight, newHeight) {
        var wF = newWidth / oldWidth;
        var hF = newHeight / oldHeight;
        return {
            widthFactor: wF,
            heightFactor: hF
        };
    }

    var UpdateSlot = function(slot, scale) {

        var updatedX = 0;
        var updatedY = 0;
        // Getting
        var xforms = slot.transform.baseVal; // An SVGTransformList
        var firstXForm = xforms.getItem(0);       // An SVGTransform
        if (firstXForm.type == SVGTransform.SVG_TRANSFORM_TRANSLATE) {
            updatedX = firstXForm.matrix.e / scale.widthFactor;
            updatedY = firstXForm.matrix.f / scale.heightFactor;
        }

        // Setting
        slot.transform.baseVal.getItem(0).setTranslate(updatedX, updatedY);

    }


    function link_fn(scope, element, attrs) {
        var svgElement = element[0];
        var initialCalendar = initialCalendarArea(svgElement);

        scope.oldWidth = initialCalendar.width;
        scope.newWidth = svgElement.getBoundingClientRect().width;
        scope.oldHeight = initialCalendar.width;
        scope.newHeight = svgElement.getBoundingClientRect().height;

        function onResize() {

            // Reset timeout
            $timeout.cancel(scope.resizing);

            // Add a timeout to not call the resizing function every pixel
            scope.resizing = $timeout(function () {
                scope.oldWidth = scope.newWidth;
                scope.newWidth = svgElement.getBoundingClientRect().width;
                scope.oldHeight = scope.newHeight;
                scope.newHeight = svgElement.getBoundingClientRect().height;

                var factor = scale(scope.oldWidth, scope.newWidth, scope.oldHeight, scope.newHeight);

                var slots = svgElement.querySelectorAll('g');

                angular.forEach(slots, function (slot) {
                    UpdateSlot(slot, factor);
                });
            }, 300);
        };

        function cleanUp() {
            angular.element($window).off('resize', onResize);
        }

        angular.element($window).on('resize', onResize);
        scope.$on('$destroy', cleanUp);
    }


    return {
        link: link_fn
    };

}]);