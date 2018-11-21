using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace formular.Classes
{
    class PersonValidator : AbstractValidator<Person>
    {
        public PersonValidator()
        {
            RuleFor(person => person.Firstname).NotEmpty().WithMessage("Prázdné jméno.");
            RuleFor(person => person.Surname).NotEmpty().WithMessage("Prázdné příjmení.");
            RuleFor(person => person.Email).NotEmpty().WithMessage("Prázdný e-mail.");
            RuleFor(person => person.Email).EmailAddress().WithMessage("Špatný formát e-mailu.");
        }
    }
}
