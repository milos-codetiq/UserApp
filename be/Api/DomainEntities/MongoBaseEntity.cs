namespace Api.DomainEntities
{
    public class MongoBaseEntity
    {
        public DateTime CreatedAtUtc { get; set; }
        public Guid Id { get; set; }

        public MongoBaseEntity()
        {
            Id = Guid.NewGuid();
            CreatedAtUtc = DateTime.UtcNow;
        }
    }
}
