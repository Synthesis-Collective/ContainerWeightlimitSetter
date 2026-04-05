using Mutagen.Bethesda;
using Mutagen.Bethesda.Skyrim;

namespace ContainerWeightlimitSetter.Utility;

public class SkseExports
{

    public readonly HashSet<string> IgnoredContainers = [];
    
    public bool ExportedIgnoredContainerEditorId(IContainerGetter container )
    {
        var ignoredContainers = Program.IgnoredContainers;
        if (container.EditorID == null || !ignoredContainers.Contains(container)) return false;
        IgnoredContainers.Add(container.EditorID);
        return true;

    }
    
}