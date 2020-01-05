<?php
include 'database.php';
include 'json.php';

validateData($data, 
array('id_customer', 'id_dataset'));
$id_customer = $data['id_customer'];
$id_dataset = $data['id_dataset'];

$stm = $pdo->prepare('SELECT id, name, prompt, settype, requestedAccuracy, entry_count, annotations_total,
completion, reviewed, approved, created, description FROM datasets 
WHERE id_owner = :id AND id = :dataset');
$row = $stm->execute(array('id' => $id_customer, 'dataset' => $id_dataset));
$row = $stm->fetch();
retData(json_encode($row));
?>