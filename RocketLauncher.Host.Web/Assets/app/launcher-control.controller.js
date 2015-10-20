angular.module("launcher").controller("LauncherControl", function ($scope, $routeParams, $http, $resource) {

    $scope.launcherName = $routeParams.launcherName;
    var Sequence = $resource('api/launcher/:launcherName/sequence/:id', { launcherName: $scope.launcherName });
    $scope.savedSequences = Sequence.query();
    
    $scope.onKeyDown = function (event) {
        var command = '';
        switch (event.keyCode) {
            case 38:
                command = 'up';
                break;
            case 37:
                command = 'left';
                break;
            case 39:
                command = 'right';
                break;
            case 40:
                command = 'down';
                break;
            case 32:
                command = 'fire';
                break;
        } 

        $scope.sendCommand(command);
    };
     
    $scope.onKeyUp = function (event) {
        $scope.sendCommand('stop');
    };

    $scope.sendCommand = function (key) {
        if (key.length > 0) {
            $http.get('api/command/' + $scope.launcherName + '?key=' + key); 
        }
    };

    $scope.sendSequence = function (sequences) {
        $http.post('api/command/' + $scope.launcherName, sequences);
    };

    $scope.saveSequence = function (sequences) {
        var name = prompt("Name for the sequence?");
        if (name.length) {
            var sequence = { launcherName: $scope.launcherName, name: name, sequences: angular.copy(sequences) };
            Sequence.save(sequence, function () { $scope.savedSequences = Sequence.query(); });
        }
    };

    $scope.removeSequence = function (sequences) {
        if (confirm("Are you sure you want to this sequence: " + sequences.name + "?")) {
            Sequence.delete({ id: sequences.name }, function () { $scope.savedSequences = Sequence.query(); });
        }
    };
});