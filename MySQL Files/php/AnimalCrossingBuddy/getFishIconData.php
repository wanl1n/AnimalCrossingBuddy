<?php 
$connection = mysqli_connect("localhost", "root", "root", "animal_crossing_buddy");

if (mysqli_connect_errno()) {
    echo "[1] : Connection Failed";
    exit();
}

$table = $_POST["table"];
$id = $_POST["iconID"];

$query = "SELECT * FROM `" .$table. "` WHERE Id = $id;";

$dataQuery = mysqli_query($connection, $query) 
or die("[2] : QUERY failed.");

$retrievedIcon = mysqli_fetch_array($dataQuery);
$name = $retrievedIcon["Name"];
$link = $retrievedIcon["Icon Image Link"];
$sellPrice = $retrievedIcon["Sell Price"];
$location = $retrievedIcon["Location"];
$shadowSize = $retrievedIcon["Shadow Size"];
$catchDifficulty = $retrievedIcon["Catch Difficulty"];
$totalCatches = $retrievedIcon["Total Catches"];


$numColumns = mysqli_num_fields($dataQuery);
for ($i = 9; $i < $numColumns - 1; $i++) 
{
    if ($retrievedIcon[$i] != "NA")
    {
        $time = $retrievedIcon[$i];
        break;
    }
}

echo "0\t" . $name . "\t" . $link . "\t<b>Sell Price: </b>" . $sellPrice . "<b>\tLocation: </b>" . $location . 
"<b>\tShadow Size: </b>" . $shadowSize . "<b>\tCatch Difficulty: </b>" . $catchDifficulty . 
"<b>\tTotal Catches: </b>" . $totalCatches . "<b>\tTime: </b>" . $time;

?>