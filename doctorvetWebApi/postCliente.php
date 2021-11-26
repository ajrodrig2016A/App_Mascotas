<?php

include "config.php";
include "utils.php";

// ini_set('display_errors', 1);
// ini_set('startup_errors', 1);
// error_reporting(E_ALL);

$dbConn =  connect($db);

//SELECT
if ($_SERVER['REQUEST_METHOD'] == 'GET')
{
    if (isset($_GET['idCliente']))
    {
      //Mostrar un post
      $sql = $dbConn->prepare("SELECT * cliente  where idCliente=:idCliente");
      $sql->bindValue(':idCliente', $_GET['idCliente']);
      $sql->execute();
      header("HTTP/1.1 200 OK");
      echo json_encode(  $sql->fetch(PDO::FETCH_ASSOC)  );
      exit();
	}
else {
      //Mostrar lista de post
      $sql = $dbConn->prepare("SELECT * FROM cliente");
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
    $sql = "INSERT INTO cliente
          (numeroDocumento, nombres, apellidos, fechaRegistro, direccion, email, numeroContacto)
          VALUES
          (:numeroDocumento, :nombres, :apellidos, :fechaRegistro, :direccion, :email, :numeroContacto)";
    $statement = $dbConn->prepare($sql);
    bindAllValues($statement, $input);
    $statement->execute();

    $postCodigo = $dbConn->lastInsertId();
    if($postCodigo)
    {
      $input['idCliente'] = $postCodigo;
      header("HTTP/1.1 200 OK");
      echo json_encode($input);
      exit();
	}
}

if ($_SERVER['REQUEST_METHOD'] == 'DELETE')
{
	$idCliente = $_GET['idCliente'];
  $statement = $dbConn->prepare("DELETE FROM  cliente where idCliente=:idCliente");
  $statement->bindValue(':idCliente', $idCliente);
  $statement->execute();
	header("HTTP/1.1 200 OK");
	exit();
}

//Actualizar
if ($_SERVER['REQUEST_METHOD'] == 'PUT')
{
    $input = $_GET;
    $postCodigo = $input['idCliente'];
    $fields = getParams($input);

    $sql = "
          UPDATE cliente
          SET $fields
          WHERE idCliente='$postCodigo'
           ";

    $statement = $dbConn->prepare($sql);
    bindAllValues($statement, $input);

    $statement->execute();
    header("HTTP/1.1 200 OK");
    exit();
}

?>


