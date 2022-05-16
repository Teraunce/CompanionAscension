﻿using BlueprintCore.Blueprints.Configurators.Classes;
using BlueprintCore.Blueprints.Configurators.Classes.Selection;
using BlueprintCore.Blueprints.Configurators.Abilities;
using BlueprintCore.Blueprints.Components;
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

namespace CompanionAscension.NewContent.Features
{
    class TricksterCompanionChoice
    {
        public static readonly string Guid = "095edab4d08f4b7dab6eb7450e93cfca";
        private static readonly BlueprintFeatureSelection TricksterRank1Selection = ResourcesLibrary.TryGetBlueprint<BlueprintFeatureSelection>("4fbc563529717de4d92052048143e0f1");
        private static readonly string TricksterCompanionChoiceName = "TricksterCompanionChoice";
        private static readonly string TricksterCompanionChoiceDisplayName = "Second Companion Ascension";
        private static readonly string TricksterCompanionChoiceDisplayNameKey = "TricksterCompanionChoiceName";
        private static readonly string TricksterCompanionChoiceDescription = "";
        private static readonly string TricksterCompanionChoiceDescriptionKey = "TricksterCompanionChoiceDescription";

        private static readonly string TricksterProgression = "cc64789b0cc5df14b90da1ffee7bbeea";

        [HarmonyPatch(typeof(BlueprintsCache), "Init")]
        static class BlueprintsCache_Init_patch
        {
            static bool Initialized;

            [HarmonyPriority(Priority.First)]
            static void Postfix()
            {
                if (Initialized) return;
                Initialized = true;

                //PatchTricksterCompanionChoice();
                //try { PatchTricksterCompanionChoice(); }
                //catch (Exception ex) { Tools.LogMessage("EXCEPTION: " + ex.ToString()); }
            }

            public static void PatchTricksterCompanionChoice()
            {
                Tools.LogMessage("New Content: Building Trickster Companion Choices");

                var _tricksterCompanionChoice = FeatureSelectionConfigurator.New(TricksterCompanionChoiceName, Guid)
                    .SetDisplayName(LocalizationTool.CreateString(TricksterCompanionChoiceDisplayNameKey, TricksterCompanionChoiceDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(TricksterCompanionChoiceDescriptionKey, TricksterCompanionChoiceDescription))
                    //.PrerequisitePlayerHasFeature(TricksterProgression)
                    //.SetHideInUi(true)
                    .Configure();
                _tricksterCompanionChoice.m_AllFeatures = TricksterRank1Selection.m_AllFeatures;
                Tools.LogMessage("Built: Trickster Companion Choices -> " + _tricksterCompanionChoice.AssetGuidThreadSafe);
            }
        }
    }
}