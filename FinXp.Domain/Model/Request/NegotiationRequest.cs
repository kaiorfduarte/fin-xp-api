using FinXp.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace FinXp.Domain.Model.Request;

public class NegotiationRequest
{
    [Required]
    public int ProductId { get; init; }

    [Required]
    public int ClientId { get; init; }

    [Required]
    public int Quantity { get; init; }

    [Required]
    public OperationType OperationTypeId { get; init; }
}
