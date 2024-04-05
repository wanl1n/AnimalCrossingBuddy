<?php
$connection = mysqli_connect("localhost", "root", "root", "animal_crossing_buddy");

if (mysqli_connect_errno()) {
    echo "[1] : Connection Failed.";
    exit();
}

$accessCode = $_POST["accessCode"];

$query = "DROP TABLE IF EXISTS `admin_access_codes`";

$deleteQuery = mysqli_query($connection, $query) or die("[2] : DELETE QUERY failed.");

$query = "CREATE TABLE IF NOT EXISTS `admin_access_codes` (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Username VARCHAR(20),
    Hash VARCHAR(100), 
    Salt VARCHAR(50))";

$createQuery = mysqli_query($connection, $query) or die("[2] : CREATE QUERY failed.");

$username = "Admin";
$salt = "\$5\$rounds=5000\$" . "any_value_here" . $username . "\$";

$hash1 = crypt("I'm an Admin, no cap", $salt);
$hash2 = crypt("zachary8888", $salt);
$hash3 = crypt("31874106", $salt);
$hash4 = crypt("jpjrr/SAsLCZNunpFnccg0hWQ9Rn4PEc4M5yGHLFEol48AJvnibdyk5vG93F/iYdJPlKCyop/4PeRlFBZB7dHw==", $salt);

$query = "INSERT IGNORE INTO `admin_access_codes` (Id, Username, Salt, Hash)
          VALUES 
          (1, \"$username\", \"$salt\", \"$hash1\"), 
          (2, \"$username\", \"$salt\", \"$hash2\"), 
          (3, \"$username\", \"$salt\", \"$hash3\"), 
          (4, \"$username\", \"$salt\", \"$hash4\");";

$insertQuery = mysqli_query($connection, $query) or die("[2] : INSERT QUERY failed.");

$hash = crypt($accessCode, $salt);
$loginQuery = "SELECT COUNT(*) AS count FROM `admin_access_codes` WHERE `Hash` = \"" . $hash . "\"";

$usernameCheck = mysqli_query($connection, $loginQuery) or die ("[2] : LOGIN QUERY Failed");

$row = mysqli_fetch_assoc($usernameCheck);

$count = $row['count'];

if ($count != 0)
    echo "0";
else 
    echo "1";

mysqli_free_result($columnQuery);
mysqli_close($connection);