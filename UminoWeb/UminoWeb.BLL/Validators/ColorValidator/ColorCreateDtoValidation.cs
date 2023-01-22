using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UminoWeb.BLL.Dto;

namespace UminoWeb.BLL.Validators.ColorValidator
{
    public class ColorCreateDtoValidation : AbstractValidator<ColorCreateDto>
    {
        public ColorCreateDtoValidation()
        {
            RuleFor(x => x.ColorName)
                .NotNull().WithMessage("name null ola bilmez")
                .NotEmpty().WithMessage("name daxil edilmelidir");

            RuleFor(x => x.ColorCode)
                .NotNull().WithMessage("code null ola bilmez")
                .NotEmpty().WithMessage("code daxil edilmelidir");
        }
    }
}
