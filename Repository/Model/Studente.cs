using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Model;

public class Studente
{
    [Column("matricola")]
    public int Matricola { get; set; }
    [Column("nome")] 
    public required string Nome { get; set; }
    [Column("cognome")]
    public required string Cognome { get; set; }
    [Column("data_nascita")]
    public DateTime DataNascita { get; set; }
    [Column("data_immatricolazione")]
    public DateTime DataImmatricolazione { get; set; }
    
    public List<Esame>? Esami { get; set; }
    
    [Column("rowversion")]
    [Timestamp]
    public byte[] Version { get; set; } = null!;
}