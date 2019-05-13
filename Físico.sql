
USE MrVeggie;

CREATE TABLE Agenda (
    dia int NOT NULL, 
    refeicao char(1) NOT NULL, 
    utilizador int NOT NULL, 
    receita int NOT NULL, 
    PRIMARY KEY (dia, refeicao, utilizador)
);

CREATE TABLE HistoricoUtilizador (
    utilizador int NOT NULL, 
    receita int NOT NULL, 
    data_conf int NOT NULL, 
    avaliacao int NULL, 
    PRIMARY KEY (utilizador, receita, data_conf)
);

CREATE TABLE Ingrediente (
    id_ingrediente int IDENTITY(1, 1) NOT NULL, 
    nome varchar(50) NOT NULL, 
    calorias real NOT NULL, 
    url_imagem varchar(300) NOT NULL, 
    PRIMARY KEY (id_ingrediente)
);

CREATE TABLE IngredientesPasso (
    passo int NOT NULL, 
    ingrediente int NOT NULL,
    quantidade int NOT NULL, 
	unidade varchar(100) NOT NULL,
    PRIMARY KEY (passo, ingrediente)
);

CREATE TABLE Operacao (
    id_op int IDENTITY(1, 1) NOT NULL, 
    [desc] varchar(50) NOT NULL, 
    PRIMARY KEY (id_op)
);

CREATE TABLE Passo (
    id_passo int IDENTITY(1, 1) NOT NULL, 
    nr int NOT NULL, 
    tempo real NOT NULL, 
    operacao int NOT NULL, 
    receita int NOT NULL, 
    sub_receita int NULL, 
	ultimo bit NOT NULL DEFAULT 0,
    PRIMARY KEY (id_passo)
);

CREATE TABLE Receita (
    id_receita int IDENTITY(1, 1) NOT NULL, 
	nome varchar(50) NOT NULL,
    [desc] varchar(500) NOT NULL, 
    tempo_conf real NOT NULL, 
    avaliacao real NOT NULL,
    n_avaliacoes int NOT NULL, 
    dificuldade int NOT NULL, 
    calorias int NOT NULL, 
    n_pessoas int NOT NULL, 
    url_imagem varchar(300) NOT NULL, 
    PRIMARY KEY (id_receita)
);

CREATE TABLE Utilizador (
    id_utilizador int IDENTITY(1, 1) NOT NULL, 
    nome varchar(50) NOT NULL, 
    email varchar(50) NOT NULL, 
    password varchar(50) NOT NULL, 
    idade int NOT NULL, 
    sexo char(1) NOT NULL, 
    PRIMARY KEY (id_utilizador)
);

CREATE TABLE UtilizadorIngredientePref (
    utilizador int NOT NULL, 
    ingrediente int NOT NULL, 
    PRIMARY KEY (utilizador, ingrediente)
);

CREATE TABLE UtilizadorReceitasPref (
    utilizador int NOT NULL, 
    receita int NOT NULL, 
    PRIMARY KEY (utilizador, receita)
);



CREATE INDEX Ingrediente_id_ingrediente ON Ingrediente (id_ingrediente);
CREATE INDEX Operacao_id_op ON Operacao (id_op);
CREATE INDEX Passo_id_passo ON Passo (id_passo);
CREATE INDEX Receita_id_receita ON Receita (id_receita);

ALTER TABLE Agenda ADD CONSTRAINT FKAgenda50798 FOREIGN KEY (receita) REFERENCES Receita (id_receita);
ALTER TABLE Agenda ADD CONSTRAINT FKAgenda98980 FOREIGN KEY (utilizador) REFERENCES Utilizador (id_utilizador);
ALTER TABLE Passo ADD CONSTRAINT FKPasso74792 FOREIGN KEY (sub_receita) REFERENCES Receita (id_receita);
ALTER TABLE Passo ADD CONSTRAINT FKPasso568056 FOREIGN KEY (operacao) REFERENCES Operacao (id_op);
ALTER TABLE IngredientesPasso ADD CONSTRAINT FKIngredient52444 FOREIGN KEY (ingrediente) REFERENCES Ingrediente (id_ingrediente);
ALTER TABLE IngredientesPasso ADD CONSTRAINT FKIngredient895049 FOREIGN KEY (passo) REFERENCES Passo (id_passo);
ALTER TABLE HistoricoUtilizador ADD CONSTRAINT FKHistoricoU247281 FOREIGN KEY (receita) REFERENCES Receita (id_receita);
ALTER TABLE HistoricoUtilizador ADD CONSTRAINT FKHistoricoU199099 FOREIGN KEY (utilizador) REFERENCES Utilizador (id_utilizador);
ALTER TABLE UtilizadorIngredientePref ADD CONSTRAINT FKUtilizador467783 FOREIGN KEY (ingrediente) REFERENCES Ingrediente (id_ingrediente);
ALTER TABLE UtilizadorIngredientePref ADD CONSTRAINT FKUtilizador595656 FOREIGN KEY (utilizador) REFERENCES Utilizador (id_utilizador);
ALTER TABLE UtilizadorReceitasPref ADD CONSTRAINT FKUtilizador874405 FOREIGN KEY (receita) REFERENCES Receita (id_receita);
ALTER TABLE UtilizadorReceitasPref ADD CONSTRAINT FKUtilizador922587 FOREIGN KEY (utilizador) REFERENCES Utilizador (id_utilizador);
ALTER TABLE Passo ADD CONSTRAINT FKPasso200762 FOREIGN KEY (receita) REFERENCES Receita (id_receita);