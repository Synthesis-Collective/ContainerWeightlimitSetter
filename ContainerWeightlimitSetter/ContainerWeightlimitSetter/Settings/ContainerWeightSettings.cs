using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.Synthesis.Settings;
using Mutagen.Bethesda.WPF.Reflection.Attributes;

// ReSharper disable CollectionNeverUpdated.Global
// ReSharper disable FieldCanBeMadeReadOnly.Global

namespace ContainerWeightlimitSetter.Settings;

public class ContainerWeightSettings
{
    [MaintainOrder]
    [SynthesisDescription("Default Weight for Containers that start empty.")]
    [SynthesisTooltip("Default Weight for Containers that start empty.")]
    public float DefaultFallbackWeight = 150;
        
    public float SmallContainerWeight = 100;
    public HashSet<IFormLinkGetter<IContainerGetter>> SmallContainers = new ();
        
    public float MediumContainerWeight = 300;
    public HashSet<IFormLinkGetter<IContainerGetter>> MediumContainers = new();
        
    public float LargeContainerWeight = 1200;
    public HashSet<IFormLinkGetter<IContainerGetter>> LargeContainers = new();
}