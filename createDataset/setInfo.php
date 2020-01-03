<?php
include '../database.php';
include '../json.php';

validateData($data,
array('id_customer', 'id_creation', 'title', 'prompt', 'description'));

$id_customer = $data['id_customer'];
$id_creation = $data['id_creation'];
$title = $data['title'];
$prompt = $data['prompt'];
$description = $data['description'];

$stm = $pdo->prepare('UPDATE datasets 
SET name = :title, prompt = :prompt, description = :description
 WHERE id =  :id_dataset');
$stm->execute(
    array(
        'title'=> $title, 
        'prompt' => $prompt,
        'id_dataset' => $id_creation,
        'description' => $description));
print_r($pdo->errorInfo());
?>