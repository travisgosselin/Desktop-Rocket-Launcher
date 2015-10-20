angular.module("launcher").controller("LauncherSettings", function ($scope, $routeParams, $http, $resource) {
    $scope.results = '';
    $scope.createSubscription = function(userId, tfsUrl) {
        $http.post('api/TfsSubscription?userId=' + userId + '&tfsUrl=' + tfsUrl).then(function(success) {
            $scope.results = success.data == 'true' ? 'URL was successfully added' : 'Event already existed, no changes made.';
        }, function() {
            $scope.results = 'A problem occured creating the subscription.';
        });
    };
});