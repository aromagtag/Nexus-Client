using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace AetherTemp.Mods
{
    internal class Advantages
    {

        public static void NoTagOnJoin()
        {
            PlayerPrefs.SetString("tutorial", "nope");
            PlayerPrefs.SetString("didTutorial", "nope");
            Hashtable h = new Hashtable();
            h.Add("didTutorial", false);
            PhotonNetwork.LocalPlayer.SetCustomProperties(null, null);
            PlayerPrefs.Save();
        }

        public static void UntagSelf()
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                Room.Reconnect();
                NoTagOnJoin();
            }
            else
            {
                RemoveInfected(PhotonNetwork.LocalPlayer);
            }
            GorillaLocomotion.GTPlayer.Instance.disableMovement = false;
        }

        private static void RemoveInfected(Photon.Realtime.Player localPlayer)
        {
            throw new NotImplementedException();
        }

        public static void AntiTag()
        {
            if (PhotonNetwork.InRoom)
            {
                if (PlayerIsTagged(GorillaTagger.Instance.offlineVRRig))
                {
                    UntagSelf();
                }
            }
            else
            {
                NoTagOnJoin();
            }
        }

        private static bool PlayerIsTagged(VRRig offlineVRRig)
        {
            throw new NotImplementedException();
        }








    }
}
