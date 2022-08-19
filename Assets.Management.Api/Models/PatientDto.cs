namespace Assets.Core.Assets.Management.Api.Models;

    public class PatientDto
    {
        public int Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public DateTime ServiceDate { get; set; }

        public string? Summary { get; set; }
    }