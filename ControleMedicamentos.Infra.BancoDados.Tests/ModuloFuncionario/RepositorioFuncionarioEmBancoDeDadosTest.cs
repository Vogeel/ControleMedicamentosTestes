using ControleMedicamentos.Dominio.ModuloFuncionario;
using ControleMedicamentos.Infra.BancoDados.ModuloFuncionario;
using FluentValidation.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Infra.BancoDados.Tests.ModuloFuncionario
{

    

    [TestClass]
    public class RepositorioFuncionarioEmBancoDeDadosTest
    {
         RepositorioFuncionarioEmBancoDeDados _repositorioFuncionario;
        Funcionario funcionario;

        [TestMethod]
        public void Deve_inserir_funcionario()
        {

            RepositorioFuncionarioEmBancoDeDados repositorioFuncionario = new();

            Funcionario funcionario = new("Jorge", "jorginho123", "123456");

            RepositorioFuncionarioEmBancoDeDados _repositorioFunc = new();

            ValidationResult vr = _repositorioFunc.Inserir(funcionario);

            Assert.IsTrue(vr.IsValid);

        }
        [TestMethod]
        public void Deve_Excluir_Funcionario()
        {
            _repositorioFuncionario.Inserir(funcionario);

            _repositorioFuncionario.Excluir(funcionario);

            var funcionarioEncontrado = _repositorioFuncionario.SelecionarPorID(funcionario.Numero);

            Assert.IsNull(funcionarioEncontrado);
        }
        [TestMethod]
        public void Deve_Editar_Funcionario()
        {
            _repositorioFuncionario.Inserir(funcionario);

            funcionario.Nome = "kaio";
            funcionario.Login = "kaio7658";
            funcionario.Senha = "000000000";


            _repositorioFuncionario.Editar(funcionario);

            var funcionarioEncontrado = _repositorioFuncionario.SelecionarPorID(funcionario.Numero);

            Assert.IsNotNull(funcionarioEncontrado);
            Assert.AreEqual(funcionario, funcionarioEncontrado);
        }
    }
}
