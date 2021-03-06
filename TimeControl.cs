using BepInEx;
using BepInEx.Logging;
using DiskCardGame;
using HarmonyLib;
using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace TimeControl
{
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    public class Plugin : BaseUnityPlugin
    {
        private const string PluginGuid = "IngoH.inscryption.TimeControl";
        private const string PluginName = "TimeControl";
        private const string PluginVersion = "1.0.0";

        internal static ManualLogSource Log;

        public static float time;
        public static long lastTimeChange;

        private void Awake()
        {
            Logger.LogInfo($"Loaded {PluginName}!");
            Plugin.Log = base.Logger;

            time = 1.0f;
        }

        private void Update()
        {
            Time.timeScale = time;
            if (Input.GetKey("="))
            {
                time = (float) Math.Min(100, Time.timeScale * 1.01);
                lastTimeChange = DateTime.Now.Ticks;
            }
            if (Input.GetKey("-"))
            {
                time = (float) Math.Max(0.01, Time.timeScale / 1.01);
                lastTimeChange = DateTime.Now.Ticks;
            }
            if (Input.GetKey("0"))
            {
                time = 1;
                lastTimeChange = DateTime.Now.Ticks;
            }
        }
    }
}
