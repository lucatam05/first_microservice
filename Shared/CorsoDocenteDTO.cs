namespace Shared;

public class CorsoDocenteDTO
{
    public required int IdCorso { get; set; }
    public required int MatricolaDocente { get; set; }
    public DateTime? DataAssegnazione { get; set; }
}