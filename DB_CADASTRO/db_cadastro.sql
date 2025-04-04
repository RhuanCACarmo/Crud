CREATE DATABASE db_cadastro;

CREATE TABLE dadosdecliente(
    codigo INT AUTO_INCREMENT PRIMARY KEY,
    nomecompleto VARCHAR(100) NOT NULL,
    nomesocial VARCHAR (100),
    email VARCHAR(50) NOT NULL,
    cpf VARCHAR (20) UNIQUE NOT NULL
);

INSERT INTO dadadosdecliente