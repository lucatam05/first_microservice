using Repository.Model;

namespace Repository.Abstractions;

public interface IRepositoryApp
{
    Task<Studente?> GetStudenteAsync(int matricola, CancellationToken cancellationToken = default);
    Task InsertStudenteAsync(string nome, string cognome, DateTime dataNascita, 
        DateTime dataImmatricolazione, CancellationToken cancellationToken = default);
    Task UpdateStudenteAsync(int matricola, string nome, string cognome, DateTime dataNascita, 
        DateTime dataImmatricolazione, CancellationToken cancellationToken = default);
    Task DeleteStudenteAsync(int matricola, CancellationToken cancellationToken = default);
    
    Task<Docente?> GetDocenteAsync(int matricolaDocente, CancellationToken cancellationToken = default);
    Task InsertDocenteAsync(string nome, string cognome, CancellationToken cancellationToken = default);
    Task DeleteDocenteAsync(int matricolaDocente, CancellationToken cancellationToken = default);
    Task UpdateDocenteAsync(int matricolaDocente, string nome, string cognome,CancellationToken cancellationToken = default);
    
    Task<Esame?> GetEsameAsync(int matricola, int idCorso, CancellationToken  cancellationToken = default);
    Task InsertEsameAsync(int matricola, int idCorso, DateTime data, int voto, bool lode, CancellationToken cancellationToken = default);
    Task DeleteEsameAsync(int matricola, int idCorso, CancellationToken  cancellationToken = default);
    Task UpdateEsameAsync(int matricola, int idCorso, DateTime data, int voto, bool lode, CancellationToken  cancellationToken = default);
    
    Task<Corso?> GetCorsoAsync(int idCorso, CancellationToken cancellationToken = default);
    Task InsertCorsoAsync(string nome, int cfu, CancellationToken cancellationToken = default);
    Task DeleteCorsoAsync(int id, CancellationToken  cancellationToken = default);
    Task UpdateCorsoAsync(int id, string nome, int cfu, CancellationToken cancellationToken = default);

    Task<CorsoDocente?> GetRelazioneCorsoDocenteAsync(int id, int matricola, CancellationToken cancellationToken = default);
    
    Task InsertRelazioneCorsoDocenteAsync(int id, int matricola, DateTime dataAssegnazione, CancellationToken cancellationToken = default);
    Task UpdateRelazioneCorsoDocenteAsync(int id, int matricola, DateTime dataAssegnazione, CancellationToken cancellationToken = default);
    Task DeleteRelazioneCorsoDocenteAsync(int id, int matricola, CancellationToken cancellationToken = default);
    

    Task<List<CorsoDocente>> GetCorsiDocenteAsync(int matricolaDocente, CancellationToken cancellationToken = default);

    Task<List<CorsoDocente>> GetDocentiPerCorsoAsync(int id, CancellationToken cancellationToken = default);
    Task<List<Esame>> GetEsamiAsync(int matricola, CancellationToken cancellationToken = default);
    Task<float> GetMediaEsamiAsync(int matricola, CancellationToken cancellationToken = default);
    Task<float> GetMediaCorsoAsync(int id, CancellationToken cancellationToken = default);

    Task<int> GetCFUAsync(int matricola, CancellationToken cancellationToken = default);


}