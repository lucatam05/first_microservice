using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Model;

public class Docente
{
    [Column("matricola")]
    public int Matricola { get; set; }
    
    [Column("nome")]
    public required string Nome { get; set; }
    
    [Column("cognome")]
    public required string Cognome { get; set; }
    
    [Column("rowversion")]
    [Timestamp]
    public byte[] Version { get; set; } = null!;
    
    public List<CorsoDocente>? Corsi { get; set; }

}