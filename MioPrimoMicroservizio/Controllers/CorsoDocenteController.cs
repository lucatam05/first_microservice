using Business.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared;
using Shared.Exceptions;


namespace MioPrimoMicroservizio.Controllers;


[ApiController]
[Route("[controller]/[action]")]
public class CorsoDocenteController(IBusinessApp businessApp) : ControllerBase
{
    [HttpGet(Name = "GetRelazioneCorsoDocente")]
    public async Task<CorsoDocenteDTO?> GetRelazioneCorsoDocenteAsync(int idCorso, int matricola, CancellationToken cancellationToken = default)
    {
        return await businessApp.GetRelazioneCorsoDocenteAsync(idCorso, matricola, cancellationToken);
    }

    [HttpPost(Name = "InsertRelazioneCorsoDocente")]
    public async Task<ActionResult> InsertRelazioneCorsoDocenteAsync(int id, int matricola, DateTime dataAssegnazione, CancellationToken cancellationToken = default)
    {
        try
        {
            await businessApp.InsertRelazioneCorsoDocenteAsync(id, matricola, dataAssegnazione, cancellationToken);

            return Ok();
        }
        catch (ModelAlreadyThereException ex)
        {
            return NotFound(ex.Message);
        }
        catch (DbUpdateConcurrencyException)
        {
            return Conflict("La relazione è stata modificata da un altro utente");
        }
    }

    [HttpPost(Name = "UpdateRelazioneCorsoDocente")]
    public async Task<ActionResult> UpdateRelazioneCorsoDocenteAsync(int id, int matricola, DateTime dataAssegnazione, CancellationToken cancellationToken = default)
    {
        try
        {
            await businessApp.UpdateRelazioneCorsoDocenteAsync(id, matricola, dataAssegnazione, cancellationToken);

            return Ok();
        }
        catch (ModelNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (DbUpdateConcurrencyException)
        {
            return Conflict("La relazione è stata modificata da un altro utente");
        }
    }
    [HttpPost(Name = "DeleteRelazioneCorsoDocente")]
    public async Task<ActionResult> DeleteRelazioneCorsoDocenteAsync(int id, int matricola, CancellationToken cancellationToken = default)
    {
        try
        {
            await businessApp.DeleteRelazioneCorsoDocenteAsync(id, matricola, cancellationToken);

            return Ok();
        }
        catch (ModelNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (DbUpdateConcurrencyException)
        {
            return Conflict("Il corso è stato modificato da un altro utente");
        }
    }
}