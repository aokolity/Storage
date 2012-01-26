using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Storage.Models
{
    public class LogOnModel
    {
        [Required]
        [DisplayName("E-mail")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Пароль")]
        public string Password { get; set; }

        [DisplayName("Запомнить Меня")]
        public bool RememberMe { get; set; }
    }
}
