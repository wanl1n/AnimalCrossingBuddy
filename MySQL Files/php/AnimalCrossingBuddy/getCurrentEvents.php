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

$query = "select seasons_and_events.Name, `Icon Image Link`, `Type`, `Display Name`, `Sort Date`, `Start Time`, `End Time`, `Year`, `" . $hemisphereString . " Start Date`, `" . $hemisphereString . " End Date` from seasons_and_events, hemisphere_date_ranges where `Year` = '" . date("Y", strtotime($input)) . "' and seasons_and_events.Name = hemisphere_date_ranges.Name and '" . date("Y-m-d", strtotime($input)) . "' between STR_TO_DATE(CONCAT(`" . $hemisphereString . " Start Date`, ' ', '" . date("Y", strtotime($input)) . "'), '%b %e %Y') and STR_TO_DATE(CONCAT(`" . $hemisphereString . " End Date`, ' ', '" . date("Y", strtotime($input)) . "'), '%b %e %Y');";

$dateQuery = mysqli_query($connection, $query) or die("[2] : QUERY failed: " . $query);

echo "0";

while ($row = mysqli_fetch_assoc($dateQuery)) {
    $data = array(
        'Name' => $row["Name"],
        'Icon Image Link' => $row["Icon Image Link"],
        'Type' => $row["Type"],
        'Display Name' => $row["Display Name"],
        'Sort Date' => $row["Sort Date"],
        'Start Time' => $row["Start Time"],
        'End Time' => $row["End Time"],
        'Start Date' => $row[$hemisphereString . " Start Date"],
        'End Date' => $row[$hemisphereString . " End Date"],
    );
    echo "\t";
    echo json_encode($data, JSON_FORCE_OBJECT);
    // foreach ($row as $key => $value) {
    //     $column = explode(" ", $key);
    //     if ($column[0] == date("Y", strtotime($input)) && $column[1] == $hemisphereString) {
    //         $dateRange = explode("-", $value);
    //         $startDate = date('m-d', strtotime($dateRange[0]));
    //         $endDate = date('m-d', strtotime($dateRange[1]));

    //         if (($datetime >= $startDate) && ($datetime <= $endDate)) {
    //             $data = array(
    //                 'Name' => $row["Name"],
    //                 'Icon Image Link' => $row["Icon Image Link"],
    //                 'Type' => $row["Type"],
    //                 'Display Name' => $row["Display Name"],
    //                 'Sort Date' => $row["Sort Date"],
    //                 'Date Varies By Year' => $row["Date Varies By Year"],
    //                 'Start Time' => $row["Start Time"],
    //                 'End Time' => $row["End Time"],
    //                 'Start Date' => date('M-d', strtotime($dateRange[0])),
    //                 'End Date' => date('M-d', strtotime($dateRange[1])),
    //             );
    //             echo "\t";
    //             echo json_encode($data, JSON_FORCE_OBJECT);
    //         }
    //     }
    // }
}

mysqli_free_result($dateQuery);
mysqli_close($connection);