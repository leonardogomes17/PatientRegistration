using System.Diagnostics.CodeAnalysis;
using PatientRegistrations.Domain.Dtos;
using PatientRegistrations.Domain.Helpers;

namespace PatientRegistrations.Domain.Patient
{
    public class Patient : Entity
    {

        public void ValidateAndSet(PatientDto dto, Agreement.Agreement agreement)
        {
            DomainException.When(string.IsNullOrEmpty(dto.Name), "Name is required");
            DomainException.When(string.IsNullOrEmpty(dto.Gender), "Name is required");
            DomainException.When(string.IsNullOrEmpty(dto.SurName), "SurName is required");
            DomainException.When(dto.DateBirth <= DateTime.Now.AddYears(-130) || dto.DateBirth > DateTime.Now, "Date of Birth Invalid");
            DomainException.When(string.IsNullOrEmpty(dto.Rg), "Rg is required");
            DomainException.When(string.IsNullOrEmpty(dto.UfRg) || dto.UfRg.Length != 2, "UF RG is invalid");
            DomainException.When(!string.IsNullOrEmpty(dto.Cellphone) && (dto.Cellphone.Length != 11 || !ValidateHelper.IsNumber(dto.Cellphone)), "Cellphone invalid 99 99999 9999");
            DomainException.When(!string.IsNullOrEmpty(dto.Phone) && (dto.Phone.Length != 10 ||  !ValidateHelper.IsNumber(dto.Phone)), "Phone invalid 99 9999 9999");
            DomainException.When(dto.AgreementId <= 0, "Agreement invalid");
            DomainException.When(string.IsNullOrEmpty(dto.CardNumber) || !ValidateHelper.IsNumber(dto.CardNumber), "Card Number invalid");
            DomainException.When(dto.CardValidate < DateTime.Now, "Card Date Validate is Invalid");
            DomainException.When(!string.IsNullOrEmpty(dto.Cpf) &&  !ValidateHelper.ValidateCpf(dto.Cpf), "Cpf is Invalid");
            DomainException.When(string.IsNullOrEmpty(dto.Email) || !ValidateHelper.ValidateEmail(dto.Email), "E-mail is Invalid");
            DomainException.When(string.IsNullOrEmpty(dto.Cellphone) && string.IsNullOrEmpty(dto.Phone), "Fill in at least one phone number");

            Id = dto.PatientId;
            Name = dto.Name;
            SurName = dto.SurName;
            DateBirth = dto.DateBirth;
            Cpf = dto.Cpf;
            Rg = dto.Rg;
            UfRg = dto.UfRg;
            Email = dto.Email;
            Cellphone = dto.Cellphone;
            Phone = dto.Phone;
            AgreementId = dto.AgreementId;
            CardNumber = dto.CardNumber;
            CardValidate = dto.CardValidate;
            Active = true;
            Agreement = agreement;
            Gender = dto.Gender; 
        }

        public void Update(PatientDto dto, Agreement.Agreement agreement)
        {
            ValidateAndSet(dto, agreement);
        }

        public string Name { get; set; }
        public string SurName { get; set; }
        public DateTime DateBirth { get; set; }
        public string Gender { get; set; }
        public string? Cpf { get; set; }
        public string Rg { get; set; }
        public string UfRg { get; set; }
        public string Email { get; set; }
        public string? Cellphone { get; set; }
        public string? Phone { get; set; }
        public int AgreementId { get; set; }
        public string CardNumber { get; set; }
        public DateTime CardValidate { get; set; }
        public bool Active { get; set; }
        public Agreement.Agreement Agreement { get; private set; }
    }
}
