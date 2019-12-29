<?php
include 'database.php';
include 'json.php';

validateData($data, array('id_customer'));
$id_customer = $data['id_customer'];

//$stm = $pdo->prepare('SELECT gen_random_uuid()')

$stm = $pdo->prepare('SELECT id, name, prompt, settype, requestedaccuracy, entry_count, completion FROM datasets WHERE id_owner = :id');
$stm->execute(array('id' => $id_customer));
$datasets = $stm->fetchAll(PDO::FETCH_ASSOC);

$json = json_encode($datasets);
retData($json);
?>