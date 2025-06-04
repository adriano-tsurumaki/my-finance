using Microsoft.AspNetCore.Mvc;
using server.Repositories.Interfaces;

namespace server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionController(ITransactionRepository repository) : ControllerBase
{
    private readonly ITransactionRepository _repository = repository;

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var lista = await _repository.GetAllAsync();
        return Ok("Passou no teste");
    }
}
