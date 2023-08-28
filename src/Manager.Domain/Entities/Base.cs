using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using FluentValidation;
using FluentValidation.Results;

namespace Manager.Domain.Entities{
    public abstract class Base{
        
        public long Id { get; set; }

        internal List<string> _errors;

        public IReadOnlyCollection<string> Erros => _errors;

        public bool IsValid() => _errors.Count == 0;

        private void AddErrorList(IList<ValidationFailure> erros)
        {
            foreach(var error in erros)
                _errors.Add(error.ErrorMessage);
        }

        protected bool Validate<V, O>(V validator, O obj)
        //                      Na hora da chamada ele descobre qual o tipo das vareaveis;
            where V : AbstractValidator<O>
            {
                var validation = validator.Validate(obj);

                if(validation.Errors.Count > 0)
                    AddErrorList(validation.Errors);
                
                return _errors.Count == 0;
            }
        
    }
}