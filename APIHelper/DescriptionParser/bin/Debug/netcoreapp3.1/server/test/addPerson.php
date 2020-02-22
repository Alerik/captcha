<?php
//Function addPerson
include 'validation.php';
checkPost();
checkArguments($_POST, array(array('first_name', 'optional'),array('last_name', 'optional')));
$first_name = $_POST['first_name'];
$last_name = $_POST['last_name'];
//This is where the SUPERFUNCTIONS go
//Your code here

?>