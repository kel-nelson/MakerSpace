
(function($){
    
    var ng_app = angular.module("ng_app");
    ng_app.controller("loginDemo", ["$scope", function($scope){
        $scope.user_login = { username: "", password: "" };

        $scope.set_login = function(username, password){
            $scope.user_login = { username: username, password: password };
            console.log($scope.user_login);

        }
    }]);

})(jQuery);