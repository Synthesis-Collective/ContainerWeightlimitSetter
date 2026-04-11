# ContainerWeightlimitSetter — Synthesis patcher

Set Container weight limits for Skyrim SE via Synthesis. 
Patch generates game records that set the weight field of container to be used by the [Companion SKSE Plugin](https://www.nexusmods.com/skyrimspecialedition/mods/176627) 
And it generates an optional SKSE config where the patcher mostly acts as a visual GUI to make the changes.

Companion SKSE plugin (mandatory requirement): https://www.nexusmods.com/skyrimspecialedition/mods/176627

## What it does
- Assign weights to empty containers (DefaultFallbackWeight).
- Generate weights for prefilled containers using generator (Factor/Mode/MinWeight).
- Skips containers listed in IgnoredContainers or matching QA/Vendor/Merchant heuristics.
- Exports a JSON object that contains the SKSE Plugin configuration (can be toggled).

## Config Options

### ContainerWeightSettings

- DefaultFallbackWeight (float, default 150) → weight applied to empty containers.
- WeightGroups (list) → explicit weight groups: each group {Weight, Containers}.
- UseCustomWeightIfDynamicWeightIsLower (bool, default true) → if dynamic weight < custom, prefer custom.
  - only impacts containers which aren't empty
    - custom can fall back to the default weight.
    - if set to false then the container uses it's generated weight based on its default content.

### WeightGeneratorSettings

- WeightGenerationMode
  - MaxWeightMultipliedByFactor (default)
  - AddMaxWeightMultipliedByFactorToMaxWeight
- Factor (float, default 2.2) → multiplier for generated weight.
- MinWeight (float, default 60) → lower clamp for generated weight.

### IgnoredContainers
- HashSet of container form-links. Containers here skipped by patcher.

### SKSEConfigSettings

- ExportSKSEConfig (bool, default true) → if a SKSE config should be exported
- ExcludedCells (list) → SKSE plugin disabled inside these cells.
- ExcludedContainers (list) → SKSE plugin disabled for these containers.

## Usage notes

- Requires SKSE plugin DLL present under Data\SKSE\Plugins (styyx-container-limit.dll). Checker throws with Nexus link above.
- Install the SKSE mod and add this patcher to your Synthesis Pipeline.
- Profit.


