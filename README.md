# AI Code Review Setup for ai-enable Repository

This package contains everything you need to set up AI-powered code reviews for your AccountApi project.

## What's Included

```
ai-enable-setup/
├── .github/
│   ├── workflows/
│   │   └── ai-code-review.yml          # GitHub Actions workflow
│   └── scripts/
│       ├── README.md                    # Script documentation
│       ├── PrepareReview/
│       │   ├── PrepareReview.csproj    # C# project
│       │   └── Program.cs              # Prepares AI review
│       └── ExtractReview/
│           ├── ExtractReview.csproj    # C# project
│           └── Program.cs              # Extracts AI review
└── docs/
    ├── glossary.md                      # Company terminology
    └── naming-conventions.md            # Coding standards
```

## Installation Steps

### 1. Copy Files to Your Repository

Copy the entire structure to your `ai-enable` repository root:

```bash
# Navigate to your ai-enable repo
cd /path/to/ai-enable

# Copy all files (preserving directory structure)
cp -r /path/to/ai-enable-setup/.github .
cp -r /path/to/ai-enable-setup/docs .
```

Your final structure should be:
```
ai-enable/                              (your repo root)
├── .github/
│   ├── workflows/
│   │   └── ai-code-review.yml
│   └── scripts/
│       ├── README.md
│       ├── PrepareReview/
│       │   ├── PrepareReview.csproj
│       │   └── Program.cs
│       └── ExtractReview/
│           ├── ExtractReview.csproj
│           └── Program.cs
├── docs/
│   ├── glossary.md
│   └── naming-conventions.md
└── AccountApi/                          (your existing solution)
    └── ...
```

### 2. Set Up Anthropic API Key

1. Get an API key from https://console.anthropic.com/
2. Go to your GitHub repo: `https://github.com/YOUR_USERNAME/ai-enable`
3. Click **Settings** → **Secrets and variables** → **Actions**
4. Click **New repository secret**
5. Name: `ANTHROPIC_API_KEY`
6. Value: Your API key
7. Click **Add secret**

### 3. Commit and Push

```bash
git add .github/ docs/
git commit -m "Add AI code review with AccountApi conventions"
git push origin main
```

### 4. Test It!

Create a test PR with intentional naming violations:

```bash
# Create test branch
git checkout -b test-naming-violations

# Create a file with violations
cat > AccountApi/Services/CustomerHelper.cs << 'EOF'
// This has multiple naming violations!
public class CustomerHelper  // ❌ Should be AccountService
{
    private ICustomerRepo repo;  // ❌ Several issues
    
    public Customer Get(string id)  // ❌ Should be GetAccountAsync
    {
        return repo.GetById(id);
    }
}
EOF

# Commit and push
git add .
git commit -m "Test: Add file with naming violations"
git push origin test-naming-violations
```

Then:
1. Go to GitHub and create a PR from `test-naming-violations` → `main`
2. Wait ~30-60 seconds
3. You'll see an AI comment pointing out all the naming violations! 🎉

## What the AI Reviews For

### Naming Conventions
- ✅ Services end with `Service`
- ✅ Repositories end with `Repository`
- ✅ Helpers are static and end with `Helper`
- ✅ Clients end with `Client`
- ✅ Dtos end with `Dto`
- ✅ Dbos end with `Dbo`

### Terminology
- ✅ Use `Account` not `Customer`
- ✅ Proper use of domain terms
- ✅ Consistent naming across codebase

### Code Quality
- ✅ Async methods have `Async` suffix
- ✅ Private fields use `_camelCase`
- ✅ Proper use of `readonly`
- ✅ Interface naming with `I` prefix

## Customizing for Your Needs

### Modify Conventions
Edit `docs/naming-conventions.md` to change rules:
- Add new suffixes or patterns
- Change method naming rules
- Add project-specific standards

### Modify Terminology
Edit `docs/glossary.md` to:
- Add domain-specific terms
- Define business concepts
- Clarify technical patterns

### Modify AI Behavior
Edit `.github/scripts/PrepareReview/Program.cs` to:
- Change the prompt structure
- Add more instructions
- Adjust review format

After any changes, just commit and push - the next PR will use the updated rules!

## Troubleshooting

### Workflow doesn't run
- Check that `.github/workflows/ai-code-review.yml` is committed to main
- Verify you have Actions enabled in repo settings

### API key error
- Verify `ANTHROPIC_API_KEY` secret is set correctly
- Check you have API credits at https://console.anthropic.com/

### Review is generic (doesn't use conventions)
- Verify `docs/glossary.md` and `docs/naming-conventions.md` exist
- Check the files are committed to your repo
- Look at the PR comment - it will tell you if docs are missing

### Build errors
- Ensure .NET 8.0 is being used (GitHub Actions should handle this)
- Check the C# files for syntax errors
- Review GitHub Actions logs for details

## For Your Interview

**Key Points to Mention:**

✅ "I built an AI-powered code review system that enforces our company's naming conventions and terminology automatically"

✅ "It's implemented entirely in C#/.NET - no Python dependencies"

✅ "The system reads our company glossary and coding standards, then reviews PRs against those rules"

✅ "It's educational - doesn't just say 'wrong', it explains why and shows the correct pattern"

✅ "Completely customizable - just edit markdown files to change rules"

✅ "Saves senior dev time - junior devs get immediate feedback on conventions"

**Demo Ready:**
- You have a working example in your `ai-enable` repo
- Can show the actual PR comments
- Can show how to customize the conventions
- Can discuss the architecture (GitHub Actions → C# scripts → Claude API)

## Questions?

If you have issues or want to extend this:
1. Check `.github/scripts/README.md` for script documentation
2. Review the YAML file comments for workflow details
3. Test scripts locally before committing (see scripts README)

Good luck with your interview! 🚀
