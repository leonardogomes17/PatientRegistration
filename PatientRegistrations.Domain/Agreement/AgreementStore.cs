using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PatientRegistrations.Domain.Dtos;
using PatientRegistrations.Domain.Interfaces;

namespace PatientRegistrations.Domain.Agreement
{
    public class AgreementStore
    {
        private readonly IRepository<Agreement> _agreementRepository;
        public AgreementStore(IRepository<Agreement> agreementRepository)
        {
            _agreementRepository = agreementRepository;
        }

        public IEnumerable<AgreementDto> GetAll()
        {
            var agreements = _agreementRepository.GetAll().AsEnumerable();

            var dtoAgreement = agreements.Select(x => new AgreementDto()
            {
                AgreementId = x.Id,
                AgreementName = x.AgreementName
            });

            return dtoAgreement;
        }
    }

    
        
}
