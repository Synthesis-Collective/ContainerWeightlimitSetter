using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Plugins.Cache;
using Mutagen.Bethesda.Skyrim;

namespace ContainerWeightlimitSetter.Utility;

public class ContainerEntryProcessor(ILinkCache linkCache)
{
    private Dictionary<FormKey, float> ContainerItemMaxWeightCache = new ();
    private FormKey entryPointFormKey;

    private float GetItemWeight(FormKey itemFormKey)
    {
        if (ContainerItemMaxWeightCache.TryGetValue(itemFormKey, out var maxEntryWeight)) return maxEntryWeight;
        
        if (linkCache.TryResolve<ILeveledItemGetter>(itemFormKey, out var leveledItemGetter))
        {
            if (leveledItemGetter.Entries == null) return 0f;

            return leveledItemGetter.Entries.Max(leveledItemEntry =>
            {
                var leveledListMaxWeight = leveledItemEntry.Data == null ? 0f :  GetItemWeight(leveledItemEntry.Data.Reference.FormKey);
                if (leveledItemEntry.Data != null) ContainerItemMaxWeightCache[leveledItemEntry.Data.Reference.FormKey] = leveledListMaxWeight;
                return leveledListMaxWeight;
            });
        }
        
        if (linkCache.TryResolve<AlchemicalApparatus>(itemFormKey, out var alchemicalApparatus))
        {
            return alchemicalApparatus.Weight;
        }

        if (linkCache.TryResolve<IAmmunitionGetter>(itemFormKey, out var ammunitionGetter))
        {
            ContainerItemMaxWeightCache[itemFormKey] = ammunitionGetter.Weight;
            return ammunitionGetter.Weight;
        }

        if (linkCache.TryResolve<IArmorGetter>(itemFormKey, out var armorGetter))
        {
            ContainerItemMaxWeightCache[itemFormKey] = armorGetter.Weight;
            return armorGetter.Weight;
        }

        if (linkCache.TryResolve<IBookGetter>(itemFormKey, out var bookGetter))
        {
            ContainerItemMaxWeightCache[itemFormKey] = bookGetter.Weight;
            return bookGetter.Weight;
        }

        if (linkCache.TryResolve<IContainerGetter>(itemFormKey, out var containerGetter))
        {
            ContainerItemMaxWeightCache[itemFormKey] = containerGetter.Weight;
            return containerGetter.Weight;
        }

        if (linkCache.TryResolve<IIngestibleGetter>(itemFormKey, out var ingestibleGetter))
        {
            ContainerItemMaxWeightCache[itemFormKey] = ingestibleGetter.Weight;
            return ingestibleGetter.Weight;
        }

        if (linkCache.TryResolve<IIngredientGetter>(itemFormKey, out var ingredientGetter))
        {
            ContainerItemMaxWeightCache[itemFormKey] = ingredientGetter.Weight;
            return ingredientGetter.Weight;
        }

        if (linkCache.TryResolve<IKeyGetter>(itemFormKey, out var keyGetter))
        {
            ContainerItemMaxWeightCache[itemFormKey] = keyGetter.Weight;
            return keyGetter.Weight;
        }

        if (linkCache.TryResolve<ILightGetter>(itemFormKey, out var lightGetter))
        {
            ContainerItemMaxWeightCache[itemFormKey] = lightGetter.Weight;
            return lightGetter.Weight;
        }

        if (linkCache.TryResolve<IMiscItemGetter>(itemFormKey, out var miscItemGetter))
        {
            ContainerItemMaxWeightCache[itemFormKey] = miscItemGetter.Weight;
            return miscItemGetter.Weight;
        }

        if (linkCache.TryResolve<IScrollGetter>(itemFormKey, out var scrollGetter))
        {
            ContainerItemMaxWeightCache[itemFormKey] = scrollGetter.Weight;
            return scrollGetter.Weight;
        }

        if (linkCache.TryResolve<ISoulGemGetter>(itemFormKey, out var soulGemGetter))
        {
            ContainerItemMaxWeightCache[itemFormKey] = soulGemGetter.Weight;
            return soulGemGetter.Weight;
        }

        if (!linkCache.TryResolve<IWeaponGetter>(itemFormKey, out var weaponGetter)) return 0f;
        if (weaponGetter.BasicStats == null) return 0f;
        ContainerItemMaxWeightCache[itemFormKey] = weaponGetter.BasicStats.Weight;
        
        return 0f;
        
    }

    public float GetMaxEntryWeight(IContainerEntryGetter containerEntry)
    {
        entryPointFormKey = containerEntry.Item.Item.FormKey;
        var itemWeight = GetItemWeight(entryPointFormKey);
        var itemCount = containerEntry.Item.Count;
        
        var maxEntryWeight = itemWeight * itemCount;

        return maxEntryWeight;
    }
    
}