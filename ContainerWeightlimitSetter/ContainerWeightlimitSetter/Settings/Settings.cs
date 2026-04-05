using Mutagen.Bethesda.Plugins;
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
        [SynthesisDescription("Settings for Handling Container Weights that can't be dynamically determined.")]
        [SynthesisTooltip("Settings for Handling Container Weights that can't be dynamically determined.")]
        public ContainerWeightSettings ContainerWeightSettings = new();
        
        [MaintainOrder]
        [SynthesisDescription("Settings for Generating Weights for prefilled Containers.")]
        [SynthesisTooltip("Settings for Generating Weights for prefilled Containers.")]
        public WeightGeneratorSettings WeightGeneratorSettings = new();

        [MaintainOrder]
        [SynthesisDescription("Containers that will be ignored by the patcher.")]
        [SynthesisTooltip("Containers that will be ignored by the patcher.")]
        public HashSet<IFormLinkGetter<IContainerGetter>> IgnoredContainers = new()
        {
            /*
            Skyrim.Container.EvidenceChestPlayerInventory,
            HearthFires.Container.BYOHHouseVendorChestSmall,
            HearthFires.Container.BYOHHouseVendorChest,
            HearthFires.Container.BYOHBYOHApiary,
            HearthFires.Container.BYOHHouseBarrelFish01_NoRespawn,
            HearthFires.Container.BYOHHouseUpperEndTable01,
            HearthFires.Container.BYOHHouseStrongBox,
            HearthFires.Container.BYOHHouseNobleChestDrawers01,
            HearthFires.Container.BYOHHouseNobleCupboard02,
            HearthFires.Container.BYOHHouseNobleWardrobe01,
            HearthFires.Container.BYOHHouseUpperEndTable02,
            HearthFires.Container.BYOHHouseNobleCupboard01,
            HearthFires.Container.BYOHHouseNobleNightTable01,
            HearthFires.Container.BYOHHouseUpperWardrobe01,
            HearthFires.Container.BYOHHouseUpperCupboard01,
            HearthFires.Container.BYOHHouseKnapsack,
            HearthFires.Container.BYOHHouseNobleChestDrawers02,
            HearthFires.Container.BYOHHouseSatchel,
            HearthFires.Container.BYOHHouseUpperDresser01,
            HearthFires.Container.BYOHHouseNobleChest01,
            HearthFires.Container.BYOHHouseUpperChest,
            HearthFires.Container.BYOHPlanterContainer,
            HearthFires.Container.BYOHHouseSafewithLockPlayer,
            HearthFires.Container.TreasSatchelEMPTY,
            HearthFires.Container.BYOHUrchin_SofieChest,
            HearthFires.Container.BYOHHouseCraftingChest
            */
        };

    }

}