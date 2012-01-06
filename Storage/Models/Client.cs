using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Storage.Models
{
    public class ClientModel
    {
        public int ID { get; set; }
        [DisplayName("ФИО")]
        [DataType(DataType.MultilineText)]
        public string Name { get; set; }
        [DisplayName("Контактный телефон")]
        public string Telephone { get; set; }
        [DataType(DataType.MultilineText)]
        [DisplayName("Адрес")]
        public string Address { get; set; }
    }
}