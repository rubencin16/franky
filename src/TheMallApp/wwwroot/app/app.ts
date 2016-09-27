module app {
    var main = angular.module("mallApp", []);
    main.run(Runner);

    function Runner(): void {
        console.log("we are up and running");
    }
}