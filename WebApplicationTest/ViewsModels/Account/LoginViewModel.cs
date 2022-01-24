using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationTest.ViewsModels.Account
{
    public class LoginViewModel
    {
        [Display(Name = "Логин")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Поле \"Логин\" не заполнено.")]
        [StringLength(75, ErrorMessage = "Длинна логина должна быть больше {1} и меньше {0}", MinimumLength = 5)]
        public string Name { get; set; }

        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Поле \"Пароля\" не заполнено.")]
        [StringLength(75, ErrorMessage = "Длинна Пароля должна быть больше {1} и меньше {0}", MinimumLength = 7)]
        public string Password { get; set; }
    }
}
