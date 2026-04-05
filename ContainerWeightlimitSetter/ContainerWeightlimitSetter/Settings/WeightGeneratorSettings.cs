using ContainerWeightlimitSetter.Settings.Enums;

// ReSharper disable FieldCanBeMadeReadOnly.Global

namespace ContainerWeightlimitSetter.Settings;

public class WeightGeneratorSettings
{
    public WeightGenerationMode WeightGenerationMode = WeightGenerationMode.MaxWeightMultipliedByFactor;
    public float Factor = 2.2f;
    public float MinWeight = 60f;
}