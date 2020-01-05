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
    $stmt->execute($insert_values);
} catch (PDOException $e){
    echo $e->getMessage();
}
$pdo->commit();
retData(json_encode(array('entry_count' => count($lines))));
?>