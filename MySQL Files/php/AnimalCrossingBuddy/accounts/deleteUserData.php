<?php
$connection = mysqli_connect("localhost", "root", "root", "animal_crossing_buddy");

if (mysqli_connect_errno()) {
    echo "[1] : Connection Failed.";
    exit();
}

$username = $_POST["username"];

$query1 = "DELETE FROM `collected_villagers` WHERE Username = '" . $username . "';";
$delete1Query = mysqli_query($connection, $query1) or die("[2] : DELETE QUERY 1 failed.");

$query2 = "DELETE FROM `caught_critters` WHERE Username = '" . $username . "';";
$delete2Query = mysqli_query($connection, $query2) or die("[2] : DELETE QUERY 2 failed.");

echo "0 \t";

echo $query;

mysqli_free_result($createQuery);
mysqli_close($connection);