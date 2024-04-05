<?php
$connection = mysqli_connect("localhost", "root", "root", "animal_crossing_buddy");

if (mysqli_connect_errno()) {
    echo "[1] : Connection Failed.";
    exit();
}

$table = $_POST["table"];

$type = $table;
if ($table == "sea_creatures")
    $type = "Sea Creatures";


$query = "INSERT INTO `main_database` (Name, Type) 
SELECT Name, \"" . $type . "\" FROM `" . $table . "` AS t WHERE NOT EXISTS 
(SELECT 1 FROM `main_database` WHERE Name = t.name AND Type = \"" . $type . "\");";

echo $query;

$insertQuery = mysqli_query($connection, $query) or die("[2] : INSERT QUERY failed.");

echo "0 \t";


mysqli_free_result($createQuery);
mysqli_close($connection);