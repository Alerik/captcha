<?php
include 'database.php';
include 'json.php';
$id_customer = $_POST['id_customer'];
$id_dataset = $_POST['id_dataset'];
$path = $_FILES['entry_file']['tmp_name'];

$lines = file($path, FILE_IGNORE_NEW_LINES);

function placeholders($text, $count=0, $separator=","){
    $result = array();
    if($count > 0){
        for($x=0; $x<$count; $x++){
            $result[] = $text;
        }
    }

    return implode($separator, $result);
}

$datafields = array('id_dataset', 'innertext');

$pdo->beginTransaction(); // also helps speed up your inserts.
$insert_values = array();
foreach($lines as $d){
    $question_marks[] = '('  . placeholders('?', 2) . ')';
    $insert_values = array_merge($insert_values, array($id_dataset, trim($d)));
}

$sql = "INSERT INTO text_index_entries (" . implode(",", $datafields ) . ") VALUES " .
       implode(',', $question_marks);

$stmt = $pdo->prepare ($sql);
try {
    if(!$stmt->execute($insert_values))
        echo($pdo->errorInfo());
} catch (PDOException $e){
    echo($e->getMessage());
}
$pdo->commit();
$stm = $pdo->prepare('SELECT id_dataset, consensus_start, consensus_end, certified, 
querry_total, accuracy, innertext, id, complete FROM text_index_entries 
WHERE id_dataset = :id ORDER BY RANDOM() LIMIT :cnt');

//Check if the user is authorized
//Unsafe :)
$count = 10;
$stm->execute(array('id' => $id_dataset, 'cnt' => $count));
$entries = $stm->fetchAll(PDO::FETCH_ASSOC);
$jentries = json_encode($entries);

header('Content-Type: application/json');
retData(json_encode(array('entry_count' => count($lines), 'entries' => $jentries)));
?>