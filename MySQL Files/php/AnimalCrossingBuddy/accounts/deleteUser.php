<?php
$connection = mysqli_connect("localhost", "root", "root", "animal_crossing_buddy");

if (mysqli_connect_errno()) {
    echo "[1] : Connection Failed.";
    exit();
}

$username = $_POST["username"];

$query1 = "DELETE FROM `users` WHERE Username = '" . $username . "';";
$delete1Query = mysqli_query($connection, $query1) or die("[2] : DELETE QUERY failed.");

echo "0 \t";

mysqli_free_result($createQuery);
mysqli_close($connection);