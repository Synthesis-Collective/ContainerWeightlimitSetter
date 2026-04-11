using ContainerWeightlimitSetter.Settings;
using Mutagen.Bethesda;
using Mutagen.Bethesda.Plugins.Cache;
using Mutagen.Bethesda.Skyrim;
// ReSharper disable UnusedMember.Global

namespace ContainerWeightlimitSetter.Utility;

public class LoggingExports(ContainerWeightSettings containerWeightSettings)
{
    
    public readonly HashSet<string> IgnoredContainers = [];
    public HashSet<ReadableWeightGroup> weightGroups = [];
    
    public bool ExportedIgnoredContainerEditorId(IContainerGetter container )
    {
        var ignoredContainers = Program.IgnoredContainers;
        if (container.EditorID == null || !ignoredContainers.Contains(container)) return false;
        IgnoredContainers.Add(container.EditorID);
        return true;

    }

    public void ExportWeightGroups(ILinkCache linkCache)
    {
        weightGroups.UnionWith(containerWeightSettings.WeightGroups.Select(group =>
        {
            return new ReadableWeightGroup(group.Weight,
                group.Containers.Select(container =>
                {
                    if (container.TryResolve<IContainerGetter>(linkCache, out var containerGetter) 
                        && containerGetter.EditorID != null)
                    {
                        return containerGetter.EditorID;
                    } 
                    return $"Missing EditorID for {container.FormKey}";
                }).ToHashSet());
        }));
    }
    
    public class ReadableWeightGroup(float weight, HashSet<string> containers)
    {
        public float Weight = weight;
        public HashSet<string> Containers = containers;
    }
    
}