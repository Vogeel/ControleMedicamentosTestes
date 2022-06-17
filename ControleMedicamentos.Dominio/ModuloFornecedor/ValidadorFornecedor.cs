using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Dominio.ModuloFornecedor
{
    public class ValidadorFornecedor : AbstractValidator<Fornecedor>
    {
        public ValidadorFornecedor() // arrumar dps mais validações
        {
            RuleFor(x => x.Nome)
                .NotNull().NotEmpty().MinimumLength(5);
            RuleFor(x => x.Telefone)
                .NotNull().NotEmpty().MinimumLength(10).MaximumLength(11);
            RuleFor(x => x.Email)
                .EmailAddress(EmailValidationMode.AspNetCoreCompatible).NotNull().NotEmpty();
        }
    }
}
