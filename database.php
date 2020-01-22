<?php
$host='greenpinedev.postgres.database.azure.com';
$port=5432 ;
$dbname='captcha' ;
$user='devadmin@greenpinedev';
$password=';gJwgQJrLC{G6HS' ;

$pdo = new PDO('pgsql:host=greenpinedev.postgres.database.azure.com; dbname=captcha', $user, $password);
$pdo->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
function gen_uuid(){
    $stm = $pdo->prepare('SELECT gen_random_uuid()');
    $stm->execute();
    return $stm->fetch()['gen_random_uuid'];    
}
?>