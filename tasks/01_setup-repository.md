# Setup Repository for MCP Server

This task guides you through creating a GitHub repository and setting up your custom .NET MCP Server project locally.

## Prerequisites

- **.NET 10 SDK** - [Download](https://dotnet.microsoft.com/download/dotnet/10.0)
- **Visual Studio Code** with GitHub Copilot extension
- **Git** installed and configured
- **GitHub account**

---

## Part 1: Create GitHub Repository

### Step 1: Create Repository on GitHub

1. Go to [GitHub](https://github.com/Doerfo/WorkshopTemplate)
2. Click **"Use this template"** to create a new repository from the WorkshopTemplate
![use this template](../images/use_this_template.png)
3. Configure the repository:
   - **Owner:** Select your user
   - **Repository name:** [e.g., `CustomMcpServer`]
   - **Visibility:** Public (recommended -> free advanced security features)
4. Click **"Create repository"**

### Step 2: Open in GitHub Codespace

> **Documentation:** [What are GitHub Codespaces?](https://docs.github.com/en/codespaces/about-codespaces/what-are-codespaces)  
> **Free quota:** 15 GB-month storage, 120 hrs/month compute

You can use GitHub Codespaces for a cloud-based development environment:

1. On your repository page, click the green **"Code"** button
2. Select the **"Codespaces"** tab
3. Click **"Create codespace on main"**
4. Close the opened browser tab (codespace will continue to run in background)
5. Install the **GitHub Codespaces** extension if not already installed:
   - Click the Extensions icon in the sidebar
   - Search for "GitHub Codespaces"
   - Click **Install**

![Install Codespace Extension](../images/install_codespace_extension.png)

6. Connect to your codespace

![Open Codespace](../images/open_codespace.png)

7. Wait for the codespace to load (may take a few minutes)

8. Once ready, install the **GitHub Copilot** extension if not already installed:
   - Click the Extensions icon in the sidebar
   - Search for "GitHub Copilot"
   - Click **Install**

> **Tip:** Codespaces come with .NET pre-installed, so you can skip local SDK installation.


### Step 3: (Optional) Clone Repository Locally Instead of Codespace

```bash
git clone https://github.com/<your-username>/<your-repository-name>.git
cd <your-repository-name>
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
