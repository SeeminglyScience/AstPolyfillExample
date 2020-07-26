# AstPolyfillExample

This repo is a demonstration of how it may be possible to reference new AST types without throwing
at type load in older PowerShell versions.

## How it works

It works a little bit like `netstandard` does. All of the new AST types are put in a new assembly
with two implementations. One implementation for the old PowerShell version where these types do not
yet exist, and one for the new PowerShell version where they do.

In this example, it's Windows PowerShell 5.1 vs PowerShell 7

### Windows PowerShell 5.1

This implementation is basically a reference library. It has the new AST types copied from PowerShell/PowerShell (except the members have no body).

One tricky thing is that they must be abstract, even if the real types are not. This is due to the fact
that `Ast.Accept` is abstract and internal, so you can't actually implement them outside of PowerShell
itself (unless the class you're inheriting already has). This doesn't actually affect anything unless
you try to construct one though.

### PowerShell 7

This is nothing but a couple of `TypeForwardedToAttribute` decorations. This keeps type identity the
same even though you are referencing something other than `System.Management.Automation`.

## How this could be better

### DefaultVisit

Until this is in a version we can all target, things are going to be a little rough. Even with this
workaround, there still isn't a good way to implement `ICustomAstVisitor2` *and* support new AST
types.

Once it's in though, this method will make doing that incredibly easy:

```csharp
public class MyPolyfilledAstVisitor : ICustomAstVisitor2
{
    object ICustomAstVisitor2.VisitCommand(CommandAst commandAst) => commandAst.Extent.Text;

    // etc

    object ICustomAstVisitor2.DefaultVisit(Ast ast)
    {
        return ast switch
        {
            TernaryExpressionAst ternary => VisitTernaryExpression(ternary);
            _ => HandleUnexpectedAst(ast);
        };
    }

    // Not actually an interface implementation. It'll still go through `DefaultVisit`.
    public object VisitTernaryExpression(TernaryExpressionAst ternaryExpressionAst)
    {
        return ternaryExpressionAst.Extent.Text;
    }

    private static object HandleUnexpectedAst(Ast ast)
    {
        throw new NotSupportedException(ast.GetType().Name);
    }
}
```

### Loaded automatically by PowerShell

So right now you still have to load the implementation that matches the PowerShell version you're in.
If you're going to do this, you could also just cross compile and achieve a pretty similar experience.

However, if newer versions of PowerShell loaded the redirection assembly at start up (or just added
assembly resolution logic to force load the one provided by PowerShell) then all you'd have to do
is reference the reference assembly. Older versions will load the project local copy like normal,
and newer versions will see the assembly is already loaded.

## Building the example

Have [.NET 5.0 preview](https://dotnet.microsoft.com/download/dotnet/5.0) installed (I have `5.0.100-preview.6.20318.15` installed currently) and run this:

```powershell
git clone https://github.com/SeeminglyScience/AstPolyfillExample.git
cd AstPolyfillExample
./build.ps1
Import-Module ./ConstructedModule/AstPolyfillExample.psd1

# Replace inner scriptblock with any other code for testing in 5.1. Should return nothing
# but most importantly not throw at type load.
Find-Ternary { $true ? $true : $false }
```
