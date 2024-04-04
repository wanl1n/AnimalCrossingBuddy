<?php
$connection = mysqli_connect("localhost", "root", "root", "animal_crossing_buddy");

if (mysqli_connect_errno()) {
    echo "[1] : Connection Failed.";
    exit();
}

$accessCode = $_POST["accessCode"];

$query = "CREATE TABLE IF NOT EXISTS `admin_access_codes` (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    `Access Code` VARCHAR(20) UNIQUE )";

$createQuery = mysqli_query($connection, $query) or die("[2] : CREATE QUERY failed.");

$query = "INSERT IGNORE INTO `admin_access_codes` (Id, `Access Code`)
          VALUES (NULL, 'zachary8888');";

$insertQuery = mysqli_query($connection, $query) or die("[2] : INSERT QUERY failed.");

$loginQuery = "SELECT COUNT(*) AS count FROM `admin_access_codes` WHERE `Access Code` = \"" . $accessCode . "\"";

$usernameCheck = mysqli_query($connection, $loginQuery) or die ("[2] : LOGIN QUERY Failed");

$row = mysqli_fetch_assoc($usernameCheck);

$count = $row['count'];

if ($count != 0)
    echo "0";
else 
    echo "1";

mysqli_free_result($columnQuery);
mysqli_close($connection);