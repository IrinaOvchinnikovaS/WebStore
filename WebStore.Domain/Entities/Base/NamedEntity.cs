using WebStore.Domain.Entities.Base.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace WebStore.Domain.Entities.Base;

public abstract class NamedEntity : Entity, INamedEntity
{
    [Required] //уникальное поле
    public string Name { get; set; }
}
