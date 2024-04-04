<?php 
	$connection = mysqli_connect ("localhost", "root", "root", "animal_crossing_buddy");

	if (mysqli_connect_errno()) {
		echo "[1] : Connection Failed.";
		exit();
	}

	$username = $_POST["username"];
	$name = $_POST["name"];
	$toggle = $_POST["toggle"];

	$getTypeQuery = "SELECT * FROM `main_database` WHERE Name = '" . $name . "';";

	$typeQuery = mysqli_query($connection, $getTypeQuery) or die("[2] : SEARCH TYPE QUERY failed.");
	$retrievedType = mysqli_fetch_assoc($typeQuery);
	$type = $retrievedType["Type"];

	$table = "";
	$tableType = 0;
	if (!strcmp($type, "Villagers")) {
		$table = "collected_villagers";
		$tableType = 1;
	}
	else if (!strcmp($type, "Fish") || !strcmp($type, "Insects") || !strcmp($type, "Sea Creatures")) {
		$table = "caught_critters";
		$tableType = 2;
	}

	if ($table == "") {
		echo "[3] : No Table Name Determined. " . $type;
		exit();
	}

	if ($toggle) {
		if ($tableType == 1) {
			$insertIntoUserStatsQuery = "INSERT INTO `" . $table . "` (Username, Name) VALUES ('" . $username . "', '" . $name . "');";
		}
		else if ($tableType == 2) {
			$insertIntoUserStatsQuery = "INSERT INTO `" . $table . "` (Username, Name, Type) VALUES ('" . $username . "', '" . $name . "', '" . $type . "');";
		}

		mysqli_query($connection, $insertIntoUserStatsQuery) or die("[4] : INSERT QUERY " . $insertIntoUserStatsQuery . " failed.");
	}
	else {
		$removeFromUserStatsQuery = "DELETE FROM `" . $table . "` WHERE Name = '" . $name . "' AND Username = '" . $username . "';";
		mysqli_query($connection, $removeFromUserStatsQuery) or die("[4] : REMOVE QUERY failed.");
	}

	echo "0";
?>