<?php 
	$connection = mysqli_connect ("localhost", "root", "root", "animal_crossing_buddy");

	if (mysqli_connect_errno()) {
		echo "[1] : Connection Failed.";
		exit();
	}

	$username = $_POST["username"];
	$name = $_POST["name"];
	$type = $_POST["type"];

	$table = "";
	if (!strcmp($type, "Villagers")) {
		$table = "collected_villagers";
	}
	else if (!strcmp($type, "Fish") || !strcmp($type, "Insects") || !strcmp($type, "Sea Creatures")) {
		$table = "caught_critters";
	}

	if ($table == "") {
		echo "[3] : No Table Name Determined. " . $type;
		exit();
	}

	$dupeCheckQuery = "SELECT * FROM `" . $table . "` WHERE Name = '" . $name . "' AND Username = '" . $username . "';";
	$dupeChecker = mysqli_query($connection, $dupeCheckQuery) or die("[3] : DUPE SELECT QUERY failed.");
	
	if (mysqli_num_rows($dupeChecker) > 0) {
        echo "1";
        exit();
    }

	echo "0";
?>