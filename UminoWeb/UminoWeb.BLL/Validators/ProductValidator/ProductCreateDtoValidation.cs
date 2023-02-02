using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UminoWeb.BLL.Dto;

namespace UminoWeb.BLL.Validators.ProductValidator
{
    public class ProductCreateDtoValidation : AbstractValidator<ProductCreateDto>
    {
        public ProductCreateDtoValidation()
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage("name null ola bilmez")
                .NotEmpty().WithMessage("name daxil edilmelidir")
                .MinimumLength(3).WithMessage("name 3 simvoldan chox olmalidir")
                .MaximumLength(30).WithMessage("name 30 simvoldan az olmalidir");
        }
    }
}
