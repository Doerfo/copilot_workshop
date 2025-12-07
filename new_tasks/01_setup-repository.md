# Setup Repository for MCP Server

This task guides you through creating a GitHub repository and setting up your custom MCP Server project locally.

## Prerequisites

- **.NET 10 SDK** (Preview 6 or higher) - [Download](https://dotnet.microsoft.com/download/dotnet/10.0)
- **Visual Studio Code** with GitHub Copilot extension
- **Git** installed and configured
- **GitHub account**

---

## Part 1: Create GitHub Repository

### Step 1: Create Repository on GitHub

1. Go to [GitHub](https://github.com) and click **"New repository"** (or go to [github.com/new](https://github.com/new))
2. Configure the repository:
   - **Repository name:** `CustomMcpServer`
   - **Description:** (optional) "A custom MCP Server built with .NET"
   - **Visibility:** Public (or Private, your choice)
   - ✅ **Check "Add a README file"**
   - **Add .gitignore:** Select `.NET`
3. Click **"Create repository""

### Step 2: Set Up README with Copilot

1. On your new repository page, click on the **README.md** file
2. Click the **Edit** button (pencil icon)
3. Click the **Copilot** button in the editor toolbar
4. Use this prompt:

```
Create a comprehensive README for a .NET MCP Server project that includes:
- Project description
- Features (MCP Server with stdio and HTTP transport)
- Prerequisites (.NET 10 SDK, VS Code with Copilot)
- Getting Started instructions (build and run)
- Configuration information (VS Code mcp.json)
```

5. Review the generated content and commit directly to main branch

### Step 3: Clone Repository Locally

```bash
git clone https://github.com/YOUR_USERNAME/CustomMcpServer.git
cd CustomMcpServer
```

---

## Part 2: Install MCP Server Template and Create Project

### Step 4: Install the MCP Server Template

```bash
dotnet new install Microsoft.Extensions.AI.Templates
```

### Step 5: Create Your MCP Server Project

```bash
dotnet new mcpserver -n CustomMcpServer --output .
```

> **Note:** Using `--output .` creates the project in the current directory (which already has README.md and .gitignore from GitHub).

### Step 6: Commit and Push Template Files

```bash
git add .
git commit -m "Add MCP Server project from template"
git push
```

---

## Part 3: Configure VS Code for MCP Server

### Step 7: Create VS Code Configuration Directory

```bash
mkdir -p .vscode
```

### Step 8: Add MCP Server Configuration

Create `.vscode/mcp.json` with stdio and HTTP configurations:

```json
{
  "servers": {
    "CustomMcpServer": {
      "type": "stdio",
      "command": "dotnet",
      "args": [
        "run",
        "--project",
        "."
      ]
    },
    "CustomMcpServer-HTTP": {
      "type": "http",
      "url": "http://localhost:5000/mcp"
    }
  }
}
```

### Step 9: Add HTTP Transport Support (Optional but Recommended)

To enable HTTP transport, you need to modify `Program.cs`. Ask Copilot:

```
Add HTTP transport support to Program.cs using app.MapMcp() so the server 
can be accessed via HTTP at /mcp endpoint on port 5000
```

Or manually update your `Program.cs` to include:

```csharp
// Add this after builder setup
var app = builder.Build();

// Map MCP endpoint for HTTP access
app.MapMcp();

app.Run();
```

### Step 10: Commit and Push Configuration

```bash
git add .
git commit -m "Add VS Code MCP configuration with stdio and HTTP support"
git push
```

---

## Part 4: Verify Setup

### Test stdio Connection

1. Open the project in VS Code: `code .`
2. Open GitHub Copilot Chat in **Agent Mode**
3. Click the **Select tools** icon (🔧) to verify `CustomMcpServer` appears
4. Ask: "Give me a random number between 1 and 100"
5. Verify the MCP server's random number tool is called

### Test HTTP Connection (if configured)

1. Start the server: `dotnet run`
2. In VS Code, select the `CustomMcpServer-HTTP` server from the tools
3. Test with a similar prompt

---

## Summary

You've completed the repository setup:

| Component | Status |
|-----------|--------|
| GitHub Repository | ✅ |
| README.md (with Copilot) | ✅ |
| Local Clone | ✅ |
| MCP Server Template | ✅ |
| VS Code MCP Configuration | ✅ |

## Project Structure

```
CustomMcpServer/
├── .vscode/
│   └── mcp.json          # MCP server configuration
├── .gitignore            # From GitHub
├── CustomMcpServer.csproj
├── Program.cs
└── README.md             # Created on GitHub with Copilot
```

## Next Steps

- Proceed to [Setup Instructions](02_setup-instructions.md) to configure GitHub Copilot instructions
- Add custom tools and prompts to your MCP server

## Useful Resources

- [Microsoft Docs: Build MCP Server](https://learn.microsoft.com/en-us/dotnet/ai/quickstarts/build-mcp-server)
- [MCP C# SDK on GitHub](https://github.com/modelcontextprotocol/csharp-sdk)
````
