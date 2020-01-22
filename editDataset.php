<?php
include 'database.php';
include 'json.php';

validateData($data, array('id', 'prompt', 'description', 'name'));
$id = $data['id'];
$prompt = $data['prompt'];
$description = $data['description'];
$name = $data['name'];

$stm = $pdo->prepare('UPDATE datasets SET prompt = :prompt, description = :description, name = :name, changed = true
 WHERE id = :id');
$stm->execute(array('id' => $id, 'prompt' => $prompt, 'description' => $description, 'name'=> $name));
?>