using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UminoWeb.BLL.Dto;

namespace UminoWeb.BLL.Validators.SliderValidator
{
    public class SliderUpdateDtoValidation : AbstractValidator<SliderUpdateDto>
    {
        public SliderUpdateDtoValidation()
        {
            //RuleFor(x => x.Image)
            //    .Must(file => file != null ? Regex.IsMatch(Path.GetExtension(file.FileName), "^.jpeg$|^.jpg$|^.png$") : false)
            //    .WithMessage("shekil jpeg, jpg ve ya png formatinda ola biler");
        }
    }
}
