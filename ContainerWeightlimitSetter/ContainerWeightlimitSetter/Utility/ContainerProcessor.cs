using Mutagen.Bethesda;
using Mutagen.Bethesda.Plugins.Cache;
using Mutagen.Bethesda.Skyrim;
using ContainerWeightlimitSetter.Settings.Enums;

namespace ContainerWeightlimitSetter.Utility;

public class ContainerProcessor
{
    public HashSet<string> Defaultcontainers = [];
    public HashSet<string> SmallContainers = []; 
    public HashSet<string> MediumContainers = [];
    public HashSet<string> LargeContainers = [];
    
    public Dictionary<string, float> ContainerMaxWeightDictionary = new ();
    
    private ContainerEntryProcessor ContainerEntryProcessor;
    
    

    public ContainerProcessor(ILinkCache linkCache)
    {
        ContainerEntryProcessor = new (linkCache);
    }

    
    
    public float EstimateWeight(IContainerGetter container)
    {
        if (container.Items == null || container.Items.Count == 0) return GetContainerClassWeight(container);
        
        var maxContainerWeight =  container.Items.Sum(entry => ContainerEntryProcessor.GetMaxEntryWeight(entry));

        var returnContainerWeight = maxContainerWeight;
        
        switch (Program.WeightGeneratorSettings.WeightGenerationMode)
        {
            case WeightGenerationMode.AddMaxWeightMultipliedByFactorToMaxWeight:
                returnContainerWeight += maxContainerWeight * Program.WeightGeneratorSettings.Factor;
                break;
            case WeightGenerationMode.MaxWeightMultipliedByFactor:
                returnContainerWeight *= Program.WeightGeneratorSettings.Factor;
                break;
            default:
                Console.WriteLine($"Unknown WeightGenerationMode: {Program.WeightGeneratorSettings.WeightGenerationMode}");
                return 0f;
        }
        
        if (container.EditorID != null) ContainerMaxWeightDictionary[container.EditorID] = returnContainerWeight;
        
        return returnContainerWeight;
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