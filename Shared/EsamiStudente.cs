namespace Shared;

public class EsamiStudente
{
    public int Matricola { get; set; }

    public required List<EsameLight> EsamiSostenuti { get; set; }
}