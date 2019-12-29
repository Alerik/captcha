<?php
include 'database.php';

//This will take in the users' UUID
$IDUSER = 'id_user';
//Their dataset's uuid
$IDDATASET = 'id_dataset';
//And the row they'd like to add
$TEXTINDEXENTRY = 'text_index_entry';

function isValidtext_index_entry($str){

}

if(!empty($_POST)){
    if(isset($_POST[$IDUSER]) && isset($_POST[$IDDATASET])){
        $id_user = $_POST[$IDUSER];
        $id_dataset = $_POST[$IDDATASET];

        //Check if the user own's the dataset
        $check = $pdo->prepare('SELECT id_owner FROM datasets WHERE id = :id');
        $check->execute(array('id' => $id_dataset));
        $row_check = $check->fetch();
        
        if($row_check){
            //The user owns this table
            if($row_check['id_owner'] == $id_user){
                //Check if it's a text_index_entry
                if(isset($_POST[$TEXTINDEXENTRY]) 
                && !ctype_space($_POST[$TEXTINDEXENTRY])){
                    $text_index_entry = $_POST[$TEXTINDEXENTRY];
        
                    $insert = $pdo->prepare('INSERT INTO text_index_entries (id_dataset, innertext) VALUES (:id, :innertext)');
                    $insert->execute(array('id' => $id_dataset, 'innertext' => $text_index_entry));
                }
                //Check if another type of entry
                else{
                    echo('Invalid entry. Currently, we only support text_index_entries');
                }
            }
            else{
                echo('This user does not own this dataset');
            }  
        }
        else{
            echo('No such dataset');
        }
    }
}
?>