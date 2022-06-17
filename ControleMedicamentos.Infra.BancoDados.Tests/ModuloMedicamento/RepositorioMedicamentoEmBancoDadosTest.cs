using ControleMedicamento.Infra.BancoDados.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Dominio.ModuloMedicamento;
using ControleMedicamentos.Infra.BancoDados.ModuloFornecedor;
using FluentValidation.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ControleMedicamento.Infra.BancoDados.Tests.ModuloMedicamento
{
    [TestClass]
    public class RepositorioMedicamentoEmBancoDadosTest
    {
        Medicamento med;
        RepositorioMedicamentoEmBancoDados _repositorioMed;
        RepositorioFornecedorEmBancoDeDados _repositorioForn;
        Fornecedor forn;
        Fornecedor fornEditar;
        [TestMethod]
        public void Deve_inserir_medicamento()
        {

            _repositorioForn.Inserir(forn);

            med.Fornecedor = forn;

            _repositorioMed.Inserir(med);

            var medicamentoEncontrado = _repositorioMed.SelecionarPorID(med.Numero);

            medicamentoEncontrado.Validade = DateTime.SpecifyKind(medicamentoEncontrado.Validade, DateTimeKind.Local); 

            Assert.IsNotNull(medicamentoEncontrado);

            Assert.AreEqual(med, medicamentoEncontrado);
        }

        [TestMethod]
        public void Deve_Excluir_Medicamento()
        {
            _repositorioForn.Inserir(forn);

            med.Fornecedor = forn;

            _repositorioMed.Inserir(med);

            _repositorioMed.Excluir(med.Numero);

            var medicamentoEncontrado = _repositorioMed.SelecionarPorID(med.Numero);

            Assert.IsNull(medicamentoEncontrado);
        }

        [TestMethod]
        public void Deve_Editar_Medicamento()
        {
            _repositorioForn.Inserir(forn);
            _repositorioForn.Inserir(fornEditar);

            med.Fornecedor = forn;

            _repositorioMed.Inserir(med);

            med.Nome = "estomazil";
            med.Fornecedor = fornEditar;

            _repositorioMed.Editar(med);

            var medicamentoEncontrado = _repositorioMed.SelecionarPorID(med.Numero);

            Assert.IsNotNull(medicamentoEncontrado);

            Assert.AreEqual(med, medicamentoEncontrado);
        }
    }
}
