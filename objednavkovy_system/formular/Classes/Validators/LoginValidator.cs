using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace formular.Classes
{
    class LoginValidator : AbstractValidator<Login>
    {
        public LoginValidator()
        {
            RuleFor(person => person.Username).NotEmpty().WithMessage("Prázdné uživatelské jméno.");
            RuleFor(person => person.Password).NotEmpty().WithMessage("Prázdné heslo.");
        }
    }
}
