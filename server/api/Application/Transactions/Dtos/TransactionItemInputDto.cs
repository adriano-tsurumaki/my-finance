namespace server.Application.Transactions.Dtos;

public class TransactionItemInputDto
{
    public string Name { get; set; } = string.Empty;
    public decimal Quantity { get; set; } = 1.0m;
    public string UnitOfMeasure { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
}
