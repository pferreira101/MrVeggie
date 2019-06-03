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
  data_reg  datetime NOT NULL, 
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
  avaliacao  int NOT NULL, 
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


/*
USE [msdb]
GO
DECLARE @jobId BINARY(16)
EXEC  msdb.dbo.sp_add_job @job_name=N'Limpa Agenda', 
		@enabled=1, 
		@notify_level_eventlog=0, 
		@notify_level_email=2, 
		@notify_level_page=2, 
		@delete_level=0, 
		@category_name=N'[Uncategorized (Local)]', 
		@owner_login_name=N'DESKTOP-F88H99P\nelso', @job_id = @jobId OUTPUT
select @jobId
GO
EXEC msdb.dbo.sp_add_jobserver @job_name=N'Limpa Agenda', @server_name = N'DESKTOP-CM5S8K1'
GO
USE [msdb]
GO
EXEC msdb.dbo.sp_add_jobstep @job_name=N'Limpa Agenda', @step_name=N'Elimina', 
		@step_id=1, 
		@cmdexec_success_code=0, 
		@on_success_action=1, 
		@on_fail_action=2, 
		@retry_attempts=0, 
		@retry_interval=0, 
		@os_run_priority=0, @subsystem=N'TSQL', 
		@command=N'DELETE FROM Agenda
WHERE dia >= 0;', 
		@database_name=N'MrVeggie', 
		@flags=0
GO
USE [msdb]
GO
EXEC msdb.dbo.sp_update_job @job_name=N'Limpa Agenda', 
		@enabled=1, 
		@start_step_id=1, 
		@notify_level_eventlog=0, 
		@notify_level_email=2, 
		@notify_level_page=2, 
		@delete_level=0, 
		@description=N'', 
		@category_name=N'[Uncategorized (Local)]', 
		@owner_login_name=N'DESKTOP-F88H99P\nelso', 
		@notify_email_operator_name=N'', 
		@notify_page_operator_name=N''
GO
USE [msdb]
GO
DECLARE @schedule_id int
EXEC msdb.dbo.sp_add_jobschedule @job_name=N'Limpa Agenda', @name=N'Segunda', 
		@enabled=1, 
		@freq_type=8, 
		@freq_interval=2, 
		@freq_subday_type=1, 
		@freq_subday_interval=0, 
		@freq_relative_interval=0, 
		@freq_recurrence_factor=1, 
		@active_start_date=20190602, 
		@active_end_date=99991231, 
		@active_start_time=0, 
		@active_end_time=235959, @schedule_id = @schedule_id OUTPUT
select @schedule_id
GO
*/