using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;

[System.Serializable]
public enum Device
{
    MOUSE_DEBUG,
    DAYDREAM,
    OCULUS_GO
}

public class ApplicationManager : MonoBehaviourPun {

    public Microphone mic;

    public static ApplicationManager manager;

    public Device device;

    private UnityEvent currentButtonEvent;
    private Transform raycastTransform;

    private GameObject networkedPlayer;

    public Ray OvrRay { get; private set; }
    public RaycastHit OvrHit;

    public LayerMask ovrLayerMask;

    private void Awake()
    {
        mic = new Microphone();

        if (manager == null)
            manager = this;
        else if(manager != this)
            Destroy(this.gameObject);
    }

    private void Start()
    {
        if (device == Device.OCULUS_GO)
            raycastTransform = GameObject.FindWithTag("OvrRaycastPoint").transform;
    }

    private void GetNetworkedPlayer(){
        GameObject[] players = GameObject.FindGameObjectsWithTag("NetworkPlayer");
        foreach (GameObject player in players)
        {
            if (player.GetComponent<PhotonView>().IsMine)
                networkedPlayer = player;
        }
    }

    private void Update()
    {
        MoveNetworkedPlayer();

        if (device == Device.OCULUS_GO)
        {
            if (OVRInput.GetDown(OVRInput.Button.PrimaryTouchpad))
            {
                if (OvrHit.transform.gameObject.layer == LayerMask.NameToLayer("TeleportSurface"))
                {
                    Transform cameraRig = GameObject.FindWithTag("Player").transform;
                    cameraRig.position = new Vector3(OvrHit.point.x, 0, OvrHit.point.z);
                    MoveNetworkedPlayer();
                }
            }

            OvrRay = new Ray(raycastTransform.position, raycastTransform.forward);
            if (Physics.Raycast(OvrRay, out OvrHit, 1000f, ovrLayerMask))
            {
                try{
                    OVR_UIButton button = OvrHit.transform.gameObject.GetComponent<OVR_UIButton>();
                    currentButtonEvent = button.onClick;
                }
                catch(Exception e)
                {

                }

                raycastTransform.GetComponent<LineRenderer>().SetPosition(0, raycastTransform.position);
                raycastTransform.GetComponent<LineRenderer>().SetPosition(1, OvrHit.point);
                if (OVRInput.Get(OVRInput.Button.PrimaryTouchpad))
                    currentButtonEvent.Invoke();
            }
            else
            {
                raycastTransform.GetComponent<LineRenderer>().SetPosition(0, raycastTransform.position);
                raycastTransform.GetComponent<LineRenderer>().SetPosition(1, (raycastTransform.position + raycastTransform.forward) * 2);
            }
        }
    }

    public void MoveNetworkedPlayer(){
        Transform cameraRig = GameObject.FindWithTag("Player").transform;

        if (networkedPlayer == null)
            GetNetworkedPlayer();

        if (networkedPlayer == null)
            return;

        networkedPlayer.transform.position = new Vector3(cameraRig.position.x, networkedPlayer.transform.position.y, cameraRig.position.z);
    }
}
