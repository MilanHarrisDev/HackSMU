using Photon.Pun;
using Photon.Realtime;
using Photon.Chat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PhotonLobby : MonoBehaviourPunCallbacks {

    public static PhotonLobby lobby;

    public bool joinOnMasterServerConnect = false;

    private GameObject myPlayer;

    public void OnjoinButtonClick()
    {
        PhotonNetwork.JoinRoom("interview");
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("Connected to Photon master servers.");
        //joinButton.SetActive(true);
        if(joinOnMasterServerConnect)
            OnjoinButtonClick();
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        myPlayer = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonNetworkUser"), new Vector3(Random.Range(-3f,3f), 0, 0), Quaternion.identity);
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
	//void Update () {
    //    if(ApplicationManager.manager.device == Device.MOUSE_DEBUG)
    //    {
    //        if (Input.GetKeyDown(KeyCode.Space))
    //            OnjoinButtonClick();
    //    }
	//}
}
