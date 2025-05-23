namespace PatientRegistrations.Domain.Dtos
{
    public class PatientDto
    {
        public int PatientId { get; set; }
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
    }
}
