using ControleMedicamento.Infra.BancoDados.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Dominio.ModuloFuncionario;
using ControleMedicamentos.Dominio.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloPaciente;
using ControleMedicamentos.Dominio.ModuloRequisicao;
using ControleMedicamentos.Infra.BancoDados.ModuloFornecedor;
using ControleMedicamentos.Infra.BancoDados.ModuloFuncionario;
using ControleMedicamentos.Infra.BancoDados.ModuloPaciente;
using ControleMedicamentos.Infra.BancoDados.ModuloRequisicao;
using FluentValidation.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Infra.BancoDados.Tests.ModuloRequisicao
{
    [TestClass]
    public class RepositorioRequisicaoEmBancoDeDadosTest
    {
        Requisicao req;
        Fornecedor forn;
        Funcionario func;
        Funcionario funcEditar;
        Paciente pac;
        Paciente pacEditar;
        Medicamento med;
        RepositorioRequisicaoEmBancoDeDados _repositorioReq;
        RepositorioMedicamentoEmBancoDados _repositorioMed;
        RepositorioFornecedorEmBancoDeDados _repositorioForn;
        RepositorioPacienteEmBancoDeDados _repositorioPac;
        RepositorioFuncionarioEmBancoDeDados _repositorioFunc;
        [TestMethod]
        public void Deve_inserir_requisicao()
        {

            _repositorioForn.Inserir(forn);
            med.Fornecedor = forn;
            _repositorioMed.Inserir(med);

            _repositorioFunc.Inserir(func);

            _repositorioPac.Inserir(pac);

            _repositorioReq.Inserir(req);

            var requisicaoEncontrada = _repositorioReq.SelecionarPorId(req.Numero);

            Assert.IsNotNull(requisicaoEncontrada);

            Assert.AreEqual(req, requisicaoEncontrada);
        }
        [TestMethod]
        public void DeveEditarRequisicao()
        {
            _repositorioForn.Inserir(forn);
            med.Fornecedor = forn;
            _repositorioMed.Inserir(med);

            _repositorioFunc.Inserir(func);

            _repositorioPac.Inserir(pac);

            _repositorioReq.Inserir(req);

            _repositorioFunc.Inserir(funcEditar);

            _repositorioPac.Inserir(pacEditar);

            req.QtdMedicamento = 15;
            req.Paciente = pacEditar;
            req.Funcionario = funcEditar;

            _repositorioReq.Editar(req);

            var requisicaoEncontrada = _repositorioReq.SelecionarPorId(req.Numero);

            Assert.IsNotNull(requisicaoEncontrada);

            Assert.AreEqual(req, requisicaoEncontrada);
        }

        [TestMethod]
        public void DeveExcluirRequisicao()
        {
            _repositorioForn.Inserir(forn);
            med.Fornecedor = forn;
            _repositorioMed.Inserir(med);

            _repositorioFunc.Inserir(func);

            _repositorioPac.Inserir(pac);

            _repositorioReq.Inserir(req);

            _repositorioReq.Excluir(req);

            var requisicaoEncontrada = _repositorioReq.SelecionarPorId(req.Numero);

            Assert.IsNull(requisicaoEncontrada);
        }

    }
}
