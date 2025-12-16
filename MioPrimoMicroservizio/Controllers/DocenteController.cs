using Business.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared;
using Shared.Exceptions;


namespace MioPrimoMicroservizio.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class DocenteController(IBusinessApp businessApp) : ControllerBase
{
    [HttpGet(Name = "GetDocente")]
    public async Task<DocenteDTO?> GetDocenteAsync(int matricola, CancellationToken cancellationToken = default)
    {
        return await businessApp.GetDocenteAsync(matricola, cancellationToken);
    }

    [HttpPost(Name = "InsertDocente")]
    public async Task InsertDocenteAsync(string nome, string cognome, CancellationToken cancellationToken = default)
    {
        await businessApp.InsertDocenteAsync(nome, cognome, cancellationToken);
    }

    [HttpPost(Name = "UpdateDocente")]
    public async Task<ActionResult> UpdateDocenteAsync(int matricolaDocente, string nome, string cognome,
        CancellationToken cancellationToken = default)
    {
        try
        {
            await businessApp.UpdateDocenteAsync(matricolaDocente, nome, cognome, cancellationToken);

            return Ok();
        }
        catch (ModelNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (DbUpdateConcurrencyException)
        {
            return Conflict("Questo docente è stato modificato da un altro utente");
        }
    }

    [HttpPost(Name = "DeleteDocente")]
    public async Task<ActionResult> DeleteDocenteAsync(int matricolaDocente, CancellationToken cancellationToken = default)
    {
        try
        {
            await businessApp.DeleteDocenteAsync(matricolaDocente, cancellationToken);

            return Ok();
        }
        catch (ModelNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (DbUpdateConcurrencyException)
        {
            return Conflict("Questo docente è stato modificato da un altro utente");
        }
    }

    [HttpPost(Name = "GetCorsi")]
    public async Task<CorsiDocente> GetCorsoDocenteAsync(int matricolaDocente, CancellationToken cancellationToken = default)
    {
        return await businessApp.GetCorsiDocenteAsync(matricolaDocente, cancellationToken);
    }
    
}