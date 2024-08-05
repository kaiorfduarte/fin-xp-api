using FinXp.Domain.Enum;
using System.Reflection.Emit;

namespace FinXp.Domain.Model.Request;

public class NegotiationRequest
{
    public int ProductId { get; init; }

    public int ClientId { get; init; }

    public int Quantity { get; init; }

    public OperationType OperationTypeId { get; init; }
}
