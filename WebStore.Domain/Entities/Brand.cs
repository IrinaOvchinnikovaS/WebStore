using WebStore.Domain.Entities.Base;
using WebStore.Domain.Entities.Base.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace WebStore.Domain.Entities
{
    //[Table("Brandssss")] имя таблицы в БД
    [Index(nameof(Name), IsUnique = true)]
    public class Brand : NamedEntity, IOrderedEntity
    {
        //[Column("BrandOrder")]
        public int Order { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
