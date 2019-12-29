<?php
include 'database.php';
session_start();
$id_user = $_SESSION['id_user'];
?>
<head>
</head>
<body>
    <h1>Welcome <?php echo($id_user)?></h1>

    <table>
        <tr>Dataset</tr>
        <tr>AnnotationType</tr>
        <tr>Entries</tr>
        <tr>Completion</tr>
        <?php
        $
        ?>
    </table>

    <form>
        <input type="submit" value="Logout"/>
    </form>
</body>