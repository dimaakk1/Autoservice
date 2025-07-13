using FluentValidation;
using Autoservice.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoservice.BLL.Validator
{
    public class RecordUpdateDtoValidator : AbstractValidator<RecordUpdateDto>
    {
        public RecordUpdateDtoValidator()
        {
            RuleFor(x => x.ClientId).
                GreaterThan(0).WithMessage("Client ID must be valid");
            RuleFor(x => x.CarId)
                .GreaterThan(0).WithMessage("Car ID must be valid");
            RuleFor(x => x.ServiceId)
                .GreaterThan(0).WithMessage("Service ID must be valid");
            RuleFor(x => x.Date)
                .GreaterThan(DateTime.Now)
                .WithMessage("Date must be in the future");
        }
    }
}
