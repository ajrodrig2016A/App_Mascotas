<?php
include "config.php";
include "utils.php";

$dbConn =  connect($db);

//SELECT
if ($_SERVER['REQUEST_METHOD'] == 'GET')
{
    if (isset($_GET['idCita']))
    {
      //Mostrar un post
      $sql = $dbConn->prepare("SELECT * cita  where idCita=:idCita");
      print $sql;
      $sql->bindValue(':idCita', $_GET['idCita']);
      $sql->execute();
      header("HTTP/1.1 200 OK");
      echo json_encode(  $sql->fetch()  );
      exit();
	}
else {
      //Mostrar lista de post
      $sql = $dbConn->prepare("SELECT * FROM cita");
      $sql->execute();
      $sql->setFetchMode();
      header("HTTP/1.1 200 OK");
      echo json_encode( $sql->fetchAll()  );
      exit();
	}
}

//INSERTAR
if ($_SERVER['REQUEST_METHOD'] == 'POST')
{
    $input = $_POST;
    $sql = "INSERT INTO cita
          (idMascota, idCliente, idUsuario, fecha, hora, diagnostico, estado, direccion, coordenadas)
          VALUES
          (:idMascota, :idCliente, :idUsuario, :fecha, :hora, :diagnostico, :estado, :direccion, :coordenadas)";
    $statement = $dbConn->prepare($sql);
    bindAllValues($statement, $input);
    $statement->execute();

    $postCodigo = $dbConn->lastInsertId();
    if($postCodigo)
    {
      $input['idCita'] = $postCodigo;
      header("HTTP/1.1 200 OK");
      echo json_encode($input);
      exit();
	}
}

if ($_SERVER['REQUEST_METHOD'] == 'DELETE')
{
	$idCita = $_GET['idCita'];
  $statement = $dbConn->prepare("DELETE FROM  cita where idCita=:idCita");
  $statement->bindValue(':idCita', $idCita);
  $statement->execute();
	header("HTTP/1.1 200 OK");
	exit();
}

//Actualizar
if ($_SERVER['REQUEST_METHOD'] == 'PUT')
{
    $input = $_GET;
    $postCodigo = $input['idCita'];
    $fields = getParams($input);

    $sql = "
          UPDATE cita
          SET $fields
          WHERE idCita='$postCodigo'
           ";

    $statement = $dbConn->prepare($sql);
    bindAllValues($statement, $input);

    $statement->execute();
    header("HTTP/1.1 200 OK");
    exit();
}

?>


