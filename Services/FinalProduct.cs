using DTOs;
namespace Services
{
    public class FinalProducts
    {
        public List<ProductDto> Items { get; set; }
        public int TotalCount { get; set; }
        public bool HasNext { get; set; }
        public bool HasPrev { get; set; }
    }
}