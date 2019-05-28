USE MrVeggie;

CREATE TABLE Receita (
  id_receita   int IDENTITY(1, 1) NOT NULL, 
  nome         varchar(50) NOT NULL, 
  [desc]       varchar(500) NOT NULL, 
  tempo_conf   real NOT NULL, 
  avaliacao    real NOT NULL, 
  n_avaliacoes int NOT NULL, 
  dificuldade  int NOT NULL, 
  calorias     int NOT NULL, 
  n_pessoas    int NOT NULL, 
  url_imagem   varchar(300) NOT NULL, 
  PRIMARY KEY (id_receita));

CREATE TABLE Operacao (
  id_op  int IDENTITY(1, 1) NOT NULL, 
  [desc] varchar(50) NOT NULL, 
  PRIMARY KEY (id_op));

CREATE TABLE Ingrediente (
  id_ingrediente int IDENTITY(1, 1) NOT NULL, 
  nome           varchar(50) NOT NULL, 
  url_imagem     varchar(300) NOT NULL, 
  PRIMARY KEY (id_ingrediente));

CREATE TABLE Passo (
  id_passo    int IDENTITY(1, 1) NOT NULL, 
  nr          int NOT NULL, 
  [desc]      text NOT NULL, 
  tempo       real NOT NULL, 
  operacao    int NOT NULL, 
  receita     int NOT NULL, 
  sub_receita int NULL, 
  ultimo      bit DEFAULT '0' NOT NULL, 
  PRIMARY KEY (id_passo));

CREATE TABLE Utilizador (
  id_utilizador   int IDENTITY(1, 1) NOT NULL, 
  nome            varchar(50) NOT NULL, 
  email           varchar(50) NOT NULL, 
  password        varchar(50) NOT NULL, 
  idade           int NOT NULL, 
  sexo            varchar(50) NOT NULL, 
  admin           bit DEFAULT '0' NOT NULL, 
  config_inicial  bit DEFAULT '0' NOT NULL, 
  PRIMARY KEY (id_utilizador));

CREATE TABLE UtilizadorReceitasPref (
  utilizador int NOT NULL, 
  receita    int NOT NULL, 
  PRIMARY KEY (utilizador, receita));

CREATE TABLE UtilizadorIngredientesPref (
  utilizador  int NOT NULL, 
  ingrediente int NOT NULL, 
  PRIMARY KEY (utilizador, ingrediente));

CREATE TABLE HistoricoUtilizador (
  utilizador int NOT NULL, 
  receita    int NOT NULL, 
  data_conf  datetime NOT NULL, 
  avaliacao  int NULL, 
  PRIMARY KEY (utilizador, receita, data_conf));

CREATE TABLE IngredientesPasso (
  passo       int NOT NULL, 
  ingrediente int NOT NULL, 
  quantidade  real NOT NULL, 
  unidade     int NOT NULL, 
  PRIMARY KEY (passo, ingrediente));

CREATE TABLE Agenda (
  dia        int NOT NULL, 
  refeicao   char(1) NOT NULL, 
  utilizador int NOT NULL, 
  receita    int NOT NULL, 
  PRIMARY KEY (dia, refeicao, utilizador));

CREATE TABLE UtensiliosReceita (
  receita   int NOT NULL, 
  utensilio int NOT NULL, 
  PRIMARY KEY (receita, utensilio));

CREATE TABLE Utensilio (
  id_utensilio int IDENTITY NOT NULL, 
  nome         varchar(50) NOT NULL, 
  url_imagem   varchar(300) NOT NULL, 
  PRIMARY KEY (id_utensilio));
  
CREATE TABLE Unidade (
  id_unidade int IDENTITY NOT NULL, 
  [desc]     varchar(50) NOT NULL, 
  PRIMARY KEY (id_unidade));


CREATE INDEX Receita_id_receita ON Receita (id_receita);
CREATE INDEX Operacao_id_op ON Operacao (id_op);
CREATE INDEX Ingrediente_id_ingrediente ON Ingrediente (id_ingrediente);
CREATE INDEX Passo_id_passo ON Passo (id_passo);
CREATE INDEX Passo_receita ON Passo (receita);
CREATE INDEX Utilizador_id_utilizador ON Utilizador (id_utilizador);
CREATE INDEX UtilizadorReceitasPref_utilizador ON UtilizadorReceitasPref (utilizador);
CREATE INDEX UtilizadorIngredientesPref_utilizador ON UtilizadorIngredientesPref (utilizador);
CREATE INDEX HistoricoUtilizador_utilizador ON HistoricoUtilizador (utilizador);
CREATE INDEX IngredientesPasso_passo ON IngredientesPasso (passo);
CREATE INDEX Agenda_utilizador ON Agenda (utilizador);
CREATE INDEX UtensiliosReceita_receita ON UtensiliosReceita (receita);
CREATE INDEX Utensilio_id_utensilio ON Utensilio (id_utensilio);
CREATE INDEX Unidade_id_unidade ON Unidade (id_unidade);


ALTER TABLE Passo ADD CONSTRAINT FKPasso200762 FOREIGN KEY (receita) REFERENCES Receita (id_receita);
ALTER TABLE UtilizadorReceitasPref ADD CONSTRAINT FKUtilizador922587 FOREIGN KEY (utilizador) REFERENCES Utilizador (id_utilizador);
ALTER TABLE UtilizadorReceitasPref ADD CONSTRAINT FKUtilizador874405 FOREIGN KEY (receita) REFERENCES Receita (id_receita);
ALTER TABLE UtilizadorIngredientesPref ADD CONSTRAINT FKUtilizador420902 FOREIGN KEY (utilizador) REFERENCES Utilizador (id_utilizador);
ALTER TABLE UtilizadorIngredientesPref ADD CONSTRAINT FKUtilizador642537 FOREIGN KEY (ingrediente) REFERENCES Ingrediente (id_ingrediente);
ALTER TABLE HistoricoUtilizador ADD CONSTRAINT FKHistoricoU199099 FOREIGN KEY (utilizador) REFERENCES Utilizador (id_utilizador);
ALTER TABLE HistoricoUtilizador ADD CONSTRAINT FKHistoricoU247281 FOREIGN KEY (receita) REFERENCES Receita (id_receita);
ALTER TABLE IngredientesPasso ADD CONSTRAINT FKIngredient895049 FOREIGN KEY (passo) REFERENCES Passo (id_passo);
ALTER TABLE IngredientesPasso ADD CONSTRAINT FKIngredient52444 FOREIGN KEY (ingrediente) REFERENCES Ingrediente (id_ingrediente);
ALTER TABLE Passo ADD CONSTRAINT FKPasso568056 FOREIGN KEY (operacao) REFERENCES Operacao (id_op);
ALTER TABLE Passo ADD CONSTRAINT FKPasso74792 FOREIGN KEY (sub_receita) REFERENCES Receita (id_receita);
ALTER TABLE Agenda ADD CONSTRAINT FKAgenda98980 FOREIGN KEY (utilizador) REFERENCES Utilizador (id_utilizador);
--ALTER TABLE Agenda ADD CONSTRAINT FKAgenda98981 FOREIGN KEY (utilizador) REFERENCES Utilizador (id_utilizador);
ALTER TABLE Agenda ADD CONSTRAINT FKAgenda50798 FOREIGN KEY (receita) REFERENCES Receita (id_receita);
ALTER TABLE UtensiliosReceita ADD CONSTRAINT FKUtensilios678874 FOREIGN KEY (utensilio) REFERENCES Utensilio (id_utensilio);
ALTER TABLE IngredientesPasso ADD CONSTRAINT FKIngredient7679 FOREIGN KEY (unidade) REFERENCES Unidade (id_unidade);
ALTER TABLE UtensiliosReceita ADD CONSTRAINT FKUtensilios299728 FOREIGN KEY (receita) REFERENCES Receita (id_receita);
