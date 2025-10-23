using System.ComponentModel.DataAnnotations.Schema;
using Core.Entities;  // ← IEntity üçin!

namespace Core.Entities.Concrete
{
    public class UserOperationClaim : IEntity  // ← IEntity implement et!
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public int OperationClaimId { get; set; }
        [ForeignKey("OperationClaimId")]
        public virtual OperationClaim OperationClaim { get; set; }
    }
}