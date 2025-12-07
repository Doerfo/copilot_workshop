# Add Repository Prompts

This task guides you through setting up a **Prompt Library** for your MCP Server repository using **Prompt Files**. Prompt files allow you to define reusable, pre-configured prompts with specific context and tools, making it easier to perform common MCP development tasks consistently.

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

## Step 2: Create Your First Prompt File - Add MCP Tool

Let's create a prompt to quickly add new MCP tools to your server. This is one of the most common tasks when building an MCP server.

1. **Create the file:** `.github/prompts/add-mcp-tool.prompt.md`

```bash
touch .github/prompts/add-mcp-tool.prompt.md
```

2. **Add the content:**

```markdown
---
name: add-mcp-tool
description: Add a new MCP tool to the server based on a description
agent: agent
argument-hint: Describe the tool you want to create
tools:
  ['vscode', 'execute', 'read', 'agent', 'edit', 'search', 'web', 'microsoft-docs/*', 'todo']
---

# Add MCP Tool

Create a new MCP Server tool based on the following description.

## Requirements

1. **Tool Class**: Create or update the appropriate Tools class with `[McpServerToolType]`
2. **Tool Method**: Add a method with `[McpServerTool]` and clear `[Description]` attributes
3. **Parameters**: Define parameters with `[Description]` for AI discoverability
4. **Return Format**: Return user-friendly formatted strings (use emojis for status)
5. **Error Handling**: Include proper validation and error messages
6. **Service Integration**: If needed, inject and use appropriate services

## Code Style

- Use async/await for operations that may take time
- Follow C# naming conventions (PascalCase for methods)
- Keep tools focused on a single responsibility
- Use descriptive parameter names

Generate the complete implementation and update any necessary files (Program.cs for DI registration if new services are needed).
```

---

## Step 3: Create Additional Prompts

### 3.1 Review Selected Code (`review-selected-code.prompt.md`)

Create a prompt to review code for quality and security:

```bash
touch .github/prompts/review-selected-code.prompt.md
```

**Add content:**

```markdown
---
name: review-selected-code
description: Review selected code for quality, security, and best practices
agent: agent
tools: 
  ['read', 'search', 'web', 'microsoft-docs/*', 'todo']
---

# Review Selected Code

Review the selected code for MCP Server best practices, security, and performance.

## Check For

### MCP Server Specific
- [ ] `[McpServerTool]` or `[McpServerPrompt]` attributes used correctly
- [ ] Clear `[Description]` attributes for AI discoverability
- [ ] User-friendly return formats (emojis, structured text)
- [ ] Proper dependency injection

### Security (OWASP)
- [ ] Input validation and sanitization
- [ ] No SQL injection vulnerabilities
- [ ] No hardcoded secrets or sensitive data
- [ ] Proper error handling (no sensitive info in errors)

### Performance
- [ ] Async/await used appropriately
- [ ] No blocking calls on async code
- [ ] Efficient data structures (e.g., ConcurrentDictionary for thread-safety)
- [ ] No unnecessary allocations

### Code Quality
- [ ] Modern C# features used appropriately (records, primary constructors)
- [ ] Consistent naming conventions (PascalCase, camelCase)
- [ ] No code duplication
- [ ] Clear method and variable names

## Output Format

For each issue found, provide:
- **Issue**: Description of the problem
- **Severity**: Critical / High / Medium / Low
- **Recommendation**: How to fix it

Provide specific, actionable recommendations.
```

### 3.2 (optional) Test Current File (`test-current-file.prompt.md`)

Create a prompt to generate tests for the currently open file:

```bash
touch .github/prompts/test-current-file.prompt.md
```

**Add content:**

````markdown
---
name: test-current-file
description: Generate xUnit tests for the currently open file
agent: agent
tools: 
  ['vscode', 'execute', 'read', 'agent', 'edit', 'search', 'web', 'microsoft-docs/*', 'todo']
---

# Test Current File

Generate comprehensive xUnit tests for ${file}.

## Requirements

1. **Test Class**: Create in a Tests folder with proper naming (ClassNameTests)
2. **Test Cases**:
   - Happy path scenarios
   - Edge cases (empty input, null values, boundary conditions)
   - Error conditions and exception handling
3. **Mocking**: Mock dependencies using Moq
4. **Assertions**: Clear, specific assertions for return values and side effects
5. **Naming**: Test method names should clearly describe what is being tested

## Test Method Naming Pattern

Use: `MethodName_Scenario_ExpectedResult`

Examples:
- `GetRandomNumber_WithValidRange_ReturnsNumberInRange`
- `GetRandomNumber_WithInvalidRange_ThrowsException`
- `GetRandomNumber_WithEqualMinMax_ReturnsThatNumber`

## Example Structure

```csharp
public class RandomNumberToolsTests
{
    private readonly RandomNumberTools _tools;

    public RandomNumberToolsTests()
    {
        _tools = new RandomNumberTools();
    }

    [Fact]
    public void GetRandomNumber_WithValidRange_ReturnsNumberInRange()
    {
        // Arrange
        var min = 1;
        var max = 100;
        
        // Act
        var result = _tools.GetRandomNumber(min, max);
        
        // Assert
        Assert.InRange(result, min, max);
    }

    [Fact]
    public void GetRandomNumber_WithInvalidRange_ThrowsException()
    {
        // Arrange
        var min = 100;
        var max = 1;
        
        // Act & Assert
        Assert.Throws<ArgumentException>(() => _tools.GetRandomNumber(min, max));
    }
}
```

Generate complete test coverage for all public methods.
````

---

## Step 4: Verify Prompts Setup

### 4.1 Check File Structure

Your `.github/prompts/` directory should now contain:

```
.github/prompts/
├── add-mcp-tool.prompt.md
├── review-selected-code.prompt.md
└── test-current-file.prompt.md
```

### 4.2 Test Your Prompts

1. **Reload VS Code** (or wait a moment) to ensure the new prompts are detected
2. **Open Copilot Chat**
3. **Type `/`** to see the list of commands. You should see your new prompts:
   - `/add-mcp-tool`
   - `/review-selected-code`
   - `/test-current-file`

4. **Test `/add-mcp-tool`**:
   ```
   /add-mcp-tool Create a tool that flips a coin and returns "Heads" or "Tails".
   ```

5. **Test `/review-selected-code`**:
   - Select some code in a file
   - Run `/review-selected-code`
   - Verify it provides structured feedback

6. **Test `/test-current-file` (optional)**:
   - Create a new xUnit test project (you should use copilot to help)
   - Open a Tools or Services file 
   - Run `/test-current-file`
   - Verify it generates comprehensive xUnit tests

## Summary

You've created a comprehensive prompt library for MCP Server development:

| Prompt | Description | Usage |
|--------|-------------|-------|
| `/add-mcp-tool` | Add new MCP tools | `/add-mcp-tool [description]` |
| `/review-selected-code` | Review code for best practices | Select code, then `/review-selected-code` |
| `/test-current-file` | Generate tests for current file | Open file, then `/test-current-file` |

## References

- [VS Code: Prompt Files](https://code.visualstudio.com/docs/copilot/customization/prompt-files)
- [04_prompts.md](../knowledge/04_prompts.md)