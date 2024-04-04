<?php
$connection = mysqli_connect("localhost", "root", "root", "animal_crossing_buddy");

if (mysqli_connect_errno()) {
    echo "[1] : Connection Failed.";
    exit();
}


$birthday = $_POST["birthday"];

$query = "SELECT * FROM villagers AS v WHERE Birthday = \"$birthday\" AND 
        (SELECT COUNT(*) FROM `main_database` WHERE Name = v.Name AND Type = \"Villagers\") != 0;";

$columnQuery = mysqli_query($connection, $query) or die("[2] : QUERY failed.");

echo "0\t";

while ($row = mysqli_fetch_assoc($columnQuery)) {
    echo json_encode($row) . "\t";
}

mysqli_free_result($columnQuery);
mysqli_close($connection);

?>