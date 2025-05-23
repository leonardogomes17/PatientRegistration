using PatientRegistrations.Domain.Agreement;
using System.Numerics;
using System.Xml.Linq;
using PatientRegistrations.Domain.Dtos;
using PatientRegistrations.Domain.Helpers;
using PatientRegistrations.Domain.Interfaces;

namespace PatientRegistrations.Domain.Patient
{
    public class PatientStore
    {
        private readonly IRepository<Patient> _patientRepository;
        
        private readonly IRepository<Agreement.Agreement> _agreementRepository;
        public PatientStore(IRepository<Patient> patientRepository,
            IRepository<Agreement.Agreement> agreementRepository) 
        {
            _patientRepository = patientRepository;
            _agreementRepository = agreementRepository;
        }
        public IEnumerable<PatientDto> GetAll()
        {
            var patients = _patientRepository.GetAll().Where(x => x.Active).AsEnumerable();

            var dtoPatients = patients.Select(x => convertDtoPatient(x));

            return dtoPatients;
        }

        private PatientDto convertDtoPatient(Patient x)
        {
            return new PatientDto()
            {
                AgreementId = x.AgreementId,
                CardNumber = x.CardNumber,
                CardValidate = x.CardValidate,
                Cellphone = x.Cellphone,
                Cpf = x.Cpf,
                DateBirth = x.DateBirth,
                Email = x.Email,
                Name = x.Name,
                PatientId = x.Id,
                Phone = x.Phone,
                Rg = x.Rg,
                SurName = x.SurName,
                UfRg = x.UfRg,
                Gender = x.Gender,
            };
        }
        public PatientDto GetByID(int id)
        {
            var patient = _patientRepository.GetByID(id);

            DomainException.When(patient == null, "Patient not found");

            DomainException.When(!patient.Active, "Patient not found");

            return convertDtoPatient(patient);
        }
        public void DeleteByID(int id)
        {
            var patient = _patientRepository.GetByID(id);
            patient.Active = false;
            //Salvo por causa do UnitOfWork que commita as transições no final de cada requisição.
        }

        public void CreateOrEditPatient(PatientDto patientDto)
        {
            Patient? patient = null;

            if (patientDto.PatientId > 0)
                patient = _patientRepository.GetByID(patientDto.PatientId);
            else if (!string.IsNullOrEmpty(patientDto.Cpf) && ValidateHelper.ValidateCpf(patientDto.Cpf))
            {
                patient = _patientRepository.GetAll().Where(x => x.Cpf == patientDto.Cpf).FirstOrDefault();
                DomainException.When(patient != null, "CPF Exists In Base");
            }

            var agreement = _agreementRepository.GetByID(patientDto.AgreementId);
            DomainException.When(agreement == null, "Agreement not found");

            if (patient == null)
            {
                patient = new Patient();
                patient.ValidateAndSet(patientDto, agreement);
                _patientRepository.Save(patient);
            }
            else
                patient.Update(patientDto, agreement);
        }
    }
}
