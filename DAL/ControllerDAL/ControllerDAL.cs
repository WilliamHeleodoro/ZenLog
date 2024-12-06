using Dapper;
using Dapper.Contrib.Extensions;
using ZenLog.Conexoes;
using ZenLog.Models.ModelsAPI.ModelsRetornoAPI;
using ZenLog.Models.ModelsController.ModelsDiversasController;
using ZenLog.Models.ModelsController.ModelsEntradaController;
using ZenLog.Models.ModelsController.ModelsIntegracaoController;
using ZenLog.Models.ModelsController.ModelsMaterialController;
using ZenLog.Models.ModelsController.ModelsPedidoController;

namespace ZenLog.DAL.ControllerDAL
{
    public class ControllerDAL
    {
        public async Task<List<PedidoController>> BuscarPedidos(long codigoEmpresa)
        {
            try
            {
                using (var conexao = new ConexaoSQL())
                {
                    conexao.Conectar();

                    var sql = @"SELECT 
                            	 PEDIDO.*, PEDIDO_ENVIO.CD_ID AS CD_ID_PEDIDO_INTEGRACAO, CLIENTE.NR_CPFCNPJ AS NR_CPFCNPJ_CLIENTE, CLIENTE.DS_ENTIDADE AS DS_RAZAOSOCIAL_CLIENTE
										 , ESTADO_CLIENTE.DS_UF AS DS_UF_CLIENTE, CIDADE_CLIENTE.CD_MUNIBGE AS CD_MUNIBGE_CLIENTE
										 , CLIENTE.NR_CEP AS NR_CEP_CLIENTE, CLIENTE.DS_BAIRRO AS DS_BAIRRO_CLIENTE
										 , CLIENTE.NR_NUMERO AS NR_NUMERO_CLIENTE, CLIENTE.DS_ENDERECO AS DS_ENDERECO_CLIENTE
										 , CLIENTE.DS_COMPLEMENTO AS DS_COMPLEMENTO_CLIENTE, TRANSPORTADOR.NR_CPFCNPJ AS NR_CPFCNPJ_TRANSPORTADOR
										 ,EMPRESA.NR_IE AS NR_IE_EMPRESA
                            	,SERVICOS.*, SERVICOS.CD_SERVICO AS CD_MATERIAL , SERVICOS.DS_SERVICO AS DS_MATERIAL
                            	,ITENS.*
                            	,PARCELAS.*
                            FROM TBL_VENDAS_PEDIDO PEDIDO
                            INNER JOIN TBL_PEDIDOS_INTEGRACAO_ENVIO PEDIDO_ENVIO   ON PEDIDO_ENVIO.CD_PEDIDO	     = PEDIDO.CD_PEDIDO
                            LEFT JOIN TBL_VENDAS_PEDIDO_SERVICOS	SERVICOS       ON SERVICOS.CD_PEDIDO		     = PEDIDO.CD_PEDIDO
                            LEFT JOIN TBL_VENDAS_PEDIDO_ITENS	    ITENS		   ON ITENS.CD_PEDIDO			     = PEDIDO.CD_PEDIDO
                            LEFT JOIN TBL_VENDAS_PEDIDO_PARCELAS	PARCELAS	   ON PARCELAS.CD_PEDIDO		     = PEDIDO.CD_PEDIDO
                            LEFT JOIN TBL_ENTIDADES                 EMPRESA        ON EMPRESA.CD_ENTIDADE		     = PEDIDO.CD_EMPRESA
							LEFT JOIN TBL_ENTIDADES                 CLIENTE        ON CLIENTE.CD_ENTIDADE		     = PEDIDO.CD_CLIENTE
                            LEFT JOIN TBL_ENTIDADES				    TRANSPORTADOR  ON TRANSPORTADOR.CD_TRANSPORTADOR = PEDIDO.CD_TRANSPORTADOR
							LEFT JOIN TBL_CEP_CIDADE                CIDADE_CLIENTE ON CIDADE_CLIENTE.CD_CIDADE       = CLIENTE.CD_CIDADE
							LEFT JOIN TBL_CEP_ESTADO                ESTADO_CLIENTE ON ESTADO_CLIENTE.CD_ESTADO       = CIDADE_CLIENTE.CD_ESTADO
                            WHERE PEDIDO_ENVIO.X_ENVIADO = 0 AND PEDIDO.CD_EMPRESA = @CD_EMPRESA AND PEDIDO_ENVIO.DS_INTEGRACAO = 'ZENLOG'";

                    var parametros = new { @CD_EMPRESA = codigoEmpresa };

                    var pedidoDict = new Dictionary<long, PedidoController>();

                    var result = await conexao.ObjetoConexao.QueryAsync<PedidoController, PedidoItensController, PedidoItensController, PedidoParcelasController, PedidoController>(
                        sql,
                        (pedido, servico, itens, parcela) =>
                        {
                            // Verifica se o pedido já está no dicionário
                            if (!pedidoDict.TryGetValue(pedido.CD_PEDIDO, out var pedidoEntry))
                            {
                                pedidoEntry = pedido;
                                pedidoEntry.servicosController = new List<PedidoItensController>();
                                pedidoEntry.itensController = new List<PedidoItensController>();
                                pedidoEntry.parcelasController = new List<PedidoParcelasController>();
                                pedidoDict.Add(pedido.CD_PEDIDO, pedidoEntry);
                            }

                            // Adiciona o item de serviço sem duplicar o pedido
                            if (servico != null && pedidoEntry.servicosController != null && !pedidoEntry.servicosController.Any(s => s.CD_ITEM == servico.CD_ITEM))
                            {
                                pedidoEntry.servicosController.Add(servico);
                            }

                            // Adiciona o item de material sem duplicar o pedido
                            if (itens != null && pedidoEntry.itensController != null && !pedidoEntry.itensController.Any(s => s.CD_ITEM == itens.CD_ITEM))
                            {
                                pedidoEntry.itensController.Add(itens);
                            }


                            // Adiciona o item de material sem duplicar o pedido
                            if (parcela != null && pedidoEntry.parcelasController != null && !pedidoEntry.parcelasController.Any(s => s.NR_PARCELA == parcela.NR_PARCELA))
                            {
                                pedidoEntry.parcelasController.Add(parcela);
                            }



                            return pedidoEntry;
                        },
                        parametros,
                        splitOn: "CD_PEDIDO"
                    );

                    return result.Distinct().ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível buscar os pedidos para integração " + ex.Message);
            }
        }

        public async Task<List<EntradaController>> BuscarEntradas(long codigoEmpresa)
        {
            try
            {
                using (var conexao = new ConexaoSQL())
                {
                    conexao.Conectar();

                    var sql = @"SELECT 
                            	 ENTRADA.*, ENTRADA_ENVIO.CD_ID AS CD_ID_ENTRADA_INTEGRACAO, FORNECEDOR.NR_CPFCNPJ AS NR_CPFCNPJ_FORNECEDOR, FORNECEDOR.DS_ENTIDADE AS DS_RAZAOSOCIAL_FORNECEDOR
										 , ESTADO_FORNECEDOR.DS_UF AS DS_UF_FORNECEDOR, CIDADE_FORNECEDOR.CD_MUNIBGE AS CD_MUNIBGE_FORNECEDOR
										 , FORNECEDOR.NR_CEP AS NR_CEP_FORNECEDOR, FORNECEDOR.DS_BAIRRO AS DS_BAIRRO_FORNECEDOR
										 , FORNECEDOR.NR_NUMERO AS NR_NUMERO_FORNECEDOR, FORNECEDOR.DS_ENDERECO AS DS_ENDERECO_FORNECEDOR
										 , FORNECEDOR.DS_COMPLEMENTO AS DS_COMPLEMENTO_FORNECEDOR, TRANSPORTADOR.NR_CPFCNPJ AS NR_CPFCNPJ_TRANSPORTADOR
										 , FORNECEDOR.NR_TELEFONE AS NR_TELEFONE_FORNECEDOR, FORNECEDOR.NR_IE AS NR_IE_FORNECEDOR, EMPRESA.NR_IE AS NR_IE_EMPRESA
                            	,ITENS.*
                            FROM TBL_NOTAFISCAL_ENTRADA				 ENTRADA
                            INNER JOIN TBL_ENTRADAS_INTEGRACAO_ENVIO ENTRADA_ENVIO     ON ENTRADA_ENVIO.CD_ENTRADA	     = ENTRADA.CD_ENTRADA
                            LEFT JOIN TBL_NOTAFISCAL_ENTRADA_ITENS	 ITENS		       ON ITENS.CD_ENTRADA			     = ENTRADA.CD_ENTRADA
                            LEFT JOIN TBL_ENTIDADES                  EMPRESA           ON EMPRESA.CD_ENTIDADE		     = ENTRADA.CD_EMPRESA
							LEFT JOIN TBL_ENTIDADES                  FORNECEDOR		   ON FORNECEDOR.CD_ENTIDADE		 = ENTRADA.CD_FORNECEDOR
                            LEFT JOIN TBL_ENTIDADES				     TRANSPORTADOR	   ON TRANSPORTADOR.CD_TRANSPORTADOR = ENTRADA.CD_TRANSPORTADOR
							LEFT JOIN TBL_CEP_CIDADE                 CIDADE_FORNECEDOR ON CIDADE_FORNECEDOR.CD_CIDADE    = FORNECEDOR.CD_CIDADE
							LEFT JOIN TBL_CEP_ESTADO                 ESTADO_FORNECEDOR ON ESTADO_FORNECEDOR.CD_ESTADO    = CIDADE_FORNECEDOR.CD_ESTADO
                            WHERE ENTRADA_ENVIO.X_ENVIADO = 0 AND ENTRADA.CD_EMPRESA = @CD_EMPRESA AND ENTRADA_ENVIO.DS_INTEGRACAO = 'ZENLOG'

";

                    var parametros = new { @CD_EMPRESA = codigoEmpresa };

                    var entradaDict = new Dictionary<long, EntradaController>();

                    var result = await conexao.ObjetoConexao.QueryAsync<EntradaController, EntradaItensController, EntradaController>(
                        sql,
                        (entrada, itens) =>
                        {
                            // Verifica se a entrada já está no dicionário
                            if (!entradaDict.TryGetValue(entrada.CD_ENTRADA, out var entradaEntry))
                            {
                                entradaEntry = entrada;
                                entradaEntry.itensEntradaController = new List<EntradaItensController>();
                                entradaDict.Add(entrada.CD_ENTRADA, entradaEntry);
                            }

                            // Adiciona o item de material sem duplicar a entrada
                            if (itens != null && entradaEntry.itensEntradaController != null && !entradaEntry.itensEntradaController.Any(s => s.CD_ITEM == itens.CD_ITEM))
                            {
                                entradaEntry.itensEntradaController.Add(itens);
                            }

                            return entradaEntry;
                        },
                        parametros,
                        splitOn: "CD_ENTRADA"
                    );

                    return result.Distinct().ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível buscar as entradas para integração " + ex.Message);
            }
        }

        public async Task<List<NotaDiversasController>> BuscarDiversas(long codigoEmpresa)
        {
            try
            {
                using (var conexao = new ConexaoSQL())
                {
                    conexao.Conectar();

                    var sql = @"SELECT 
                            	 DIVERSAS.*, ENTIDADE.NR_CPFCNPJ AS NR_CPFCNPJ_ENTIDADE, ENTIDADE.DS_ENTIDADE AS DS_RAZAOSOCIAL_ENTIDADE
										 , ESTADO_ENTIDADE.DS_UF AS DS_UF_ENTIDADE, CIDADE_ENTIDADE.CD_MUNIBGE AS CD_MUNIBGE_ENTIDADE
										 , ENTIDADE.NR_CEP AS NR_CEP_ENTIDADE, ENTIDADE.DS_BAIRRO AS DS_BAIRRO_ENTIDADE
										 , ENTIDADE.NR_NUMERO AS NR_NUMERO_ENTIDADE, ENTIDADE.DS_ENDERECO AS DS_ENDERECO_ENTIDADE
										 , ENTIDADE.DS_COMPLEMENTO AS DS_COMPLEMENTO_ENTIDADE, TRANSPORTADOR.NR_CPFCNPJ AS NR_CPFCNPJ_TRANSPORTADOR
										 ,EMPRESA.NR_IE AS NR_IE_EMPRESA
                            	,ITENS.*
                            FROM TBL_NOTAFISCAL_DIVERSAS DIVERSAS
                            LEFT JOIN TBL_NOTAFISCAL_DIVERSAS_ITENS ITENS		    ON ITENS.CD_LANCAMENTO			  = DIVERSAS.CD_LANCAMENTO
							INNER JOIN TBL_CONTABIL_CME CME						    ON CME.CD_CME					  = ITENS.CD_CME
							INNER JOIN TBL_NOTAFISCAL_OPERACAO OPERACAO			    ON OPERACAO.CD_OPERACAO			  = DIVERSAS.CD_OPERACAO
                            LEFT JOIN TBL_ENTIDADES                 EMPRESA         ON EMPRESA.CD_ENTIDADE		      = DIVERSAS.CD_EMPRESA
							LEFT JOIN TBL_ENTIDADES                 ENTIDADE         ON ENTIDADE.CD_ENTIDADE		  = DIVERSAS.CD_ENTIDADE
                            LEFT JOIN TBL_ENTIDADES				    TRANSPORTADOR   ON TRANSPORTADOR.CD_TRANSPORTADOR = DIVERSAS.CD_TRANSPORTADOR
							LEFT JOIN TBL_CEP_CIDADE                CIDADE_ENTIDADE ON CIDADE_ENTIDADE.CD_CIDADE      = ENTIDADE.CD_CIDADE
							LEFT JOIN TBL_CEP_ESTADO                ESTADO_ENTIDADE ON ESTADO_ENTIDADE.CD_ESTADO      = CIDADE_ENTIDADE.CD_ESTADO
							WHERE DIVERSAS.CD_EMPRESA = @CD_EMPRESA AND DIVERSAS.CD_STATUS = 2 AND DIVERSAS.NR_DOCUMENTO > 0 AND OPERACAO.X_OPERACAO = 1
							AND CME.X_ESTOQUE = 1 AND CAST(DIVERSAS.DT_EMISSAO AS DATE) >= (SELECT TOP 1 DT_CADASTRO FROM TBL_CADASTRO_INTEGRACOES WHERE CD_INTEGRACAO = 2 AND X_ATIVAR = 1) 
							AND DIVERSAS.CD_LANCAMENTO NOT IN 
										(SELECT CD_LANCAMENTO 
												FROM TBL_NOTAS_DIVERSAS_INTEGRACAO_ENVIO DIV_INT
											WHERE DIV_INT.DS_INTEGRACAO = 'ZENLOG' AND DIV_INT.CD_LANCAMENTO = DIVERSAS.CD_LANCAMENTO)";

                    var parametros = new { @CD_EMPRESA = codigoEmpresa };

                    var diversasDict = new Dictionary<long, NotaDiversasController>();

                    var result = await conexao.ObjetoConexao.QueryAsync<NotaDiversasController, NotaDiversasItensController, NotaDiversasController>(
                        sql,
                        (diversas, itens) =>
                        {
                            // Verifica se o pedido já está no dicionário
                            if (!diversasDict.TryGetValue(diversas.CD_LANCAMENTO, out var diversasEntry))
                            {
                                diversasEntry = diversas;
                                diversasEntry.itensDiversasController = new List<NotaDiversasItensController>();
                                diversasDict.Add(diversas.CD_LANCAMENTO, diversasEntry);
                            }

                            // Adiciona o item de material sem duplicar o pedido
                            if (itens != null && diversasEntry.itensDiversasController != null && !diversasEntry.itensDiversasController.Any(s => s.CD_ITEM == itens.CD_ITEM))
                            {
                                diversasEntry.itensDiversasController.Add(itens);
                            }

                            return diversasEntry;
                        },
                        parametros,
                        splitOn: "CD_LANCAMENTO"
                    );

                    return result.Distinct().ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível buscar as notas diversas para integração " + ex.Message);
            }
        }

        public async Task<MaterialController> BuscarMaterialController(long codigoMaterial)
        {
            try
            {
                using (var conexao = new ConexaoSQL())
                {
                    conexao.Conectar();

                    string sql = @"SELECT *
                            FROM TBL_MATERIAIS               MAT
                            INNER JOIN TBL_MATERIAIS_UNIDADE UN ON UN.CD_UNIDADE = MAT.CD_UNIDADE
                            WHERE CD_MATERIAL = @CD_MATERIAL";

                    var parametros = new { CD_MATERIAL = codigoMaterial };


                    return await conexao.ObjetoConexao.QueryFirstAsync<MaterialController>(sql, parametros);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível buscar o material do controller para integração " + ex.Message);
            }
        }

        public async Task<MaterialIntegracaoController?> BuscarCodigoMaterialIntegracaoController(long codigoMaterial)
        {
            try
            {
                using (var conexao = new ConexaoSQL())
                {
                    conexao.Conectar();

                    string sql = @"SELECT *
                            FROM TBL_MATERIAIS_INTEGRACAO_ENVIO
                            WHERE CD_MATERIAL = @CD_MATERIAL AND DS_INTEGRACAO = 'ZENLOG'";

                    var parametros = new { CD_MATERIAL = codigoMaterial };


                    return await conexao.ObjetoConexao.QueryFirstOrDefaultAsync<MaterialIntegracaoController>(sql, parametros);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível buscar se o material do controller já foi integrado com API " + ex.Message);
            }
        }

        public async Task InserirMaterialIntegracaoController(MaterialIntegracaoController materialIntegracaoController)
        {
            try
            {
                using (var conexao = new ConexaoSQL())
                {
                    conexao.Conectar();

                    await conexao.ObjetoConexao.InsertAsync(materialIntegracaoController);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível inserir o material integração no controller " + ex.Message);
            }
        }

        public async Task AtualizarMaterialIntegracaoController(MaterialIntegracaoController materialIntegracaoController)
        {
            try
            {
                using (var conexao = new ConexaoSQL())
                {
                    conexao.Conectar();

                    await conexao.ObjetoConexao.UpdateAsync(materialIntegracaoController);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível inserir o material integração no controller " + ex.Message);
            }
        }

        public async Task AtualizarPedidoIntegracaoController(PedidoIntegracaoController pedidoIntegracaoController)
        {
            try
            {
                using (var conexao = new ConexaoSQL())
                {
                    conexao.Conectar();

                    await conexao.ObjetoConexao.UpdateAsync(pedidoIntegracaoController);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível inserir o pedido integração no controller " + ex.Message);
            }
        }

        public async Task<List<string>> BuscarCodigosPedidosEnviados()
        {
            try
            {
                using (var conexao = new ConexaoSQL())
                {
                    conexao.Conectar();

                    string sql = @"SELECT CD_PEDIDO
                            FROM TBL_PEDIDOS_INTEGRACAO_ENVIO  
                                WHERE X_ENVIADO = 1 AND CD_STATUS = 1 AND DS_STATUS_API IS NULL AND DS_INTEGRACAO = 'ZENLOG'";

                    var resultado = await conexao.ObjetoConexao.QueryAsync<string>(sql);
                    return resultado.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível buscar os pedidos do controller que já foram enviados para conferir se foram expedidos" + ex.Message);
            }
        }

        public async Task AtualizarPedido(Documentos documento)
        {
            using (var conexao = new ConexaoSQL())
            {
                conexao.Conectar();

                using (var transacao = conexao.ObjetoConexao.BeginTransaction())
                {
                    try
                    {
                        string sql = @"UPDATE TBL_PEDIDOS_INTEGRACAO_ENVIO
                                       SET DS_STATUS_API = 'CONFERIDO'
                                        WHERE CD_PEDIDO = @CD_PEDIDO";

                        var parametros = new { CD_PEDIDO = documento.pedido.Replace("PD", "").Replace("ND", "")};

                        await conexao.ObjetoConexao.ExecuteAsync(sql, parametros, transaction: transacao);

                        string sqlPedido = @"UPDATE TBL_VENDAS_PEDIDO
                                                SET 
                                                 NR_QUANTIDADE = @NR_QUANTIDADE
                                                ,NR_PESOBRUTO = @NR_PESOBRUTO
                                                ,NR_PESOLIQUIDO = @NR_PESOLIQUIDO
                                                WHERE CD_PEDIDO = @CD_PEDIDO";

                        var parametrosPedido = new
                        {
                            CD_PEDIDO = documento.pedido.Replace("PD", "").Replace("ND", ""),
                            NR_QUANTIDADE = documento.quantidadeVolumes,
                            NR_PESOBRUTO = documento.pesoBruto,
                            NR_PESOLIQUIDO = documento.pesoLiquido,
                        };

                        await conexao.ObjetoConexao.ExecuteAsync(sqlPedido, parametrosPedido, transaction: transacao);

                        transacao.Commit();
                    }

                    catch (Exception ex)
                    {
                        transacao.Rollback();

                        throw new Exception("Não foi possível atualizar os dados do pedido no controller" + ex.Message);
                    }
                }
            }
        }

        public async Task<List<PedidoController>> BuscarPedidosControllerExpedidos()
        {
            try
            {
                using (var conexao = new ConexaoSQL())
                {
                    conexao.Conectar();

                    string sql = @"SELECT PEDIDO.*, INTE.CD_ID AS CD_ID_PEDIDO_INTEGRACAO, EMPRESA.NR_IE AS NR_IE_EMPRESA
                            FROM TBL_VENDAS_PEDIDO PEDIDO
                            INNER JOIN TBL_PEDIDOS_INTEGRACAO_ENVIO  INTE           ON INTE.CD_PEDIDO       = PEDIDO.CD_PEDIDO
                            LEFT JOIN TBL_ENTIDADES                  EMPRESA        ON EMPRESA.CD_ENTIDADE  = PEDIDO.CD_EMPRESA
                                WHERE INTE.X_ENVIADO = 1 AND PEDIDO.CD_STATUS = 2 AND INTE.DS_STATUS_API = 'CONFERIDO' AND PEDIDO.NR_NOTAFISCAL > 0
                                    AND INTE.DS_INTEGRACAO = 'ZENLOG'";

                    var resultado = await conexao.ObjetoConexao.QueryAsync<PedidoController>(sql);
                    return resultado.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível buscar os pedidos do controller que já foram conferidos e emitidos" + ex.Message);
            }
        }

        public async Task InserirDiversaIntegracaoController(NotaDiversaIntegracaoController diversaIntegracaoController)
        {
            try
            {
                using (var conexao = new ConexaoSQL())
                {
                    conexao.Conectar();

                    await conexao.ObjetoConexao.InsertAsync(diversaIntegracaoController);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível inserir a nota diversa integração no controller " + ex.Message);
            }
        }

        public async Task AtualizarEntradaIntegracaoController(EntradaIntegracaoController entradaIntegracaoController1)
        {
            try
            {
                using (var conexao = new ConexaoSQL())
                {
                    conexao.Conectar();

                    await conexao.ObjetoConexao.UpdateAsync(entradaIntegracaoController1);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível atualizar a entrada integração no controller " + ex.Message);
            }
        }

        public void InserirLog(IntegracoesLogs integracoesLogs)
        {
            try
            {
                using (var conexao = new ConexaoSQL())
                {
                    conexao.Conectar();

                    conexao.ObjetoConexao.Insert(integracoesLogs);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Não foi possível inserir os log's no banco de dados " + ex.Message);
            }
        }

        public async Task<List<CadastroIntegracoes>> BuscarIntegracao()
        {
            try
            {
                using (var conexao = new ConexaoSQL())
                {
                    conexao.Conectar();

                    var sql = @"SELECT INTE.*, ROTI.*, EMPRESAS.*
                                    FROM SEL_CADASTRO_INTEGRACOES INTE
                                    INNER JOIN TBL_CADASTRO_INTEGRACOES_ROTINAS         ROTI     ON ROTI.CD_CADASTRO_INTEGRACAO     = INTE.CD_ID
                                    LEFT JOIN TBL_CADASTRO_INTEGRACOES_EMPRESAS        EMPRESAS ON EMPRESAS.CD_CADASTRO_INTEGRACAO = INTE.CD_ID
                                    WHERE INTE.X_ATIVAR = 1";

                    var integracaoDict = new Dictionary<long, CadastroIntegracoes>();

                    await conexao.ObjetoConexao.QueryAsync<CadastroIntegracoes, CadastroIntegracoesRotinas, CadastroIntegracoesEmpresas, CadastroIntegracoes>(
                       sql,
                       (integracao, rotina, empresa) =>
                       {
                           // Verifica se a integração já está no dicionário
                           if (!integracaoDict.TryGetValue(integracao.CD_ID, out var integracaoEntry))
                           {
                               integracaoEntry = integracao;
                               integracaoEntry.integracoesRotinas = new List<CadastroIntegracoesRotinas>();
                               integracaoEntry.integracoesEmpresas = new List<CadastroIntegracoesEmpresas>();
                               integracaoDict.Add(integracao.CD_ID, integracaoEntry);
                           }

                           // Adiciona a rotina sem duplicar a integração
                           if (rotina != null && integracaoEntry.integracoesRotinas != null && !integracaoEntry.integracoesRotinas.Any(s => s.CD_ID == rotina.CD_ID))
                           {
                               integracaoEntry.integracoesRotinas.Add(rotina);
                           }

                           // Adiciona a empresa sem duplicar a integração
                           if (empresa != null && integracaoEntry.integracoesEmpresas != null && !integracaoEntry.integracoesEmpresas.Any(s => s.CD_ID == empresa.CD_ID))
                           {
                               integracaoEntry.integracoesEmpresas.Add(empresa);
                           }

                           return integracaoEntry;
                       },
                       splitOn: "CD_ID"
                   );

                    return integracaoDict.Values.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Não foi possível obter os dados da integração " + ex.Message);
            }
        }
    }
}
