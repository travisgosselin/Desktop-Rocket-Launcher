angular.module("launcher").controller("LauncherListing", function ($scope, $http, $location, $resource) {
    
    var Launcher = $resource('api/launcher/:id');
    $scope.launchers = Launcher.query();

    $scope.selectLauncher = function (launcher) {
        $location.path('/' + launcher.name);
    };

    $scope.createLauncher = function () {
        var name = prompt("Name for the the launcher?");
        if (name && name.length) {
            Launcher.save({ name: name }, function(){  $scope.launchers = Launcher.query(); });
        }
    };

    $scope.removeLauncher = function (launcher) {
        if (confirm("Are you sure you want to remove all data with this launcher?")) {
            Launcher.delete({ id: launcher.name }, function () { $scope.launchers = Launcher.query(); });
        }
    };
});