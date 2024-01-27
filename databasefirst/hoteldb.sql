USE master;
DROP DATABASE IF EXISTS Hotel;
CREATE DATABASE Hotel;

USE Hotel;

CREATE TABLE TipoQuarto
(
    cod_tipoquarto INT IDENTITY,
    descricao_tipoquarto VARCHAR(64) NOT NULL,

    PRIMARY KEY(cod_tipoquarto)
);

CREATE TABLE TipoPagamento
(
    cod_tipopagamento INT IDENTITY,
    descricao_tipopagamento VARCHAR(64) NOT NULL,

    PRIMARY KEY(cod_tipopagamento)
);

CREATE TABLE TipoFuncionario
(
    cod_tipofuncionario INT IDENTITY,
    descricao_tipofuncionario VARCHAR(64) NOT NULL,

    PRIMARY KEY(cod_tipofuncionario)
);

CREATE TABLE ServicoLavanderia
(
    cod_servicolavanderia INT IDENTITY,
    descricao_servicolavanderia VARCHAR(64) NOT NULL,
	custo_servicolavanderia DECIMAL(12, 2) NOT NULL,

    PRIMARY KEY(cod_servicolavanderia)
);

CREATE TABLE Nacionalidade
(
    cod_nacionalidade INT IDENTITY,
    pais_nacionalidade VARCHAR(64) NOT NULL,
	titulo_nacionalidade VARCHAR(64) NOT NULL,
    sigla_nacionalidade CHAR(2) NOT NULL,

    PRIMARY KEY(cod_nacionalidade)
);

CREATE TABLE Funcionario
(
    cod_funcionario INT IDENTITY,
    cod_tipofuncionario INT NOT NULL,
    nome_funcionario VARCHAR(64) NOT NULL,
    endereco_funcionario VARCHAR(256) NOT NULL,
    email_funcionario VARCHAR(128) NOT NULL,

    PRIMARY KEY(cod_funcionario),
    FOREIGN KEY(cod_tipofuncionario) REFERENCES TipoFuncionario(cod_tipofuncionario)
);

CREATE TABLE Cliente
(
    cod_cliente INT IDENTITY,
    cod_nacionalidade INT NOT NULL,
    nome_cliente VARCHAR(64) NOT NULL,
    endereco_cliente VARCHAR(256) NOT NULL,
    email_cliente VARCHAR(128) NOT NULL,

    PRIMARY KEY(cod_cliente),
    FOREIGN KEY(cod_nacionalidade) REFERENCES Nacionalidade(cod_nacionalidade)
);

CREATE TABLE Telefone
(
    cod_telefone INT IDENTITY,
    numero_telefone VARCHAR(24) NOT NULL,
    cod_proprietario INT NOT NULL, -- Chave estrangeira implicita (Cliente ou Funcionario)
    tipoproprietario_telefone CHAR NOT NULL, -- Cliente (C), Funcionario (F)

    PRIMARY KEY(cod_telefone)
);

CREATE TABLE Quarto
(
    cod_quarto INT IDENTITY,
    numero_quarto INT UNIQUE NOT NULL,
    cod_tipoquarto INT NOT NULL,
    adaptadoespecial_quarto BIT NOT NULL DEFAULT 0,
    reservavel_quarto BIT NOT NULL DEFAULT 1,
	capacidade_quarto INT NOT NULL,

    PRIMARY KEY(cod_quarto),
    FOREIGN KEY(cod_tipoquarto) REFERENCES TipoQuarto(cod_tipoquarto)
);

CREATE TABLE Reserva
(
    cod_reserva INT IDENTITY,
    cod_quarto INT NOT NULL,
    cod_cliente INT NOT NULL,
    cod_funcionario INT NOT NULL,
	colchao_reserva BIT NOT NULL,
    datacriacao_reserva DATETIME NOT NULL DEFAULT GETDATE(),
    dataprevista_reserva DATETIME NOT NULL,

    PRIMARY KEY(cod_reserva),
    FOREIGN KEY(cod_quarto) REFERENCES Quarto(cod_quarto),
    FOREIGN KEY(cod_cliente) REFERENCES Cliente(cod_cliente),
    FOREIGN KEY(cod_funcionario) REFERENCES Funcionario(cod_funcionario)
);

CREATE TABLE Estadia
(
    cod_estadia INT IDENTITY,
    cod_reserva INT NOT NULL,
    datacheckin_estadia DATETIME NOT NULL DEFAULT GETDATE(),
    datacheckout_estadia DATETIME DEFAULT NULL,

    PRIMARY KEY(cod_estadia),
    FOREIGN KEY(cod_reserva) REFERENCES Reserva(cod_reserva)
);

CREATE TABLE NotaFiscal
(
    chavenfe_notafiscal CHAR(44) NOT NULL,
    cod_tipopagamento INT NOT NULL,
    cod_estadia INT NOT NULL,

    PRIMARY KEY(chavenfe_notafiscal),
    FOREIGN KEY(cod_tipopagamento) REFERENCES TipoPagamento(cod_tipopagamento),
    FOREIGN KEY(cod_estadia) REFERENCES Estadia(cod_estadia)
);

CREATE TABLE Filial
(
	cod_filial INT IDENTITY,
	nome_filial VARCHAR(64) NOT NULL,
	endereco_filial VARCHAR(256) NOT NULL,
	estrelas_filial FLOAT NOT NULL,

	PRIMARY KEY(cod_filial)
);

CREATE TABLE Quarto_Filial
(
	cod_filial INT NOT NULL,
	cod_tipoquarto INT NOT NULL,
	quantidade_quarto_filial INT NOT NULL DEFAULT 0,

	PRIMARY KEY(cod_filial, cod_tipoquarto),
	FOREIGN KEY(cod_filial) REFERENCES Filial(cod_filial),
    FOREIGN KEY(cod_tipoquarto) REFERENCES TipoQuarto(cod_tipoquarto)
);

CREATE TABLE ConsumoRefeicao
(
	cod_consumorefeicao INT IDENTITY,
	cod_estadia INT NOT NULL,
	entreguequarto_consumorefeicao BIT NOT NULL,
	descricao_consumorefeicao VARCHAR(64) NOT NULL,
	custo_consumorefeicao DECIMAL(12, 2) NOT NULL,
	data_consumorefeicao DATETIME NOT NULL DEFAULT GETDATE(),
	
	PRIMARY KEY(cod_consumorefeicao),
	FOREIGN KEY(cod_estadia) REFERENCES Estadia(cod_estadia)
);

CREATE TABLE ConsumoLavanderia
(
	cod_consumolavanderia INT IDENTITY,
	cod_servicolavanderia INT NOT NULL,
	cod_estadia INT NOT NULL,
	data_consumoservico DATETIME NOT NULL DEFAULT GETDATE(),

	PRIMARY KEY(cod_consumolavanderia),
	FOREIGN KEY(cod_servicolavanderia) REFERENCES ServicoLavanderia(cod_servicolavanderia),
	FOREIGN KEY(cod_estadia) REFERENCES Estadia(cod_estadia)
);
