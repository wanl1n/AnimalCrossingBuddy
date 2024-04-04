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

$currentMonth = $_POST["currentMonth"];

$query = "SELECT * FROM `" . $table . "` AS t WHERE `" .$currentMonth . "` != \"NA\"" . "AND 
          (SELECT COUNT(*) FROM `main_database` WHERE Name = t.Name AND Type =  \"" . $type . "\") != 0;";

$columnQuery = mysqli_query($connection, $query) or die("[2] : QUERY failed.");

echo "0 \t";

while ($row = mysqli_fetch_assoc($columnQuery)) {
    echo json_encode($row) . "\t";
}

mysqli_free_result($columnQuery);
mysqli_close($connection);