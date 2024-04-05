<?php 
	$connection = mysqli_connect ("localhost", "root", "root", "animal_crossing_buddy");

	if (mysqli_connect_errno()) {
		echo "[1] : Connection Failed.";
		exit();
	}

	$username = $_POST["username"];
	$name = $_POST["name"];
	$iconLink = $_POST["iconLink"];
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
		$dupeCheckQuery = "SELECT * FROM `" . $table . "` WHERE Name = '" . $name . "' AND Username = '" . $username . "';";
		$dupeChecker = mysqli_query($connection, $dupeCheckQuery) or die("[3] : DUPE SELECT QUERY failed.");
	
		if ($name == "Anchovy") {
			if (str_contains($iconLink, "Fish")) {
				$table = "caught_critters";
				$tableType = 2;
				$type = "Fish";
			}
	
			if (mysqli_num_rows($dupeChecker) > 1) {
				echo "[3] : Entry already exists.";
				exit();
			}
		}
		else {
			if (mysqli_num_rows($dupeChecker) > 0) {
				echo "[3] : Entry already exists.";
				exit();
			}
		}

		if ($tableType == 1) {
			$insertIntoUserStatsQuery = "INSERT INTO `" . $table . "` (`Icon Image Link`, Username, Name) VALUES ('" . $iconLink . "', '" . $username . "', '" . $name . "');";
		}
		else if ($tableType == 2) {
			$insertIntoUserStatsQuery = "INSERT INTO `" . $table . "` (`Icon Image Link`, Username, Name, Type) VALUES ('" . $iconLink . "', '" . $username . "', '" . $name . "', '" . $type . "');";
		}

		mysqli_query($connection, $insertIntoUserStatsQuery) or die("[4] : INSERT QUERY " . $insertIntoUserStatsQuery . " failed.");
	}
	else {
		if ($name == "Anchovy") {
			if (str_contains($iconLink, "Fish")) {
				$table = "caught_critters";
			}

		}
		else {
			$table = "collected_villagers";
		}
		
		$removeFromUserStatsQuery = "DELETE FROM `" . $table . "` WHERE Name = '" . $name . "' AND Username = '" . $username . "';";
		mysqli_query($connection, $removeFromUserStatsQuery) or die("[4] : REMOVE QUERY failed.");
	}

	echo "0";
?>