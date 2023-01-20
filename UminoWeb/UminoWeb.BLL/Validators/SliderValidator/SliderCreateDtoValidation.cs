using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UminoWeb.BLL.Dto;

namespace UminoWeb.BLL.Validators.SliderValidator
{
    public class SliderCreateDtoValidation : AbstractValidator<SliderCreateDto>
    {
        public SliderCreateDtoValidation()
        {
            RuleFor(x => x.Title)
                .NotNull().WithMessage("title null ola bilmez")
                .NotEmpty().WithMessage("title daxil edilmelidir")
                .MinimumLength(3).WithMessage("title 3 simvoldan chox olmalidir")
                .MaximumLength(50).WithMessage("title 50 simvoldan az olmalidir");

            RuleFor(x => x.Image)
                .NotEmpty().WithMessage("image null ola bilmez")
                .Must(file => file != null ? Regex.IsMatch(Path.GetExtension(file.FileName), "^.jpeg$|^.jpg$|^.png$") : false)
                .WithMessage("shekil jpeg, jpg ve ya png formatinda ola biler");
        }
    }
}
