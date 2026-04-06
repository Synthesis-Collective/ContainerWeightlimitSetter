using Mutagen.Bethesda.FormKeys.SkyrimSE;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.Synthesis.Settings;
using Mutagen.Bethesda.WPF.Reflection.Attributes;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable CollectionNeverUpdated.Global
// ReSharper disable FieldCanBeMadeReadOnly.Global

namespace ContainerWeightlimitSetter.Settings;

public class ContainerWeightSettings
{
    [MaintainOrder]
    [SynthesisDescription("Default Weight for Containers that start empty.")]
    [SynthesisTooltip("Default Weight for Containers that start empty.")]
    public float DefaultFallbackWeight = 150;
    
    [MaintainOrder]
    [SynthesisDescription("Custom Weights for Configurable Containers.")]
    public HashSet<WeightGroup> WeightGroups = new ()
    {
        new WeightGroup(100,[
            Skyrim.Container.NobleWardrobe01
        ]),
        new WeightGroup(300,[]),
        new WeightGroup(1200,[]),
    };
    
    [MaintainOrder]
    public bool UseCustomWeightIfDynamicWeightIsLower = true;
    
    public class WeightGroup(int weight, HashSet<IFormLinkGetter<IContainerGetter>> containers) : IComparable<WeightGroup>
    {
        [MaintainOrder]
        [SynthesisDescription("Weight for Containers Listed Below.")]
        [SynthesisTooltip("Weight for Containers Listed Below.")]
        public int Weight = weight;
        [MaintainOrder]
        [SynthesisDescription("Containers that will be assigned this Weight.")]
        [SynthesisTooltip("Containers that will be assigned this Weight.")]
        public HashSet<IFormLinkGetter<IContainerGetter>> Containers = containers;
        
        public int CompareTo(WeightGroup? other)
        {
            return (other != null) ? Weight.CompareTo(other.Weight) : Weight.CompareTo(0);
        }
    }
}