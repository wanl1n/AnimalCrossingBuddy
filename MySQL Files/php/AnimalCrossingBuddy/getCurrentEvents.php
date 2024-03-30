<?php
$connection = mysqli_connect("localhost", "root", "root", "animal_crossing_buddy");

if (mysqli_connect_errno()) {
    echo "[1] : Connection Failed.";
    exit();
}

$input = $_POST["datetime"];
$isSouthernHemisphere = filter_var($_POST["isSouthernHemisphere"], FILTER_VALIDATE_BOOLEAN, FILTER_NULL_ON_FAILURE);
$datetime = date('m-d', strtotime($input));

$query = "SELECT * FROM seasons_and_events";

$dateQuery = mysqli_query($connection, $query) or die("[2] : QUERY failed.");

echo "0 \t";

while ($row = mysqli_fetch_assoc($dateQuery)) {
    foreach ($row as $key => $value) {
        $column = explode(" ", $key);
        if ($column[0] == date("Y")) {
            $dateRange = explode("-", $value);
            $startDate = date('m-d', strtotime($dateRange[0]));
            $endDate = date('m-d', strtotime($dateRange[1]));

            if (($datetime >= $startDate) && ($datetime <= $endDate)) {
                $data = array(
                    'Name' => $row["Name"],
                    'Icon Image Link' => $row["Icon Image Link"],
                    'Type' => $row["Type"],
                    'Display Name' => $row["Display Name"],
                    'Sort Date' => $row["Sort Date"],
                    'Date Varies By Year' => $row["Date Varies By Year"],
                    'Start Time' => $row["Start Time"],
                    'End Time' => $row["End Time"],
                    'Start Date' => $startDate,
                    'End Date' => $endDate,
                );

                echo json_encode($data, JSON_FORCE_OBJECT) . "\t";
            }
        }
    }
}

mysqli_free_result($dateQuery);
mysqli_close($connection);