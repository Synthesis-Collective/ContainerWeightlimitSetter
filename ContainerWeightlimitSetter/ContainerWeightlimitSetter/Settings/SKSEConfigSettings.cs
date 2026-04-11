using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.FormKeys.SkyrimSE;
using Mutagen.Bethesda.Synthesis.Settings;
using Mutagen.Bethesda.WPF.Reflection.Attributes;

namespace ContainerWeightlimitSetter.Settings;

public class SKSEConfigSettings
{
    [MaintainOrder]
    public bool ExportSKSEConfig = true;
    
    [MaintainOrder]
    [SynthesisDescription("Cells in which the SKSE plugin should be disabled.")]
    [SynthesisTooltip("Cells in which the SKSE plugin should be disabled.")]
    public HashSet<IFormLinkGetter<ICellGetter>> ExcludedCells =
    [
        Skyrim.Cell.QASmoke
    ];
    [MaintainOrder]
    [SynthesisDescription("Containers for which the SKSE plugin should be disabled.")]
    [SynthesisTooltip("Containers for which the SKSE plugin should be disabled.")]
    public HashSet<IFormLinkGetter<IContainerGetter>> ExcludedContainers = [];
    /*
    [MaintainOrder]
    [SynthesisDescription("PlacedObjects for which the SKSE plugin should be disabled.")]
    [SynthesisTooltip("PlacedObjects for which the SKSE plugin should be disabled.")]
    public HashSet<IFormLinkGetter<IPlacedObjectGetter>> ExcludedPlacedObjects = [];
    */
}