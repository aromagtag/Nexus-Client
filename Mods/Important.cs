using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using BepInEx;
using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AetherTemp.Mods
{
    class Important
    {
        public static void Disconnect()
        {
            PhotonNetwork.Disconnect();
        }
        public static void JoinDiscord()
        {
            Process.Start("https://discord.gg/QMdN9d8AXp");
        }

        public static void DisableTOS()
        {
            GorillaMetaReport component = GameObject.Find("Miscellaneous Scripts").transform.Find("MetaReporting").GetComponent<GorillaMetaReport>();
            component.gameObject.SetActive(false);

            component.enabled = false;


            GameObject.Find("PrivateUIRoom").gameObject.SetActive(false);

        }

        public static void DisableQuitBox()
        {
            GameObject.Find("Environment Objects/TriggerZones_Prefab/ZoneTransitions_Prefab/QuitBox").SetActive(false);
        }

        public static void EnableQuitBox()
        {
            GameObject.Find("Environment Objects/TriggerZones_Prefab/ZoneTransitions_Prefab/QuitBox").SetActive(true);
        }

        public static void RestartGame()
        {
            Process.Start("steam://rungameid/1533390");
            Application.Quit();
        }

        public static void QuitGame()
        {
            Application.Quit();
        }



        private static bool wasenabled = true;




    }
}
