var app;
(function (app) {
    var main = angular.module("mallApp", []);
    main.run(Runner);
    function Runner() {
        console.log("we are up and running");
    }
})(app || (app = {}));
