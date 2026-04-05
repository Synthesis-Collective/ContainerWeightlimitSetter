using Mutagen.Bethesda;
using Mutagen.Bethesda.Skyrim;

namespace ContainerWeightlimitSetter.Utility;

public class ContainerProcessor
{
    public HashSet<string> Defaultcontainers = [];
    public HashSet<string> SmallContainers = []; 
    public HashSet<string> MediumContainers = [];
    public HashSet<string> LargeContainers = [];
    
    public float EstimateWeight(IContainerGetter container)
    {
        if (container.Items == null || container.Items.Count == 0) return GetContainerClassWeight(container);
        
        //TODO: IMPLEMENT CONTAINER LOOT BASED WEIGHT LIMIT GENERATION
        
        return 42;
    }
    
    private float GetContainerClassWeight(IContainerGetter container)
    {
        var containerWeightSettings = Program.ContainerWeightSettings;

        if (containerWeightSettings.SmallContainers.Contains(container))
        {
            SmallContainers.Add(container.EditorID!);
            return containerWeightSettings.SmallContainerWeight;
        } 
            
        if (containerWeightSettings.MediumContainers.Contains(container))
        {
            MediumContainers.Add(container.EditorID!);
            return containerWeightSettings.MediumContainerWeight;
        }
            
        if (containerWeightSettings.LargeContainers.Contains(container))
        {
            LargeContainers.Add(container.EditorID!);
            return containerWeightSettings.LargeContainerWeight;
        }

        Defaultcontainers.Add(container.EditorID!);
        return containerWeightSettings.DefaultFallbackWeight;
    }

}