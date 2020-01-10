<?php
include 'database.php';
include 'json.php';

validateData($data, array('id_customer', 'id_dataset', 'count'));  
$id_customer = $data['id_customer'];
$id_dataset = $data['id_dataset'];
$count = $data['count'];
        
$stm = $pdo->prepare('SELECT id_dataset, consensus_start, consensus_end, certified, 
querry_total, accuracy, innertext, id, complete FROM text_index_entries 
WHERE id_dataset = :id ORDER BY RANDOM() LIMIT :cnt');

//Check if the user is authorized
//Unsafe :)
$stm->execute(array('id' => $id_dataset, 'cnt' => $count));
$entries = $stm->fetchAll(PDO::FETCH_ASSOC);
$jentries = json_encode($entries);

retData('{"entries":' . $jentries .'}');
?>