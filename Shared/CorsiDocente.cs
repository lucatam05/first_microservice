namespace Shared;

public class CorsiDocente
{
    public int Matricola { get; set; }

    public required List<CorsoLight> CorsiTenuti { get; set; }
}