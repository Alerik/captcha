<?php
include 'database.php';
session_start();

if(isset($_SESSION['id_user'])){
    header('Location: portal.php');
    exit;
}

if($_SERVER['REQUEST_METHOD'] == 'POST'){
    $email = $_POST['email'];
    $password = $_POST['password'];

    $stm = $pdo->prepare('SELECT id, passwordhash FROM customers WHERE email = :email');
    $stm->execute(array('email' => $email));
    $customer = $stm->fetch();
    $uuid = $customer['id'];
    $hash = $customer['passwordhash'];
    
    if(password_verify($password, $hash)){
        session_start();
        $_SESSION['id_user'] =  $uuid;
        header('Location: portal.php');
        exit;
    }
    else{
        echo('<h2>Invalid password</h2>');
    }
}
?>
<head>

</head>
<body>
    <form action="" method="POST">
        <input type="text" name="email"/>
        <input type="password" name="password"/>
        <input type="submit" value="Login"/>
    </form>
</body>