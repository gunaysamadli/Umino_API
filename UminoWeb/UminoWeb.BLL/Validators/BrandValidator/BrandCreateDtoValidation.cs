using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UminoWeb.BLL.Dto;

namespace UminoWeb.BLL.Validators.BrandValidator
{
    public class BrandCreateDtoValidation : AbstractValidator<BrandCreateDto>
    {
        public BrandCreateDtoValidation()
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage("name null ola bilmez")
                .NotEmpty().WithMessage("name daxil edilmelidir")
                .MinimumLength(3).WithMessage("name 3 simvoldan chox olmalidir")
                .MaximumLength(20).WithMessage("name 20 simvoldan az olmalidir");

            RuleFor(x => x.Image)
                .NotEmpty().WithMessage("image null ola bilmez")
                .Must(file => file != null ? Regex.IsMatch(Path.GetExtension(file.FileName), "^.jpeg$|^.jpg$|^.png$") : false)
                .WithMessage("shekil jpeg, jpg ve ya png formatinda ola biler");

        }
    }
}
