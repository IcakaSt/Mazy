using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine;

public class JoinNewRoom : MonoBehaviourPunCallbacks
{
    public InputField roomCode;
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom("aa");
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Online Test");
    }
}
