<?php
include 'database.php';
$uuid = gen_uuid();
$stm = $pdo->prepare('INSERT INTO users (id) VALUES (:id)');
$stm->execute(array('id' => $uuid));
echo(json_encode(array('id_user' => $uuid)));
?>