using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using StupidTemplate.Menu;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace AetherTemp.Mods
{
    internal class Visuals
    {
        public static void Tracer()
        {
            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                if (vrrig != GorillaTagger.Instance.offlineVRRig)
                {
                    GameObject line = new GameObject("Line");
                    LineRenderer liner = line.AddComponent<LineRenderer>();
                    UnityEngine.Color thecolor = vrrig.playerColor;
                    liner.startColor = thecolor; liner.endColor = thecolor; liner.startWidth = 0.025f; liner.endWidth = 0.025f; liner.positionCount = 2; liner.useWorldSpace = true;
                    liner.SetPosition(0, GorillaTagger.Instance.rightHandTransform.position);
                    liner.SetPosition(1, vrrig.transform.position);
                    liner.material.shader = Shader.Find("GUI/Text Shader");
                    UnityEngine.Object.Destroy(line, Time.deltaTime);
                }
            }
        }

        public static void SmokeBombSpammer()
        {
            Vector3 position = GorillaTagger.Instance.rightHandTransform.transform.position;
            Quaternion identity = Quaternion.identity;
            Vector3 forward = GorillaTagger.Instance.rightHandTransform.forward;
            GorillaTagger.Instance.offlineVRRig.enabled = false;
            GorillaTagger.Instance.offlineVRRig.transform.position = new Vector3(-51.4897f, 16.9286f, -120.1083f);
            RaiseEventOptions raiseEventOptions = new RaiseEventOptions
            {
                Receivers = (ReceiverGroup)1
            };
            TransferrableObject.PositionState positionState = 0;
            object[] array = new object[]
            {
        -201078895,
        positionState,
        position,
        identity,
        forward
            };
            PhotonNetwork.RaiseEvent(176, array, raiseEventOptions, SendOptions.SendReliable);
        }

        public static void J3VUNameCycle()
        {
            bool flag = Time.time > Main.nameCycleDelay;
            if (flag)
            {
                Main.nameCycleIndex++;
                bool flag2 = Main.nameCycleIndex > 4;
                if (flag2)
                {
                    Main.nameCycleIndex = 1;
                }
                bool flag3 = Main.nameCycleIndex == 1;
                if (flag3)
                {
                    Main.ChangeName("J3VU");
                }
                bool flag4 = Main.nameCycleIndex == 2;
                if (flag4)
                {
                    Main.ChangeName("HAS");
                }
                bool flag5 = Main.nameCycleIndex == 3;
                if (flag5)
                {
                    Main.ChangeName("BECOME");
                }
                bool flag6 = Main.nameCycleIndex == 4;
                if (flag6)
                {
                    Main.ChangeName("HOSTILE");
                }
                Main.nameCycleDelay = Time.time + 1f;
            }
        }
        public static void PBBVNameCycle()
        {
            bool flag = Time.time > Main.nameCycleDelay;
            if (flag)
            {
                Main.nameCycleIndex++;
                bool flag2 = Main.nameCycleIndex > 3;
                if (flag2)
                {
                    Main.nameCycleIndex = 1;
                }
                bool flag3 = Main.nameCycleIndex == 1;
                if (flag3)
                {
                    Main.ChangeName("PBBV");
                }
                bool flag4 = Main.nameCycleIndex == 2;
                if (flag4)
                {
                    Main.ChangeName("IS");
                }
                bool flag5 = Main.nameCycleIndex == 3;
                if (flag5)
                {
                    Main.ChangeName("HERE");
                }
                Main.nameCycleDelay = Time.time + 1f;
            }
        }

    }
}
