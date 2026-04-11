using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Plugins.Records;
using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.Synthesis.Settings;
using Mutagen.Bethesda.WPF.Reflection.Attributes;

// ReSharper disable ConvertToConstant.Global
// ReSharper disable FieldCanBeMadeReadOnly.Global
// ReSharper disable CollectionNeverUpdated.Global

namespace ContainerWeightlimitSetter.Settings
{
    public class Settings
    {

        [MaintainOrder]
        [SynthesisDescription("Settings for Handling Container Weights Defaults and Custom Weights.")]
        [SynthesisTooltip("Settings for Handling Container Weights Defaults and Custom Weights.")]
        public ContainerWeightSettings ContainerWeightSettings = new();
        
        [MaintainOrder]
        [SynthesisDescription("Settings for Generating Weights for prefilled Containers.")]
        [SynthesisTooltip("Settings for Generating Weights for prefilled Containers.")]
        public WeightGeneratorSettings WeightGeneratorSettings = new();

        [MaintainOrder]
        [SynthesisDescription("Containers that will be ignored by the patcher.")]
        [SynthesisTooltip("Containers that will be ignored by the patcher.")]
        public HashSet<IFormLinkGetter<IContainerGetter>> IgnoredContainers = [];
        
        [MaintainOrder]
        [SynthesisDescription("Settings Generator GUI for the SKSE Plugin")]
        public SKSEConfigSettings SKSEConfigSettings = new();

    }

}