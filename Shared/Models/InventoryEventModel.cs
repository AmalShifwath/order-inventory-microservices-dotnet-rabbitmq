public class InventoryEventModel
{
    public string ItemId { get; set; }
    public string EventType { get; set; }
    public int QuantityChanged { get; set; }
    public DateTime EventDate { get; set; }
}