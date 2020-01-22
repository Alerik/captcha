<?php
include 'database.php';
include 'json.php';

validateData($data, array(name, prompt, description, id));  
$name =  $data['name'];
$prompt =  $data['prompt'];
$description =  $data['description'];
$id =  $data['id'];
        
$stm = $pdo->prepare('SELECT completion, created, reviewed, query_count, completion, created, reviewed, query_count FROM datasets');
$stm->execute(array());
$entries = $stm->fetchAll(PDO::FETCH_ASSOC);
$jentries = json_encode($entries);

retData('{"entries":' . $jentries  .'}');
?>