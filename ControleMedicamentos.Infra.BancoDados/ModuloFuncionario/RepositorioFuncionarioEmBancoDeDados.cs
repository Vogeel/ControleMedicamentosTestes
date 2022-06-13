using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Infra.BancoDados.ModuloFuncionario
{
    public class RepositorioFuncionarioEmBancoDeDados
    {
        private const string enderecobanco =
          @"Data Source=(LocalDB)\MSSqlLocalDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private const string sqlInserir =
           @" USE MEDICAMENTOSDB;
            INSERT INTO [TBFUNCIONARIO]
            (

                [NOME],
                [LOGIN],
                [SENHA]

            )
            VALUES
        
            (
                @NOME,
                @LOGIN,
                @SENHA



            ); SELECT SCOPE_IDENTITY();";

        private const string sqlEditar =
            @" UPDATE [TBFUNCIONARIO]
               SET 
                    [NOME] = @NOME,
                    [LOGIN] = @LOGIN,
                    [SENHA] = @SENHA

                WHERE [ID] = @ID";

        private const string sqlExcluir =
            @"DELETE FROM [TBFUNCIONARIO]
                WHERE [ID] = @ID";

        private const string sqlSelecionarTodos =
             @"SELECT 
                    [ID],
		            [NOME],
                    [LOGIN],
                    [SENHA]

 
	            FROM 
                    [TBFUNCIONARIO] ";

        private const string sqlSelecionarPorID =
             @"SELECT 
					    [ID],
					    [NOME],
					    [LOGIN],
					    [SENHA]
	            FROM 
                    [TBFUNCIONARIO]

		        WHERE
                    [ID] = @ID";


    }
}
