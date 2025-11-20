# AccountApi Naming Conventions

This document defines naming standards and patterns for the AccountApi codebase.

---

## General Principles

- **Be explicit over implicit**: `accountId` is better than `id`
- **Be consistent**: If you use `Async` suffix, use it everywhere
- **Be specific**: `JsonHelper` is better than `DataHelper`
- **Follow .NET conventions**: PascalCase for public members, camelCase for parameters

---

## Class Naming

### Controllers
**Pattern**: `{Domain}Controller`

**Examples**:
- ✅ `AccountController`
- ✅ `OrderController`
- ❌ `AccountsController` (don't pluralize)
- ❌ `AccountApiController` (redundant, already in API project)

**Location**: `AccountApi/Controllers/`

---

### Services
**Pattern**: `{Domain}Service`

**Examples**:
- ✅ `AccountService`
- ✅ `NotificationService`
- ✅ `ValidationService`
- ❌ `AccountBusinessLogic` (use Service suffix)
- ❌ `AccountManager` (use Service unless truly managing/orchestrating multiple services)

**Location**: `AccountApi/Services/`

---

### Repositories
**Pattern**: `I{Domain}Repository` (interface), `{Domain}Repository` (implementation)

**Examples**:
- ✅ `IAccountRepository` / `AccountRepository`
- ✅ `IOrderRepository` / `OrderRepository`
- ❌ `AccountRepo` (don't abbreviate)
- ❌ `AccountDataAccess` (use Repository suffix)

**Location**: `AccountApi/Repositories/`

---

### Helpers
**Pattern**: `{Purpose}Helper`

**Rules**:
- MUST be static classes
- Suffix with `Helper`
- Be specific about purpose

**Examples**:
- ✅ `JsonHelper`
- ✅ `ValidationHelper`
- ✅ `StringHelper`
- ❌ `Helper` (too generic)
- ❌ `Utils` (use Helper suffix)
- ❌ `AccountHelper` (non-static, should probably be AccountService)

**Location**: `AccountApi/Helpers/`

---

### Extensions
**Pattern**: `{Type}Extensions`

**Rules**:
- MUST be static classes
- Name after the type being extended
- Suffix with `Extensions`

**Examples**:
- ✅ `StringExtensions`
- ✅ `DateTimeExtensions`
- ✅ `IEnumerableExtensions`
- ❌ `Extensions` (too generic)
- ❌ `HelperExtensions` (doesn't indicate what type)

**Location**: `AccountApi/Extensions/`

---

### Clients
**Pattern**: `{System}Client`

**Examples**:
- ✅ `PaymentClient`
- ✅ `EmailClient`
- ✅ `NotificationClient`
- ❌ `PaymentService` (use Client for external system access)
- ❌ `PaymentApi` (use Client suffix)

**Location**: `AccountApi/Clients/`

---

### Data Objects

#### Database Objects (Dbo)
**Pattern**: `{Domain}Dbo`

**Examples**:
- ✅ `AccountDbo`
- ✅ `OrderDbo`
- ❌ `AccountModel` (use Dbo for database objects)
- ❌ `AccountEntity` (use Dbo for serialization objects)

**Location**: `AccountApi/Data/` or `AccountApi/Models/Dbos/`

#### Data Transfer Objects (Dto)
**Pattern**: `{Action}{Domain}Dto` or `{Domain}Dto`

**Examples**:
- ✅ `AccountDto` (for general/response)
- ✅ `CreateAccountDto` (for creation requests)
- ✅ `UpdateAccountDto` (for update requests)
- ✅ `AccountSummaryDto` (for list/summary views)
- ❌ `AccountModel` (use Dto for API contracts)
- ❌ `AccountRequest` (use Dto suffix)
- ❌ `AccountResponse` (use Dto suffix)

**Location**: `AccountApi/Models/Dtos/` or `AccountApi/Dtos/`

#### Domain Entities
**Pattern**: `{Domain}` (no suffix)

**Examples**:
- ✅ `Account`
- ✅ `Order`
- ❌ `AccountEntity` (no suffix needed)
- ❌ `AccountModel` (no suffix needed)

**Location**: `AccountApi/Models/` or `AccountApi/Domain/`

---

## Interface Naming

**Pattern**: `I{ClassName}`

**Examples**:
- ✅ `IAccountRepository`
- ✅ `IAccountService`
- ✅ `IPaymentClient`
- ❌ `AccountRepositoryInterface` (use I prefix)
- ❌ `AccountIRepository` (I is a prefix, not infix)

---

## Method Naming

### General Pattern
**Pattern**: `{Verb}{Entity}[Qualifier]`

**Examples**:
- ✅ `GetAccount(string accountId)`
- ✅ `CreateAccount(CreateAccountDto dto)`
- ✅ `UpdateAccount(string accountId, UpdateAccountDto dto)`
- ✅ `DeleteAccount(string accountId)`
- ✅ `GetAccountsByStatus(string status)`
- ❌ `AccountGet()` (wrong order)
- ❌ `DoGetAccount()` (don't prefix with Do)
- ❌ `Get()` (too vague)

### Async Methods
**Pattern**: `{Verb}{Entity}Async`

**Rules**:
- ALL async methods MUST have `Async` suffix
- Async is a suffix, not a prefix

**Examples**:
- ✅ `GetAccountAsync(string accountId)`
- ✅ `CreateAccountAsync(CreateAccountDto dto)`
- ✅ `SaveChangesAsync()`
- ❌ `AsyncGetAccount()` (wrong position)
- ❌ `GetAccount()` when method is async (missing Async suffix)

### CRUD Operation Verbs
Use these standard verbs consistently:

- **Create**: `CreateAccountAsync()`
- **Read/Retrieve**: `GetAccountAsync()`, `GetAllAccountsAsync()`, `FindAccountAsync()`
- **Update**: `UpdateAccountAsync()`
- **Delete**: `DeleteAccountAsync()`

**Don't use**:
- ❌ `AddAccount()` → ✅ Use `CreateAccount()`
- ❌ `RemoveAccount()` → ✅ Use `DeleteAccount()`
- ❌ `FetchAccount()` → ✅ Use `GetAccount()`
- ❌ `SaveAccount()` → ✅ Use `CreateAccount()` or `UpdateAccount()`

### Boolean Methods
**Pattern**: `Is{Condition}` or `Has{Condition}` or `Can{Action}`

**Examples**:
- ✅ `IsValidEmail(string email)`
- ✅ `HasAccess(string accountId)`
- ✅ `CanDeleteAccount(string accountId)`
- ❌ `ValidEmail()` (unclear return type)
- ❌ `CheckAccess()` (unclear return type)

---

## Variable and Parameter Naming

### Private Fields
**Pattern**: `_{camelCase}`

**Examples**:
- ✅ `_accountRepository`
- ✅ `_logger`
- ✅ `_configuration`
- ❌ `accountRepository` (missing underscore)
- ❌ `_AccountRepository` (should be camelCase)
- ❌ `m_accountRepository` (use underscore, not m_)

### Parameters and Local Variables
**Pattern**: `{camelCase}`, be specific

**Examples**:
- ✅ `accountId`
- ✅ `createAccountDto`
- ✅ `cancellationToken`
- ❌ `id` (too vague - what kind of ID?)
- ❌ `dto` (too vague - what DTO?)
- ❌ `AccountId` (should be camelCase)

### Property Names
**Pattern**: `{PascalCase}`

**Examples**:
- ✅ `AccountId`
- ✅ `Name`
- ✅ `Email`
- ✅ `CreatedAt`
- ❌ `accountId` (should be PascalCase)
- ❌ `account_id` (no underscores)

### Constants
**Pattern**: `PascalCase` (NOT SCREAMING_SNAKE_CASE)

**Examples**:
- ✅ `MaxRetryAttempts`
- ✅ `DefaultTimeoutSeconds`
- ✅ `ApiVersion`
- ❌ `MAX_RETRY_ATTEMPTS`
- ❌ `max_retry_attempts`

---

## File Naming

### General Rules
- File names MUST match the primary class name
- One primary class per file
- Use PascalCase for file names

**Examples**:
- ✅ `AccountController.cs` contains `AccountController` class
- ✅ `IAccountRepository.cs` contains `IAccountRepository` interface
- ✅ `AccountService.cs` contains `AccountService` class
- ❌ `account-controller.cs` (use PascalCase)
- ❌ `accountcontroller.cs` (use PascalCase)

### Directory Structure
```
AccountApi/
├── Controllers/
│   └── AccountController.cs
├── Services/
│   └── AccountService.cs
├── Repositories/
│   ├── IAccountRepository.cs
│   └── AccountRepository.cs
├── Models/
│   ├── Account.cs
│   ├── Dtos/
│   │   ├── AccountDto.cs
│   │   ├── CreateAccountDto.cs
│   │   └── UpdateAccountDto.cs
│   └── Dbos/
│       └── AccountDbo.cs
├── Helpers/
│   └── JsonHelper.cs
├── Extensions/
│   └── StringExtensions.cs
└── Clients/
    └── PaymentClient.cs
```

---

## Namespace Conventions

**Pattern**: `{Company}.{Product}.{Layer}[.{Feature}]`

**Examples**:
```csharp
namespace AccountApi.Controllers;
namespace AccountApi.Services;
namespace AccountApi.Repositories;
namespace AccountApi.Models;
namespace AccountApi.Models.Dtos;
namespace AccountApi.Models.Dbos;
namespace AccountApi.Helpers;
namespace AccountApi.Extensions;
namespace AccountApi.Clients;
```

**For larger projects with multiple features**:
```csharp
namespace AccountApi.Features.Accounts.Controllers;
namespace AccountApi.Features.Orders.Services;
```

---

## Specific Conventions

### Dependency Injection
Always inject interfaces, not concrete implementations:

```csharp
// ✅ Correct
public class AccountController
{
    private readonly IAccountService _accountService;
    private readonly ILogger<AccountController> _logger;
    
    public AccountController(
        IAccountService accountService,
        ILogger<AccountController> logger)
    {
        _accountService = accountService;
        _logger = logger;
    }
}

// ❌ Incorrect
public class AccountController
{
    private readonly AccountService _accountService; // Should be interface
}
```

### Readonly Fields
Always mark injected dependencies as `readonly`:

```csharp
// ✅ Correct
private readonly IAccountRepository _accountRepository;

// ❌ Incorrect
private IAccountRepository _accountRepository; // Missing readonly
```

### Async/Await
Always use async/await consistently:

```csharp
// ✅ Correct
public async Task<ActionResult<AccountDto>> GetAccount(string accountId)
{
    var account = await _accountService.GetAccountAsync(accountId);
    return Ok(account);
}

// ❌ Incorrect - missing Async suffix
public async Task<ActionResult<AccountDto>> GetAccount(string accountId)
{
    var account = await _accountService.GetAccount(accountId);
    return Ok(account);
}
```

---

## Examples of Correct vs. Incorrect Naming

### Example 1: Service
```csharp
// ❌ Incorrect
public class CustomerHelper
{
    private ICustomerRepo repo;
    
    public Customer Get(string id)
    {
        return repo.GetById(id);
    }
}

// ✅ Correct
public class AccountService
{
    private readonly IAccountRepository _accountRepository;
    
    public async Task<Account> GetAccountAsync(string accountId)
    {
        return await _accountRepository.GetByIdAsync(accountId);
    }
}
```

### Example 2: Repository
```csharp
// ❌ Incorrect
public class AccountRepo
{
    public Account GetAccount(string id)
    {
        var dbo = ReadFromFile();
        return dbo; // Returns Dbo instead of Account
    }
}

// ✅ Correct
public class AccountRepository : IAccountRepository
{
    public async Task<Account> GetByIdAsync(string accountId)
    {
        var accountDbo = await ReadFromFileAsync();
        return MapToAccount(accountDbo); // Converts Dbo to Account
    }
}
```

### Example 3: Helper
```csharp
// ❌ Incorrect - not static
public class JsonHelper
{
    public T Deserialize<T>(string json) { }
}

// ✅ Correct - static class
public static class JsonHelper
{
    public static T Deserialize<T>(string json) { }
}
```

---

## Quick Reference Checklist

When reviewing code, check:
- ☐ Does class name match file name?
- ☐ Is the suffix correct? (Service, Repository, Helper, Client, Dto, Dbo)
- ☐ Are Helpers static classes?
- ☐ Do async methods have `Async` suffix?
- ☐ Are private fields prefixed with underscore?
- ☐ Are private fields `readonly` when appropriate?
- ☐ Are interfaces prefixed with `I`?
- ☐ Are we using `Account` not `Customer`?
- ☐ Are parameter names specific? (`accountId` not `id`)
- ☐ Do method names start with verb? (`GetAccount` not `AccountGet`)
- ☐ Are we injecting interfaces not concrete classes?
