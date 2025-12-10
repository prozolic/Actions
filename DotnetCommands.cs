#:package ProcessX@1.5.6

using Zx;
using static Zx.Env;

var dotnetlistsdks = await "dotnet --list-sdks > dotnetlistsdk.txt";
var dotnetversion = await "dotnet --version > dotnetversion.txt";

var sdks = File.ReadAllLines("dotnetlistsdk.txt");
var sdkInfo = string.Join(Environment.NewLine, 
    sdks.Select(sdk => {
        var parts = sdk.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var version = parts[0];
        var path = parts.Length == 2 ? parts[1] : string.Concat(parts.AsSpan(1, parts.Length - 1));
    return $"| {version} | {path} |";
}));

var version = File.ReadAllText("dotnetversion.txt");

var markdown = $"""
# Installed .NET SDK Information

| SDK | Path |
| ---- | ---- |
{sdkInfo}

# .NET Version

{version}

""";

log(markdown);

