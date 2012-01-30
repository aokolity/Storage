using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Storage.Models
{
    public class UserModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Введите пожалуйста Имя")]
        [DisplayName("Имя")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Введите пожалуйста Фамилию")]
        [DisplayName("Фамилия")]
        public string Surname { get; set; }
    }
}