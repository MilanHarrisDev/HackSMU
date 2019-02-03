using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PhotonPlayer : MonoBehaviour {

    private PhotonView PV;
    public Transform head;

	// Use this for initialization
	void Start () {
        PV = GetComponent<PhotonView>();
        if (PV.IsMine == true)
        {
            head.rotation = Camera.main.transform.rotation;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
