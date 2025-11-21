using System.Text.Json;

// Read content directly from files (no placeholders needed!)
var diff = File.ReadAllText("pr-diff.txt");

var glossary = File.Exists("docs/glossary.md")
    ? File.ReadAllText("docs/glossary.md")
    : "";

var conventions = File.Exists("docs/naming-conventions.md")
    ? File.ReadAllText("docs/naming-conventions.md")
    : "";

// Build the prompt based on whether we have company documentation
string prompt;

if (!string.IsNullOrEmpty(glossary) && !string.IsNullOrEmpty(conventions))
{
    prompt = $@"You are reviewing code for the AccountApi project. Your job is to:

1. **Check naming conventions** - Verify all naming follows the company standards
2. **Check terminology** - Ensure correct use of domain terms (Account, not Customer)
3. **Identify violations** - Point out specific naming issues with line references
4. **Suggest corrections** - Provide the correct names according to conventions
5. **Provide other feedback** - Note any other code quality issues

## CRITICAL: Company Glossary and Terminology
{glossary}

## CRITICAL: Naming Conventions to Enforce
{conventions}

## Code Changes to Review:
{diff}

## Your Review Format:

### 🔍 Naming Convention Issues
For each issue found:
- **Current name**: What was used
- **Issue**: Why it violates conventions
- **Correct name**: What it should be
- **Reference**: Quote the relevant convention/glossary entry
- **Example**: Show corrected code

### ✅ What Looks Good
Mention things that follow conventions correctly.

### 💡 Other Code Quality Notes
Any other observations (bugs, best practices, etc.)

Be specific, educational, and constructive. If everything looks good, say so!
";
}
else
{
    prompt = $@"You are reviewing code changes. Check for:
- Code quality issues
- Potential bugs
- Best practices
- Naming consistency

Code changes:
{diff}

Note: Company documentation not found. Review will be generic.
";
}

// Create JSON request for Claude API
var request = new
{
    model = "claude-sonnet-4-20250514",
    max_tokens = 3000,
    messages = new[]
    {
        new { role = "user", content = prompt }
    }
};

// Serialize to JSON file
var json = JsonSerializer.Serialize(request, new JsonSerializerOptions
{
    WriteIndented = true
});

File.WriteAllText("request.json", json);

Console.WriteLine("✅ Prompt prepared and saved to request.json");
