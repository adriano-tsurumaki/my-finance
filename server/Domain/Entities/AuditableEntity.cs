namespace server.Domain.Entities
{
    public class AuditableEntity
    {
        //Soft delete
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
