using ContainerWeightlimitSetter.Settings;
using Mutagen.Bethesda;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Plugins.Cache;
using Mutagen.Bethesda.Plugins.Records;
using Mutagen.Bethesda.Skyrim;

namespace ContainerWeightlimitSetter.Utility;

public class ExportForSKSE(SKSEConfigSettings config, ILinkCache linkCache)
{
    public HashSet<string> ExcludedCells = config.ExcludedCells
        .Select(f => f.TryResolve(linkCache, out var cell) ? cell.EditorID : "")
        .Where(editorId => !string.IsNullOrWhiteSpace(editorId))
        .Select(id => id!)
        .ToHashSet();

    public HashSet<string> ExcludedForms = GetFormStrings(config.ExcludedContainers);
    /*
    public HashSet<string> ExcludedForms = GetFormStrings(config.ExcludedContainers)
        .Concat(GetFormStrings(config.ExcludedPlacedObjects)).ToHashSet();
    */

    private static HashSet<string> GetFormStrings(IEnumerable<IFormLinkGetter<IMajorRecordGetter>>  formLinkGetters) 
    {
        return formLinkGetters.Select(GetFormStrings).ToHashSet();
    }

    private static string GetFormStrings(IFormLinkGetter<IMajorRecordGetter> fromLink)
    {
        return $"{fromLink.FormKey.ModKey.FileName}|0x{fromLink.FormKey.IDString()}" ;
    }
}