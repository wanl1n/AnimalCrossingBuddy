<?php
$connection = mysqli_connect("localhost", "root", "root", "animal_crossing_buddy");

if (mysqli_connect_errno()) {
    echo "[1] : Connection Failed.";
    exit();
}

$name = $_POST["name"];

$type = $_POST["type"];

$query = "DELETE FROM `main_database` WHERE Name = '" . $name ."' AND Type = \"$type\";";

$deleteQuery = mysqli_query($connection, $query) or die("[2] : DELETE QUERY failed.");

echo "0 \t";

echo $query;

mysqli_free_result($createQuery);
mysqli_close($connection);