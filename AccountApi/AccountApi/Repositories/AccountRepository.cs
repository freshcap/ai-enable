using System.Text.Json;
using AccountApi.Models;

namespace AccountApi.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly string _dataFilePath;
    private readonly JsonSerializerOptions _jsonOptions;

    public AccountRepository(IWebHostEnvironment environment)
    {
        _dataFilePath = Path.Combine(environment.ContentRootPath, "Data", "accounts.json");
        _jsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        EnsureDataFileExists();
    }

    private void EnsureDataFileExists()
    {
        var directory = Path.GetDirectoryName(_dataFilePath);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory!);
        }

        if (!File.Exists(_dataFilePath))
        {
            File.WriteAllText(_dataFilePath, "[]");
        }
    }

    private async Task<List<AccountDbo>> ReadAccountsAsync()
    {
        var json = await File.ReadAllTextAsync(_dataFilePath);
        return JsonSerializer.Deserialize<List<AccountDbo>>(json, _jsonOptions) ?? new List<AccountDbo>();
    }

    private async Task WriteAccountsAsync(List<AccountDbo> accounts)
    {
        var json = JsonSerializer.Serialize(accounts, _jsonOptions);
        await File.WriteAllTextAsync(_dataFilePath, json);
    }

    public async Task<IEnumerable<AccountDbo>> GetAllAsync()
    {
        return await ReadAccountsAsync();
    }

    public async Task<AccountDbo?> GetByIdAsync(int id)
    {
        var accounts = await ReadAccountsAsync();
        return accounts.FirstOrDefault(a => a.Id == id);
    }

    public async Task<IEnumerable<AccountDbo>> GetByNameAsync(string name)
    {
        var items = await ReadAccountsAsync();
        var helper = new NameHelper();
        return items.Where(x => helper.HasName(x, name));
    }

    public async Task<AccountDbo> CreateAsync(AccountDbo account)
    {
        var accounts = await ReadAccountsAsync();

        account.Id = accounts.Count > 0 ? accounts.Max(a => a.Id) + 1 : 1;
        account.CreatedAt = DateTime.UtcNow;
        account.UpdatedAt = DateTime.UtcNow;

        accounts.Add(account);
        await WriteAccountsAsync(accounts);

        return account;
    }

    public async Task<AccountDbo?> UpdateAsync(AccountDbo account)
    {
        var accounts = await ReadAccountsAsync();
        var existingIndex = accounts.FindIndex(a => a.Id == account.Id);

        if (existingIndex == -1)
        {
            return null;
        }

        account.CreatedAt = accounts[existingIndex].CreatedAt;
        account.UpdatedAt = DateTime.UtcNow;
        accounts[existingIndex] = account;

        await WriteAccountsAsync(accounts);
        return account;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var accounts = await ReadAccountsAsync();
        var account = accounts.FirstOrDefault(a => a.Id == id);

        if (account == null)
        {
            return false;
        }

        accounts.Remove(account);
        await WriteAccountsAsync(accounts);
        return true;
    }
}

public class NameHelper
{
    public bool HasName(AccountDbo customer, string name)
    {
        if (customer.FirstName == name) return true;
        if (customer.LastName == name) return true;
        return false;
    }
}
