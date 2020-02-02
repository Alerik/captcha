<?php
define('REQUIRED', 'required');
define('OPTIONAL', 'optional');

//Check if request method is get, otherwise exit immediatelly
function checkGet(){
    if($_SERVER['REQUEST_METHOD'] === 'GET'){
        return true;
    }
    else{
        http_response_code(405);
        exit('Expected GET');
    }
}

//Check if request method is get, otherwise exit immediatelly
function checkPost(){
    if($_SERVER['REQUEST_METHOD'] === 'POST'){
        return true;
    }
    else{
        http_response_code(405);
        exit('Expected POST');
    }
}

function checkArguments($toCheck, $args){
    $valid = true;
    foreach($args as $arg){
        if($arg[1] === REQUIRED){
            if(!array_key_exists($arg[0], $toCheck)){
                print('Expected arg ' . $arg[0]);
                $valid = false;
            }
        }
        else if($arg[1] === OPTIONAL){

        }
    }

    if(!$valid){
        http_response_code(400);
        exit('Missing arguments');
    }
    return true;
}
?>