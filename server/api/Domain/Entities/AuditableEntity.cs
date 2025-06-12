namespace api.Domain.Entities
{
    public class AuditableEntity
    {
        //Soft delete
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
