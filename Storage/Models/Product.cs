using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Storage.Models
{
    public class ProductModel
    {
        public ProductModel()
        {
            Category = new CategoryModel();
        }

        public int ID { get; set; }
        [DisplayName("Код")]
        [Required(ErrorMessage = "Введите пожалуйста Код")]
        [StringLength(4, ErrorMessage = "Максимальная длина Кода 4 символа")]
        [Remote("IsProductCodeAvailable", "Validation", AdditionalFields = "CodeInitialValue")]
        public string Code { get; set; }
        [DataType(DataType.MultilineText)]
        [DisplayName("Наименование")]
        [Required(ErrorMessage = "Введите пожалуйста Наименование")]
        [StringLength(70, ErrorMessage = "Максимальная длина Наименования 70 символов")]
        public string Name { get; set; }
        [DisplayName("Категория")]
        public CategoryModel Category { get; set; }
        [DisplayName("Единица измерения")]
        [Required(ErrorMessage = "Введите пожалуйста Единицу измерения")]
        [StringLength(7, ErrorMessage = "Максимальная длина Единицы измерения 7 символов")]
        public string Unit { get; set; }
        [DisplayName("Цена (опт)")]
        [Required(ErrorMessage = "Введите пожалуйста Цену (опт)")]
        public decimal WholesalePrice { get; set; }
        [DisplayName("Цена (мелкий опт)")]
        [Required(ErrorMessage = "Введите пожалуйста Цену (мелкий опт)")]
        public decimal ShallowWholesalePrice { get; set; }
        [DisplayName("Цена (розница)")]
        [Required(ErrorMessage = "Введите пожалуйста Цену (розница)")]
        public decimal RetailPrice { get; set; }
    }
}