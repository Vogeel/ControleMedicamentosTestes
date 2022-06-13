using ControleMedicamento.Infra.BancoDados.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Dominio.ModuloMedicamento;
using FluentValidation.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ControleMedicamentos.Dominio.Tests.ModuloMedicamento
{
    [TestClass]
    public class MedicamentoTest
    {
        [TestMethod]

        public void Deve_inserir_medicamento()
        {
            Medicamento medicamento = new("nome", "10 caracteres", "Teste", DateTime.MaxValue);
            medicamento.Fornecedor = new Fornecedor("nome", "9999-9999", "jajaja@gmail.com", "lages", "sc");

            RepositorioMedicamentoEmBancoDados _repositorioMed = new();

            ValidationResult vr = _repositorioMed.Inserir(medicamento);

            Assert.IsTrue(vr.IsValid);
        }
        
    }
}
