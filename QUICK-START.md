# Quick Start Guide

## 3 Steps to Get AI Code Reviews Running

### Step 1: Copy Files (2 minutes)
```bash
cd your-ai-enable-repo
cp -r path/to/ai-enable-setup/.github .
cp -r path/to/ai-enable-setup/docs .
```

### Step 2: Add API Key (1 minute)
1. Get key from https://console.anthropic.com/
2. GitHub repo → Settings → Secrets and variables → Actions
3. New repository secret
4. Name: `ANTHROPIC_API_KEY`
5. Paste your key

### Step 3: Test (5 minutes)
```bash
git add .github/ docs/
git commit -m "Add AI code review"
git push origin main

# Create test PR with bad naming
git checkout -b test-ai
echo 'public class CustomerHelper { }' > AccountApi/Test.cs
git add . && git commit -m "test" && git push origin test-ai
```

Go to GitHub, create PR, and watch the AI review appear! 🎉

## Files Included

✅ **9 files total**
- 1 GitHub Actions workflow
- 4 C# script files (2 projects)
- 2 Documentation files (glossary + conventions)
- 2 README files

## What It Does

🤖 **Automatically reviews every PR for:**
- Naming convention violations
- Incorrect terminology (Customer vs Account)
- Code quality issues
- Missing async suffixes
- Field naming patterns

📚 **Educational feedback:**
- Explains WHY something is wrong
- Shows the CORRECT pattern
- References your conventions
- Includes code examples

## Customization

Want different rules? Just edit:
- `docs/glossary.md` - Change terminology
- `docs/naming-conventions.md` - Change naming rules

Commit changes → Next PR uses new rules!

## Complete Setup Instructions

See `README.md` for full details, troubleshooting, and interview talking points.
