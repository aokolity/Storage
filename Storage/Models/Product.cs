using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Storage.Models
{
    public class ProductModel
    {
        public int ID { get; set; }
        [DisplayName("Код")]
        public string Code { get; set; }
        [DataType(DataType.MultilineText)]
        [DisplayName("Наименование")]
        public string Name { get; set; }
        [DisplayName("Единица измерения")]
        public string Unit { get; set; }
        [DisplayName("Цена (опт)")]
        public decimal WholesalePrice { get; set; }
        [DisplayName("Цена (мелкий опт)")]
        public decimal ShallowWholesalePrice { get; set; }
        [DisplayName("Цена (розница)")]
        public decimal RetailPrice { get; set; }
    }
}