using Microsoft.EntityFrameworkCore;
using Repository.Abstractions;
using Repository.Model;
using Shared;
using Shared.Exceptions;

namespace Repository;

public class RepositoryApp(UniprDbContext context) : IRepositoryApp
{
    public async Task<Studente?> GetStudenteAsync(int matricola, CancellationToken cancellationToken = default)
    {
        return await context.Studenti.Where(s => s.Matricola == matricola).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task InsertStudenteAsync(string nome, string cognome, DateTime dataNascita,
        DateTime dataImmatricolazione, CancellationToken cancellationToken = default)
    {
        Studente studente = new Studente
        {
            Nome = nome,
            Cognome = cognome,
            DataNascita = dataNascita,
            DataImmatricolazione = dataImmatricolazione
        };

        context.Add(studente);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateStudenteAsync(int matricola, string nome, string cognome, DateTime dataNascita,
        DateTime dataImmatricolazione, CancellationToken cancellationToken = default)
    {
        Studente? studente = await GetStudenteAsync(matricola, cancellationToken);
        if (studente is null)
            throw new ModelNotFoundException("Non Trovato");
        
        studente.Nome = nome;
        studente.Cognome = cognome;
        studente.DataNascita = dataNascita;
        studente.DataImmatricolazione = dataImmatricolazione;

        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteStudenteAsync(int matricola, CancellationToken cancellationToken = default)
    {
        var studente = await GetStudenteAsync(matricola, cancellationToken);
        if (studente is null)
            throw new ModelNotFoundException("Non Trovato");
        
        context.Remove(studente);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Docente?> GetDocenteAsync(int matricolaDocente, CancellationToken cancellationToken = default)
    {
        return await context.Docenti.Where(d => d.Matricola == matricolaDocente).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task InsertDocenteAsync(string nome, string cognome,
        CancellationToken cancellationToken = default)
    {
        Docente docente = new Docente
        {
            Nome = nome,
            Cognome = cognome,
        };

        await context.AddAsync(docente, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteDocenteAsync(int matricolaDocente, CancellationToken cancellationToken = default)
    {
        var docente = await GetDocenteAsync(matricolaDocente, cancellationToken);
        if (docente is null)
            throw new ModelNotFoundException("Non Trovato");
        
        context.Remove(docente);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateDocenteAsync(int matricolaDocente, string nome, string cognome,
        CancellationToken cancellationToken = default)
    {
        Docente? docente = await GetDocenteAsync(matricolaDocente, cancellationToken);
        if (docente is null)
            throw new ModelNotFoundException("Non Trovato");
        
        docente.Nome = nome;
        docente.Cognome = cognome;

        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Esame?> GetEsameAsync(int matricola, int idCorso,
        CancellationToken cancellationToken = default)
    {
        return await context.Esami.Where(e =>
            e.MatricolaStudente == matricola && 
            e.IdCorso == idCorso)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task InsertEsameAsync(int matricola, int idCorso, DateTime data, int voto, bool lode,
        CancellationToken cancellationToken = default)
    {
        Esame esame = new Esame
        {
            MatricolaStudente = matricola,
            IdCorso = idCorso,
            DataEsame = data,
            Voto = voto,
            Lode = lode
        };

        context.Add(esame);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteEsameAsync(int matricola, int idCorso,
        CancellationToken cancellationToken = default)
    {
        var esame = await GetEsameAsync(matricola, idCorso, cancellationToken);
        if (esame is null)
            throw new ModelNotFoundException("Non Trovato");
        
        context.Remove(esame);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateEsameAsync(int matricola, int idCorso, DateTime data, int voto, bool lode,
        CancellationToken cancellationToken = default)
    {
        Esame? esame = await GetEsameAsync(matricola, idCorso, cancellationToken);
        
        if (esame is null)
            throw new ModelNotFoundException("Non Trovato");
        
        esame.DataEsame = data;
        esame.Voto = voto;
        esame.Lode = lode;

        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Corso?> GetCorsoAsync(int id, CancellationToken cancellationToken = default)
    {
        return await context.Corsi.Where(c => c.ID == id).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task InsertCorsoAsync(string nome, int cfu,
        CancellationToken cancellationToken = default)
    {
        Corso corso = new Corso
        {
            Nome = nome,
            CFU = cfu,
        };

        await context.AddAsync(corso, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteCorsoAsync(int id, CancellationToken cancellationToken = default)
    {
        Corso? corso = await GetCorsoAsync(id, cancellationToken);
        
        if (corso is null)
            throw new ModelNotFoundException("Non Trovato");
        
        context.Remove(corso);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateCorsoAsync(int id, string nome, int cfu,
        CancellationToken cancellationToken = default)
    {
        Corso? corso = await GetCorsoAsync(id, cancellationToken);
        
        if (corso is null)
            throw new ModelNotFoundException("Non Trovato!");

        corso.Nome = nome;
        corso.CFU = cfu;

        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<CorsoDocente?> GetRelazioneCorsoDocenteAsync(int id, int matricola, CancellationToken cancellationToken = default)
    {
        return await context.CorsoDocente_.Where(p => p.IdCorso == id && p.MatricolaDocente == matricola)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task InsertRelazioneCorsoDocenteAsync(int id, int matricola, DateTime dataAssegnazione, CancellationToken cancellationToken = default)
    {
        CorsoDocente? temp = await GetRelazioneCorsoDocenteAsync(id, matricola, cancellationToken);
        if (temp is not null)
            throw new ModelAlreadyThereException("Docente già presente in questo corso");
        CorsoDocente corsoDocente = new CorsoDocente
        {
            DataAssegnazione = dataAssegnazione,
            MatricolaDocente = matricola,
            IdCorso = id
        };
        await context.AddAsync(corsoDocente, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateRelazioneCorsoDocenteAsync(int id, int matricola, DateTime dataAssegnazione, CancellationToken cancellationToken = default)
    {
        CorsoDocente? temp = await GetRelazioneCorsoDocenteAsync(id, matricola, cancellationToken);
        if (temp is null)
            throw new ModelNotFoundException("Non è presente questa relazione");
        temp.DataAssegnazione = dataAssegnazione;
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteRelazioneCorsoDocenteAsync(int id, int matricola, CancellationToken cancellationToken = default)
    {
        CorsoDocente? temp = await GetRelazioneCorsoDocenteAsync(id, matricola, cancellationToken);
        if (temp is null)
            throw new ModelNotFoundException("Non è presente questa relazione");
        context.Remove(temp);
        await context.SaveChangesAsync(cancellationToken);
    }
    
    public async Task<List<CorsoDocente>> GetCorsiDocenteAsync(int matricolaDocente, CancellationToken cancellationToken = default)
    {
      return await context.CorsoDocente_
          .Where(c => c.MatricolaDocente == matricolaDocente)
          .Include(c => c.Corso)
          .ToListAsync(cancellationToken);
    }

    public async Task<List<CorsoDocente>> GetDocentiPerCorsoAsync(int id, CancellationToken cancellationToken = default)
    {
        return await context.CorsoDocente_
            .Where(c => c.IdCorso == id)
            .Include(c => c.Docente)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Esame>> GetEsamiAsync(int matricola, CancellationToken cancellationToken = default)
    {
        return await context.Esami
            .Where(c => c.MatricolaStudente == matricola)
            .Include(c => c.Studente)
            .ToListAsync(cancellationToken);
    }

    public async Task<float> GetMediaEsamiAsync(int matricola, CancellationToken cancellationToken = default)
    {
        List<Esame> esami = await GetEsamiAsync(matricola, cancellationToken);
        
        if (!esami.Any())
            throw new ModelNotFoundException("Non ci sono esami collegati a questa matricola!");

        return CalcoloMedia(esami);
    }

    public async Task<float> GetMediaCorsoAsync(int id, CancellationToken cancellationToken = default)
    {
        List<Esame> esami = await context.Esami.
            Where(c => c.IdCorso == id).ToListAsync(cancellationToken);
        
        if (!esami.Any())
            throw new ModelNotFoundException("Non sono stati registrati esami per questo corso!");
        
        return CalcoloMedia(esami);
    }

    private float CalcoloMedia(List<Esame> esami)
    {
        float temp = 0;
        foreach (Esame esame in esami)
            temp += esame.Voto;

        return temp / esami.Count;
    }

    public async Task<int> GetCFUAsync(int matricola, CancellationToken cancellationToken = default)
    {
        var query = await context.Esami.Where(e => e.MatricolaStudente == matricola)
            .Join(context.Corsi,
                e => e.IdCorso, c => c.ID, (e, c) => new { e, c })
            .ToListAsync(cancellationToken);

        int temp = 0;
        foreach (var item in query)
        {
            temp += item.c.CFU;
        }

        return temp;
    }
}