using Photon.Pun;
using Photon.Realtime;
using Photon.Chat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonLobby : MonoBehaviourPunCallbacks {

    public static PhotonLobby lobby;

    public GameObject joinButton;

    public void OnjoinButtonClick()
    {
        joinButton.SetActive(false);
        PhotonNetwork.JoinRoom("interview");
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("Connected to Photon master servers.");
        joinButton.SetActive(true);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("Joined room.");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        Debug.Log("Join room failed.");
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 5 };
        PhotonNetwork.CreateRoom("interview", roomOps);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        Debug.Log("Room already exists.");
    }

    // Use this for initialization
    void Start () {
        PhotonNetwork.ConnectUsingSettings();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
