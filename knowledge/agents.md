# GitHub Copilot Custom Agents - Advanced Guide

## What are Custom Agents?

**Custom Agents** allow you to create specialized AI personas with tailored expertise for specific development tasks. Each agent can have its own behavior, available tools, and instructions — enabling you to quickly switch between configurations optimized for planning, code review, security analysis, or other specialized roles.

Custom agents are defined in `.agent.md` Markdown files and can be stored in your workspace for team use, or in your user profile for personal use across workspaces.

## Agent File Location

Agent files use the `.agent.md` extension and are placed in the `.github/agents/` directory:

```
your-repo/
├── .github/
│   └── agents/
│       ├── planner.agent.md       # Planning agent
│       ├── reviewer.agent.md      # Code review agent
│       ├── security.agent.md      # Security specialist
│       └── test-specialist.agent.md
```

> **Note:** VS Code also detects any `.md` files in `.github/agents/` as custom agents. Previously, agents used `.chatmode.md` extension in `.github/chatmodes/` — VS Code still recognizes these but you can migrate them.

---

## Agent Frontmatter Configuration

The frontmatter section at the top of an `.agent.md` file defines the agent's metadata, capabilities, and behavior.

### Complete Frontmatter Reference

```yaml
---
name: Plan
description: Researches and outlines multi-step plans
argument-hint: Outline the goal or problem to research
model: Claude Sonnet 4
target: vscode
tools: ['search', 'fetch', 'usages', 'githubRepo']
handoffs:
  - label: Start Implementation
    agent: agent
    prompt: Start implementation
    send: false
---
```

### Frontmatter Properties

| Property | Required | Type | Description | GitHub.com | VS Code |
|----------|----------|------|-------------|------------|---------|
| `name` | ❌ No | string | Display name (defaults to filename without `.agent.md`) | ✅ | ✅ |
| `description` | ✅ Yes | string | Brief description of what the agent does | ✅ | ✅ |
| `tools` | ❌ No | array/string | List of tools the agent can use (defaults to all) | ✅ | ✅ |
| `target` | ❌ No | string | Target environment: `vscode` or `github-copilot` | ✅ | ✅ |
| `model` | ❌ No | string | AI model to use (e.g., `Claude Sonnet 4`) | ❌ | ✅ |
| `argument-hint` | ❌ No | string | Placeholder text in the chat input | ❌ | ✅ |
| `handoffs` | ❌ No | array | Actions to suggest after agent completes | ❌ | ✅ |
| `mcp-servers` | ❌ No | object | MCP servers for org/enterprise agents | ✅ (org/ent only) | ❌ |
| `metadata` | ❌ No | object | Custom annotation data | ✅ | ❌ |

> **Note:** Properties like `model`, `argument-hint`, and `handoffs` are VS Code-specific and ignored on GitHub.com for compatibility.

---

## Detailed Property Breakdown

### `name`

The display name for your agent. If not specified, defaults to the filename (without `.agent.md`).

```yaml
name: Plan
```

### `description` (Required)

A brief explanation of the agent's purpose and capabilities. Shown in the agent picker UI as placeholder text.

```yaml
description: Researches and outlines multi-step plans
```

### `argument-hint` (VS Code only)

Hint text shown in the chat input field to guide users on how to interact with the agent.

```yaml
argument-hint: Outline the goal or problem to research
```

### `model` (VS Code only)

Specify which AI model the agent should use. If not set, uses the currently selected model.

```yaml
model: Claude Sonnet 4
```

### `target`

Restrict the agent to a specific environment. If omitted, available in both.

```yaml
target: vscode          # Only VS Code
target: github-copilot  # Only GitHub.com
```

### `tools`

Control which tools are available to your agent. If omitted, all tools are enabled.

```yaml
# Enable all tools (default if omitted)
tools: ["*"]

# Enable specific tools only
tools: ["read", "edit", "search"]

# Disable all tools
tools: []
```

#### Tool Format Examples

```yaml
tools:
  # Built-in tool aliases
  - read                    # Read file contents
  - edit                    # Edit files
  - search                  # Search for files/text
  - shell                   # Execute shell commands
  - web                     # Fetch web content
  - custom-agent            # Invoke other agents
  - todo                    # Task list management (VS Code)
  
  # MCP Server tools (format: server-name/tool-name)
  - github/get_issue
  - github/get_issue_comments
  - playwright/*            # All tools from playwright server
  
  # VS Code extension tools
  - github.vscode-pull-request-github/issue_fetch
  - azure.some-extension/some-tool
```

#### Tool Aliases Reference

| Alias | Underlying Tools | Description |
|-------|------------------|-------------|
| `shell` | Bash, powershell | Execute shell commands |
| `read` | Read, NotebookRead | Read file contents |
| `edit` | Edit, MultiEdit, Write, NotebookEdit | Edit files |
| `search` | Grep, Glob | Search for files or text |
| `web` | WebSearch, WebFetch | Fetch URLs and web search |
| `custom-agent` | Task | Invoke other custom agents |
| `todo` | TodoWrite | Task list management (VS Code) |

### `handoffs` (VS Code only)

Define follow-up actions that appear as buttons after the agent responds. Handoffs enable workflow chaining between agents.

```yaml
handoffs:
  - label: Start Implementation
    agent: agent
    prompt: Start implementation
    send: false
    
  - label: Open in Editor
    agent: agent
    prompt: '#createFile the plan as is into an untitled file (`untitled:plan-${camelCaseName}.prompt.md` without frontmatter)'
    send: true
```

#### Handoff Properties

| Property | Required | Type | Default | Description |
|----------|----------|------|---------|-------------|
| `label` | ✅ Yes | string | - | Button text shown to user |
| `agent` | ✅ Yes | string | - | Target agent identifier to switch to |
| `prompt` | ✅ Yes | string | - | Prompt text sent to the target agent |
| `send` | ❌ No | boolean | `false` | Auto-submit the prompt when clicked |

#### Handoff Workflow Examples

| Workflow | Description |
|----------|-------------|
| Planning → Implementation | Generate a plan, then hand off to implement |
| Implementation → Review | Complete code, then switch to review agent |
| Write Failing Tests → Make Tests Pass | Generate failing tests, then implement code |

### `mcp-servers` (Org/Enterprise only)

Configure MCP servers directly in the agent profile (only for organization or enterprise-level agents).

```yaml
mcp-servers:
  custom-mcp:
    type: 'local'
    command: 'some-command'
    args: ['--arg1', '--arg2']
    tools: ["*"]
    env:
      ENV_VAR_NAME: ${{ secrets.MY_SECRET }}
```

---

## Agent vs Instructions Comparison

| Aspect | Instructions | Custom Agents |
|--------|-------------|---------------|
| **File Extension** | `.md` / `.instructions.md` | `.agent.md` |
| **Location** | `.github/copilot-instructions.md` or `.github/instructions/` | `.github/agents/` |
| **Primary Purpose** | Coding standards & context | Specialized workflows & personas |
| **Tool Restrictions** | ❌ No | ✅ Yes |
| **Handoffs** | ❌ No | ✅ Yes (VS Code) |
| **Model Selection** | ❌ No | ✅ Yes (VS Code) |
| **Triggered By** | File matching (globs) | User selection / handoff |
| **Stacking** | Additive | Single agent active |

---

## Creating Agents in VS Code

### From the UI

1. Select **Configure Custom Agents** from the agents dropdown
2. Click **Create new custom agent**
3. Choose location:
   - **Workspace**: Creates in `.github/agents/` (shared with team)
   - **User profile**: Creates in user profile folder (personal use)
4. Enter a filename for the agent
5. Configure the frontmatter and instructions

### Settings

No special settings required — VS Code automatically detects `.agent.md` files in `.github/agents/`.

---

## Best Practices

### ✅ Do

| Practice | Reason |
|----------|--------|
| Keep agents focused | Single responsibility per agent |
| Use descriptive names | Easy discovery in UI |
| Limit tools appropriately | Prevent misuse, improve performance |
| Chain with handoffs | Build complex workflows |
| Include clear instructions | Guide the agent's behavior |
| Use `description` effectively | Helps users choose the right agent |

### ❌ Don't

| Anti-pattern | Why |
|--------------|-----|
| Include all tools when not needed | Slows down agent, dilutes focus |
| Vague descriptions | Users won't understand when to use |
| Circular handoffs | Creates infinite loops |
| Mix concerns | One agent should do one thing well |

---

## Key Takeaways

1. **File location** — Agents go in `.github/agents/` with `.agent.md` extension
2. **Description is required** — Always provide a clear description
3. **Tool restrictions** — Omit `tools` for all, or specify a subset
4. **Handoffs (VS Code)** — Chain agents into multi-step workflows
5. **Platform differences** — Some properties only work in VS Code or GitHub.com

## Useful Links

- [Creating Custom Agents](https://docs.github.com/en/copilot/how-tos/use-copilot-agents/coding-agent/create-custom-agents)
- [Custom Agents Configuration Reference](https://docs.github.com/en/copilot/reference/custom-agents-configuration)
- [Custom Agents in VS Code](https://code.visualstudio.com/docs/copilot/customization/custom-agents)
- [Awesome Copilot Agents](https://github.com/github/awesome-copilot/tree/main/agents)
