using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API_Task.Models
{
    [Table("Tarefas")]
    public class Tarefa
    {
        [Key]
        public int id_tarefa { get; set; }

        [Display(Name = "Descrição da tarefa")]
        [Required]
        public string descricao_tarefa { get; set; }

        [Display(Name = "Data prevista de conclusão")]
        [Required]
        public DateTime data_prevista { get; set; }

        [Display(Name = "Data de conclusão")]
        public DateTime? data_conclusao { get; set; }

        [ForeignKey("fk_usuario")]
        [Display(Name = "Id do usuário")]
        [Required]
        public int fk_usuario { get; set; }

        public Usuario Usuario { get; set; }
    }
}
