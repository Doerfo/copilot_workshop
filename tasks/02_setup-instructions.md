# Setup Copilot Instructions

This task will guide you through setting up GitHub Copilot Instructions for your .NET codespace repository with a Backend (Minimal API) and Frontend (Blazor) structure.

## Prerequisites

- Completed [Setup Repository](01_setup-repository.md)
- VS Code with GitHub Copilot extension installed
- Repository structure: `SampleApp/BackEnd/` and `SampleApp/FrontEnd/`

---

## Step 1: Enable Copilot Instructions in VS Code

### 1.1 Open VS Code Settings

- Press `Cmd + ,` (macOS) or `Ctrl + ,` (Windows/Linux)
- Or: Menu → **Settings**

### 1.2 Enable Instruction Files

Search for "copilot instructions" and enable both:

- ✅ `github.copilot.chat.codeGeneration.useInstructionFiles`
- ✅ `github.copilot.chat.useAgentInstructions`

**Alternative:** Add to `.vscode/settings.json`:

```json
{
  "github.copilot.chat.codeGeneration.useInstructionFiles": true,
  "github.copilot.chat.useAgentInstructions": true
}
```

---

## Step 2: Create Directory Structure

### 2.1 Create Required Directories

Create the necessary folders for Copilot instructions:

```bash
mkdir -p .github/instructions
```

---

## Step 3: Use Copilot to Generate Repository-Wide Instructions

### 3.1 Create Empty File

Create the file `.github/copilot-instructions.md`:

```bash
touch .github/copilot-instructions.md
```

### 3.2 Generate Content with Copilot

1. **Open the file** in VS Code
2. **Open GitHub Copilot Chat** (Ctrl+I / Cmd+I or sidebar)
3. **Use this prompt:**

```
Analyze the repository structure and existing code to create comprehensive 
repository-wide Copilot instructions for .github/copilot-instructions.md.

Include:
- Tech stack (.NET 9, C# 13, Blazor, Minimal API)
- General coding standards
- Project structure (SampleApp/BackEnd and SampleApp/FrontEnd)
- Common patterns (DI, async/await)
- Ports and URLs (Backend: 8080, Frontend: 8081, Scalar API docs)
- Error handling guidelines
```

4. **Review and save** the generated content

---

## Step 4: Generate Path-Specific Instructions with Copilot

### 4.1 Backend Instructions

1. **Create file:** `.github/instructions/backend.instructions.md`

```bash
touch .github/instructions/backend.instructions.md
```

2. **Open Copilot Chat** and use this prompt:

```
Create path-specific Copilot instructions for the backend.

File: .github/instructions/backend.instructions.md

Include frontmatter:
---
applyTo: "SampleApp/BackEnd/**/*.cs"
---

Cover:
- Minimal API patterns (MapGet, MapPost, etc.)
- OpenAPI/Scalar integration (.WithOpenApi(), .WithName())
- Service layer conventions (IService interfaces)
- HTTP status codes
- CORS configuration for frontend (port 8081)
- Configuration management (appsettings.json)

Include code examples for endpoints and services.
```

3. **Review and save**

### 4.2 Frontend Instructions

1. **Create file:** `.github/instructions/frontend.instructions.md`

```bash
touch .github/instructions/frontend.instructions.md
```

2. **Open Copilot Chat** and use this prompt:

```
Create path-specific Copilot instructions for the Blazor frontend.

File: .github/instructions/frontend.instructions.md

Include frontmatter:
---
applyTo: "SampleApp/FrontEnd/**/*.{razor,cs}"
---

Cover:
- Blazor component structure (Pages/, Shared/, Data/)
- Naming conventions (.razor, .razor.cs files)
- HttpClient configuration with IHttpClientFactory
- Component lifecycle (OnInitializedAsync)
- Event handling patterns
- Forms and validation (EditForm, DataAnnotationsValidator)
- State management (@inject, [Parameter])

Include Blazor component examples.
```

3. **Review and save**

---

## Step 5: Verify Setup

### 5.1 Check File Structure

Your `.github/` directory should look like:

```
.github/
├── copilot-instructions.md
└── instructions/
    ├── backend.instructions.md
    └── frontend.instructions.md
```

### 5.2 Test Copilot Instructions

1. Open a backend file (e.g., `SampleApp/BackEnd/Program.cs`)
2. Ask Copilot: "Add a new GET endpoint for user data"
3. Verify it suggests Minimal API syntax with `.WithOpenApi()`

4. Open a frontend file (e.g., `SampleApp/FrontEnd/Pages/Home.razor`)
5. Ask Copilot: "Create a component to fetch user data"
6. Verify it uses `@inject IHttpClientFactory` and proper Blazor patterns

### 5.3 Verify Settings

Run in VS Code:
- Open Command Palette (`Cmd+Shift+P` / `Ctrl+Shift+P`)
- Type: "Preferences: Open Settings (JSON)"
- Confirm both settings are `true`:

```json
{
  "github.copilot.chat.codeGeneration.useInstructionFiles": true,
  "github.copilot.chat.useAgentInstructions": true
}
```

---

## Step 6: Commit and Push

```bash
git add .github/
git commit -m "Add Copilot instructions for backend and frontend"
git push
```

---

## Summary

You've set up a comprehensive Copilot instruction system:

| Type | Location | Applies To |
|------|----------|------------|
| **Repository-Wide** | `.github/copilot-instructions.md` | All files (always) |
| **Backend Path-Specific** | `.github/instructions/backend.instructions.md` | `SampleApp/BackEnd/**/*.cs` |
| **Frontend Path-Specific** | `.github/instructions/frontend.instructions.md` | `SampleApp/FrontEnd/**/*.{razor,cs}` |

## Next Steps

- Experiment with Copilot Chat on different files
- Refine instructions based on actual usage
- Add project-specific patterns as you develop
- Share instructions with your team

## Tips

- Keep instructions concise and actionable
- Update as your project evolves
- Use examples to show expected patterns
- Test instructions regularly to ensure they work
