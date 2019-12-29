<?php
include 'database.php';

$grab = $pdo->prepare('SELECT id_dataset, innertext, text_index_entries.id, prompt 
FROM text_index_entries JOIN datasets ON id_dataset = datasets.id
WHERE NOT COMPLETE ORDER BY RANDOM() LIMIT 1');
$grab->execute();
$row = $grab->fetch();

$id_dataset = $row['id_dataset'];
$innertext = $row['innertext'];
$id = $row['id'];
$prompt = $row['prompt'];
$data = array(
    //'id_dataset' => $id_dataset,
    'id_entry' => $id,
    'innertext' => $innertext,
    'prompt' => $prompt
);
echo(json_encode($data));
?>