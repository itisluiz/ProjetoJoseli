-- TipoQuarto
INSERT INTO TipoQuarto(descricao_tipoquarto) VALUES ('Solteiro'); -- 1
INSERT INTO TipoQuarto(descricao_tipoquarto) VALUES ('Casal'); -- 2
INSERT INTO TipoQuarto(descricao_tipoquarto) VALUES ('Família'); -- 3
INSERT INTO TipoQuarto(descricao_tipoquarto) VALUES ('Presidencial'); -- 4

-- TipoPagamento
INSERT INTO TipoPagamento(descricao_tipopagamento) VALUES ('Dinheiro'); -- 1
INSERT INTO TipoPagamento(descricao_tipopagamento) VALUES ('PIX'); -- 2
INSERT INTO TipoPagamento(descricao_tipopagamento) VALUES ('Cartão de Crédito'); -- 3
INSERT INTO TipoPagamento(descricao_tipopagamento) VALUES ('Cartão de Débito'); -- 4

-- TipoFuncionario
INSERT INTO TipoFuncionario(descricao_tipofuncionario) VALUES ('Atendente'); -- 1
INSERT INTO TipoFuncionario(descricao_tipofuncionario) VALUES ('Gerente'); -- 2
INSERT INTO TipoFuncionario(descricao_tipofuncionario) VALUES ('Diretor'); -- 3
INSERT INTO TipoFuncionario(descricao_tipofuncionario) VALUES ('Lavanderia'); -- 4
INSERT INTO TipoFuncionario(descricao_tipofuncionario) VALUES ('Cozinha'); -- 5
INSERT INTO TipoFuncionario(descricao_tipofuncionario) VALUES ('Contabilidade'); -- 6
INSERT INTO TipoFuncionario(descricao_tipofuncionario) VALUES ('Faxina'); -- 7

-- ServicoLavanderia
INSERT INTO ServicoLavanderia(descricao_servicolavanderia, custo_servicolavanderia) VALUES ('Lavar e passar terno', 50.00); -- 1
INSERT INTO ServicoLavanderia(descricao_servicolavanderia, custo_servicolavanderia) VALUES ('Lavar e passar vestido', 40.00); -- 2
INSERT INTO ServicoLavanderia(descricao_servicolavanderia, custo_servicolavanderia) VALUES ('Lavar e passar camisa social', 20.00); -- 3

-- Nacionalidade
INSERT INTO Nacionalidade(pais_nacionalidade, titulo_nacionalidade, sigla_nacionalidade) VALUES ('Brasil', 'Brasileiro', 'BR'); -- 1
INSERT INTO Nacionalidade(pais_nacionalidade, titulo_nacionalidade, sigla_nacionalidade) VALUES ('Estados-Unidos', 'Estadunidense', 'US'); -- 2
INSERT INTO Nacionalidade(pais_nacionalidade, titulo_nacionalidade, sigla_nacionalidade) VALUES ('Argentina', 'Argentino', 'AR'); -- 3

-- Quarto
INSERT INTO Quarto(numero_quarto, cod_tipoquarto, capacidade_quarto) VALUES (100, 1, 1); -- 1
INSERT INTO Quarto(numero_quarto, cod_tipoquarto, capacidade_quarto) VALUES (101, 2, 2); -- 2
INSERT INTO Quarto(numero_quarto, cod_tipoquarto, capacidade_quarto) VALUES (201, 3, 4); -- 3

-- Filial
INSERT INTO Filial(nome_filial, endereco_filial, estrelas_filial) VALUES ('Filial A', 'Rua A', 4.7); -- 1
INSERT INTO Filial(nome_filial, endereco_filial, estrelas_filial) VALUES ('Filial B', 'Rua B', 4.5); -- 2
INSERT INTO Filial(nome_filial, endereco_filial, estrelas_filial) VALUES ('Filial C', 'Rua C', 3.0); -- 3

-- Quarto_Filial
INSERT INTO Quarto_Filial(cod_filial, cod_tipoquarto, quantidade_quarto_filial) VALUES (1, 1, 10); -- 1
INSERT INTO Quarto_Filial(cod_filial, cod_tipoquarto, quantidade_quarto_filial) VALUES (1, 2, 6); -- 2
INSERT INTO Quarto_Filial(cod_filial, cod_tipoquarto, quantidade_quarto_filial) VALUES (1, 4, 4) -- 3

INSERT INTO Quarto_Filial(cod_filial, cod_tipoquarto, quantidade_quarto_filial) VALUES (2, 1, 20); -- 4
INSERT INTO Quarto_Filial(cod_filial, cod_tipoquarto, quantidade_quarto_filial) VALUES (2, 2, 50); -- 5
INSERT INTO Quarto_Filial(cod_filial, cod_tipoquarto, quantidade_quarto_filial) VALUES (2, 3, 10); -- 6
INSERT INTO Quarto_Filial(cod_filial, cod_tipoquarto, quantidade_quarto_filial) VALUES (2, 4, 10); -- 7

INSERT INTO Quarto_Filial(cod_filial, cod_tipoquarto, quantidade_quarto_filial) VALUES (3, 1, 15); -- 8
INSERT INTO Quarto_Filial(cod_filial, cod_tipoquarto, quantidade_quarto_filial) VALUES (3, 2, 15); -- 9

-- Funcionario
INSERT INTO Funcionario(cod_tipofuncionario, nome_funcionario, endereco_funcionario, email_funcionario) VALUES (1, 'Funcionario A', 'Rua A', 'funcionarioa@email.com'); -- 1
INSERT INTO Funcionario(cod_tipofuncionario, nome_funcionario, endereco_funcionario, email_funcionario) VALUES (5, 'Funcionario B', 'Rua B', 'funcionariob@email.com'); -- 2
INSERT INTO Funcionario(cod_tipofuncionario, nome_funcionario, endereco_funcionario, email_funcionario) VALUES (3, 'Funcionario C', 'Rua C', 'funcionarioc@email.com'); -- 3

-- Cliente
INSERT INTO Cliente(cod_nacionalidade, nome_cliente, endereco_cliente, email_cliente) VALUES (1, 'Cliente A', 'Rua A', 'clientea@email.com'); -- 1
INSERT INTO Cliente(cod_nacionalidade, nome_cliente, endereco_cliente, email_cliente) VALUES (2, 'Cliente B', 'Rua B', 'clienteb@email.com'); -- 2
INSERT INTO Cliente(cod_nacionalidade, nome_cliente, endereco_cliente, email_cliente) VALUES (3, 'Cliente C', 'Rua C', 'clientec@email.com'); -- 3

-- Telefone
INSERT INTO Telefone(numero_telefone, cod_proprietario, tipoproprietario_telefone) VALUES ('111111111', 1, 'F'); -- 1
INSERT INTO Telefone(numero_telefone, cod_proprietario, tipoproprietario_telefone) VALUES ('222222222', 2, 'F'); -- 2
INSERT INTO Telefone(numero_telefone, cod_proprietario, tipoproprietario_telefone) VALUES ('333333333', 3, 'F'); -- 3
INSERT INTO Telefone(numero_telefone, cod_proprietario, tipoproprietario_telefone) VALUES ('444444444', 1, 'C'); -- 4
INSERT INTO Telefone(numero_telefone, cod_proprietario, tipoproprietario_telefone) VALUES ('555555555', 2, 'C'); -- 5
INSERT INTO Telefone(numero_telefone, cod_proprietario, tipoproprietario_telefone) VALUES ('666666666', 3, 'C'); -- 6

-- Reserva
INSERT INTO Reserva(cod_quarto, cod_cliente, cod_funcionario, colchao_reserva, dataprevista_reserva) VALUES (1, 1, 1, 0, '2024-06-01'); -- 1
INSERT INTO Reserva(cod_quarto, cod_cliente, cod_funcionario, colchao_reserva, dataprevista_reserva) VALUES (2, 2, 1, 0, '2024-07-01'); -- 2
INSERT INTO Reserva(cod_quarto, cod_cliente, cod_funcionario, colchao_reserva, dataprevista_reserva) VALUES (3, 3, 1, 1, '2024-08-01'); -- 3

-- Estadia
INSERT INTO Estadia(cod_reserva, datacheckout_estadia) VALUES (1, '2024-06-03'); -- 1
INSERT INTO Estadia(cod_reserva, datacheckout_estadia) VALUES (2, '2024-07-03'); -- 2
INSERT INTO Estadia(cod_reserva, datacheckout_estadia) VALUES (3, '2024-08-03'); -- 3

-- ConsumoRefeicao
INSERT INTO ConsumoRefeicao(cod_estadia, entreguequarto_consumorefeicao, descricao_consumorefeicao, custo_consumorefeicao) VALUES (1, 0, 'Café da manhã básico', 12.00); -- 1
INSERT INTO ConsumoRefeicao(cod_estadia, entreguequarto_consumorefeicao, descricao_consumorefeicao, custo_consumorefeicao) VALUES (1, 0, 'Almoço básico', 18.00); -- 2
INSERT INTO ConsumoRefeicao(cod_estadia, entreguequarto_consumorefeicao, descricao_consumorefeicao, custo_consumorefeicao) VALUES (2, 0, 'Almoço básico', 18.00); -- 3
INSERT INTO ConsumoRefeicao(cod_estadia, entreguequarto_consumorefeicao, descricao_consumorefeicao, custo_consumorefeicao) VALUES (3, 1, 'Vinho tinto', 86.00); -- 4

-- ConsumoLavanderia
INSERT INTO ConsumoLavanderia(cod_estadia, cod_servicolavanderia) VALUES (1, 1); -- 1
INSERT INTO ConsumoLavanderia(cod_estadia, cod_servicolavanderia) VALUES (1, 1); -- 2
INSERT INTO ConsumoLavanderia(cod_estadia, cod_servicolavanderia) VALUES (2, 2); -- 3
INSERT INTO ConsumoLavanderia(cod_estadia, cod_servicolavanderia) VALUES (3, 2); -- 4
INSERT INTO ConsumoLavanderia(cod_estadia, cod_servicolavanderia) VALUES (3, 3); -- 5

-- NotaFiscal
INSERT INTO NotaFiscal(chavenfe_notafiscal, cod_tipopagamento, cod_estadia) VALUES ('AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA', 3, 1); -- 1
INSERT INTO NotaFiscal(chavenfe_notafiscal, cod_tipopagamento, cod_estadia) VALUES ('BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB', 2, 2); -- 2
INSERT INTO NotaFiscal(chavenfe_notafiscal, cod_tipopagamento, cod_estadia) VALUES ('CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC', 1, 3); -- 3
