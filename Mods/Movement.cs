using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace AetherTemp.Mods
{
    internal class Movement
    {

        public static void Noclip()
        {
            bool enableColliders = ControllerInputPoller.instance.rightControllerIndexFloat <= 0.1f;

            foreach (MeshCollider meshCollider in Resources.FindObjectsOfTypeAll<MeshCollider>())
            {
                meshCollider.enabled = enableColliders;
            }
        }

        public static void PlatformMod()

        {
            if (ControllerInputPoller.instance.leftGrab && leftplat == null)
            {
                leftplat = CreatePlatformOnHand(GorillaTagger.Instance.leftHandTransform);
            }

            if (ControllerInputPoller.instance.rightGrab && rightplat == null)
            {
                rightplat = CreatePlatformOnHand(GorillaTagger.Instance.rightHandTransform);
            }

            if (ControllerInputPoller.instance.rightGrabRelease && rightplat != null)
            {
                rightplat.Disable();
                rightplat = null;

            }

            if (ControllerInputPoller.instance.leftGrabRelease && leftplat != null)
            {
                leftplat.Disable();
                leftplat = null;
            }
        }
        private static GameObject leftplat = null;
        private static GameObject rightplat = null;
        private static bool rightPrimary;

        private static GameObject CreatePlatformOnHand(Transform handTransform)
        {
            GameObject plat = GameObject.CreatePrimitive(PrimitiveType.Cube);
            plat.transform.localScale = new Vector3(0.025f, 0.3f, 0.4f);

            plat.transform.position = handTransform.position;
            plat.transform.rotation = handTransform.rotation;

            float h = (Time.frameCount / 180f) % 1f;
            plat.GetComponent<Renderer>().material.color = Color.blue;
            return plat;
        } //working platforms code

        public static void SlowSpeedboost()
        {
            GorillaLocomotion.GTPlayer.Instance.maxJumpSpeed = 2.8f;
            GorillaLocomotion.GTPlayer.Instance.jumpMultiplier = 6.4f;
        }

        public static void NormalSpeedboost()
        {
            GorillaLocomotion.GTPlayer.Instance.maxJumpSpeed = 7f;
            GorillaLocomotion.GTPlayer.Instance.jumpMultiplier = 7f;
        }
        public static void MosaSpeedboost()
        {
            GorillaLocomotion.GTPlayer.Instance.maxJumpSpeed = 11.0f;
            GorillaLocomotion.GTPlayer.Instance.jumpMultiplier = 5.4f;
        }

        public static void FastSpeedboost()
        {
            GorillaLocomotion.GTPlayer.Instance.maxJumpSpeed = 15.7f;
            GorillaLocomotion.GTPlayer.Instance.jumpMultiplier = 7.8f;
        }

        public static void SuperSpeedboost()
        {
            GorillaLocomotion.GTPlayer.Instance.maxJumpSpeed = 20.0f;
            GorillaLocomotion.GTPlayer.Instance.jumpMultiplier = 10.0f;
        }

        public static void MegaSpeedboost()
        {
            GorillaLocomotion.GTPlayer.Instance.maxJumpSpeed = 30.0f;
            GorillaLocomotion.GTPlayer.Instance.jumpMultiplier = 15.0f;
        }

        public static void UltraSpeedboost()
        {
            GorillaLocomotion.GTPlayer.Instance.maxJumpSpeed = 40.0f;
            GorillaLocomotion.GTPlayer.Instance.jumpMultiplier = 20.0f;
        }

        public static void HyperSpeedboost()
        {
            GorillaLocomotion.GTPlayer.Instance.maxJumpSpeed = 50.0f;
            GorillaLocomotion.GTPlayer.Instance.jumpMultiplier = 25.0f;
        }

        public static void GodSpeedboost()
        {
            GorillaLocomotion.GTPlayer.Instance.maxJumpSpeed = 100.0f;
            GorillaLocomotion.GTPlayer.Instance.jumpMultiplier = 50.0f;
        }

        public static void SpeedOfLightSpeedboost()
        {
            GorillaLocomotion.GTPlayer.Instance.maxJumpSpeed = 1000.0f;
            GorillaLocomotion.GTPlayer.Instance.jumpMultiplier = 500.0f;
        }

        public static void Fly()
        {
            if (ControllerInputPoller.instance.rightControllerPrimaryButton)
            {
                GorillaLocomotion.GTPlayer.Instance.transform.position += (GorillaLocomotion.GTPlayer.Instance.headCollider.transform.forward * Time.deltaTime) * 25;
                GorillaLocomotion.GTPlayer.Instance.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }

        public static void UpAndDown()
        {
            if (ControllerInputPoller.instance.leftGrab)
            {
                GorillaLocomotion.GTPlayer.Instance.transform.position += GorillaLocomotion.GTPlayer.Instance.headCollider.transform.up * Time.deltaTime * 3.5f;
                GorillaLocomotion.GTPlayer.Instance.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
            if (ControllerInputPoller.instance.rightGrab)
            {
                GorillaLocomotion.GTPlayer.Instance.transform.position += GorillaLocomotion.GTPlayer.Instance.headCollider.transform.up * Time.deltaTime * -3.5f;
                GorillaLocomotion.GTPlayer.Instance.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }



    }
}

