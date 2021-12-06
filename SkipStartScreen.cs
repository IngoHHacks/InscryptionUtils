using BepInEx;
using BepInEx.Logging;
using System;
using DiskCardGame;
using HarmonyLib;
using System.Reflection;
using UnityEngine;
using System.Collections.Generic;

namespace SkipStartScreen
{

    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    public class Plugin : BaseUnityPlugin
    {
        private const string PluginGuid = "IngoH.inscryption.SkipStartScreen";
        private const string PluginName = "SkipStartScreen";
        private const string PluginVersion = "1.0.0";

        internal static ManualLogSource Log;

        private void Awake()
        {
            Logger.LogInfo($"Loaded {PluginName}!");
            Plugin.Log = base.Logger;

            Harmony harmony = new Harmony(PluginGuid);
            harmony.PatchAll();

        }

        [HarmonyPatch(typeof(StartScreenController), "Start")]
        public class StartupPatch : StartScreenController
        {
            public static bool Prefix(StartScreenController __instance)
            {
                if (!startedGame)
                {
                    Application.runInBackground = GameOptions.Options.runInBackground;
                    AudioController.Instance.StopAllLoops();
                    startedGame = true;
                    AudioController.Instance.FadeBGMMixerParam("BGMLowpassFreq", 5000f, 0f);
                    SaveManager.LoadFromFile();
                    LoadingScreenManager.LoadScene(SaveManager.SaveFile.currentScene);
                    SaveManager.savingDisabled = false;
                    return false;
                }
                else return true;
            }
        }
    }
}
