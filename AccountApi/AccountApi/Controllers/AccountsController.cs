using Microsoft.AspNetCore.Mvc;
using AccountApi.Models;
using AccountApi.Services;

namespace AccountApi.Controllers;

[ApiController]
[Route("accounts")]
public class AccountsController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountsController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AccountDto>>> GetAll()
    {
        var accounts = await _accountService.GetAllAccountsAsync();
        var dtos = accounts.Select(MapToDto);
        return Ok(dtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AccountDto>> GetById(int id)
    {
        var account = await _accountService.GetAccountByIdAsync(id);

        if (account == null)
        {
            return NotFound();
        }

        return Ok(MapToDto(account));
    }

    [HttpPost]
    public async Task<ActionResult<AccountDto>> Create(AccountDto accountDto)
    {
        var dbo = MapToDbo(accountDto);
        var created = await _accountService.CreateAccountAsync(dbo);
        var dto = MapToDto(created);

        return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<AccountDto>> Update(int id, AccountDto accountDto)
    {
        if (id != accountDto.Id)
        {
            return BadRequest("ID in URL does not match ID in body");
        }

        var dbo = MapToDbo(accountDto);
        var updated = await _accountService.UpdateAccountAsync(dbo);

        if (updated == null)
        {
            return NotFound();
        }

        return Ok(MapToDto(updated));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _accountService.DeleteAccountAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }

    private static AccountDto MapToDto(AccountDbo dbo)
    {
        return new AccountDto
        {
            Id = dbo.Id,
            FirstName = dbo.FirstName,
            LastName = dbo.LastName,
            Email = dbo.Email,
            Phone = dbo.Phone
        };
    }

    private static AccountDbo MapToDbo(AccountDto dto)
    {
        return new AccountDbo
        {
            Id = dto.Id,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            Phone = dto.Phone
        };
    }
}
