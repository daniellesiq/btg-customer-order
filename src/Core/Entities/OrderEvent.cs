namespace Core.Entities
{
    public record OrderEvent
    {
        public int OrderId { get; init; } = default!;
        public int CustomerId { get; init; } = default!;
        public List<Items> Items { get; init; } = default!;
    }
}