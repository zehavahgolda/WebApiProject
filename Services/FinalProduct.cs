using DTOs;
namespace Services
{
    public class FinalProducts
    {
        public IEnumerable<ProductDto> Items { get; set; }
        public int TotalCount { get; set; }
        public bool HasNext { get; set; }
        public bool HasPrev { get; set; }
        public FinalProducts(IEnumerable<ProductDto> items, int total, bool hasNext, bool hasPrev)
        {
            Items = items;
            TotalCount = total;
            HasNext = hasNext;
            HasPrev = hasPrev;
        }

    }


}