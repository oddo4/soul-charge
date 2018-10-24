using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace formular.Classes
{
    static class ValidationExtensions
    {
        public static ValidationResult Validate<T>(this IValidator validator, T instance, params string[] properties)
        {
            var context = new ValidationContext<T>(instance, new PropertyChain(), ValidatorOptions.ValidatorSelectors.MemberNameValidatorSelectorFactory(properties));
            return validator.Validate(context);
        }
    }
}
