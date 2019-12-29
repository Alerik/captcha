<?php
include 'database.php';

$IDUSER = 'id_user';
$IDENTRY = 'id_entry';
$START = 'start';
$END = 'end';
$CATEGORY = 'category';

if(!empty($_POST)){
    if(isset($_POST[$IDUSER]) && isset($_POST[$IDENTRY])
    && isset($_POST[$START]) && isset($_POST[$END])){
        $id_user = $_POST[$IDUSER];
        $id_entry = $_POST[$IDENTRY];
        $start = $_POST[$START];
        $end = $_POST[$END];
        $category = isset($_POST[$CATEGORY]) ? $_POST[$CATEGORY] : 0;
        
        $insert = $pdo->prepare('INSERT INTO text_index_annotations
         (id_entry, id_user, index_start, index_end, category) 
         VALUES (:id_entry, :id_user, :index_start, :index_end, :category)');
         if(!$insert){
             echo('Problem preparing');
         }
         else{
             echo("prepared");
         }
        $result = $insert->execute(array(
            'id_entry' => $id_entry, 
            'id_user' => $id_user,
            'index_start' => $start,
            'index_end' => $end,
            'category' => $category));
        
        if(!$result){
            echo('ERROR: ' . $insert->errorCode());
            var_dump($insert->errorInfo());
        }
        
    }
    else{
        echo('Not all things set');
    }
}
else{
    echo('No post');
}
?>