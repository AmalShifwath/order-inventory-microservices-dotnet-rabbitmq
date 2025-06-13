public class OrderEventModel
{
    public string OrderId { get; set; }
    public string ProductId { get; set; }
    public int Quantity { get; set; }
    public string Status { get; set; }
    public DateTime EventDate { get; set; }
}