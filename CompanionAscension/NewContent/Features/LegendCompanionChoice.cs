﻿using BlueprintCore.Blueprints.Configurators.Classes;
using BlueprintCore.Blueprints.Configurators.Classes.Selection;
using BlueprintCore.Utils;
using HarmonyLib;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Blueprints;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.Blueprints.Classes.Prerequisites;
using System;
using CompanionAscension.Utilities;
using BlueprintCore.Blueprints.Configurators.UnitLogic;
using BlueprintCore.Blueprints.Configurators.UnitLogic.Customization;
using BlueprintCore.Blueprints.Configurators.UnitLogic.Properties;
using BlueprintCore.Blueprints.Configurators.EntitySystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.Utility;
using System.Linq;
using Kingmaker.EntitySystem;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics.Components;
using BlueprintCore.Conditions.Builder;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using CompanionAscension.Utilities.TTTCore;
using System.Text.RegularExpressions;
using CompanionAscension.NewContent.Components;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Abilities;
using static CompanionAscension.NewContent.Components.CustomMechanicsFeatures;

namespace CompanionAscension.NewContent.Features
{
    class LegendCompanionChoice
    {
        public static readonly string LegendCompanionChoiceGUID = "e1aedb0ed75248faaa79d0a2a0fa1dd8";
        private static readonly string LegendCompanionChoiceName = "LegendCompanionChoice";
        private static readonly string LegendCompanionChoiceDisplayName = "Legend Companion Ascension";
        private static readonly string LegendCompanionChoiceDisplayNameKey = "LegendCompanionChoiceName";
        private static readonly string LegendCompanionChoiceDescription = "At 8th mythic rank, Legend's companions can gain further power.";
        private static readonly string LegendCompanionChoiceDescriptionKey = "LegendCompanionChoiceDescription";

        private static readonly string LegendProgression = "905383229aaf79e4b8d7e2d316b68715";

        [HarmonyPatch(typeof(BlueprintsCache), "Init")]
        static class BlueprintsCache_Init_patch
        {
            static bool Initialized;

            [HarmonyPriority(Priority.First)]
            static void Postfix()
            {
                if (Initialized) return;
                Initialized = true;

                //PatchLegendCompanionChoice();
                try { PatchLegendCompanionChoice(); }
                catch (Exception ex) { Tools.LogMessage("EXCEPTION: " + ex.ToString()); }
            }

            public static void PatchLegendCompanionChoice()
            {
                Tools.LogMessage("New Content: Building Legend Companion Choices");

                string _legendAbilityScoreBonusName = "LegendAbilityScoreBonus";
                string _legendAbilityScoreBonusGUID = "bc6e0de28fce416e90c12f688fef95c5";
                string _legendAbilityScoreBonusDisplayName = "Legendary Ability Scores";
                string _legendAbilityScoreBonusDisplayNameKey = "LegendAbilityScoreBonusNameKey";
                string _legendAbilityScoreBonusDescription =
                    "All of your ability scores are increased by 2.";
                string _legendAbilityScoreBonusDescriptionKey = "LegendAbilityScoreBonusDescriptionKey";
                var _legendAbilityScoreBonus = FeatureConfigurator.New(_legendAbilityScoreBonusName, _legendAbilityScoreBonusGUID)
                    .SetDisplayName(LocalizationTool.CreateString(_legendAbilityScoreBonusDisplayNameKey, _legendAbilityScoreBonusDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(_legendAbilityScoreBonusDescriptionKey, _legendAbilityScoreBonusDescription))
                    .AddStatBonus(
                        stat: StatType.Strength,
                        descriptor: ModifierDescriptor.None,
                        value: 2)
                    .AddStatBonus(
                        stat: StatType.Dexterity,
                        descriptor: ModifierDescriptor.None,
                        value: 2)
                    .AddStatBonus(
                        stat: StatType.Constitution,
                        descriptor: ModifierDescriptor.None,
                        value: 2)
                    .AddStatBonus(
                        stat: StatType.Wisdom,
                        descriptor: ModifierDescriptor.None,
                        value: 2)
                    .AddStatBonus(
                        stat: StatType.Intelligence,
                        descriptor: ModifierDescriptor.None,
                        value: 2)
                    .AddStatBonus(
                        stat: StatType.Charisma,
                        descriptor: ModifierDescriptor.None,
                        value: 2)
                    .Configure();

                string _legendLegendaryCompanionName = "LegendLegendaryCompanion";
                string _legendLegendaryCompanionGUID = "8DF46707-8090-464E-8509-4D7E85D81938";
                string _legendLegendaryCompanionDisplayName = "Legendary Companion";
                string _legendLegendaryCompanionDisplayNameKey = "LegendLegendaryCompanionNameKey";
                string _legendLegendaryCompanionDescription =
                    "Your level cap has become 28 (you still can only get 20 levels in one character " +
                    "class), and the amount of XP needed to level up is drastically decreased.";
                string _legendLegendaryCompanionDescriptionKey = "LegendLegendaryCompanionDescriptionKey";
                var _legendLegendaryCompanionFeature = FeatureConfigurator.New(_legendLegendaryCompanionName, _legendLegendaryCompanionGUID)
                    .SetDisplayName(LocalizationTool.CreateString(_legendLegendaryCompanionDisplayNameKey, _legendLegendaryCompanionDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(_legendLegendaryCompanionDescriptionKey, _legendLegendaryCompanionDescription))
                    .Configure();
                _legendLegendaryCompanionFeature.AddComponent<AddCustomMechanicsFeature>(c => { c.Feature = CustomMechanicsFeature.LegendaryCompanion; });

                var _legendCompanionChoice = FeatureSelectionConfigurator.New(LegendCompanionChoiceName, LegendCompanionChoiceGUID)
                    .SetDisplayName(LocalizationTool.CreateString(LegendCompanionChoiceDisplayNameKey, LegendCompanionChoiceDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(LegendCompanionChoiceDescriptionKey, LegendCompanionChoiceDescription))
                    .AddToAllFeatures(new Blueprint<BlueprintFeature, BlueprintFeatureReference>[] { _legendLegendaryCompanionFeature.AssetGuidThreadSafe, _legendAbilityScoreBonus.AssetGuidThreadSafe })
                    //.PrerequisitePlayerHasFeature(LegendProgression)
                    .SetHideInUI(true)
                    .SetHideInCharacterSheetAndLevelUp(true)
                    .SetHideNotAvailibleInUI(true)
                    .Configure();
                Tools.LogMessage("Built: Legend Companion Choices -> " + _legendCompanionChoice.AssetGuidThreadSafe);
            }
        }
    }
}
