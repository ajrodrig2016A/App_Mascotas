<?php
include "config.php";
include "utils.php";

$dbConn =  connect($db);

//SELECT
if ($_SERVER['REQUEST_METHOD'] == 'GET')
{
    if (isset($_GET['idUsuario']))
    {
      //Mostrar un post
      $sql = $dbConn->prepare("SELECT * from usuario  where idUsuario=:idUsuario");
      $sql->bindValue(':idUsuario', $_GET['idUsuario']);
      $sql->execute();
      header("HTTP/1.1 200 OK");
      echo json_encode(  $sql->fetch(PDO::FETCH_ASSOC)  );
      exit();
	}
else {
      //Mostrar lista de post
      $sql = $dbConn->prepare("SELECT * FROM usuario");
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
    $sql = "INSERT INTO usuario
      (numeroDocumento, nombres, apellidos, fechaRegistro, cargo, email, telefono, rol, password, direccion, coordenadas)
          VALUES
      (:numeroDocumento, :nombres, :apellidos, :fechaRegistro, :cargo, :email, :telefono, :rol, :password, :direccion, :coordenadas)";

    $statement = $dbConn->prepare($sql);
    bindAllValues($statement, $input);
    $statement->execute();

    $postCodigo = $dbConn->lastInsertId();
    if($postCodigo)
    {
      $input['idUsuario'] = $postCodigo;
      header("HTTP/1.1 200 OK");
      echo json_encode($input);
      exit();
	}
}

if ($_SERVER['REQUEST_METHOD'] == 'DELETE')
{
	$idUsuario = $_GET['idUsuario'];
  $statement = $dbConn->prepare("DELETE FROM  usuario where idUsuario=:idUsuario");
  $statement->bindValue(':idUsuario', $idUsuario);
  $statement->execute();
	header("HTTP/1.1 200 OK");
	exit();
}
print ($_SERVER['REQUEST_METHOD']);
//Actualizar
if ($_SERVER['REQUEST_METHOD'] == 'PUT')
{
    $input = $_GET;
    $postCodigo = $input['idUsuario'];
    $fields = getParams($input);

    $sql = "
          UPDATE usuario
          SET $fields
          WHERE idUsuario='$postCodigo'
           ";

    $statement = $dbConn->prepare($sql);
    bindAllValues($statement, $input);

    $statement->execute();
    header("HTTP/1.1 200 OK");
    exit();
}

?>


