using AccountApi.Models;

namespace AccountApi.Repositories;

public interface IAccountRepository
{
    Task<IEnumerable<AccountDbo>> GetAllAsync();
    Task<AccountDbo?> GetByIdAsync(int id);
    Task<AccountDbo> CreateAsync(AccountDbo account);
    Task<AccountDbo?> UpdateAsync(AccountDbo account);
    Task<bool> DeleteAsync(int id);
}
