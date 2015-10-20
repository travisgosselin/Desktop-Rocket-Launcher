var app = angular.module("launcher", ['ngRoute', 'ngResource']);
app.config(['$routeProvider',
  function ($routeProvider) {
      $routeProvider
        .when('/', {
            templateUrl: 'Assets/app/launcher-listing.template.html',
            controller: 'LauncherListing'
        })
        .when('/settings', {
            templateUrl: 'Assets/app/launcher-settings.template.html',
            controller: 'LauncherSettings'
        })
        .when('/:launcherName', {
            templateUrl: 'Assets/app/launcher-control.template.html',
            controller: 'LauncherControl'
        })
        .otherwise({
            redirectTo: '/launcher'
        });
  }]);