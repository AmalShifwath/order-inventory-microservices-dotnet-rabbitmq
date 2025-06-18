public class UpdateInventoryDto
{
    public Guid productId { get; set; }
    public int count { get; set; }
    public bool deficit { get; set; }
}