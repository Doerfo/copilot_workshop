# Add Repository Prompts

This task guides you through setting up a **Prompt Library** for your repository using **Prompt Files**. Prompt files allow you to define reusable, pre-configured prompts with specific context and tools, making it easier for your team to perform common tasks consistently.

## Prerequisites

- Completed [Setup Instructions](02_setup-instructions.md)
- VS Code with GitHub Copilot Chat

---

## Step 1: Create the Prompts Directory

Prompt files are stored in the `.github/prompts/` directory and use the `.prompt.md` extension.

1. **Create the directory:**

```bash
mkdir -p .github/prompts
```

---

## Step 2: Create Your First Prompt File

Let's create a prompt to generate backend tests. This prompt will automatically use the `@workspace` context and specific instructions.

1. **Create the file:** `.github/prompts/create-backend-test.prompt.md`

```bash
touch .github/prompts/create-backend-test.prompt.md
```

2. **Add the content:**

Write the prompt content yourself. It should define the frontmatter and the instructions for Copilot.

**Goal:** Generate xUnit tests for the currently open backend file.

```markdown
---
name: create-backend-test
description: Generate xUnit tests for a backend endpoint or service
tools: [(configure necessary tools here, e.g., 'read', 'search')]
---

(Write your prompt instructions here)
```

---

## Step 3: Create Additional Prompts (Exercise)

Now it's your turn. Create the following prompt files in `.github/prompts/` based on the goals below. Think about what instructions and tools are needed for each.

### 3.1 Code Review Prompt (`review-selection.prompt.md`)

**Goal:** Review selected code for security, performance, and standards.
**Requirements:**
- Name: `review-selection`
- Description: Review code for quality and security
- Prompt logic:
    - Check for OWASP vulnerabilities
    - Check for performance bottlenecks
    - Verify .NET coding standards
    - Output structured feedback (Issue, Severity, Recommendation)

### 3.2 Documentation Prompt (`document-endpoint.prompt.md`)

**Goal:** Generate OpenAPI documentation for a Minimal API endpoint.
**Requirements:**
- Name: `document-endpoint`
- Description: Generate OpenAPI metadata and XML docs
- Prompt logic:
    - Add `.WithSummary` and `.WithDescription`
    - Add `.Produces<T>` for success and error codes
    - Describe parameters

### 3.3 Refactoring Prompt (`refactor-to-service.prompt.md`)

**Goal:** Refactor logic from a Minimal API handler into a Service class.
**Requirements:**
- Name: `refactor-to-service`
- Description: Move endpoint logic to a service
- Prompt logic:
    - Define an interface
    - Create implementation
    - Inject service
    - Keep endpoint thin

---

## Step 4: Test Your Prompts

1. **Reload VS Code** (or wait a moment) to ensure the new prompts are detected.
2. **Open Copilot Chat**.
3. **Type `/`** to see the list of commands. You should see your new prompts (e.g., `/create-backend-test`).
4. **Test `/create-backend-test`**:
    - Open a backend file (e.g., `SampleApp/BackEnd/Program.cs`).
    - Type `/create-backend-test` in Copilot Chat.
    - Verify it generates relevant tests.
5. **Test your other prompts**:
    - Select code and try `/review-selection`.
    - Select an endpoint and try `/document-endpoint`.

## References

- [VS Code: Prompt Files](https://code.visualstudio.com/docs/copilot/customization/prompt-files)
