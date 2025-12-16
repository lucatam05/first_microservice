using Business.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared;
using Shared.Exceptions;


namespace MioPrimoMicroservizio.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class CorsoController(IBusinessApp businessApp) : ControllerBase
{
    [HttpGet(Name = "GetCorso")]
    public async Task<CorsoLight?> GetCorsoAsync(int idCorso, CancellationToken cancellationToken = default)
    {
        return await businessApp.GetCorsoAsync(idCorso, cancellationToken);
    }

    [HttpPost(Name = "InsertCorso")]
    public async Task InsertCorsoAsync(string nome, int cfu,
        CancellationToken cancellationToken = default)
    {
        await businessApp.InsertCorsoAsync(nome, cfu, cancellationToken);
    }

    [HttpPost(Name = "UpdateCorso")]
    public async Task<ActionResult> UpdateCorsoAsync(int id, string nome, int cfu,
        CancellationToken cancellationToken = default)
    {
        try
        {
            await businessApp.UpdateCorsoAsync(id, nome, cfu, cancellationToken);

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

    [HttpPost(Name = "DeleteCorso")]
    public async Task<ActionResult> DeleteCorsoAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            await businessApp.DeleteCorsoAsync(id, cancellationToken);

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

    [HttpGet(Name = "GetMediaCorso")]
    public async Task<ActionResult> GetMediaCorsoAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            float i = await businessApp.GetMediaCorsoAsync(id, cancellationToken);

            return Ok($"Media: {i}");
        }
        catch (ModelNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
    
    [HttpPost(Name = "GetDocentiPerCorso")]
    public async Task<DocentiPerCorso> GetDocentiPerCorso(int id, CancellationToken cancellationToken = default)
    {
        return await businessApp.GetDocentiPerCorsoAsync(id, cancellationToken);
    }

}