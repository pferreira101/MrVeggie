USE MrVeggie;


INSERT INTO Utilizador VALUES ('Pedro', 'pedro@gmail.com', '123', 20, 'M');

INSERT INTO Ingrediente VALUES ('Alface', 10, 'teste'), ('Cogumelos', 10, 'teste'), ('Lentilhas', 10, 'teste') , ('Soja', 20, 'teste');

INSERT INTO Receita VALUES ('Lasanha', 'lasanha asdfsadf', 40, 4, 2, 1, 200, 2, 'teste');

INSERT INTO Operacao VALUES('Teste');

INSERT INTO Passo VALUES (1, 20, 1, 1, null);
INSERT INTO Passo VALUES (2, 25, 1, 1, null);

INSERT INTO IngredientesPasso VALUES (1, 1, 10), (1, 2, 20), (2, 3, 1), (2, 1, 30);

SELECT * FROM Utilizador;
SELECT * FROM Ingrediente;
SELECT * FROM Receita;
SELECT * FROM Operacao;
SELECT * FROM Passo;