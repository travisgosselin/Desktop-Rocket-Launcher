angular.module('starter.controllers', [])

.controller('LaunchersCtrl', function ($scope, Launchers, $ionicLoading, $state) {
        $ionicLoading.show();
        Launchers.getAll().then(
            function (data) {
                $scope.launchers = data.data;
                $ionicLoading.hide();
            },
            function() {
                $ionicLoading.show({
                    template: 'Error loading launchers...',
                    duration: 2000
                });
            });

        $scope.selectLauncher = function(launcherName) {
            $state.go("launcher");
        };

    })

    .controller('LauncherDetailsCtrl', function ($scope, Launchers, $ionicLoading, $stateParams, $http, $timeout) {
        $scope.launcher = {
            name: $stateParams.launcherName
        };
        
        $scope.sendCommand = function (key) {
            if (key.length > 0) {
                $http.get('http://rocketlauncher.azurewebsites.net/api/command/' + $scope.launcher.name + '?key=' + key);
            }
        };

    })

.controller('AccountCtrl', function($scope) {
  $scope.settings = {
    enableFriends: true
  };
});
