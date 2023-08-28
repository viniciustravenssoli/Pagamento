using System.ComponentModel.DataAnnotations;

namespace Manager.API.ViewModels{
    public class UpdateUserViewModel{

        [Required(ErrorMessage = "O Id nao pode ser vazio")]
        [Range(1, int.MaxValue, ErrorMessage = "O id deve ter no minomo 1 caracteres")]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome nao pode ser vazio")]
        [MinLength(3, ErrorMessage = "O nome deve ter no minomo 3 caracteres")]
        [MaxLength(80, ErrorMessage = "O Nome nao pode ter mais de 80 caracteres")]
        public string Name { get;  set; }

        [Required(ErrorMessage = "O Email nao pode ser vazio")]
        [MinLength(8, ErrorMessage = "O Email deve ter no minomo 8 caracteres")]
        [MaxLength(180, ErrorMessage = "O Email nao pode ter mais de 180 caracteres")]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "O email é invalido")]

        public string Email { get;  set; }

        [Required(ErrorMessage = "A senha nao pode ser vazio")]
        [MinLength(8, ErrorMessage = "A senha deve ter no minomo 8 caracteres")]
        [MaxLength(60, ErrorMessage = "A senha nao pode ter mais de 60 caracteres")]
        public string Password { get;  set; }
    }
}