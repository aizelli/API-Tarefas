namespace API_Task.DTO
{
    public class TarefaDTO
    {
        public int id_tarefa { get; set; }
        public string descricao_tarefa { get; set; }
        public DateTime data_prevista { get; set; }
        public DateTime? data_conclusao { get; set; }
        public int fk_usuario { get; set; }
    }
}
