<?php
//GET function getStuff(customer_key TOKEN, id_dataset UUID) 
//: KEYLOCK(tbl_customers, customer_key, tbl_datasets, id_dataset)
include 'validation.php';
include 'database.php';

checkGet();
checkArguments($_GET, array(array('customer_key', 'required'), array('id_dataset', 'required')));
$customer_key = $_GET['customer_key'];
$id_dataset = $_GET['id_dataset'];

//This is where the SUPERFUNCTIONS go
$table_key = 'customers';
$table_lock = 'datasets';

echo($table_key);
echo($table_lock);
echo($customer_key);
echo($id_dataset);

$stm = $pdo->prepare('SELECT * FROM ' . $table_lock . ' WHERE 
id = :id_row AND
id_lock = (SELECT id FROM ' . $table_key . ' WHERE access_token = :access_token)');
$success = $stm->execute(
    array( 
        'access_token' => $customer_key,
        'id_row' => $id_dataset)
    );

if(!$success){

}

$row = $stm->fetchAll()[0];
//Your code here
var_dump($row);?>