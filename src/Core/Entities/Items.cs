namespace Core.Entities
{
    public record Items
    {
        public string Product { get; init; } = default!;
        public int Amount { get; init; } = default!;
        public decimal Price { get; init; } = default!;
    }
}
