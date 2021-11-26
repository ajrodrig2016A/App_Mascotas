<?php
include "config.php";
include "utils.php";

$dbConn =  connect($db);

//SELECT
if ($_SERVER['REQUEST_METHOD'] == 'GET')
{
    if (isset($_GET['idMascota']))
    {
      //Mostrar un post
      $sql = $dbConn->prepare("SELECT * from mascota  where idMascota=:idMascota");
      $sql->bindValue(':idMascota', $_GET['idMascota']);
      $sql->execute();
      header("HTTP/1.1 200 OK");
      echo json_encode(  $sql->fetch(PDO::FETCH_ASSOC)  );
      exit();
	}
else {
      //Mostrar lista de post
      $sql = $dbConn->prepare("SELECT * FROM mascota");
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
    $sql = "INSERT INTO mascota
          (idRaza, idCliente, nombre, fechaNacimiento, genero, esterilizado, color, foto)
          VALUES
          (:idRaza, :idCliente, :nombre, :fechaNacimiento, :genero, :esterilizado, :color, :foto)";
    $statement = $dbConn->prepare($sql);
    bindAllValues($statement, $input);
    $statement->execute();

    $postCodigo = $dbConn->lastInsertId();
    if($postCodigo)
    {
      $input['idMascota'] = $postCodigo;
      header("HTTP/1.1 200 OK");
      echo json_encode($input);
      exit();
	}
}

if ($_SERVER['REQUEST_METHOD'] == 'DELETE')
{
	$idMascota = $_GET['idMascota'];
  $statement = $dbConn->prepare("DELETE FROM  mascota where idMascota=:idMascota");
  $statement->bindValue(':idMascota', $idMascota);
  $statement->execute();
	header("HTTP/1.1 200 OK");
	exit();
}
print ($_SERVER['REQUEST_METHOD']);
//Actualizar
if ($_SERVER['REQUEST_METHOD'] == 'PUT')
{
    $input = $_GET;
    $postCodigo = $input['idMascota'];
    $fields = getParams($input);

    $sql = "
          UPDATE mascota
          SET $fields
          WHERE idMascota='$postCodigo'
           ";

    $statement = $dbConn->prepare($sql);
    bindAllValues($statement, $input);

    $statement->execute();
    header("HTTP/1.1 200 OK");
    exit();
}

?>


