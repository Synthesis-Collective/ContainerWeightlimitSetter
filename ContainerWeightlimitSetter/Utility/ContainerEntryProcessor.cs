using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Plugins.Cache;
using Mutagen.Bethesda.Skyrim;

namespace ContainerWeightlimitSetter.Utility;

public class ContainerEntryProcessor(ILinkCache linkCache)
{
    private readonly Dictionary<FormKey, float> _containerItemMaxWeightCache = new ();
    private FormKey _entryPointFormKey;

    private float GetItemWeight(FormKey itemFormKey)
    {
        if (_containerItemMaxWeightCache.TryGetValue(itemFormKey, out var maxEntryWeight)) return maxEntryWeight;
        
        if (linkCache.TryResolve<ILeveledItemGetter>(itemFormKey, out var leveledItemGetter))
        {
            if (leveledItemGetter.Entries == null) return 0f;

            return leveledItemGetter.Entries.Max(leveledItemEntry =>
            {
                var leveledListMaxWeight = leveledItemEntry.Data == null ? 0f :  GetItemWeight(leveledItemEntry.Data.Reference.FormKey);
                if (leveledItemEntry.Data != null) _containerItemMaxWeightCache[leveledItemEntry.Data.Reference.FormKey] = leveledListMaxWeight;
                return leveledListMaxWeight;
            });
        }
        
        if (linkCache.TryResolve<AlchemicalApparatus>(itemFormKey, out var alchemicalApparatus))
        {
            return alchemicalApparatus.Weight;
        }

        if (linkCache.TryResolve<IAmmunitionGetter>(itemFormKey, out var ammunitionGetter))
        {
            _containerItemMaxWeightCache[itemFormKey] = ammunitionGetter.Weight;
            return ammunitionGetter.Weight;
        }

        if (linkCache.TryResolve<IArmorGetter>(itemFormKey, out var armorGetter))
        {
            _containerItemMaxWeightCache[itemFormKey] = armorGetter.Weight;
            return armorGetter.Weight;
        }

        if (linkCache.TryResolve<IBookGetter>(itemFormKey, out var bookGetter))
        {
            _containerItemMaxWeightCache[itemFormKey] = bookGetter.Weight;
            return bookGetter.Weight;
        }

        if (linkCache.TryResolve<IContainerGetter>(itemFormKey, out var containerGetter))
        {
            _containerItemMaxWeightCache[itemFormKey] = containerGetter.Weight;
            return containerGetter.Weight;
        }

        if (linkCache.TryResolve<IIngestibleGetter>(itemFormKey, out var ingestibleGetter))
        {
            _containerItemMaxWeightCache[itemFormKey] = ingestibleGetter.Weight;
            return ingestibleGetter.Weight;
        }

        if (linkCache.TryResolve<IIngredientGetter>(itemFormKey, out var ingredientGetter))
        {
            _containerItemMaxWeightCache[itemFormKey] = ingredientGetter.Weight;
            return ingredientGetter.Weight;
        }

        if (linkCache.TryResolve<IKeyGetter>(itemFormKey, out var keyGetter))
        {
            _containerItemMaxWeightCache[itemFormKey] = keyGetter.Weight;
            return keyGetter.Weight;
        }

        if (linkCache.TryResolve<ILightGetter>(itemFormKey, out var lightGetter))
        {
            _containerItemMaxWeightCache[itemFormKey] = lightGetter.Weight;
            return lightGetter.Weight;
        }

        if (linkCache.TryResolve<IMiscItemGetter>(itemFormKey, out var miscItemGetter))
        {
            _containerItemMaxWeightCache[itemFormKey] = miscItemGetter.Weight;
            return miscItemGetter.Weight;
        }

        if (linkCache.TryResolve<IScrollGetter>(itemFormKey, out var scrollGetter))
        {
            _containerItemMaxWeightCache[itemFormKey] = scrollGetter.Weight;
            return scrollGetter.Weight;
        }

        if (linkCache.TryResolve<ISoulGemGetter>(itemFormKey, out var soulGemGetter))
        {
            _containerItemMaxWeightCache[itemFormKey] = soulGemGetter.Weight;
            return soulGemGetter.Weight;
        }

        if (!linkCache.TryResolve<IWeaponGetter>(itemFormKey, out var weaponGetter)) return 0f;
        if (weaponGetter.BasicStats == null) return 0f;
        _containerItemMaxWeightCache[itemFormKey] = weaponGetter.BasicStats.Weight;
        
        return 0f;
        
    }

    public float GetMaxEntryWeight(IContainerEntryGetter containerEntry)
    {
        _entryPointFormKey = containerEntry.Item.Item.FormKey;
        var itemWeight = GetItemWeight(_entryPointFormKey);
        var itemCount = containerEntry.Item.Count;
        
        var maxEntryWeight = itemWeight * itemCount;

        return maxEntryWeight;
    }
    
}