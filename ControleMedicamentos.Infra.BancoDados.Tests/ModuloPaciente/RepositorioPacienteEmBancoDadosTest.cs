using ControleMedicamentos.Dominio.ModuloPaciente;
using ControleMedicamentos.Infra.BancoDados.ModuloPaciente;
using FluentValidation.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Infra.BancoDados.Tests.ModuloPaciente
{
    [TestClass]
    public class RepositorioPacienteEmBancoDadosTest
    {

        RepositorioPacienteEmBancoDeDados _repositorioPaciente;
        Paciente paciente;
        [TestMethod]
        public void Deve_inserir_paciente()
        {

            RepositorioPacienteEmBancoDeDados repositorioPaciente = new();

            Paciente paciente = new("Jorge", "278164127567812412");

            RepositorioPacienteEmBancoDeDados _repositorioPaci = new();

            ValidationResult vr = _repositorioPaci.Inserir(paciente);

            Assert.IsTrue(vr.IsValid);

        }

        [TestMethod]
        public void Deve_Excluir_Paciente()
        {
            _repositorioPaciente.Inserir(paciente);

            _repositorioPaciente.Excluir(paciente);

            var pacienteEncontrado = _repositorioPaciente.SelecionarPorID(paciente.Numero);

            Assert.IsNull(pacienteEncontrado);
        }
        [TestMethod]
        public void Deve_Editar_Paciente()
        {
            _repositorioPaciente.Inserir(paciente);

            paciente.Nome = "Joao Pedro";
            paciente.CartaoSUS = "64736573658362846745";


            _repositorioPaciente.Editar(paciente);

            var pacienteEncontrado = _repositorioPaciente.SelecionarPorID(paciente.Numero);

            Assert.IsNotNull(pacienteEncontrado);
            Assert.AreEqual(paciente, pacienteEncontrado);
        }
    }
}
