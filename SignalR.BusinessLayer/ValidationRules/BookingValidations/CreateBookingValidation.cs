using FluentValidation;
using SignalR.DtoLayer.BookingDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.BusinessLayer.ValidationRules.BookingValidations
{
    public class CreateBookingValidation:AbstractValidator<CreateBookingDto>
    {
        public CreateBookingValidation()
        {
            //RuleFor(x => x.Name).NotEmpty().WithMessage("İsim Alanı Boş Geçilemez");
            //RuleFor(x => x.Phone).NotEmpty().WithMessage("Telefon Alanı Boş Geçilemez");
            //RuleFor(x => x.Mail).NotEmpty().WithMessage("Mail Alanı Boş Geçilemez");
            //RuleFor(x => x.PersonCount).NotEmpty().WithMessage("Kişi Alanı Boş Geçilemez");
            //RuleFor(x => x.Date).NotEmpty().WithMessage("Tarih Alanı Boş Geçilemez");

            //RuleFor(x=>x.Name).MinimumLength(5).WithMessage("İsim Alanı En Az 5 Karakter Olmalıdır").MaximumLength(50).WithMessage("İsim Alanı En Fazla 50 Karakter Olmalıdır");
            //RuleFor(x=>x.Description).MaximumLength(500).WithMessage("Açıklama Alanı En Fazla 500 Karakter Olmalıdır");
            //RuleFor(x=>x.Mail).EmailAddress().WithMessage("Geçerli Bir Email Adresi Giriniz");

            RuleFor(x => x.Name).NotEmpty().NotNull().MinimumLength(5).MaximumLength(50).WithName("İsim");
            RuleFor(x => x.Phone).NotEmpty().NotNull().WithName("Telefon");
            RuleFor(x => x.Mail).NotEmpty().NotNull().EmailAddress().WithName("Email");
            RuleFor(x => x.Mail).MaximumLength(500).WithName("Açıklama");
            
            

        }
    }
}
