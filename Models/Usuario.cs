using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Task.Models
{
    [Table("Usuarios")]
    public class Usuario
    {
        [Key]
        public int id_usuario { get; set; }

        [Display(Name = "Nome do Usuário")]
        [Required]
        public string nome_usuario { get; set; }

        [Display(Name = "Email do Usuário")]
        [Required]
        public string email_usuario { get; set; }

        [Display(Name = "Senha do Usuário")]
        [Required]
        public string senha_usario { get; set; }

        public ICollection<Tarefa> Tarefas { get; set; }
    }
}
