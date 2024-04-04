<?php
$connection = mysqli_connect("localhost", "root", "root", "animal_crossing_buddy");

if (mysqli_connect_errno()) {
    echo "[1] : Connection Failed.";
    exit();
}

$table = $_POST["table"];

$condition = $_POST["condition"];

$query = "DROP TABLE IF EXISTS `main_database`;";

$dropQuery = mysqli_query($connection, $query) or die("[2] : DROP QUERY failed.");

$query = "CREATE TABLE `main_database` (
    `Id` int(4) NOT NULL,
    `Name` varchar(50) NOT NULL,
    `Type` varchar(20) NOT NULL
  ) ENGINE=InnoDB DEFAULT CHARSET=utf8;";


$createQuery = mysqli_query($connection, $query) or die("[2] : CREATE QUERY failed.");

$query = "ALTER TABLE `main_database`
ADD PRIMARY KEY (`Id`);";

$alterQuery = mysqli_query($connection, $query) or die("[2] : ALTER QUERY failed.");

$query = "ALTER TABLE `main_database`
MODIFY `Id` int(4) NOT NULL AUTO_INCREMENT;";

$alterQuery = mysqli_query($connection, $query) or die("[2] : ALTER QUERY failed.");

$query = "INSERT INTO `main_database` (Name, Type)
SELECT name, \"Villagers\" AS Type
FROM `villagers`;";

$insertQuery = mysqli_query($connection, $query) or die("[2] : INSERT VILLAGERS QUERY failed.");

$query = "INSERT INTO `main_database` (Name, Type)
SELECT name, \"Fish\" AS Type
FROM `fish`;";

$insertQuery = mysqli_query($connection, $query) or die("[2] : INSERT FISH QUERY failed.");

$query = "INSERT INTO `main_database` (Name, Type)
SELECT name, \"Insects\" AS Type
FROM `insects`;";

$insertQuery = mysqli_query($connection, $query) or die("[2] : INSERT INSECTS QUERY failed.");

$query = "INSERT INTO `main_database` (Name, Type)
SELECT name, \"Sea Creatures\" AS Type
FROM `sea_creatures`;";

$insertQuery = mysqli_query($connection, $query) or die("[2] : INSERT SEA CREATURES QUERY failed.");

echo "0 \t";

mysqli_free_result($createQuery);
mysqli_close($connection);