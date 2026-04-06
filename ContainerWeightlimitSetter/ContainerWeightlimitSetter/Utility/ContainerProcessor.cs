using ContainerWeightlimitSetter.Settings;
using Mutagen.Bethesda;
using Mutagen.Bethesda.Plugins.Cache;
using Mutagen.Bethesda.Skyrim;
using ContainerWeightlimitSetter.Settings.Enums;
using Mutagen.Bethesda.Plugins;

namespace ContainerWeightlimitSetter.Utility;

public class ContainerProcessor
{
    private HashSet<string> Defaultcontainers = [];
    
    private ContainerWeightSettings ContainerWeightSettings = Program.ContainerWeightSettings;
    
    public Dictionary<string, float> ContainerMaxWeightDictionary = new ();
    
    private ContainerEntryProcessor ContainerEntryProcessor;
    
    

    public ContainerProcessor(ILinkCache linkCache)
    {
        ContainerEntryProcessor = new (linkCache);
    }

    
    
    public float EstimateWeight(IContainerGetter container)
    {
        if (container.Items == null || container.Items.Count == 0) return GetContainerWeightGroupWeight(container);
        
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
        
        returnContainerWeight = (ContainerWeightSettings.UseCustomWeightIfDynamicWeightIsLower) 
            ? Math.Max(GetContainerWeightGroupWeight(container,true), returnContainerWeight)
            : returnContainerWeight;
        
        returnContainerWeight = Math.Max(Program.WeightGeneratorSettings.MinWeight, returnContainerWeight);
        
        if (container.EditorID != null) ContainerMaxWeightDictionary[container.EditorID] = returnContainerWeight;
        
        return returnContainerWeight;
    }
    
    private float GetContainerWeightGroupWeight(IContainerGetter container,bool ignoreDefaultWeight = false)
    {
        var weightGroups = ContainerWeightSettings.WeightGroups.Where(group => group.Containers.Count != 0);

        var containerWeightGroup = weightGroups
            .Where(group => group.Containers.Select(element => element.FormKey).Contains(container.FormKey))
            .ToHashSet();
        
        if (containerWeightGroup.Count > 0) return containerWeightGroup.First().Weight;

        Defaultcontainers.Add(container.EditorID!);
        return (ignoreDefaultWeight) ? 0 : ContainerWeightSettings.DefaultFallbackWeight;
    }
    
    public HashSet<FormKey> GetMerchantContainerFormKeys(HashSet<IFactionGetter> factions, ILinkCache linkCache)
    {
        return factions.Where(faction => !faction.MerchantContainer.IsNull)
            .Select(faction => faction.MerchantContainer.TryResolve(linkCache, out var placedObjectGetter) 
                ? placedObjectGetter.Base.FormKey : default)
            .Where(formKey => formKey != null).ToHashSet();
    }

}