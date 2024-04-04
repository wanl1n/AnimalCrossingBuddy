<?php 
	$connection = mysqli_connect ("localhost", "root", "root", "animal_crossing_buddy");

	if (mysqli_connect_errno()) {
		echo "[1] : Connection Failed.";
		exit();
	}

	$username = $_POST["username"];
	$password = $_POST["password"];
	$query1 = "SELECT Username FROM users WHERE Username = '" . $username . "';";

	$usernameCheck = mysqli_query($connection, $query1) or die("[2] : Username CHECK QUERY failed.");
	if (mysqli_num_rows($usernameCheck) > 0) {
        echo "[3] : Username already exists.";
        exit();
    }

	$salt = "\$5\$rounds=5000\$" . "any_value_here" . $username . "\$";
	$hash = crypt($password, $salt);

	$query2 = "INSERT INTO users (Username, Salt, Hash) VALUES ('" . $username . "', '" . $salt . "', '" . $hash . "');";
	mysqli_query($connection, $query2) or die("[4] : User INSERT QUERY Failed");

	echo "0";
?>