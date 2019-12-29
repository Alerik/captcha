<?php
include 'database.php';
include 'json.php';

validateData($data, array('title', 'prompt', 'type', 'id_owner'));
$title = $data['title'];
$prompt = $data['prompt'];
$type = $data['type'];
$id_owner = $data['id_owner'];

$stm = $pdo->prepare('SELECT gen_random_uuid()');
$stm->execute();
$id_dataset = $stm->fetch()['gen_random_uuid'];

$stm = $pdo->prepare('INSERT INTO datasets (id, name, id_owner, prompt, settype, requestedaccuracy) VALUES
(:id, :name, :id_owner, :prompt, :settype, 0.8)');
$stm->execute(array(
    'id' => $id_dataset,
    'id_owner' => $id_owner,
    'name' => $title,
    'prompt' => $prompt,
    'settype' => $type
));

retData('"'.$id_dataset.'"');
?>