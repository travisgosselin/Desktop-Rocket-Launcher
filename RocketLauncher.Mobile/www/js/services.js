angular.module('starter.services', [])

.factory('Launchers', function($http) {
  return {
    getAll: function() {
        return $http.get('http://rocketlauncher.azurewebsites.net/api/launcher');
    }
  };
});
