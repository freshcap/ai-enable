# AI-Enabled Development

This repository explores practical ways to enable and accelerate software development through the strategic use of AI tools and pipelines.

## Overview

The goal of this project is to experiment with integrating AI capabilities into real development workflows - not just using AI tools personally, but embedding them into team processes where they provide systematic value.

**Focus Areas:**
- 🤖 **Automated Code Review** - AI-powered convention enforcement
- 🚀 **CI/CD Integration** - AI in development pipelines
- 📚 **Knowledge Management** - Company-specific context for AI tools
- ⚡ **Developer Productivity** - Reducing friction in common tasks

## Projects in This Repository

### AccountApi
A basic ASP.NET Core Web API with a 3-tier architecture (API → Service → Repository) using static JSON data. This serves as a testbed for AI integration experiments.

**Stack:**
- ASP.NET Core 8.0
- C# with modern patterns
- RESTful API design
- Static JSON data storage

---

## AI Code Review System

An automated code review system that enforces company-specific naming conventions and terminology using Claude AI.

### What It Does

When a pull request is opened, the system:
1. **Captures** the code changes (git diff)
2. **Loads** company documentation (glossary and naming conventions)
3. **Analyzes** the code using Claude AI with company context
4. **Posts** a detailed review comment with specific violations and corrections

### How It Works

```
PR Created
    ↓
GitHub Actions Triggered
    ↓
Get Diff → Load Docs → Prepare Request (C#)
    ↓
Call Claude API
    ↓
Extract Review (C#) → Post Comment
```

**Key Components:**

1. **GitHub Actions Workflow** (`.github/workflows/ai-code-review.yml`)
   - Orchestrates the entire review process
   - Runs on every PR automatically
   - Uses .NET 8.0 for C# scripts

2. **PrepareReview Script** (`.github/scripts/PrepareReview/`)
   - Reads the PR diff
   - Loads company glossary and naming conventions
   - Builds a comprehensive prompt for Claude
   - Generates the API request

3. **ExtractReview Script** (`.github/scripts/ExtractReview/`)
   - Parses Claude's API response
   - Handles errors gracefully
   - Formats the review for GitHub

4. **Company Documentation** (`docs/`)
   - `glossary.md` - Defines domain terminology (Account, Service, Repository, etc.)
   - `naming-conventions.md` - Comprehensive coding standards

### Architecture Decisions

**Why C# for automation scripts?**
- Consistency with the project stack (.NET/C#)
- Better maintainability for .NET developers
- Proper syntax highlighting and IntelliSense
- Easy to test locally with `dotnet run`
- No Python dependency required

**Why standalone script files vs. inline YAML?**
- Readable - proper C# files with IDE support
- Testable - can run locally before pushing
- Maintainable - modify C# without touching YAML
- Reusable - scripts can be used in other workflows

**Why company documentation files?**
- AI needs context about project-specific terminology
- Conventions change over time - markdown files are easy to update
- Version controlled alongside code
- Educational for new team members

### What Gets Reviewed

**Naming Conventions:**
- ✅ Services must end with `Service`
- ✅ Repositories must end with `Repository` (interfaces with `I` prefix)
- ✅ Helpers must be static and end with `Helper`
- ✅ Extensions must be static and end with `Extensions`
- ✅ Clients must end with `Client`
- ✅ DTOs must end with `Dto`
- ✅ Database objects must end with `Dbo`

**Terminology:**
- ✅ Use `Account` not `Customer` for customer accounts
- ✅ Proper distinction between Dbo (internal) and Dto (API boundary)
- ✅ Consistent use of domain terms

**Code Quality:**
- ✅ Async methods have `Async` suffix
- ✅ Private fields use `_camelCase` with underscore
- ✅ Fields marked `readonly` when appropriate
- ✅ Proper dependency injection patterns

### Example Review Output

When a PR violates conventions, the AI provides:

```markdown
## 🔍 Naming Convention Issues

### Issue: Class Name `CustomerHelper`
**Current**: `CustomerHelper`
**Problem**: Violates two conventions:
1. Should use `Account` not `Customer` per company glossary
2. Non-static class should be `Service` not `Helper`

**Correct**: `AccountService`

**Reference**: "Account: A paying customer in our system. Use Account, not Customer."

**Example**:
```csharp
// ✅ Correct
public class AccountService
{
    private readonly IAccountRepository _accountRepository;
    
    public async Task<AccountDbo> GetAccountAsync(string accountId)
    {
        return await _accountRepository.GetByIdAsync(accountId);
    }
}
```
```

### Setup

See [.github/scripts/README.md](.github/scripts/README.md) for detailed setup and testing instructions.

**Quick setup:**
1. Add `ANTHROPIC_API_KEY` as a GitHub secret
2. Commit the workflow and scripts to your repo
3. Create a PR - the AI review runs automatically

### Benefits

**For Development Teams:**
- 🎯 Consistent enforcement of conventions
- 📖 Educational feedback for junior developers
- ⏱️ Saves senior developer time on nitpicky reviews
- 🔄 Conventions stay up-to-date in version control

**For Interview Demonstration:**
- 🚀 Shows practical AI pipeline integration
- 💻 Demonstrates C#/.NET automation skills
- 🏗️ Shows architectural thinking
- 🤖 Proves hands-on AI tool experience

### Customization

All behavior is controlled by markdown files:

**Change naming rules:**
Edit `docs/naming-conventions.md`

**Change terminology:**
Edit `docs/glossary.md`

**Change AI behavior:**
Edit `.github/scripts/PrepareReview/Program.cs`

Changes take effect on the next PR automatically.

### Extending This Approach

This pattern can be extended to:
- **Format checking** - Enforce code formatting standards
- **Security scanning** - Detect common security issues
- **Documentation generation** - Auto-generate API docs
- **Test generation** - Suggest missing test cases
- **Architecture validation** - Check for anti-patterns

The key is providing AI with company-specific context so it understands your domain and standards.

---

## Future Experiments

Potential areas to explore:
- RAG (Retrieval Augmented Generation) with internal documentation
- AI-assisted code generation with company patterns
- Automated test generation based on conventions
- Integration with Azure OpenAI for enterprise scenarios
- Custom fine-tuning on company codebase

---

## Technologies Used

- **C# / .NET 8.0** - Primary development stack
- **ASP.NET Core** - Web API framework
- **GitHub Actions** - CI/CD automation
- **Claude AI (Anthropic)** - AI-powered code analysis
- **Markdown** - Documentation and configuration

---

## License

This is a personal learning and demonstration project.

---

## Contact

David - Software Architect exploring AI-enabled development workflows
