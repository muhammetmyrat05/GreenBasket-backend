using System;
using System.Collections.Generic;
using Core.Entities;  // ← IEntity üçin!

namespace Core.Entities.Concrete
{
    public class User : IEntity  // ← IEntity implement et!
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedAt { get; set; }

        public string DisplayName => $"{FirstName} {LastName}";

        public virtual ICollection<UserOperationClaim> UserOperationClaims { get; set; }
    }
}