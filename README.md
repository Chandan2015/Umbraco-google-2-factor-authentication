# Umbraco-google-2-factor-authentication
Umbraco -Implement google 2 factor authentication api


A Sample application ... Generating QR Code  on the login and validate the QR before redirecting the user to Dashboard . 

Google.Authenticator -- Generate the QR and Code 

GoogleAPI ---API to generate and validte the QR with Code

In Umbraco -- Change the $scope.loginSubmit    Umbraco\Js\umbraco.controllers.js   and Umbraco\Js\umbraco.services.js  to call the api 
