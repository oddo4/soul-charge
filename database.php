<?php

	include "db.php";
	global $db;

	switch ($_SERVER["REQUEST_METHOD"]) {
		case "GET": header ("Content-Type: text/json");
		{
		    $result = $db -> prepare("SELECT * FROM evidence");
				$result -> execute();
				echo json_encode ($result -> fetchAll())
				break;
		}
	}
?>