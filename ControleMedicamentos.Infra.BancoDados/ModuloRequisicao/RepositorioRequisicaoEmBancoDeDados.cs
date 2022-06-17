using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Dominio.ModuloFuncionario;
using ControleMedicamentos.Dominio.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloPaciente;
using ControleMedicamentos.Dominio.ModuloRequisicao;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Infra.BancoDados.ModuloRequisicao
{
    public class RepositorioRequisicaoEmBancoDeDados
    {
        private const string enderecobanco =
         @"Data Source=(LocalDB)\MSSqlLocalDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private const string sqlInserir =
           @" USE MEDICAMENTOSDB;
            INSERT INTO [TBREQUISICAO]
            (

                [FUNCIONARIO_ID],
                [PACIENTE_ID],
                [MEDICAMENTO_ID],
                [QUANTIDADEDEMEDICAMENTO],
                [DATA]


            )
            VALUES
        
            (
                @FUNCIONARIO_ID,
                @PACIENTE_ID,
                @MEDICAMENTO_ID,
                @QUANTIDADEDEMEDICAMENTO,
                @DATA



            ); SELECT SCOPE_IDENTITY();";

        private const string sqlEditar =
            @" UPDATE [TBREQUISICAO]
               SET 
                    [FUNCIONARIO_ID] = @FUNCIONARIO_ID,
                    [PACIENTE_ID] = @PACIENTE_ID,
                    [MEDICAMENTO_ID] = @MEDICAMENTO_ID,
                    [QUANTIDADEDEMEDICAMENTO] = @QUANTIDADEDEMEDICAMENTO,
                    [DATA] = @DATA

                ";

        private const string sqlExcluir =
            @"DELETE FROM [TBREQUISICAO]
                WHERE [ID] = @ID";

        private const string sqlSelecionarTodos =
             @"SELECT 
        R.[ID] AS REQ_ID,       
        R.[FUNCIONARIO_ID] AS REQ_FUNC,
        R.[PACIENTE_ID] AS REQ_PAC,
        R.[MEDICAMENTO_ID] AS REQ_MEDICAMENTO,             
        R.[QUANTIDADEMEDICAMENTO] AS REQ_QNTD,                    
        R.[DATA] AS REQ_DATA,        

            FU.[ID] AS FUNC_ID,
            FU.[NOME] AS FUNC_NOME,       
            FU.[LOGIN] AS FUNC_LOGIN,             
            FU.[SENHA] AS FUNC_SENHA,
                
                P.[ID] AS PAC_ID,
                P.[NOME] AS PAC_NOME,
                P.[CARTAOSUS] AS PAC_CARTAOSUS,

                    M.[ID] AS MED_ID,
                    M.[NOME] AS MED_NOME,
                    M.[DESCRICAO] AS MED_DESC,
                    M.[LOTE] AS MED_LOTE,
                    M.[VALIDADE] AS MED_VAL,
                    M.[QUANTIDADEDISPONIVEL] AS MED_QNTDISPONIVEL,
                    M.[FORNECEDOR_ID] AS MED_FORN,

                        FO.[ID] AS FORN_ID,
                        FO.[NOME] AS FORN_NOME,
                        FO.[TELEFONE] AS FORN_TELEF,
                        FO.[EMAIL] AS FORN_EMAIL,
                        FO.[CIDADE] AS FORN_CID,
                        FO.[ESTADO] AS FORN_ESTA

                FROM
                    [TBREQUISICAO] AS R INNER JOIN [TBFUNCIONARIO] AS FU
                ON
                    FU.ID = R.FUNCIONARIO_ID INNER JOIN
                    [TBPACIENTE] AS P
                ON
                    P.ID = R.PACIENTE_ID
                    INNER JOIN [TBMEDICAMENTO] AS M
                ON
                    M.ID = R.MEDICAMENTO_ID
                    INNER JOIN [TBFORNECEDOR] AS FO
                ON
                    FO.ID = M.FORNECEDOR_ID";

        private const string sqlSelecionarPorID =
              @"SELECT 
               R.[ID] AS REQ_ID,       
        R.[FUNCIONARIO_ID] AS REQ_FUNC,
        R.[PACIENTE_ID] AS REQ_PAC,
        R.[MEDICAMENTO_ID] AS REQ_MEDICAMENTO,             
        R.[QUANTIDADEMEDICAMENTO] AS REQ_QNTD,                    
        R.[DATA] AS REQ_DATA,        

            FU.[ID] AS FUNC_ID,
            FU.[NOME] AS FUNC_NOME,       
            FU.[LOGIN] AS FUNC_LOGIN,             
            FU.[SENHA] AS FUNC_SENHA,
                
                P.[ID] AS PAC_ID,
                P.[NOME] AS PAC_NOME,
                P.[CARTAOSUS] AS PAC_CARTAOSUS,

                    M.[ID] AS MED_ID,
                    M.[NOME] AS MED_NOME,
                    M.[DESCRICAO] AS MED_DESC,
                    M.[LOTE] AS MED_LOTE,
                    M.[VALIDADE] AS MED_VAL,
                    M.[QUANTIDADEDISPONIVEL] AS MED_QNTDISPONIVEL,
                    M.[FORNECEDOR_ID] AS MED_FORN,

                        FO.[ID] AS FORN_ID,
                        FO.[NOME] AS FORN_NOME,
                        FO.[TELEFONE] AS FORN_TELEF,
                        FO.[EMAIL] AS FORN_EMAIL,
                        FO.[CIDADE] AS FORN_CID,
                        FO.[ESTADO] AS FORN_ESTA

                FROM
                    [TBREQUISICAO] AS R INNER JOIN [TBFUNCIONARIO] AS FU
                ON
                    FU.ID = R.FUNCIONARIO_ID INNER JOIN
                    [TBPACIENTE] AS P
                ON
                    P.ID = R.PACIENTE_ID
                    INNER JOIN [TBMEDICAMENTO] AS M
                ON
                    M.ID = R.MEDICAMENTO_ID
                    INNER JOIN [TBFORNECEDOR] AS FO
                ON
                    FO.ID = M.FORNECEDOR_ID";

        public ValidationResult Inserir(Requisicao novaRequisicao)
        {
            var validador = new ValidadorRequsicao();

            var resultadoValidacao = validador.Validate(novaRequisicao);

            if (!resultadoValidacao.IsValid)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new(enderecobanco);

            SqlCommand comandoInsercao = new SqlCommand(sqlInserir, conexaoComBanco);

            ConfigurarParametrosRequisicao(novaRequisicao, comandoInsercao);

            conexaoComBanco.Open();
            var id = comandoInsercao.ExecuteScalar();
            novaRequisicao.Numero = Convert.ToInt32(id);

            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Editar(Requisicao req)
        {
            var validador = new ValidadorRequsicao();

            var resultadoValidacao = validador.Validate(req);

            if (!resultadoValidacao.IsValid)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecobanco);

            SqlCommand comandoEdicao = new SqlCommand(sqlEditar, conexaoComBanco);

            ConfigurarParametrosRequisicao(req, comandoEdicao);

            conexaoComBanco.Open();
            comandoEdicao.ExecuteNonQuery();
            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public void Excluir(Requisicao req)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecobanco);

            SqlCommand comandoExclusao = new SqlCommand(sqlExcluir, conexaoComBanco);

            comandoExclusao.Parameters.AddWithValue("@ID", req.Numero);

            conexaoComBanco.Open();
            comandoExclusao.ExecuteNonQuery();
            conexaoComBanco.Close();
        }

        public List<Requisicao> SelecionarTodos()
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecobanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarTodos, conexaoComBanco);

            conexaoComBanco.Open();

            SqlDataReader leitorRequisicoes = comandoSelecao.ExecuteReader();

            List<Requisicao> requisicoes = new List<Requisicao>();

            while (leitorRequisicoes.Read())
                requisicoes.Add(ConverterParaRequisicao(leitorRequisicoes));

            conexaoComBanco.Close();

            return requisicoes;
        }

        public Requisicao SelecionarPorId(int id)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecobanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarPorID, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("@ID", id);

            conexaoComBanco.Open();

            SqlDataReader leitorRequisicao = comandoSelecao.ExecuteReader();

            Requisicao req = null;
            if (leitorRequisicao.Read())
                req = ConverterParaRequisicao(leitorRequisicao);

            conexaoComBanco.Close();

            return req;
        }

        private Requisicao ConverterParaRequisicao(SqlDataReader leitorRequisicao)
        {
            int id = Convert.ToInt32(leitorRequisicao["REQ_ID"]);
            int quantidade = Convert.ToInt32(leitorRequisicao["REQ_QNTD"]);
            DateTime dataRequisicao = Convert.ToDateTime(leitorRequisicao["REQ_DATA"]);

            Funcionario f = ConveterParaFuncionario(leitorRequisicao);
            Medicamento m = ConveterParaMedicamento(leitorRequisicao);
            Paciente p = ConverterParaPaciente(leitorRequisicao);

            return new Requisicao
            {
                Numero = id,
                QtdMedicamento = quantidade,
                Data = dataRequisicao,
                Funcionario = f,
                Medicamento = m,
                Paciente = p
            };
        }

        private void ConfigurarParametrosRequisicao(Requisicao novaRequisicao, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("@REQ_ID", novaRequisicao.Numero);
            comando.Parameters.AddWithValue("@FUNC_ID", novaRequisicao.Funcionario.Numero);
            comando.Parameters.AddWithValue("@PAC_ID", novaRequisicao.Paciente.Numero);
            comando.Parameters.AddWithValue("@MED_ID", novaRequisicao.Medicamento.Numero);
            comando.Parameters.AddWithValue("@REQ_QNTD", novaRequisicao.QtdMedicamento);
            comando.Parameters.AddWithValue("@REQ_DATA", novaRequisicao.Data);
        }
        private Funcionario ConveterParaFuncionario(SqlDataReader leitorRequisicao)
        {
            int id = Convert.ToInt32(leitorRequisicao["FUNC_ID"]);
            string nome = leitorRequisicao["FUNC_NOME"].ToString();
            string login = leitorRequisicao["FUNC_LOGIN"].ToString();
            string senha = leitorRequisicao["FUNC_SENHA"].ToString();

            return new Funcionario
            {
                Numero = id,
                Nome = nome,
                Login = login,
                Senha = senha
            };
        }

        private Paciente ConverterParaPaciente(SqlDataReader leitorRequisicao)
        {
            int id = Convert.ToInt32(leitorRequisicao["PAC_ID"]);
            string nome = leitorRequisicao["PAC_NOME"].ToString();
            string cartaoSUS = leitorRequisicao["PAC_CARTAOSUS"].ToString();

            return new Paciente
            {
                Numero = id,
                Nome = nome,
                CartaoSUS = cartaoSUS
            };
        }

        private Medicamento ConveterParaMedicamento(SqlDataReader leitorRequisicao)
        {
            int idMed = Convert.ToInt32(leitorRequisicao["MED_ID"]);
            string nomeMed = leitorRequisicao["MED_NOME"].ToString();
            string descricao = leitorRequisicao["MED_DESC"].ToString();
            string lote = leitorRequisicao["MED_LOTE"].ToString();
            DateTime validade = Convert.ToDateTime(leitorRequisicao["MED_VAL"]);
            int qtde = Convert.ToInt32(leitorRequisicao["MED_QNTDISPONIVEL"]);

            return new Medicamento
            {
                Numero = idMed,
                Nome = nomeMed,
                Descricao = descricao,
                Lote = lote,
                Validade = validade,
                QuantidadeDisponivel = qtde,
                Requisicoes = new(),
                Fornecedor = ConverterParaFornecedor(leitorRequisicao)
            };

        }

        private Fornecedor ConverterParaFornecedor(SqlDataReader leitorRequisicao)
        {
            int idForn = Convert.ToInt32(leitorRequisicao["FORN_ID"]);
            string nomeForn = leitorRequisicao["FORN_NOME"].ToString();
            string telefone = leitorRequisicao["FORN_TEL"].ToString();
            string email = leitorRequisicao["FORN_EMAIL"].ToString();
            string cidade = leitorRequisicao["FORN_CID"].ToString();
            string estado = leitorRequisicao["FORN_ESTA"].ToString();

            return new Fornecedor
            {
                Numero = idForn,
                Nome = nomeForn,
                Telefone = telefone,
                Email = email,
                Cidade = cidade,
                Estado = estado
            };
        }
    }
}
