using Microsoft.AspNetCore.Identity;

namespace Assets.Core.Identity.Service.Domain.Entities;

    public class ApplicationUser : IdentityUser
    {
        public string? Description { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public bool Disabled { get; set; }
        public string? HsaId { get; set; }
        public string? NetworkUserId { get; set; }
        public string? Ssn { get; set; }
        public string? VrkId { get; set; }
        public string? DomainId { get; set; }
    }
