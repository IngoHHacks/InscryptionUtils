using BepInEx;
using BepInEx.Logging;
using DiskCardGame;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using static DiskCardGame.TextDisplayer;

namespace DialogRemover
{
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    public class Plugin : BaseUnityPlugin
    {
        private const string PluginGuid = "IngoH.inscryption.DialogRemover";
        private const string PluginName = "DialogRemover";
        private const string PluginVersion = "1.0.0";

        internal static ManualLogSource Log;

        private void Awake()
        {
            Logger.LogInfo($"Loaded {PluginName}!");
            Plugin.Log = base.Logger;

            Harmony harmony = new Harmony(PluginGuid);
            harmony.PatchAll();

        }

        [HarmonyPatch(typeof(TalkingCardDialogueHandler), "DialogueSequence", new Type[] { typeof(string), typeof(List<TalkingCard>) })]
        public class MultiTalkingCardPatch : TalkingCardDialogueHandler
        {
            public static bool Prefix(ref TalkingCardDialogueHandler __instance)
            {
                return false;
            }
        }

        [HarmonyPatch(typeof(TextDisplayer), "ShowUntilInput", new Type[] { typeof(string), typeof(float), typeof(float), typeof(Emotion), typeof(LetterAnimation), typeof(DialogueEvent.Speaker), typeof(string[]), typeof(bool) })]
        public class TextDisplayerPatch : TextDisplayer
        {
            public static bool Prefix(ref TextDisplayer __instance)
            {
                return false;
            }
        }
    }
}
