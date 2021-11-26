<?php
include "config.php";
include "utils.php";

$dbConn =  connect($db);

//SELECT
if ($_SERVER['REQUEST_METHOD'] == 'GET')
{
    if (isset($_GET['idRaza']))
    {
      //Mostrar un post
      $sql = $dbConn->prepare("SELECT * from raza  where idRaza=:idRaza");
      $sql->bindValue(':idRaza', $_GET['idRaza']);
      $sql->execute();
      header("HTTP/1.1 200 OK");
      echo json_encode(  $sql->fetch(PDO::FETCH_ASSOC)  );
      exit();
	}
else {
      //Mostrar lista de post
      $sql = $dbConn->prepare("SELECT * FROM raza");
      $sql->execute();
      $sql->setFetchMode(PDO::FETCH_ASSOC);
      header("HTTP/1.1 200 OK");
      echo json_encode( $sql->fetchAll()  );
      exit();
	}
}

//INSERTAR
if ($_SERVER['REQUEST_METHOD'] == 'POST')
{
    $input = $_POST;
    $sql = "INSERT INTO raza
          (nombre, caracteristicas, tipoMascota)
          VALUES
          (:nombre, :caracteristicas, :tipoMascota)";
    $statement = $dbConn->prepare($sql);
    bindAllValues($statement, $input);
    $statement->execute();

    $postCodigo = $dbConn->lastInsertId();
    if($postCodigo)
    {
      $input['idRaza'] = $postCodigo;
      header("HTTP/1.1 200 OK");
      echo json_encode($input);
      exit();
	}
}

if ($_SERVER['REQUEST_METHOD'] == 'DELETE')
{
	$idRaza = $_GET['idRaza'];
  $statement = $dbConn->prepare("DELETE FROM  raza where idRaza=:idRaza");
  $statement->bindValue(':idRaza', $idRaza);
  $statement->execute();
	header("HTTP/1.1 200 OK");
	exit();
}
print ($_SERVER['REQUEST_METHOD']);
//Actualizar
if ($_SERVER['REQUEST_METHOD'] == 'PUT')
{
    $input = $_GET;
    $postCodigo = $input['idRaza'];
    $fields = getParams($input);

    $sql = "
          UPDATE raza
          SET $fields
          WHERE idRaza='$postCodigo'
           ";

    $statement = $dbConn->prepare($sql);
    bindAllValues($statement, $input);

    $statement->execute();
    header("HTTP/1.1 200 OK");
    exit();
}

?>


