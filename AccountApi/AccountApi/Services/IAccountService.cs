using AccountApi.Models;

namespace AccountApi.Services;

public interface IAccountService
{
    Task<IEnumerable<AccountDbo>> GetAllAccountsAsync();
    Task<AccountDbo?> GetAccountByIdAsync(int id);
    Task<List<AccountDto>> GetAccountsByName(string name);
    Task<AccountDbo> CreateAccountAsync(AccountDbo account);
    Task<AccountDbo?> UpdateAccountAsync(AccountDbo account);
    Task<bool> DeleteAccountAsync(int id);
}
