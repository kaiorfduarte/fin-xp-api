using FinXp.Domain.Enum;

namespace FinXp.Domain.Model;

public record NegotiationProduct(int ProductId, int ClientId, int Quantity, OperationType OperationTypeId);
