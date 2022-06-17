using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Dominio.ModuloRequisicao
{
    public class ValidadorRequsicao : AbstractValidator<Requisicao>
    {
        public ValidadorRequsicao()
        { 
            RuleFor(x => x.Paciente)
                .NotNull();
            RuleFor(x => x.Funcionario)
                .NotNull();
            RuleFor(x => x.Medicamento)
                .NotNull();
            RuleFor(x => x.QtdMedicamento)
                .NotNull();
            RuleFor(x => x.Data)
                .NotNull().NotEmpty();
        }
    }
}
