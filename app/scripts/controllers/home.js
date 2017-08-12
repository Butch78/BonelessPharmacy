'use strict';
var angular = require('angular');
/**
 * @ngdoc function
 * @name bonelessPharmacyApp.controller:HomeCtrl
 * @description
 * # HomeCtrl
 * Controller of the bonelessPharmacyApp
 */
module.exports = angular.module('bonelessPharmacyApp')
  .controller('HomeCtrl', function () {
    this.awesomeThings = [
      'HTML5 Boilerplate',
      'AngularJS',
      'Karma'
    ];
  });
