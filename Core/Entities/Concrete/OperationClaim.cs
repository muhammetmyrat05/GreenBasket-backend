using Core.Entities;  // ← IEntity üçin!

namespace Core.Entities.Concrete
{
    public class OperationClaim : IEntity  // ← IEntity implement et!
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}