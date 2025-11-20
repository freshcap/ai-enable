# AccountApi Glossary

This document defines business terminology and technical concepts used in the AccountApi project.

---

## Business Terminology

### Account
**Definition**: A paying customer in our system. An Account represents a business or individual who has purchased or subscribed to our services.

**Important**: In this codebase, "Account" always refers to a customer account, never:
- User accounts (system authentication)
- Bank accounts
- Accounting/financial accounts

**Usage in code**:
- Entity class: `Account`
- Database object: `AccountDbo`
- API contract: `AccountDto`
- Service: `AccountService`
- Repository: `AccountRepository`
- Controller: `AccountController`

**Example**:
```csharp
// ✅ Correct usage
public class AccountService
{
    public async Task<Account> GetAccountAsync(string accountId) { }
}

// ❌ Incorrect - don't use Customer
public class CustomerService // NO - use AccountService
```

---

## Technical Terminology

### Service
**Definition**: A top-level handler for working with a particular conceptual unit. Services contain business logic and orchestrate operations across repositories and other services.

**Characteristics**:
- Contains business logic and validation
- Orchestrates operations across multiple repositories or other services
- Does NOT directly access databases (uses Repositories)
- Stateless - no instance state between method calls
- Suffix class name with `Service`

**Examples**:
- `AccountService` - handles Account business logic
- `NotificationService` - handles sending notifications
- `ValidationService` - handles complex validation rules

**Usage pattern**:
```csharp
public class AccountService
{
    private readonly IAccountRepository _accountRepository;
    
    public AccountService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }
    
    public async Task<Account> CreateAccountAsync(CreateAccountDto dto)
    {
        // Business logic, validation
        // Calls repository for data operations
    }
}
```

---

### Repository
**Definition**: Data access layer component responsible for CRUD operations against data storage.

**Characteristics**:
- Only responsible for data access operations (Create, Read, Update, Delete)
- Works with Dbo (database object) classes for serialization
- Returns domain entities (Account, not AccountDbo) to services
- Uses interface pattern: `IAccountRepository` / `AccountRepository`
- Suffix interface and class with `Repository`

**Examples**:
- `IAccountRepository` / `AccountRepository`
- `IOrderRepository` / `OrderRepository`

**Usage pattern**:
```csharp
public interface IAccountRepository
{
    Task<Account> GetByIdAsync(string accountId);
    Task<Account> CreateAsync(Account account);
    Task<Account> UpdateAsync(Account account);
    Task<bool> DeleteAsync(string accountId);
}
```

---

### Helper
**Definition**: Static utility classes containing helper methods that don't fit into services or domain logic.

**Characteristics**:
- MUST be static classes
- Contains pure utility functions with no side effects
- No dependency injection (static methods)
- Suffix class name with `Helper`

**Examples**:
- `JsonHelper` - JSON serialization utilities
- `ValidationHelper` - common validation logic
- `StringHelper` - string manipulation utilities

**Usage pattern**:
```csharp
public static class JsonHelper
{
    public static T Deserialize<T>(string json) { }
    public static string Serialize<T>(T obj) { }
}

// Called as:
var account = JsonHelper.Deserialize<Account>(json);
```

---

### Extensions
**Definition**: Extension method classes that extend existing types with additional functionality.

**Characteristics**:
- Static classes containing extension methods
- Suffix class name with `Extensions`
- Group by type being extended (e.g., `StringExtensions`, `DateTimeExtensions`)
- Typically placed in `{ProjectName}.Extensions` namespace

**Examples**:
- `StringExtensions` - extends string type
- `IEnumerableExtensions` - extends IEnumerable<T>

**Usage pattern**:
```csharp
public static class StringExtensions
{
    public static bool IsNullOrEmpty(this string value)
    {
        return string.IsNullOrEmpty(value);
    }
}

// Called as:
if (accountName.IsNullOrEmpty()) { }
```

---

### Client
**Definition**: Low-level utility classes used for accessing external systems or services.

**Characteristics**:
- Encapsulates communication with external APIs or systems
- Handles HTTP calls, message queue operations, etc.
- Suffix class name with `Client`
- Uses interface pattern for testability

**Examples**:
- `PaymentClient` - communicates with payment processing API
- `EmailClient` - sends emails via external email service
- `NotificationClient` - sends push notifications

**Usage pattern**:
```csharp
public interface IPaymentClient
{
    Task<PaymentResult> ProcessPaymentAsync(PaymentRequest request);
}

public class PaymentClient : IPaymentClient
{
    private readonly HttpClient _httpClient;
    
    public async Task<PaymentResult> ProcessPaymentAsync(PaymentRequest request)
    {
        // Makes HTTP call to external payment API
    }
}
```

---

### Dbo (Database Object)
**Definition**: Classes that represent the structure of data as it's stored in the database or data file.

**Characteristics**:
- Suffix class name with `Dbo`
- Used for serialization/deserialization to/from storage
- May contain storage-specific properties (IDs, timestamps, etc.)
- Typically mapped to domain entities before returning to services

**Examples**:
- `AccountDbo` - Account as stored in database
- `OrderDbo` - Order as stored in database

**Usage pattern**:
```csharp
public class AccountDbo
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

// Repository converts Dbo to domain entity:
var accountDbo = await ReadFromStorage();
return MapToAccount(accountDbo); // Returns Account, not AccountDbo
```

---

### Dto (Data Transfer Object)
**Definition**: Classes used for data transfer between API and clients, or between services and external systems.

**Characteristics**:
- Suffix class name with `Dto`
- Used for API requests and responses
- May contain validation attributes
- May differ from domain entities (e.g., exclude sensitive fields, flatten relationships)

**Examples**:
- `AccountDto` - Account data for API responses
- `CreateAccountDto` - Account data for creation requests
- `UpdateAccountDto` - Account data for update requests

**Usage pattern**:
```csharp
public class AccountDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}

public class CreateAccountDto
{
    [Required]
    public string Name { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}
```

---

## Architecture Concepts

### 3-Tier Structure
Our application follows a 3-tier architecture:

1. **API Layer** (Controllers)
   - Handles HTTP requests and responses
   - Validates input (model binding, attributes)
   - Calls services for business logic
   - Returns DTOs to clients

2. **Service Layer** (Services)
   - Contains business logic and rules
   - Orchestrates operations across repositories
   - Transforms between DTOs and domain entities
   - Returns domain entities

3. **Repository Layer** (Repositories)
   - Handles all data access
   - Works with Dbos for serialization
   - Returns domain entities to services
   - Currently uses static JSON data

**Data Flow**:
```
Client → Controller → Service → Repository → Data Storage
         (Dto)       (Entity)   (Dbo)
```

---

## Common Mistakes to Avoid

❌ Using `Customer` → ✅ Use `Account`
❌ Using `User` for paying customers → ✅ Use `Account` (reserve User for system authentication)
❌ Non-static Helper classes → ✅ Helpers must be static
❌ Services accessing data directly → ✅ Services must use Repositories
❌ Returning Dbo from Repository → ✅ Return domain entity (Account)
❌ Using domain entities in API → ✅ Use Dtos in controllers
❌ Generic names like `DataHelper` → ✅ Be specific: `JsonHelper`, `ValidationHelper`
