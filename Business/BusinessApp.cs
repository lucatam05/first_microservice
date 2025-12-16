using Business.Abstractions;
using Repository.Abstractions;
using Repository.Model;
using Shared;

namespace Business;

public class BusinessApp(IRepositoryApp repository) : IBusinessApp
{
    public async Task<StudenteDTO?> GetStudenteAsync(int matricola, CancellationToken cancellationToken = default)
    {
        Studente? studente = await repository.GetStudenteAsync(matricola, cancellationToken);
        if (studente is null)
            return null;

        StudenteDTO studenteDto = new StudenteDTO
        {
            Matricola = studente.Matricola,
            Nome = studente.Nome,
            Cognome = studente.Cognome
        };

        return studenteDto;
    }

    public async Task InsertStudenteAsync(string nome, string cognome, DateTime dataNascita,
        DateTime dataImmatricolazione, CancellationToken cancellationToken = default)
    {
        await repository.InsertStudenteAsync(nome, cognome, dataNascita, dataImmatricolazione, cancellationToken);
    }

    public async Task UpdateStudenteAsync(int matricola, string nome, string cognome, DateTime dataNascita,
        DateTime dataImmatricolazione, CancellationToken cancellationToken = default)
    {
        await repository.UpdateStudenteAsync(matricola, nome, cognome, dataNascita, dataImmatricolazione, cancellationToken);
    }

    public async Task DeleteStudenteAsync(int matricola, CancellationToken cancellationToken = default)
    {
        await repository.DeleteStudenteAsync(matricola, cancellationToken);
    }

    public async Task<DocenteDTO?> GetDocenteAsync(int matricolaDocente, CancellationToken cancellationToken = default)
    {
        Docente? docente = await repository.GetDocenteAsync(matricolaDocente, cancellationToken);
        if (docente is null)
            return null;
        DocenteDTO docenteDto = new DocenteDTO
        {
            Nome = docente.Nome,
            Cognome = docente.Cognome,
            Matricola = docente.Matricola
        };

        return docenteDto;
    }

    public async Task InsertDocenteAsync(string nome, string cognome,
        CancellationToken cancellationToken = default)
    {
        await repository.InsertDocenteAsync(nome, cognome, cancellationToken);
    }

    public async Task DeleteDocenteAsync(int matricolaDocente, CancellationToken cancellationToken = default)
    {
        await repository.DeleteDocenteAsync(matricolaDocente, cancellationToken);
    }

    public async Task UpdateDocenteAsync(int matricolaDocente, string nome, string cognome,
        CancellationToken cancellationToken = default)
    {
        await repository.UpdateDocenteAsync(matricolaDocente, nome, cognome, cancellationToken);
    }

    public async Task<EsameLight?> GetEsameAsync(int matricola, int idCorso,
        CancellationToken cancellationToken = default)
    {
        Esame? esame = await repository.GetEsameAsync(matricola, idCorso, cancellationToken);
        if (esame is null)
            return null;
        EsameLight esameLight = new EsameLight
        {
            IDCorso = esame.IdCorso,
            DataEsame = esame.DataEsame
        };
        return esameLight;
    }

    public async Task InsertEsameAsync(int matricola, int idCorso, DateTime data, int voto, bool lode,
        CancellationToken cancellationToken = default)
    {
        await repository.InsertEsameAsync(matricola, idCorso, data, voto, lode, cancellationToken);
    }

    public async Task DeleteEsameAsync(int matricola, int idCorso, CancellationToken cancellationToken = default)
    {
        await repository.DeleteEsameAsync(matricola, idCorso, cancellationToken);
    }

    public async Task UpdateEsameAsync(int matricola, int idCorso, DateTime data, int voto, bool lode,
        CancellationToken cancellationToken = default)
    {
        await repository.UpdateEsameAsync(matricola, idCorso, data, voto, lode, cancellationToken);
    }

    public async Task<CorsoLight?> GetCorsoAsync(int idCorso, CancellationToken cancellationToken = default)
    {
        Corso? corso = await repository.GetCorsoAsync(idCorso, cancellationToken);
        if (corso is null)
            return null;
        
        CorsoLight corsoLight = new CorsoLight
        {
            Nome = corso.Nome,
            CFU = corso.CFU
        };
        
        return corsoLight;
    }

    public async Task InsertCorsoAsync(string nome, int cfu,
        CancellationToken cancellationToken = default)
    {
        await repository.InsertCorsoAsync(nome, cfu, cancellationToken);
    }

    public async Task DeleteCorsoAsync(int id, CancellationToken cancellationToken = default)
    {
        await repository.DeleteCorsoAsync(id, cancellationToken);
    }
    
    public async Task UpdateCorsoAsync(int id, string nome, int cfu,
        CancellationToken cancellationToken = default)
    {
        await repository.UpdateCorsoAsync(id, nome, cfu, cancellationToken);
    }

    public async Task<CorsoDocenteDTO?> GetRelazioneCorsoDocenteAsync(int id, int matricola, CancellationToken cancellationToken = default)
    {
        CorsoDocente? temp = await repository.GetRelazioneCorsoDocenteAsync(id, matricola, cancellationToken);
        if (temp is null)
            return null;
        CorsoDocenteDTO corsoDocenteDto = new CorsoDocenteDTO
        {
            IdCorso = temp.IdCorso,
            MatricolaDocente = temp.MatricolaDocente,
            DataAssegnazione = temp.DataAssegnazione
        };

        return corsoDocenteDto;
    }

    public async Task InsertRelazioneCorsoDocenteAsync(int id, int matricola, DateTime dataAssegnazione,
        CancellationToken cancellationToken = default)
    {
        await repository.InsertRelazioneCorsoDocenteAsync(id, matricola, dataAssegnazione, cancellationToken);
    }

    public async Task DeleteRelazioneCorsoDocenteAsync(int id, int matricola, CancellationToken cancellationToken = default)
    {
        await repository.DeleteRelazioneCorsoDocenteAsync(id, matricola, cancellationToken);
    }

    public async Task UpdateRelazioneCorsoDocenteAsync(int id, int matricola, DateTime dataAssegnazione,
        CancellationToken cancellationToken = default)
    {
        await repository.UpdateRelazioneCorsoDocenteAsync(id, matricola, dataAssegnazione, cancellationToken);
    }

    public async Task<CorsiDocente> GetCorsiDocenteAsync(int matricolaDocente, CancellationToken cancellationToken = default)
    {
        List<CorsoDocente> corsiDocente = await repository.GetCorsiDocenteAsync(matricolaDocente, cancellationToken);
        
        return new CorsiDocente
        {
            Matricola = matricolaDocente,
            CorsiTenuti = corsiDocente.Select(p => new CorsoLight
            {
                Nome = p.Corso.Nome,
                CFU = p.Corso.CFU
            }).ToList()
        };
    }

    public async Task<DocentiPerCorso> GetDocentiPerCorsoAsync(int id, CancellationToken cancellationToken = default)
    {
        List<CorsoDocente> docentiPerCorsi = await repository.GetDocentiPerCorsoAsync(id, cancellationToken);
        return new DocentiPerCorso
        {
            IdCorso = id,
            Docenti = docentiPerCorsi.Select(p => new DocenteDTO
            {
                Nome = p.Docente.Nome,
                Cognome = p.Docente.Cognome,
                Matricola = p.MatricolaDocente
            }).ToList()
        };
    }

    public async Task<EsamiStudente> GetEsamiAsync(int matricola, CancellationToken cancellationToken = default)
    {
        List<Esame> esami = await repository.GetEsamiAsync(matricola, cancellationToken);
        
        return new EsamiStudente
        {
            Matricola = matricola,
            EsamiSostenuti = esami.Select(p => new EsameLight
            {
                IDCorso = p.IdCorso,
                DataEsame = p.DataEsame
            }).ToList()
        };
    }

    public async Task<float> GetMediaEsamiAsync(int matricola, CancellationToken cancellationToken = default)
    {
        return await repository.GetMediaEsamiAsync(matricola, cancellationToken);
    }

    public async Task<float> GetMediaCorsoAsync(int id, CancellationToken cancellationToken = default)
    {
        return await repository.GetMediaCorsoAsync(id, cancellationToken);
    }

    public async Task<int> GetCFUAsync(int matricola, CancellationToken cancellationToken = default)
    {
        return await repository.GetCFUAsync(matricola, cancellationToken);
    }
}