namespace ItemLimitsBegone;

using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.DI;
using SPTarkov.Server.Core.Models.Spt.Tables;

[Injectable(TypePriority = OnLoadOrder.PostLoad + 10)]
public class ItemLimitsBegone(
    GlobalTable globalTable)
    : IOnLoad
{
    public Task OnLoadAsync(CancellationToken cancellationToken)
    {
        EditGlobals();
        
        return Task.CompletedTask;
    }
    
    private void EditGlobals()
    {
        var globals = globalTable;
        
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

        globals.Configuration.ItemsCommonSettings.MaxBackpackInserting = 42;
    }
}
