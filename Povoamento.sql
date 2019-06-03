USE MrVeggie;


--INSERT INTO Utilizador VALUES ('Pedro', 'pedro@gmail.com', '123', 20, 'Masculino', 0, 0);

INSERT INTO Ingrediente VALUES ('Alface', 'https://i.imgur.com/0Cpru8Y.jpg'), 
								('Cogumelos', 'https://i.imgur.com/kBdqmlk.jpg'), 
								('Soja', 'https://i.imgur.com/5ckWIbE.jpg'),
								('Azeite', 'https://www.celeiro.pt/media/contentmanager/content/cache/400x400/cuide-de-si/temas-de-saude/azeite.jpg'),
								('Alho', 'http://clinicaexvitam.com/wp-content/uploads/2017/11/alho.jpg'),
								('Cebola', 'https://www.mulherportuguesa.com/wp-content/uploads/2000/09/Cebola.jpg'),
								('Tomate de conserva em cubos', 'https://www.mendesgoncalves.pt/mg/sites/default/files/tomate-em-cubos_0.jpg'),
								('Cominhos', 'https://bs.simplusmedia.com/i/730/8502/cominho.jpg'),
								('Lentilhas vermelhas', 'https://i.imgur.com/3iauBsn.jpg'),
								('Agua', 'https://www.diariodoscampos.com.br/fl/normal/cdxaguacloro.jpg'),
								('Sal', 'https://nit.pt/wp-content/uploads/2017/05/05e4cf0d23f0c0676ac55b2de1ff40df-754x394.jpg'),
								('Espinafres em folha', 'https://www.biocabaz.pt/web/wp-conteudos/uploads/2016/04/Espinafre-1.jpg'),
								('Manjericão', 'https://media-manager.noticiasaominuto.com/naom_56d59237c74aa.jpg?&w=1920&v=1503960052'),
								('Iogurte Grego', 'https://cdn1.medicalnewstoday.com/content/images/articles/323/323169/greek-yoghurt-in-bowl.jpg'), --14
								('Espargos' , 'https://i.imgur.com/YXYMxhL.jpg'),
								('Pimenta', 'https://i.imgur.com/1ivRhM4.jpg'),
								('Massa Folhada', 'https://i.imgur.com/HGEZuub.jpg'),
								('Queijo', 'https://i.imgur.com/JTxNJBM.jpg'),
								('Ovo', 'https://i.imgur.com/27o8bCC.jpg'),
								('Mel' , 'https://i.imgur.com/MUHyCFD.jpg'),
								('Tomilho', 'https://i.imgur.com/5C2rnUo.jpg')
								;

select * from Receita;
INSERT INTO Receita VALUES ('Lasanha', 'lasanha asdfsadf', 40, 4, 2, 1, 200, 2, 'https://www.pingodoce.pt/wp-content/uploads/2016/07/lasanhadelegumes617.jpg'), 
						   ('Sopa de tomate com lentilhas', 'Esta sopa de tomate com lentilhas vermelhas e espinafres é uma excelente forma de começar a sua refeição. Sirva com folhas de manjericão e iogurte grego.', 
							 25, 0, 0, 1, 157, 6, 'https://www.pingodoce.pt/wp-content/uploads/2019/01/sopa-tomate-com-lentilhas.jpg'),
						   ('Picar Cebola', 'Tutorial de como picar facilmente uma cebola', 2, 0, 0, 1, 0, 0, 'https://i.imgur.com/wTllztN.jpg'),
						   ('Laços folhados com espargos', 'Uma receita rápida para servir como entrada ou para uma refeição leve. Os laços folhados com espargos frescos, acompanham com um molho de mel e tomilho fresco.' ,30, 0, 0, 1, 321, 8,'https://i.imgur.com/NTT8iV8.jpg')
						   ;

INSERT INTO Operacao VALUES('Teste'),
						   ('Aquecer'),
						   ('Cozinhar'),
						   ('Juntar'),
						   ('Picar'),
						   ('Triturar'),
						   ('Deixar Ferver'),
						   ('Descascar'),
						   ('Cortar'),
						   ('Temperar'),
						   ('Fechar'),
						   ('Pincelar'),
						   ('Levar ao Forno')
						   ;

select * from Passo;
INSERT INTO Passo VALUES (1, 'DESC', 20, 1, 1, null, 0), --1
						 (2, 'DESC', 25, 1, 1, null, 1), --2
						 (1, 'Aqueça o azeite numa panela.', 0, 2, 2, null, 0), --3
						 (2, 'Junte uma cebola picada e dois dentes de alho.', 0, 4, 2, 3, 0), --4 juntar sub receita pica
						 (3, 'Deixe cozinhar em lume moderado até a cebola estar mole.', 0, 3, 2, null, 0),--5
						 (4, 'Junte a lata de tomate e os cominhos.', 0, 4, 2, null, 0),--6
						 (5, 'Deixe cozinhar durante 8 minutos, mexendo de vez em quando.', 8, 3, 2, null, 0),--7
						 (6, 'Adicione a água a ferver e o sal.', 0, 4, 2, null, 0),--8
						 (7, 'Deixe ferver.', 0, 7, 2, null, 0),--9
						 (8, 'Introduza as lentilhas, previamente passadas por àgua.', 0, 4, 2, null, 0),--10
						 (9, 'Deixe cozer durante mais 15 minutos.', 15, 3, 2, null, 0),--11
						 (10, 'Triture muito bem a sopa.', 0, 6, 2, null, 0),--12
						 (11, 'Junte as folhas de espinafres e de manjericão e mexa.', 0, 4, 2, null, 1),--13
						 (1, 'Descasque a cebola.', 0, 8, 3, null, 0),
						 (2, 'Corte a cebola ao meio.', 0, 9, 3, null, 0),
						 (3, 'Apoie a parte maior da cebola numa tabua, e faça varios golpes na vertical, sem cortar totalmente a cebola.', 0 , 9, 3, null ,0),
						 (4, 'Rode a cebola 90 graus e corte a mesma varias vezes na vertical.', 0, 9, 3, null, 0),
						 (5, 'Repita o processo para a outra metade.' , 0, 9, 3,null, 1),
						 (1, 'Pré-aqueça o forno a 200 graus.', 0, 2, 4, null, 0), --19
						 (2, 'Corte os pés aos espargos, e coloque-os numa taça.', 0, 9, 4,null, 0),
						 (3, 'Tempere com sal, pimenta, azeite e misture.', 0, 10, 4, null, 0),
						 (4, 'Disponha os quadrados de massa folhada sobre um tabuleiro de forno forrado com papel vegetal.', 0, 4, 4, null, 0),
						 (5, 'Coloque uma fatia de queijo brie sobre cada quadrado de massa e coloque por cima três espargos.', 0 , 4, 4, null, 0),
						 (6, 'Feche os quadrados de massa, unindo duas pontas contrárias.', 0, 11, 4, null, 0),
						 (7, 'Pincele com o ovo batido', 0, 12, 4, null, 0),
						 (8, 'Leve ao forno cerca de 12 minutos, ou até estarem dourados.', 12, 13, 4, null, 0),
						 (9, 'Regue com o mel e polvilhe com o tomilho.', 0, 4, 4, null, 1)
						 ;

INSERT INTO Unidade VALUES ('gramas'), ('colher(es) de sopa'), ('unidades'), ('lata'), ('colher(es) de cafe'), ('litros'),('colher(es) de sobremesa'), ('molhos'), ('quadrados'), ('fatias');
select * from ingrediente;
select * from IngredientesPasso;
INSERT INTO IngredientesPasso VALUES (1, 1, 10, 1), 
									 (1, 2, 20, 1), 
									 (2, 3, 1, 2),
									 (2, 1, 30, 1),
									 (3, 4, 2, 2),
									 (4, 6, 1, 3),
									 (4, 5, 2, 3),
									 (6, 7, 1, 4),
									 (6, 8, 0.5, 5),
									 (8, 10, 1, 6),
									 (8, 11, 1, 7),
									 (10, 9, 150, 1),
									 (13, 12, 10, 3),
									 (13, 13, 5, 3),
									 (14, 6, 1, 3),
									 (20, 15, 2, 8),
									 (21, 11, 1, 1),
									 (21, 4, 1, 2),
									 (21, 18, 1, 5), 
									 (22, 16, 8, 9),
									 (23, 17, 8, 10),
									 (25, 19, 1, 3),
									 (27, 20, 1, 1),
									 (27, 21, 1, 1)
									 ;


--INSERT INTO UtilizadorIngredientesPref VALUES (2, 1);
--INSERT INTO UtilizadorReceitasPref VALUES (2, 2);
--INSERT INTO UtilizadorReceitasPref VALUES (3, 1);
--INSERT INTO UtilizadorReceitasPref VALUES (3, 2);
--INSERT INTO UtilizadorReceitasPref VALUES (5, 2);

--INSERT INTO HistoricoUtilizador VALUES (2, 2, '20120618 10:34:09', 4);
SELECT * FROM HistoricoUtilizador;
--INSERT INTO HistoricoUtilizador VALUES (2, 2, '20120618 12:34:09', 0);


--INSERT INTO Agenda VALUES(1, 'a', 2, 2);

INSERT INTO Utensilio VALUES ('Colher', 'url'), ('Fogão', 'url'), ('Forno','url'),('Faca','url'),('Tabua de cozinha', 'url'), ('Panela', 'url'), ('Pincel', 'url');
select * from UtensiliosReceita;
INSERT INTO UtensiliosReceita VALUES (1, 1),
									 (2, 2),
									 (2, 6),
									 (2, 5),
									 (2, 4),
									 (3, 4),
									 (3, 5),
									 (4, 3),
									 (4, 7);

Insert into Utilizador Values('Admin','admin@admin.com', '202cb962ac59075b964b07152d234b70', 21, 'Masculino', 1, 0, GETDATE());
SELECT * FROM Utilizador;

UPDATE Utilizador
SET admin = 0
WHERE email = 'pedri.nho.moreira.19@gmail.com';

SELECT * FROM Ingrediente;
SELECT * FROM Receita;
SELECT * FROM Operacao;
SELECT * FROM Passo;

--ALTER TABLE HistoricoUtilizador
--ALTER COLUMN avaliacao int not null;

--DELETE FROM HistoricoUtilizador
--WHERE avaliacao IS null;

SELECT * FROM UtilizadorIngredientesPref;

SELECT * FROM Agenda;

SELECT * FROM UtilizadorReceitasPref;

--DELETE FROM Agenda
--WHERE utilizador > 0;

--DELETE FROM Receita
--WHERE id_receita > 2;

SELECT * FROM UtensiliosReceita;

--DELETE FROM UtensiliosReceita
--WHERE receita > 1;

SELECT * FROM HistoricoUtilizador;