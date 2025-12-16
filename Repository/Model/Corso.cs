using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Model;

public class Corso
{
    [Column("id")]
    public int ID { get; set; }
    [Column("nome_insegnamento")]
    public required string Nome { get; set; }
    [Column("cfu")]
    public required int CFU { get; set; }
    
    [Column("rowversion")]
    [Timestamp]
    public byte[] Version { get; set; } = null!;
    
    public List<CorsoDocente>? CorsiDocenti { get; set; }
    public List<Esame>? Esami { get; set; }
}