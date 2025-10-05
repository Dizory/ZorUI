import * as vscode from 'vscode';
import * as fs from 'fs';
import * as path from 'path';

export function activate(context: vscode.ExtensionContext) {
    console.log('ZorUI extension is now active!');

    // Command: Create Component
    let createComponent = vscode.commands.registerCommand('zorui.createComponent', async () => {
        const componentName = await vscode.window.showInputBox({
            prompt: 'Enter component name',
            placeHolder: 'MyComponent'
        });

        if (!componentName) {
            return;
        }

        const template = generateComponentTemplate(componentName);
        createFileWithContent(componentName + '.cs', template);
    });

    // Command: Create Page
    let createPage = vscode.commands.registerCommand('zorui.createPage', async () => {
        const pageName = await vscode.window.showInputBox({
            prompt: 'Enter page name',
            placeHolder: 'HomePage'
        });

        if (!pageName) {
            return;
        }

        const template = generatePageTemplate(pageName);
        createFileWithContent(pageName + '.cs', template);
    });

    // Command: Insert Icon
    let insertIcon = vscode.commands.registerCommand('zorui.insertIcon', async () => {
        const icons = getAvailableIcons();
        const selected = await vscode.window.showQuickPick(icons, {
            placeHolder: 'Select an icon'
        });

        if (!selected) {
            return;
        }

        const editor = vscode.window.activeTextEditor;
        if (editor) {
            const snippet = new vscode.SnippetString(`new Icon(ZorIcon.${selected})`);
            editor.insertSnippet(snippet);
        }
    });

    context.subscriptions.push(createComponent, createPage, insertIcon);

    // Show welcome message on first activation
    const config = vscode.workspace.getConfiguration('zorui');
    if (!context.globalState.get('zorui.welcomed')) {
        vscode.window.showInformationMessage(
            'Welcome to ZorUI! Use "zorui" prefix for code snippets.',
            'View Documentation'
        ).then(selection => {
            if (selection === 'View Documentation') {
                vscode.env.openExternal(vscode.Uri.parse('https://github.com/zorui/zorui/docs'));
            }
        });
        context.globalState.update('zorui.welcomed', true);
    }
}

function generateComponentTemplate(name: string): string {
    return `using ZorUI.Core;
using ZorUI.Components.Layout;
using ZorUI.Components.Primitives;

namespace MyApp.Components;

/// <summary>
/// ${name} component.
/// </summary>
public class ${name} : ZorComponent
{
    public override ZorElement Build()
    {
        return new VStack(spacing: 20)
            .AddChild(
                new Text("${name}")
                    .WithFontSize(24)
                    .Bold()
            );
    }
}
`;
}

function generatePageTemplate(name: string): string {
    return `using ZorUI.Core;
using ZorUI.Core.Properties;
using ZorUI.Components.Layout;
using ZorUI.Components.Primitives;
using ZorUI.Styling;

namespace MyApp.Pages;

/// <summary>
/// ${name} page.
/// </summary>
public class ${name} : ZorComponent
{
    public override ZorElement Build()
    {
        var theme = Theme.Light();

        return new VStack(spacing: 0)
            .WithBackground(theme.Colors.Background)
            
            // Header
            .AddChild(
                new Container()
                    .WithPadding(EdgeInsets.All(theme.Spacing.Space4))
                    .WithBackground(theme.Colors.Primary)
                    .AddChild(
                        new Text("${name}")
                            .Bold()
                            .WithFontSize(theme.Typography.FontSize2Xl)
                            .WithColor(theme.Colors.PrimaryForeground)
                    )
            )
            
            // Content
            .AddChild(
                new ScrollView()
                    .Vertical()
                    .AddChild(
                        new Container()
                            .WithPadding(EdgeInsets.All(theme.Spacing.Space6))
                            .AddChild(
                                new Text("Content goes here")
                            )
                    )
            );
    }
}
`;
}

function createFileWithContent(fileName: string, content: string) {
    const workspaceFolder = vscode.workspace.workspaceFolders?.[0];
    if (!workspaceFolder) {
        vscode.window.showErrorMessage('No workspace folder open');
        return;
    }

    const filePath = path.join(workspaceFolder.uri.fsPath, fileName);
    
    fs.writeFile(filePath, content, (err) => {
        if (err) {
            vscode.window.showErrorMessage(`Failed to create file: ${err.message}`);
        } else {
            vscode.workspace.openTextDocument(filePath).then(doc => {
                vscode.window.showTextDocument(doc);
            });
            vscode.window.showInformationMessage(`Created ${fileName}`);
        }
    });
}

function getAvailableIcons(): string[] {
    return [
        'Home', 'User', 'Settings', 'Search', 'Menu', 'Close',
        'ChevronLeft', 'ChevronRight', 'ChevronUp', 'ChevronDown',
        'Check', 'X', 'Plus', 'Minus', 'Edit', 'Trash',
        'Info', 'Warning', 'Error', 'Success',
        'Mail', 'Phone', 'Calendar', 'Clock',
        'File', 'Folder', 'Download', 'Upload',
        'Heart', 'Star', 'Bookmark', 'Share'
    ];
}

export function deactivate() {}
