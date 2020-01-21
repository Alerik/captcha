<?php
include 'database.php';
include 'json.php';

validateData($data, array(--ARG_IDENTIFIERS--));  
--ARG_DEFINITIONS--
//$arg1 = $data['arg1_id'];
        
$stm = $pdo->prepare('SELECT --QUERY_COLUMNS-- FROM --TABLE_NAME--');

//Check if the user is authorized
//Unsafe :)
$stm->execute(array());
$entries = $stm->fetchAll(PDO::FETCH_ASSOC);
$jentries = json_encode($entries);

retData('{"entries":' . $jentries  .'}');
?>