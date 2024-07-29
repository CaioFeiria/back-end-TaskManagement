USE TaskManagement;

SELECT * FROM Tarefas;
SELECT * FROM Usuarios;

INSERT INTO Usuarios(nome, email, senha, cargo) 
VALUES ('Caio', 'caio@email.com', '12345pass', 'Desenvolvedor'),
	   ('Zequinha', 'zequinha@email.com', 'defh2ei', 'Suporte'),
	   ('Bruna', 'bruna@email.com', '3e29d8h3www', 'Financeiro');

INSERT INTO Tarefas(titulo, descricao, prazo, prioridade, estado, id_responsavel)
VALUES ('Conta Dae', 'Pagar conta de agua', '2024-07-03', 1, 0, 3),
	   ('Verificar erro', 'Erro da empresa McDonalds, no programa de cardapio', '2024-06-28', 1, 0, 2);

SELECT nome, email, senha, cargo FROM Usuarios;

UPDATE Usuarios 
SET nome = 'Carlos', email = 'carlos@email.com', senha = 'nova_senha', cargo = 'novo_cargo'
WHERE id = 2;
