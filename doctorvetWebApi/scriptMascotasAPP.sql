/*create database doctorvetwebapi_db;*/
CREATE TABLE Cita(
	idCita int auto_increment NOT NULL,
	idMascota int NOT NULL,
	idCliente int NOT NULL,
	idUsuario int NOT NULL,
	fecha date NOT NULL,
	hora time(6) NOT NULL,
	diagnostico varchar(250) NULL,
	estado varchar(16) NOT NULL,
	direccion varchar(100) NULL,
	coordenadas varchar(150) NULL,
 CONSTRAINT PK_Cita PRIMARY KEY CLUSTERED 
(
	idCita ASC
)
);

/****** Object:  Table Cliente    Script Date: 24/11/2021 21:26:35 ******/

CREATE TABLE Cliente(
	idCliente int auto_increment NOT NULL,
	numeroDocumento varchar(13) NOT NULL,
	nombres varchar(64) NULL,
	apellidos varchar(64) NULL,
	fechaRegistro date NOT NULL,
	direccion varchar(100) NULL,
	email varchar(64) NULL,
	numeroContacto varchar(20) NOT NULL,
 CONSTRAINT PK_Cliente PRIMARY KEY CLUSTERED 
(
	idCliente ASC
)
);

/****** Object:  Table Mascota    Script Date: 24/11/2021 21:26:35 ******/
CREATE TABLE Mascota(
	idMascota int auto_increment NOT NULL,
	idRaza int NOT NULL,
	idCliente int NOT NULL,
	nombre varchar(64) NULL,
	fechaNacimiento date NULL,
	genero varchar(45) NOT NULL,
	esterilizado bit NULL,
	color varchar(30) NULL,
	foto blob NULL,
 CONSTRAINT PK_Mascota PRIMARY KEY CLUSTERED 
(
	idMascota ASC
)
);
/****** Object:  Table Raza    Script Date: 24/11/2021 21:26:35 ******/

CREATE TABLE Raza(
	idRaza int auto_increment NOT NULL,
	nombre varchar(64) NOT NULL,
	caracteristicas varchar(200) NULL,
	tipoMascota varchar(45) NULL,
 CONSTRAINT PK_Raza PRIMARY KEY CLUSTERED 
(
	idRaza ASC
)
);
/****** Object:  Table Usuario    Script Date: 24/11/2021 21:26:35 ******/

CREATE TABLE Usuario(
	idUsuario int auto_increment NOT NULL,
	numeroDocumento varchar(13) NOT NULL,
	nombres varchar(64) NULL,
	apellidos varchar(64) NULL,
	fechaRegistro date NOT NULL,
	cargo varchar(64) NULL,
	email varchar(64) NULL,
	telefono varchar(20) NULL,
	rol char(1) NULL,
	password varchar(16) NULL,
	direccion varchar(100) NULL,
	coordenadas varchar(150) NULL,
 CONSTRAINT PK_Usuario PRIMARY KEY CLUSTERED 
(
	idUsuario ASC
)
);

ALTER TABLE Cita  ADD  CONSTRAINT FK_Cita_Cliente FOREIGN KEY(idCliente)
REFERENCES Cliente (idCliente);

ALTER TABLE Cita  ADD  CONSTRAINT FK_Cita_Mascota FOREIGN KEY(idMascota)
REFERENCES Mascota (idMascota);

ALTER TABLE Cita  ADD  CONSTRAINT FK_Cita_Usuario FOREIGN KEY(idUsuario)
REFERENCES Usuario (idUsuario);

ALTER TABLE Mascota  ADD  CONSTRAINT FK_Cliente_Mascota FOREIGN KEY(idCliente)
REFERENCES Cliente (idCliente);

ALTER TABLE Mascota  ADD  CONSTRAINT FK_Raza_Mascota FOREIGN KEY(idRaza)
REFERENCES Raza (idRaza);

