USE MrVeggie;


INSERT INTO Utilizador VALUES ('Pedro', 'pedro@gmail.com', '123', 20, 'M');

INSERT INTO Ingrediente VALUES ('Alface', 'teste'), 
								('Cogumelos', 'teste'), 
								('Soja', 'teste'),
								('Azeite', 'https://www.celeiro.pt/media/contentmanager/content/cache/400x400/cuide-de-si/temas-de-saude/azeite.jpg'),
								('Alho', 'http://clinicaexvitam.com/wp-content/uploads/2017/11/alho.jpg'),
								('Cebola', 'https://www.mulherportuguesa.com/wp-content/uploads/2000/09/Cebola.jpg'),
								('Tomate de conserva em cubos', 'https://www.mendesgoncalves.pt/mg/sites/default/files/tomate-em-cubos_0.jpg'),
								('Cominhos', 'https://bs.simplusmedia.com/i/730/8502/cominho.jpg'),
								('Lentilhas vermelhas', 'https://static-socorronacozinha.gcampaner.com.br/wp-content/uploads/2016/02/lentilhas-vermelhas-1024x573.jpg?x18500'),
								('Agua', 'https://www.diariodoscampos.com.br/fl/normal/cdxaguacloro.jpg'),
								('Sal', 'https://nit.pt/wp-content/uploads/2017/05/05e4cf0d23f0c0676ac55b2de1ff40df-754x394.jpg'),
								('Espinafres em folha', 'https://www.biocabaz.pt/web/wp-conteudos/uploads/2016/04/Espinafre-1.jpg'),
								('Manjericão', 'https://media-manager.noticiasaominuto.com/naom_56d59237c74aa.jpg?&w=1920&v=1503960052'),
								('Iogurte Grego', 'https://cdn1.medicalnewstoday.com/content/images/articles/323/323169/greek-yoghurt-in-bowl.jpg');


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


INSERT INTO Passo VALUES (1, 'DESC', 20, 1, 1, null, 0); --1
INSERT INTO Passo VALUES (2, 'DESC', 25, 1, 1, null, 1), --2
						 (1, 'DESC', 0, 2, 2, null, 0), --3
						 (2, 'DESC', 0, 5, 2, null, 0), --4
						 (3, 'DESC', 0, 4, 2, null, 0),--5
						 (4, 'DESC', 2, 3, 2, null, 0),--6
						 (5, 'DESC', 0, 4, 2, null, 0),--7
						 (6, 'DESC', 8, 3, 2, null, 0),--8
						 (7, 'DESC', 0, 4, 2, null, 0),--9
						 (8, 'DESC', 0, 7, 2, null, 0),--10
						 (9, 'DESC', 0, 4, 2, null, 0),--11
						 (10, 'DESC', 15, 3, 2, null, 0),--12
						 (11, 'DESC', 0, 6, 2, null, 0),--13
						 (12, 'DESC', 0, 4, 2, null, 1)--14
						 ;

INSERT INTO Unidade VALUES ('gramas'), ('colher(es) de sopa');

INSERT INTO IngredientesPasso VALUES (1, 1, 10, 1), 
									 (1, 2, 20, 1), 
									 (2, 3, 1, 2),
									 (2, 1, 30, 1),
									 (3, 4, 2, 2),
									 (4, 6, 1, 1),
									 (5, 6, 1, 1),
									 (5, 5, 2, 1),
									 (7, 1, 1, 1),
									 (7, 2, 0.5, 1),
									 (9, 10, 1, 2),
									 (9, 11, 1, 2),
									 (11, 9, 150, 2),
									 (14, 12, 0, 2),
									 (14, 13, 0, 2)
									 ;



INSERT INTO Utensilio VALUES ('Colher', 'url');

INSERT INTO UtensiliosReceita VALUES (1, 1);

SELECT * FROM Utilizador;
SELECT * FROM Ingrediente;
SELECT * FROM Receita;
SELECT * FROM Operacao;
SELECT * FROM Passo;