using Codem.Domain.Common;

namespace Codem.Domain.Models;

public class User : IEntity
{
    public Guid Id { get; set; }
}