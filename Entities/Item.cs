using System;
namespace Catalog.Entities
{
    public record Item
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public int Price { get; init ; }
        public DateTimeOffset  CreatedDate { get; init; }
        
    }
}