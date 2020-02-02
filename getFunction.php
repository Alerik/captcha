<?php
include 'validation.php';
checkGet();
checkArguments($_GET, array(array('age', 'required'), array('name', 'optional')));
$age = $_GET["age"];
if(key_exists("name",$_GET))
    $name = $_GET["name"];


echo($name . " is " . $age . " years old");
?>