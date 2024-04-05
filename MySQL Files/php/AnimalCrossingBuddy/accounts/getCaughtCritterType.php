<?php 
	$connection = mysqli_connect ("localhost", "root", "root", "animal_crossing_buddy");

	if (mysqli_connect_errno()) {
		echo "[1] : Connection Failed.";
		exit();
	}

	$name = $_POST["name"];
	trim($name);

	$getTypeQuery = "SELECT Type FROM `caught_critters` WHERE Name = '" . $name . "' LIMIT 1;";

	echo $getTypeQuery;

	$typeQuery = mysqli_query($connection, $getTypeQuery) or die("[2] : SEARCH TYPE QUERY failed.");
	
	if ($row = mysqli_fetch_assoc($typeQuery)) {
        // Use the fetched value in the echo statement
        echo "0\t" . $row['Type'];
    } 

	?>