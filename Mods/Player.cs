using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AetherTemp.Mods
{
    internal class Player
    {
        private static int lastBangTime;
        private static float BPM;

        public static void BackwardHead()
        {
            GorillaTagger.Instance.offlineVRRig.head.trackingRotationOffset.y = 180f;
        }

        public static void GhostMonke()
        {
            if (ControllerInputPoller.instance.rightControllerPrimaryButton || Mouse.current.rightButton.isPressed)
            {
                GorillaTagger.Instance.offlineVRRig.enabled = false;
            }


            else
            {
                GorillaTagger.Instance.offlineVRRig.enabled = true;
            }
        }

        public static void SpinHeadY()
        {
            VRMap head = GorillaTagger.Instance.offlineVRRig.head;
            head.trackingRotationOffset.y = head.trackingRotationOffset.y + 10f;
        }

        public static void SpinHeadZ()
        {
            VRMap head = GorillaTagger.Instance.offlineVRRig.head;
            head.trackingRotationOffset.z = head.trackingRotationOffset.z + 10f;
        }


        public static void SpinHeadX()
        {
            VRMap head = GorillaTagger.Instance.offlineVRRig.head;
            head.trackingRotationOffset.x = head.trackingRotationOffset.x + 10f;
        }



        public static void FixHead()
        {
            GorillaTagger.Instance.offlineVRRig.head.trackingRotationOffset.x = 0f;
            GorillaTagger.Instance.offlineVRRig.head.trackingRotationOffset.y = 0f;
            GorillaTagger.Instance.offlineVRRig.head.trackingRotationOffset.z = 0f;
        }

        public static void SteamLongArms()
        {
            GorillaLocomotion.GTPlayer.Instance.transform.localScale = new UnityEngine.Vector3(1.2f, 1.2f, 1.2f);
        }

        public static void FlipHands()
        {
            Vector3 lh = GorillaTagger.Instance.leftHandTransform.position;
            Vector3 rh = GorillaTagger.Instance.rightHandTransform.position;
            Quaternion lhr = GorillaTagger.Instance.leftHandTransform.rotation;
            Quaternion rhr = GorillaTagger.Instance.rightHandTransform.rotation;

            GorillaLocomotion.GTPlayer.Instance.rightControllerTransform.transform.position = lh;
            GorillaLocomotion.GTPlayer.Instance.leftControllerTransform.transform.position = rh;

            GorillaLocomotion.GTPlayer.Instance.rightControllerTransform.transform.rotation = lhr;
            GorillaLocomotion.GTPlayer.Instance.leftControllerTransform.transform.rotation = rhr;
        }

        public static void BrokenNeck()
        {
            GorillaTagger.Instance.offlineVRRig.head.trackingRotationOffset.z = 90f;
        }

        public static void UpsideDownHead()
        {
            GorillaTagger.Instance.offlineVRRig.head.trackingRotationOffset.z = 180f;
        }

        public static void HeadBang()
        {
            if (Time.time > lastBangTime)
            {
                GorillaTagger.Instance.offlineVRRig.head.trackingRotationOffset.x = 50f;
                lastBangTime = (int)(Time.time + (60f / BPM));
            }
            else
            {
                GorillaTagger.Instance.offlineVRRig.head.trackingRotationOffset.x = Mathf.Lerp(GorillaTagger.Instance.offlineVRRig.head.trackingRotationOffset.x, 0f, 0.1f);
            }
        }


    }
}
