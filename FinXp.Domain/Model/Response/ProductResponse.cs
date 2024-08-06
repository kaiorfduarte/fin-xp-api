namespace FinXp.Domain.Model.Response;

public record ProductResponse(int ProductId, string Name, int Quantity, DateTime RegisterDate, DateTime ProductDueDate);
