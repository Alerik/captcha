<?php
//Function addPerson
include 'validation.php';
checkPost();
checkArguments($_POST, array(array('first_name', 'required'),array('last_name', 'required')));
$first_name = $_POST['first_name'];
$last_name = $_POST['last_name'];
//Your code here

?>