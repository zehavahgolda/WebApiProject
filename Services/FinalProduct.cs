using DTOs;
using System.Text.Json.Serialization; 

namespace Services
{
    public class FinalProducts
    {
        public IEnumerable<ProductDto> Items { get; set; }
        public int TotalCount { get; set; }
        public bool HasNext { get; set; }
        public bool HasPrev { get; set; }

        [JsonConstructor] 
        public FinalProducts(IEnumerable<ProductDto> items, int totalCount, bool hasNext, bool hasPrev)
        {
            Items = items;
            TotalCount = totalCount;
            HasNext = hasNext;
            HasPrev = hasPrev;
        }
    }
}