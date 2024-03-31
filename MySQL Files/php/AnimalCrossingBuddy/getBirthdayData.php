<?php
$connection = mysqli_connect("localhost", "root", "root", "animal_crossing_buddy");

if (mysqli_connect_errno()) {
    echo "[1] : Connection Failed.";
    exit();
}


$birthday = $_POST["birthday"];

$query = "SELECT * FROM villagers WHERE Birthday = \"$birthday\";";

$columnQuery = mysqli_query($connection, $query) or die("[2] : QUERY failed.");

echo "0\t";

while ($row = mysqli_fetch_assoc($columnQuery)) {
    echo json_encode($row) . "\t";
}

mysqli_free_result($columnQuery);
mysqli_close($connection);

?>