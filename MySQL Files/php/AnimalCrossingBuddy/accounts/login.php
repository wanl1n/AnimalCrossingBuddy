<?php 
	$connection = mysqli_connect ("localhost", "root", "root", "animal_crossing_buddy");

	if (mysqli_connect_errno()) {
		echo "[1] : Connection Failed.";
		exit();
	}

	$username = $_POST["username"];
	$password = $_POST["password"];
	$query1 = "SELECT Username, Salt, Hash, Score FROM users WHERE Username = '" . $username . "';";

	$usernameCheck = mysqli_query($connection, $query1) or die("[2] : Username CHECK QUERY failed.");

    if (mysqli_num_rows($usernameCheck) != 1) {
        echo "[4] : Username doesn't exist.";
        exit();
    }

    $retrievedUser = mysqli_fetch_assoc($usernameCheck);
    $salt = $retrievedUser["Salt"];
    $hash = $retrievedUser["Hash"];
	$expectedHash = crypt($password, $salt);

    if ($hash != $expectedHash) {
        echo "[5] : Incorrect password.";
        exit();
    }

    $score = $retrievedUser["Score"];
    echo "0\t" . $score;
?>