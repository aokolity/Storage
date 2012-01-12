using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Storage.Models
{
    public class ClientModel
    {
        public int ID { get; set; }
        [DisplayName("ФИО")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Введите пожалуйста ФИО")]
        public string Name { get; set; }
        [DisplayName("Контактный телефон")]
        [Required(ErrorMessage = "Введите пожалуйста Контактный телефон")]
        public string Telephone { get; set; }
        [DataType(DataType.MultilineText)]
        [DisplayName("Адрес")]
        [Required(ErrorMessage = "Введите пожалуйста Адрес")]
        public string Address { get; set; }
    }
}