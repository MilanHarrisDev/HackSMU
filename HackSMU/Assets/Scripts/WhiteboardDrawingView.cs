using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class WhiteboardDrawingView : MonoBehaviourPun {

    private LineRenderer lineRenderer;
    [SerializeField]
    private Material[] colors;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void Init(int color, Vector3[] points){
        if (photonView.IsMine)
            photonView.RPC("RPC_InitWhiteboardLine", RpcTarget.OthersBuffered, color, points);
    }

    [PunRPC]
    private void RPC_InitWhiteboardLine(int color, Vector3[] points){
        lineRenderer.material = colors[color];
        lineRenderer.positionCount = points.Length;
        lineRenderer.SetPositions(points);
    }
}
