﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace PilotAssistant.AppLauncher
{
    [KSPAddon(KSPAddon.Startup.Flight, false)]
    public class AppLauncherInstance : MonoBehaviour
    {
        private static ApplicationLauncherButton btnLauncher;
        private static Rect window = new Rect(Screen.width - 180, 40, 30, 30);

        internal static bool bDisplayOptions = false;
        internal static bool bDisplayAssistant = false;
        internal static bool bDisplaySAS = false;
        internal static bool bDisplayModerator = false;

        void Awake()
        {
            GameEvents.onGUIApplicationLauncherReady.Add(this.OnAppLauncherReady);
        }

        void OnDestroy()
        {
            GameEvents.onGUIApplicationLauncherReady.Remove(this.OnAppLauncherReady);
            if (btnLauncher != null)
                ApplicationLauncher.Instance.RemoveModApplication(btnLauncher);
        }

        private void OnAppLauncherReady()
        {
            btnLauncher = ApplicationLauncher.Instance.AddModApplication(OnToggleTrue, OnToggleFalse,
                                                                        null, null, null, null,
                                                                        ApplicationLauncher.AppScenes.ALWAYS,
                                                                        GameDatabase.Instance.GetTexture("Pilot Assistant/Icons/AppLauncherIcon", false));
        }

        void OnGameSceneChange(GameScenes scene)
        {
            ApplicationLauncher.Instance.RemoveModApplication(btnLauncher);
        }

        private void OnToggleTrue()
        {
            bDisplayOptions = true;
        }

        private void OnToggleFalse()
        {
            bDisplayOptions = false;
        }

        private void OnGUI()
        {
            Utility.GeneralUI.Styles();
            if (bDisplayOptions)
            {
                window = GUILayout.Window(0984653, window, optionsWindow, "", GUILayout.Width(0), GUILayout.Height(0));
            }
        }

        private void optionsWindow(int id)
        {
            bool temp = GUILayout.Toggle(bDisplayAssistant, "Pilot Assistant", Utility.GeneralUI.toggleButton);
            if (temp != bDisplayAssistant)
            {
                bDisplayAssistant = temp;
                btnLauncher.toggleButton.SetFalse();
            }
            temp = GUILayout.Toggle(bDisplaySAS, "SAS Systems", Utility.GeneralUI.toggleButton);
            if (temp != bDisplaySAS)
            {
                bDisplaySAS = temp;
                btnLauncher.toggleButton.SetFalse();
            }
        }
    }
}
