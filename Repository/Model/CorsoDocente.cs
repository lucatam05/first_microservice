using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Model;

public class CorsoDocente
{
    [Column("corso_id")]
    public required int IdCorso { get; set; }
  
    [Column("matricola_docente")]
    public required int MatricolaDocente { get; set; }

    [Column("data_assegnazione")]
    public DateTime? DataAssegnazione { get; set; }
    
    [Column("rowversion")]
    [Timestamp]
    public byte[] Version { get; set; } = null!;
    
    public Corso? Corso { get; set; }
    public Docente? Docente { get; set; }
}