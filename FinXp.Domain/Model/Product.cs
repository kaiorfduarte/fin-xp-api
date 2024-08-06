namespace FinXp.Domain.Model;

public record Product(int ProductId, string Name, int Quantity, DateTime RegisterDate, DateTime ProductDueDate);