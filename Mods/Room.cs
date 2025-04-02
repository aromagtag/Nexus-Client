using GorillaNetworking;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace AetherTemp.Mods
{
    internal class Room
    {
        private static string lastRoom;
        private static string RejoinRoom;

        public static void CreateRoom(string roomName, bool isPublic)
        {
            UnityEngine.Debug.Log((string)typeof(PhotonNetworkController).GetField("platformTag", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(PhotonNetworkController.Instance));
            RoomConfig roomConfig = new RoomConfig()
            {
                createIfMissing = true,
                isJoinable = true,
                isPublic = isPublic,
                MaxPlayers = PhotonNetworkController.Instance.GetRoomSize(PhotonNetworkController.Instance.currentJoinTrigger.networkZone),
                CustomProps = new ExitGames.Client.Photon.Hashtable()
                {
                    { "gameMode", PhotonNetworkController.Instance.currentJoinTrigger.GetFullDesiredGameModeString() },
                    { "platform", (string)typeof(PhotonNetworkController).GetField("platformTag", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(PhotonNetworkController.Instance) },
                    { "queueName", GorillaComputer.instance.currentQueue }
                }
            };
            NetworkSystem.Instance.ConnectToRoom(roomName, roomConfig);
        }


        public static void DisconnectOnGrip()
        {
            if (ControllerInputPoller.instance.rightGrab)
            {
                PhotonNetwork.Disconnect();
            }
        }

        public static void JoinRandomPublic()
        {
            if (PhotonNetwork.InRoom)
            {
                UnityEngine.Debug.LogWarning("<color=blue>Photon</color> : Already connected to a room.");
                NotificationLib.SendNotification("<color=blue>Photon</color> : Already connected to a room.");
                return;
            }

            string currentMap = DetectCurrentMap();
            if (currentMap == null)
            {
                UnityEngine.Debug.LogError("<color=blue>Photon</color> : Unable to detect the current map.");
                NotificationLib.SendNotification("<color=blue>Photon</color> : Unable to detect the current map.");
                return;
            }

            string path = GetPathForGameMode(currentMap);
            if (path == null)
            {
                UnityEngine.Debug.LogError($"<color=blue>Photon</color> : No valid path found for map: {currentMap}.");
                NotificationLib.SendNotification($"<color=blue>Photon</color> : No valid path found for map: {currentMap}.");
                return;
            }

            GorillaNetworkJoinTrigger joinTrigger = GameObject.Find(path)?.GetComponent<GorillaNetworkJoinTrigger>();
            if (joinTrigger == null)
            {
                UnityEngine.Debug.LogError($"<color=blue>Photon</color> : Join trigger not found for path: {path}.");
                NotificationLib.SendNotification($"<color=blue>Photon</color> : Join trigger not found for path: {path}.");
                return;
            }

            PhotonNetworkController.Instance.AttemptToJoinPublicRoom(joinTrigger, JoinType.Solo);
        }

        private static string GetPathForGameMode(string currentMap)
        {
            throw new NotImplementedException();
        }

        private static string DetectCurrentMap()
        {
            throw new NotImplementedException();
        }


        public static void Reconnect()
        {
            RejoinRoom = PhotonNetwork.CurrentRoom.Name;
            PhotonNetwork.Disconnect();
        }
        public static void DisableNetworkTriggers()
        {
            GameObject.Find("Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab").SetActive(false);
        }

        public static void EnableNetworkTriggers()
        {
            GameObject.Find("Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab").SetActive(true);
        }


        public static void CreatePublic()
        {
            CreateRoom(RandomRoomName(), true);
        }

        private static string RandomRoomName()
        {
            throw new NotImplementedException();
        }

        public static void JoinLastRoom()
        {
            PhotonNetworkController.Instance.AttemptToJoinSpecificRoom(lastRoom, JoinType.Solo);
        }

        internal static void CreateRoom()
        {
            throw new NotImplementedException();
        }

        internal class NotificationLib
        {
            internal static void SendNotification(string v)
            {
                throw new NotImplementedException();
            }



        }
    }
}
