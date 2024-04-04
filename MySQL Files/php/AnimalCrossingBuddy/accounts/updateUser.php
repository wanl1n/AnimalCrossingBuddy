<?php 
	$connection = mysqli_connect ("localhost", "root", "root", "animal_crossing_buddy");

	if (mysqli_connect_errno()) {
		echo "[1] : Connection Failed.";
		exit();
	}

	$username = $_POST["username"];
	$name = $_POST["name"];
	$toggle = $_POST["toggle"];

	$getTypeQuery = "SELECT Type FROM `main_database` WHERE Name = '" . $name . "';";

	$type = mysqli_query($connection, $getTypeQuery) or die("[2] : SEARCH TYPE QUERY failed.");

	$table = "";
	if ($type == "Villager") {
		$table = "collected_villagers";
	}
	if ($type == "Fish" || $type == "Insects" || $type == "Sea Creatures") {
		$table = "caught_critters";
	}

	if ($table == "") {
		echo "[3] : No Table Determined." . $type;
		exit();
	}

	if ($toggle) {
		$insertIntoUserStatsQuery = "INSERT INTO `" . $table . "`(Username, Name, Type) 
									 VALUES ('" . $username . "', '" . $name . "', '" . $type . "');";

		mysqli_query($connection, $insertIntoUserStatsQuery) or die("[4] : INSERT QUERY failed.");
	}
	else {
		$removeFromUserStatsQuery = "DELETE FROM `" . $table . "` 
									 WHERE Name = '" . $name . "' AND Username = '" . $username . "';";
		mysqli_query($connection, $removeFromUserStatsQuery) or die("[4] : REMOVE QUERY failed.");
	}

	echo "0";
?>