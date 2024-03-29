<?php
$connection = mysqli_connect("localhost", "root", "root", "animal_crossing_buddy");

if (mysqli_connect_errno()) {
    echo "[1] : Connection Failed.";
    exit();
}

$id = $_POST["id"];
$table = $_POST["table"];

$query = "SELECT * FROM `" . $table . "` WHERE Id = ". $id . ";";

$columnQuery = mysqli_query($connection, $query) or die("[2] : QUERY failed.");

echo "0 \t";

while ($row = mysqli_fetch_assoc($columnQuery)) {
    echo json_encode($row) . "\t";
}

mysqli_free_result($columnQuery);
mysqli_close($connection);