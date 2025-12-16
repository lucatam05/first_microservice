using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Model;

public class Esame
{
    [Column("matricola_studente")]
    public int MatricolaStudente { get; set; }
    [Column("id_corso")]
    public int IdCorso { get; set; }
    [Column("data_esame")]
    public required DateTime DataEsame { get; set; }
    [Column("voto")]
    public required int Voto { get; set; }
    [Column("lode")]
    public bool Lode { get; set; }
    [Column("rowversion")]
    [Timestamp]
    public byte[] Version { get; set; } = null!;

    
    public Studente? Studente { get; set; }
    public Corso? Corso { get; set; }
}