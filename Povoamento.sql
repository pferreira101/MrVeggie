USE MrVeggie;


INSERT INTO Utilizador VALUES ('Pedro', 'pedro@gmail.com', '123', 20, 'M');

INSERT INTO Ingrediente VALUES ('Alface', 10, 'teste'), 
								('Cogumelos', 10, 'teste'), 
								('Soja', 20, 'teste'),
								('Azeite', 0, 'https://www.celeiro.pt/media/contentmanager/content/cache/400x400/cuide-de-si/temas-de-saude/azeite.jpg'),
								('Alho',0,'http://clinicaexvitam.com/wp-content/uploads/2017/11/alho.jpg'),
								('Cebola',0,'https://www.mulherportuguesa.com/wp-content/uploads/2000/09/Cebola.jpg'),
								('Tomate de conserva em cubos',0,'https://www.mendesgoncalves.pt/mg/sites/default/files/tomate-em-cubos_0.jpg'),
								('Cominhos',0,'https://bs.simplusmedia.com/i/730/8502/cominho.jpg'),
								('Lentilhas vermelhas', 10, 'https://static-socorronacozinha.gcampaner.com.br/wp-content/uploads/2016/02/lentilhas-vermelhas-1024x573.jpg?x18500'),
								('Agua',0,'https://www.diariodoscampos.com.br/fl/normal/cdxaguacloro.jpg'),
								('Sal',0,'https://nit.pt/wp-content/uploads/2017/05/05e4cf0d23f0c0676ac55b2de1ff40df-754x394.jpg'),
								('Espinafres em folha',0,'https://www.biocabaz.pt/web/wp-conteudos/uploads/2016/04/Espinafre-1.jpg'),
								('Manjericão',0,'https://media-manager.noticiasaominuto.com/naom_56d59237c74aa.jpg?&w=1920&v=1503960052'),
								('Iogurte Grego',0,'https://cdn1.medicalnewstoday.com/content/images/articles/323/323169/greek-yoghurt-in-bowl.jpg');


INSERT INTO Receita VALUES ('Lasanha', 'lasanha asdfsadf', 40, 4, 2, 1, 200, 2, 'teste'), 
							('Sopa de tomate com lentilhas', 'Esta sopa de tomate com lentilhas vermelhas e espinafres é uma excelente forma de começar a sua refeição. Sirva com folhas de manjericão e iogurte grego.', 
							 25, 0, 0, 1, 157, 6, 'https://www.pingodoce.pt/wp-content/uploads/2019/01/sopa-tomate-com-lentilhas.jpg');

INSERT INTO Operacao VALUES('Teste'),
						   ('Aquecer'),
						   ('Cozinhar'),
						   ('Juntar'),
						   ('Picar'),
						   ('Triturar'),
						   ('Deixar Ferver');


INSERT INTO Passo VALUES (1, 20, 1, 1, null);
INSERT INTO Passo VALUES (2, 25, 1, 1, null),
						 (1, 0, 2, 2, null),
						 (2, 0, 5, 2, null),
						 (3, 0, 4, 2, null),
						 (4, 2, 3, 2, null),
						 (5, 0, 4, 2, null),
						 (6, 8, 3, 2, null),
						 (7, 0, 4, 2, null),
						 (8, 0, 7, 2, null),
						 (9, 0, 4, 2, null),
						 (10, 15, 3, 2, null),
						 (11, 0, 6, 2, null),
						 (12, 0, 4, 2, null)
						 ;

INSERT INTO IngredientesPasso VALUES (1, 1, 10, 'teste'), 
									 (1, 2, 20, 'teste'), 
									 (2, 3, 1, 'teste'), 
									 (2, 1, 30, 'teste'),
									 (2, 1, 30, 'teste'),
									 (3, 4, 2, 'colheres de sopa'),
									 (4, 6, 1, 'unidade'),
									 (5, 6, 1, 'unidade'),
									 (5, 5, 2, 'dentes'),
									 (7, 1, 1, 'lata'),
									 (7, 1, 0.5, 'colher de café'),
									 (9, 10, 1, 'litro'),
									 (9, 11, 1, 'colher de sobremesa'),
									 (11, 9, 150, 'gramas'),
									 (14, 12, 0, 'qb'),
									 (14, 13, 0, 'qb')
									 ;

SELECT * FROM Utilizador;
SELECT * FROM Ingrediente;
SELECT * FROM Receita;
SELECT * FROM Operacao;
SELECT * FROM Passo;