<?php
$connection = mysqli_connect("localhost", "root", "root", "animal_crossing_buddy");

if (mysqli_connect_errno()) {
    echo "[1] : Connection Failed.";
    exit();
}

$query = "SELECT * FROM `fish`;";

$columnQuery = mysqli_query($connection, $query) or die("[2] : QUERY failed.");

// if (mysqli_num_rows($usernameCheck) != 1) {
//     echo "[4] : Username doesn't exist.";
//     exit();
// }

echo "0\t";
// foreach ($fishQuery as $row)
// {
//     array_push($a, $row);
// }

// print_r($a);

while ($row = mysqli_fetch_assoc($columnQuery)) {
    echo json_encode($row) . "\t";
}

mysqli_free_result($columnQuery);
mysqli_close($connection);