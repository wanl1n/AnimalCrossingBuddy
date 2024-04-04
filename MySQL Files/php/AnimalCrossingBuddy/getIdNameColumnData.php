<?php
$connection = mysqli_connect("localhost", "root", "root", "animal_crossing_buddy");

if (mysqli_connect_errno()) {
    echo "[1] : Connection Failed.";
    exit();
}


$column = $_POST["column"];
$table = $_POST["table"];

$query = "SELECT Id, Name, `" . $column . "` FROM `" . $table . "` AS t WHERE 
        (SELECT COUNT(*) FROM `main_database` WHERE Name = t.Name) != 0;";

$columnQuery = mysqli_query($connection, $query) or die("[2] : QUERY failed.");

echo "0\t";

while ($row = mysqli_fetch_assoc($columnQuery)) {
    $data = array(
        'Id' => $row["Id"],
        'Name' => $row['Name'],
        'Data' => $row[$column]);
    echo json_encode($data, JSON_FORCE_OBJECT) . "\t";
}

mysqli_free_result($columnQuery);
mysqli_close($connection);

?>