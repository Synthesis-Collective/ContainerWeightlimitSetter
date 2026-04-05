using ContainerWeightlimitSetter.Settings.Enums;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.Synthesis.Settings;
using Mutagen.Bethesda.WPF.Reflection.Attributes;

// ReSharper disable FieldCanBeMadeReadOnly.Global

namespace ContainerWeightlimitSetter.Settings;

public class WeightGeneratorSettings
{
    public WeightGenerationMode WeightGenerationMode = WeightGenerationMode.MaxWeightMultipliedByFactor;
    public float Factor = 2.2f;
    public float MinWeight = 60f;
}