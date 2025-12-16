using Business.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared;
using Shared.Exceptions;


namespace MioPrimoMicroservizio.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class EsamiController(IBusinessApp businessApp) : ControllerBase
{
    [HttpGet(Name = "GetEsame")]
    public async Task<EsameLight?> GetEsameAsync(int matricola, int idCorso,
        CancellationToken cancellationToken = default)
    {
        return await businessApp.GetEsameAsync(matricola, idCorso, cancellationToken);
    }

    [HttpPost(Name = "InsertEsame")]
    public async Task InsertEsameAsync(int matricola, int idCorso, DateTime data, int voto, bool lode,
        CancellationToken cancellationToken = default)
    {
        await businessApp.InsertEsameAsync(matricola, idCorso, data, voto, lode, cancellationToken);
    }

    [HttpPost(Name = "UpdateEsame")]
    public async Task<ActionResult> UpdateEsameAsync(int matricola, int idCorso, DateTime data, int voto, bool lode,
        CancellationToken cancellationToken = default)
    {
        try
        {
            await businessApp.UpdateEsameAsync(matricola, idCorso, data, voto, lode, cancellationToken);

            return Ok();
        }
        catch (ModelNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (DbUpdateConcurrencyException)
        {
            return Conflict("Questo esame è stato modificato da un altro utente");
        }
    }

    [HttpPost(Name = "DeleteEsame")]
    public async Task<ActionResult> DeleteEsameAsync(int matricola, int idCorso, CancellationToken cancellationToken = default)
    {
        try
        {
            await businessApp.DeleteEsameAsync(matricola, idCorso, cancellationToken);

            return Ok();
        }
        catch (ModelNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (DbUpdateConcurrencyException)
        {
            return Conflict("Questo esame è stato modificato da un altro utente");
        }
    }
}