namespace DTOs
{
    public record ProductDto
    (
        int ProductId,
        string? ProductName,
        double? Price,
        int CategoryId, 
        string? CategoryName, 
        string? Description,
        string? ImgUrl,
        string? Color,
        string? Material,
        short Quantity, 
        bool IsActive,
        string? ImgUrl2
    )
    {
        public ProductDto() : this(0, null, null, 0, null, null, null, null, null, 0, true,null) { }
    }
}