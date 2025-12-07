````markdown
# Setup Copilot Instructions

This task guides you through setting up GitHub Copilot Instructions for your MCP Server project to ensure consistent, high-quality AI-assisted code generation.

## Prerequisites

- Completed [Setup Repository](01_setup-repository.md)
- VS Code with GitHub Copilot extension installed
- MCP Server project structure in place

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
- Tech stack (.NET 10, C# 13, MCP Server)
- General coding standards for MCP servers
- Project structure (Services/, Tools/, Prompts/)
- Common patterns (DI, async/await, [McpServerTool], [McpServerPrompt])
- MCP Server conventions (tool descriptions, prompt templates)
- Error handling guidelines
- Testing approach
```

4. **Review and save** the generated content

> **Tip:** You can find example instructions for C# MCP servers in the [Awesome Copilot repository](https://github.com/github/awesome-copilot/blob/main/instructions/csharp-mcp-server.instructions.md) to use as inspiration or starting point.

---

## Step 4: Generate Path-Specific Instructions with Copilot

### 4.1 Tools Instructions

1. **Create file:** `.github/instructions/tools.instructions.md`

```bash
touch .github/instructions/tools.instructions.md
```

2. **Open Copilot Chat** and use this prompt:

```
Create path-specific Copilot instructions for MCP Server tools.

File: .github/instructions/tools.instructions.md

Include frontmatter:
---
applyTo: "**/Tools/**/*.cs"
---

Cover:
- [McpServerToolType] attribute on tool classes
- [McpServerTool] attribute on methods with clear [Description]
- Return string values formatted for AI consumption (use emojis for status)
- Async patterns for tool methods
- Dependency injection for services
- Parameter validation and error handling
- Tool naming conventions (verb + noun)

Include code examples for typical tool implementations.
```

3. **Review and save**

### 4.2 Services Instructions

1. **Create file:** `.github/instructions/services.instructions.md`

```bash
touch .github/instructions/services.instructions.md
```

2. **Open Copilot Chat** and use this prompt:

```
Create path-specific Copilot instructions for MCP Server services.

File: .github/instructions/services.instructions.md

Include frontmatter:
---
applyTo: "**/Services/**/*.cs"
---

Cover:
- Interface-first design (IService pattern)
- Singleton registration for in-memory state
- CRUD operation patterns
- Thread-safe collection usage (ConcurrentDictionary)
- Async/await patterns
- Modern C# features (records, primary constructors)
- Clear method naming and documentation

Include service implementation examples.
```

3. **Review and save**

### 4.3 Prompts Instructions

1. **Create file:** `.github/instructions/prompts.instructions.md`

```bash
touch .github/instructions/prompts.instructions.md
```

2. **Open Copilot Chat** and use this prompt:

```
Create path-specific Copilot instructions for MCP Server prompts.

File: .github/instructions/prompts.instructions.md

Include frontmatter:
---
applyTo: "**/Prompts/**/*.cs"
---

Cover:
- [McpServerPromptType] attribute on prompt classes
- [McpServerPrompt] attribute on methods with [Description]
- PromptMessage return type patterns
- Template generation for AI interactions
- Clear, actionable prompt content
- Parameter handling for dynamic prompts
- Combining multiple messages in prompts

Include prompt implementation examples.
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
    ├── tools.instructions.md
    ├── services.instructions.md
    └── prompts.instructions.md
```

### 5.2 Test Copilot Instructions

1. Open a file in the `Tools/` folder (or create one)
2. Ask Copilot: "Add a new tool to get the current time"
3. Verify it suggests code with:
   - `[McpServerTool]` attribute
   - `[Description]` attribute
   - Proper return format

4. Open a file in the `Services/` folder
5. Ask Copilot: "Create a service to manage items"
6. Verify it uses interface-first design and proper patterns

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
git commit -m "Add Copilot instructions for MCP Server development"
git push
```

---

## Summary

You've set up a comprehensive Copilot instruction system for your MCP Server:

| Type | Location | Applies To |
|------|----------|------------|
| **Repository-Wide** | `.github/copilot-instructions.md` | All files (always) |
| **Tools** | `.github/instructions/tools.instructions.md` | `**/Tools/**/*.cs` |
| **Services** | `.github/instructions/services.instructions.md` | `**/Services/**/*.cs` |
| **Prompts** | `.github/instructions/prompts.instructions.md` | `**/Prompts/**/*.cs` |

## Next Steps

- Proceed to [Add Prompts](03_add-prompts.md) to create reusable prompt templates
- Experiment with Copilot Chat on different files
- Refine instructions based on actual usage

## Tips

- Keep instructions concise and actionable
- Update as your MCP server evolves
- Use examples to show expected patterns
- Test instructions regularly to ensure they work

## Useful Resources

- [VS Code: Custom Instructions](https://code.visualstudio.com/docs/copilot/customization/custom-instructions)
- [Awesome Copilot](https://github.com/github/awesome-copilot) - Collection of example instructions and best practices
- [C# MCP Server Instructions Example](https://github.com/github/awesome-copilot/blob/main/instructions/csharp-mcp-server.instructions.md)
- [MCP Server Patterns](../knowledge/mcp-server.md)
````
