using Mutagen.Bethesda;
using Mutagen.Bethesda.Plugins.Cache;
using Mutagen.Bethesda.Skyrim;
using ContainerWeightlimitSetter.Settings.Enums;
using Mutagen.Bethesda.Plugins;
using Noggog;

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

        var returnContainerWeightFloat = maxContainerWeight;
        
        switch (Program.WeightGeneratorSettings.WeightGenerationMode)
        {
            case WeightGenerationMode.AddMaxWeightMultipliedByFactorToMaxWeight:
                returnContainerWeightFloat += maxContainerWeight * Program.WeightGeneratorSettings.Factor;
                break;
            case WeightGenerationMode.MaxWeightMultipliedByFactor:
                returnContainerWeightFloat *= Program.WeightGeneratorSettings.Factor;
                break;
            default:
                Console.WriteLine($"Unknown WeightGenerationMode: {Program.WeightGeneratorSettings.WeightGenerationMode}");
                return 0f;
        }

        var returnContainerWeight = (float) Math.Ceiling(returnContainerWeightFloat);
        
        returnContainerWeight = Math.Max(Program.WeightGeneratorSettings.MinWeight, returnContainerWeight);
        
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
    
    public HashSet<FormKey> GetMerchantContainerFormKeys(HashSet<IFactionGetter> factions, ILinkCache linkCache)
    {
        return factions.Where(faction => !faction.MerchantContainer.IsNull)
            .Select(faction => faction.MerchantContainer.TryResolve(linkCache, out var placedObjectGetter) 
                ? placedObjectGetter.Base.FormKey : default)
            .Where(formKey => formKey != null).ToHashSet();
    }

}