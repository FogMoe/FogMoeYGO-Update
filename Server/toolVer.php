<?php
$verifyVer = $_POST['toolVer'];
$fopen = fopen("CheckVersion/Version.fogmoe", "r");
$version = file_get_contents("CheckVersion/Version.fogmoe");
fclose($fopen);
if ($verifyVer == $version) {
    echo "True";
} else {
    echo "False";
}
?>