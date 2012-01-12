using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Storage.Models
{
    public class CategoryModel
    {
        public int? ID { get; set; }
        [DisplayName("Название")]
        [Required(ErrorMessage = "Введите пожалуйста Название")]
        public string Name { get; set; }
    }
}