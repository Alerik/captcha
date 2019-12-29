<?php
include 'database.php';

$sth = $pdo->prepare('SELECT * from customers');
$sth->execute();
$res = $sth->fetch();
$firstname = $res['firstname'];
$lastname = $res['lastname'];
$userid = $res['id'];
?>

<head>
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.7.1.min.js"></script>
    <script>
        function sendEntry(setid){
            let row = $("#newEntry");
            $.post("postRow.php", 
            {id_user: "<?php echo($userid)?>",
            text_index_entry: row.val(),
            id_dataset: setid.replace('3', '4')
            })
            .done(function(data){alert(data);});
        } 
    </script>
</head>
<body>
<h1>Hello <?php print($firstname . ' ' .  $lastname . ' ' . $userid)?></h1>
<h2>Your datasets</h2>
<?php
$result = $pdo->query('SELECT * FROM datasets WHERE id_owner = \''. $userid.'\';');
$datasetid;
var_dump($result);
foreach($result as $row){
    print('<h3>'. $row['name']. ' ('. $row['prompt'].')</h3>');
    $datasetid = $row['id'];
}
?>
<input id="newEntry" type="text"/>
<button id="addData" onClick="sendEntry('<?php echo($datasetid)?>')">Add Entry</button>
</body>