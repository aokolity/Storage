using System.ComponentModel;

namespace Storage.Models
{
    public class CategoryModel
    {
        public int? ID { get; set; }
        [DisplayName("Название")]
        public string Name { get; set; }
    }
}