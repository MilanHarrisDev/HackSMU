using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PhotonPlayer : MonoBehaviour {

    private PhotonView PV;

	// Use this for initialization
	void Start () {
        PV = GetComponent<PhotonView>();
        if (PV.IsMine == true)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
