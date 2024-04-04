<?php
$connection = mysqli_connect("localhost", "root", "root", "animal_crossing_buddy");

if (mysqli_connect_errno()) {
    echo "[1] : Connection Failed.";
    exit();
}

$input = $_POST["datetime"];
// echo $input . "\t";
// echo strtotime('m-d', $input) . "\t";
$datetime = date('m-d', strtotime($input));
// echo $datetime . "\t";

$isSouthernHemisphere = $_POST["isSouthernHemisphere"];
// echo $isSouthernHemisphere . "\t";

if ($isSouthernHemisphere == "True") {
    $hemisphereString = "SH";
} else
    $hemisphereString = "NH";


// echo $hemisphereString . "\t";

$query = "SELECT * FROM seasons_and_events;";

$dateQuery = mysqli_query($connection, $query) or die("[2] : QUERY failed.");

echo "0";

while ($row = mysqli_fetch_assoc($dateQuery)) {
    foreach ($row as $key => $value) {
        $column = explode(" ", $key);
        if ($column[0] == date("Y", strtotime($input)) && $column[1] == $hemisphereString) {
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
                    'Start Date' => date('M-d', strtotime($dateRange[0])),
                    'End Date' => date('M-d', strtotime($dateRange[1])),
                );
                echo "\t";
                // echo $value;
                echo json_encode($data, JSON_FORCE_OBJECT);
            }
        }
    }
}

mysqli_free_result($dateQuery);
mysqli_close($connection);