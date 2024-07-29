CREATE DATABASE TaskManagement;
GO

USE TaskManagement;
GO

CREATE TABLE Tarefas(
	id int identity(1,1) not null,
	titulo varchar(100) not null,
	descricao varchar(450),
	prazo date not null,
	prioridade bit,
	estado bit not null,
	id_responsavel int not null,
	CONSTRAINT pk_Tarefas PRIMARY KEY (id),
	CONSTRAINT fk_Tarefas_Usuarios FOREIGN KEY (id_responsavel) REFERENCES Usuarios(id)
);
GO

CREATE TABLE Usuarios(
	id int identity(1,1) not null,
	nome varchar(100) not null,
	email varchar(100) not null,
	senha varchar(30) not null,
	cargo varchar(50) not null,
	CONSTRAINT pk_Usuarios PRIMARY KEY (id)
);