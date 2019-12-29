<?php
include 'database.php';

if($_SERVER['REQUEST_METHOD'] == 'POST'){
    $email = $_POST['email'];
    $password = $_POST['password'];
    $firstname = $_POST['firstname'];
    $lastname = $_POST['lastname'];
    $stm = $pdo->prepare('SELECT gen_random_uuid()');
    $stm->execute();
    $uuid = $stm->fetch()['gen_random_uuid'];
    $stm = $pdo->prepare('INSERT INTO customers (id, firstname, lastname, email, passwordhash)
    VALUES (:id, :firstname, :lastname, :email, :passwordhash)');
    $stm->execute(array(
        'id' => $uuid,
        'firstname' => $firstname,
        'lastname' => $lastname,
        'email'=> $email,
        'passwordhash' => password_hash($password, PASSWORD_DEFAULT)
    ));
    session_start();
    $_SESSION['id_user'] = $uuid;
    header('Location: portal.php');
    exit;
}
?>

<head>

</head>
<body>
    <h1>Register</h1>
    <form action="" method="POST">
        <h2>First Name</h2>
        <input type="text" name="firstname"/>
        <h2>Last Name</h2>
        <input type="text" name="lastname"/>
        <h2>Email</h2>
        <input type="text" name="email"/>
        <h2>Password</h2>
        <input type="password" name="password"/>
        <h2>Re-Enter Password</h2>
        <input type="password" name="confirmpass"/>
        <input type="submit" value="Create Account">
    </form>
</body>