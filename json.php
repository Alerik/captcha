<?php
$data = json_decode(file_get_contents('php://input'), true);

function error($message){
    echo('{"error": "'. $message . '"}');
    exit();
}

function retData($json, $hardexit = true){
    echo('{"data": ' . $json . '}');

    if($hardexit)
        exit();
}


function validateData($data, $params, $hardexit = true){
    foreach($params as $param) {
        if(!array_key_exists($param, $data))
            if($hardexit)
                error('Missing param ' . $param);
            else
                return false;
        }
    return true;
}
?>