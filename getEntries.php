<?php
include 'database.php';
include 'json.php';

validateData($data, array('id_customer', 'id_dataset', 'start', 'count'));  
$id_customer = $data['id_customer'];
$id_dataset = $data['id_dataset'];
$start = $data['start'];
$count = $data['count'];
        
$stm = $pdo->prepare('SELECT id_dataset, consensus_start, consensus_end, certified, 
querry_total, accuracy, innertext, id, complete FROM text_index_entries 
WHERE id_dataset = :id ORDER BY querry_total DESC LIMIT :cnt OFFSET :str');

//Check if the user is authorized
//Unsafe :)
$stm->execute(array('id' => $id_dataset, 'cnt' => $count, 'str' => $start));
$entries = $stm->fetchAll(PDO::FETCH_ASSOC);
$jentries = json_encode($entries);

$stm = $pdo->prepare('SELECT a.innertext, index_start, index_end, id_entry FROM text_index_annotations 
INNER JOIN (SELECT id, innertext FROM text_index_entries ORDER BY querry_total DESC LIMIT :cnt OFFSET :str) AS a ON id_entry = id');

$stm->execute(array('cnt' => $count, 'str' => $start));
$annotations = $stm->fetchAll(PDO::FETCH_ASSOC);
$jannotations = json_encode($annotations);

retData('{"entries":' . $jentries . ', "annotations":' . $jannotations .'}');
?>