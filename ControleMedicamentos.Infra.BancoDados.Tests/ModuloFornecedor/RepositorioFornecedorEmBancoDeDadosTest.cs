using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Infra.BancoDados.ModuloFornecedor;
using FluentValidation.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Infra.BancoDados.Tests.ModuloFornecedor
{
    [TestClass]
    public class RepositorioFornecedorEmBancoDeDadosTest
    {
        RepositorioFornecedorEmBancoDeDados _repositorioFornecedor;
        Fornecedor fornecedor;


        [TestMethod]
        public void Deve_inserir_fornecedor()
        {

            RepositorioFornecedorEmBancoDeDados repositorioPaciente = new();

            Fornecedor fornecedor = new("pharma", "9999999999", "uashfuas@gmail.com", "lages", "sc");

            RepositorioFornecedorEmBancoDeDados _repositorioForn = new();

            ValidationResult vr = _repositorioForn.Inserir(fornecedor);

            Assert.IsTrue(vr.IsValid);

        }
        [TestMethod]
        public void Deve_Editar_Fornecedor()
        {
            _repositorioFornecedor.Inserir(fornecedor);

            fornecedor.Nome = "Alex";
            fornecedor.Email = "alex@importaDroga.com";
            fornecedor.Telefone = "49989097632";
            fornecedor.Cidade = "itajai";
            fornecedor.Estado = "SC";

            _repositorioFornecedor.Editar(fornecedor);

            var fornecedorEncontrado = _repositorioFornecedor.SelecionarPorID(fornecedor.Numero);

            Assert.IsNotNull(fornecedorEncontrado);
            Assert.AreEqual(fornecedor, fornecedorEncontrado);
        }

        [TestMethod]
        public void Deve_Excluir_Fornecedor()
        {
            _repositorioFornecedor.Inserir(fornecedor);

            _repositorioFornecedor.Excluir(fornecedor);

            var fornecedorEncontrado = _repositorioFornecedor.SelecionarPorID(fornecedor.Numero);

            Assert.IsNull(fornecedorEncontrado);
        }
    }
}
