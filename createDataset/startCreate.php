<?php
include '../database.php';
include '../json.php';

validateData($data,
array('id_customer'));
$id_customer = $data['id_customer'];

$stm = $pdo->prepare('SELECT gen_random_uuid()');
$stm->execute();
$uuid = $stm->fetch()['gen_random_uuid'];

$stm = $pdo->prepare('INSERT INTO datasets (id, id_owner) VALUES (:id, :id_owner)');
$stm->execute(array('id' => $uuid, 'id_owner' => $id_customer));

retData(json_encode(array('creation_id' => $uuid)));
?>