using Business.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared;
using Shared.Exceptions;


namespace MioPrimoMicroservizio.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class StudentiController(IBusinessApp businessApp) : ControllerBase
{
    [HttpGet(Name = "GetStudente")]
    public async Task<StudenteDTO?> GetStudenteAsync(int matricola, CancellationToken cancellationToken = default)
    {
        return await businessApp.GetStudenteAsync(matricola, cancellationToken);
    }

    [HttpPost(Name = "InsertStudente")]
    public async Task InsertStudenteAsync(string nome, string cognome, DateTime dataNascita, DateTime dataImmatricolazione, CancellationToken cancellationToken = default)
    {
        await businessApp.InsertStudenteAsync(nome, cognome, dataNascita, dataImmatricolazione, cancellationToken);
    }

    [HttpPost(Name = "UpdateStudente")]
    public async Task<ActionResult> UpdateStudenteAsync(int matricola, string nome, string cognome, DateTime dataNascita, DateTime dataImmatricolazione, CancellationToken cancellationToken = default)
    {
        try
        {
            await businessApp.UpdateStudenteAsync(matricola, nome, cognome, dataNascita, dataImmatricolazione, cancellationToken);

            return Ok();
        }
        catch (ModelNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (DbUpdateConcurrencyException)
        {
            return Conflict("Questo studente è stato modificato da un altro utente");
        }
    }

    [HttpPost(Name = "DeleteStudente")]
    public async Task<ActionResult> DeleteStudenteAsync(int matricola, CancellationToken cancellationToken = default)
    {
        try
        {
            await businessApp.DeleteStudenteAsync(matricola, cancellationToken);

            return Ok();
        }
        catch (ModelNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (DbUpdateConcurrencyException)
        {
            return Conflict("Questo studente è stato modificato da un altro utente");
        }
    }

    [HttpGet(Name = "GetEsami")]
    public async Task<ActionResult> GetEsamiAsync(int matricola, CancellationToken cancellationToken = default)
    {
        var result =  await businessApp.GetEsamiAsync(matricola, cancellationToken);
        return Ok(result);
    }
    
    [HttpGet(Name = "GetMediaEsami")]
    public async Task<ActionResult> GetMediaEsamiAsync(int matricola, CancellationToken cancellationToken = default)
    {
        try
        {
            float i = await businessApp.GetMediaEsamiAsync(matricola, cancellationToken);
            return Ok($"Media: {i}");
        }
        catch (ModelNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet(Name = "GetCFU")]
    public async Task<ActionResult> GetCfuAsync(int matricola, CancellationToken cancellationToken = default)
    {
        int i = await businessApp.GetCFUAsync(matricola, cancellationToken);
        return Ok($"CFU totali: {i}");
    }
}