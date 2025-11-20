# AI Code Review Scripts

This directory contains C# scripts used by the AI Code Review GitHub Action.

## Structure

```
.github/
├── workflows/
│   └── ai-code-review.yml          # GitHub Actions workflow
└── scripts/
    ├── PrepareReview/
    │   ├── PrepareReview.csproj    # .NET project file
    │   └── Program.cs              # Prepares Claude API request
    └── ExtractReview/
        ├── ExtractReview.csproj    # .NET project file
        └── Program.cs              # Extracts review from API response
```

## How It Works

### 1. PrepareReview
**Purpose**: Prepares the AI review request by combining the PR diff with company documentation.

**Process**:
- Reads placeholders from `Program.cs`: `{{DIFF}}`, `{{GLOSSARY}}`, `{{CONVENTIONS}}`
- GitHub Actions replaces these with actual content from:
  - PR diff
  - `docs/glossary.md`
  - `docs/naming-conventions.md`
- Generates `request.json` for Claude API

**Testing locally**:
```bash
cd .github/scripts/PrepareReview

# Manually replace placeholders in Program.cs, then:
dotnet run

# Check the generated request.json
cat request.json
```

### 2. ExtractReview
**Purpose**: Extracts the review text from Claude's API response.

**Process**:
- Reads `review-response.json` from Claude API
- Handles errors gracefully
- Outputs `review.txt` with the formatted review

**Testing locally**:
```bash
cd .github/scripts/ExtractReview

# Need a review-response.json file (from actual API call), then:
dotnet run

# Check the extracted review
cat review.txt
```

## Benefits of Standalone Scripts

✅ **Readable**: Proper C# syntax highlighting in your IDE  
✅ **Testable**: Can test scripts locally before committing  
✅ **Debuggable**: Easier to troubleshoot issues  
✅ **Maintainable**: Change C# code without editing YAML  
✅ **Reusable**: Scripts can be used in other workflows

## Modifying the Scripts

1. **Edit the C# files** in `PrepareReview/` or `ExtractReview/`
2. **Test locally** using `dotnet run`
3. **Commit changes** - GitHub Actions will use the updated scripts
4. **Test in a PR** to verify it works end-to-end

## Placeholders

The `PrepareReview/Program.cs` file uses these placeholders that are replaced at runtime:

- `{{DIFF}}` - The git diff for the PR
- `{{GLOSSARY}}` - Content from `docs/glossary.md`
- `{{CONVENTIONS}}` - Content from `docs/naming-conventions.md`

The GitHub Actions workflow handles the replacement using Python for reliable multiline string handling.

## Requirements

- .NET 8.0 SDK (automatically installed by GitHub Actions)
- Anthropic API key (set as `ANTHROPIC_API_KEY` secret in repo)

## Troubleshooting

**Build errors in GitHub Actions**:
- Check the .csproj files are valid
- Verify .NET 8.0 is being used

**Placeholder replacement issues**:
- Check the placeholder tokens match exactly
- Verify the docs files exist at the expected paths

**API call failures**:
- Verify the `ANTHROPIC_API_KEY` secret is set correctly
- Check if you've exceeded API rate limits
- Review the API response in GitHub Actions logs
