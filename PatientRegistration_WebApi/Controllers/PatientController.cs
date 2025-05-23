using System.Net;
using Microsoft.AspNetCore.Mvc;
using PatientRegistrations.Domain.Agreement;
using PatientRegistrations.Domain.Dtos;
using PatientRegistrations.Domain.Patient;

namespace PatientRegistration_WebApi.Controllers
{
    [ApiController]
    [Route("Patient")]
    public class PatientController : ControllerBase
    {
        private readonly PatientStore _patientStorer;

        private readonly AgreementStore _agreementStorer;
        public PatientController(PatientStore patientStorer, AgreementStore agreementStorer)
        {
            _patientStorer = patientStorer;
            _agreementStorer = agreementStorer;
        }

        [HttpGet]
        [Route("GetAll")]
        public IEnumerable<PatientDto> Get()
        {
            return _patientStorer.GetAll();
        }

        [HttpGet]
        [Route("GetById")]
        public PatientDto GetById(int id)
        {
            return _patientStorer.GetByID(id);
        }

        [HttpDelete]
        [Route("Desactive")]
        public bool DeleteByID(int id)
        {
            _patientStorer.DeleteByID(id);
            return true;
        }

        [HttpPost]
        [Route("CreateOrEditPatient")]
        public bool CreateOrEditPatient(PatientDto dto)
        {
            _patientStorer.CreateOrEditPatient(dto);
            return true;
        }

        [HttpGet]
        [Route("GetAllAgreements")]
        public IEnumerable<AgreementDto> GetAllAgreements()
        {
            return _agreementStorer.GetAll();
        }

    }
}
