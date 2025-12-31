using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.DI;
using SPTarkov.Server.Core.Models.Eft.Common;
using SPTarkov.Server.Core.Models.Spt.Mod;
using SPTarkov.Server.Core.Services;

namespace _itemLimitsBegone;

public record ModMetadata : AbstractModMetadata
{
    public override string ModGuid { get; init; } = "com.acidphantasm.itemlimitsbegone";
    public override string Name { get; init; } = "Item Limits Begone";
    public override string Author { get; init; } = "acidphantasm";
    public override List<string>? Contributors { get; init; }
    public override SemanticVersioning.Version Version { get; init; } = new("1.0.0");
    public override SemanticVersioning.Range SptVersion { get; init; } = new("~4.0.10");
    public override List<string>? Incompatibilities { get; init; }
    public override Dictionary<string, SemanticVersioning.Range>? ModDependencies { get; init; }
    public override string? Url { get; init; }
    public override bool? IsBundleMod { get; init; }
    public override string? License { get; init; } = "MIT";
}

[Injectable(TypePriority = OnLoadOrder.PostDBModLoader + 10)]
public class ItemLimitsBegone(
    DatabaseService databaseService)
    : IOnLoad
{
    public Task OnLoad()
    {
        EditGlobals();
        
        return Task.CompletedTask;
    }
    
    private void EditGlobals()
    {
        var globals = databaseService.GetGlobals();
        
        var fleaRestrictions = globals.Configuration.RagFair.ItemRestrictions;
        foreach (var restriction in fleaRestrictions)
        {
            restriction.MaxFlea = Int32.MaxValue;
            restriction.MaxFleaStacked = Int32.MaxValue;
        }

        var restrictionsInRaid = globals.Configuration.RestrictionsInRaid;
        
        foreach (var restriction in restrictionsInRaid)
        {
            restriction.MaxInRaid = Int32.MaxValue;
            restriction.MaxInLobby = Int32.MaxValue;
        }
    }
}
