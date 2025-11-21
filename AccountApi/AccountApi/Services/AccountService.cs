using AccountApi.Models;
using AccountApi.Repositories;

namespace AccountApi.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;

    public AccountService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<IEnumerable<AccountDbo>> GetAllAccountsAsync()
    {
        return await _accountRepository.GetAllAsync();
    }

    public async Task<AccountDbo?> GetAccountByIdAsync(int id)
    {
        return await _accountRepository.GetByIdAsync(id);
    }

    public async Task<List<AccountDto>> GetAccountsByName(string name)
    {
        var customers = await _accountRepository.GetByNameAsync(name);
        var result = customers.Select(x => new AccountDto { 
            Id = x.Id, FirstName = x.FirstName, LastName = x.LastName, Email = x.Email });
        return result.ToList();
    }

    public async Task<AccountDbo> CreateAccountAsync(AccountDbo account)
    {
        return await _accountRepository.CreateAsync(account);
    }

    public async Task<AccountDbo?> UpdateAccountAsync(AccountDbo account)
    {
        return await _accountRepository.UpdateAsync(account);
    }

    public async Task<bool> DeleteAccountAsync(int id)
    {
        return await _accountRepository.DeleteAsync(id);
    }
}
