using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw : MonoBehaviour
{

    private bool drawing = false;

    [SerializeField]
    private GameObject drawLine;
    [SerializeField]
    private LineRenderer currentLine;

    [SerializeField]
    private Material[] lineColors;
    private Material lineMaterial;

    [SerializeField]
    private LayerMask drawingLayer;

    private Vector3 drawStartPos;

    private float drawTimer = 0f;

    [SerializeField]
    private float drawTime = .1f;
    private bool canDraw = false;

    private void Awake()
    {
        lineMaterial = lineColors[0];
    }

    private void Update()
    {
        if (!canDraw)
        {
            if (drawTimer >= drawTime)
            {
                drawTimer = 0;
                canDraw = true;
            }
            else
                drawTimer += Time.deltaTime;
        }

        bool drawPressed = false;

        switch(ApplicationManager.manager.device)
        {
            case Device.MOUSE_DEBUG:
                drawPressed = Input.GetMouseButton(0);
                break;
            case Device.DAYDREAM:
                drawPressed = GvrControllerInput.GetDevice(GvrControllerHand.Right).GetButton(GvrControllerButton.TouchPadButton);
                break;
        }

        if (drawPressed)
            TryDraw();
        else if (drawing)
            StopDraw();
    }

    private void TryDraw(){
        RaycastHit hit = new RaycastHit();
        Camera cam = Camera.main;
        bool raycast = false;

        switch (ApplicationManager.manager.device)
        {
            case Device.MOUSE_DEBUG:
                Vector2 mousePos = Input.mousePosition;
                Ray mouseRay = cam.ScreenPointToRay(Input.mousePosition);

                raycast = Physics.Raycast(cam.transform.position, mouseRay.direction, out hit, 1000f, drawingLayer);

                Debug.DrawRay(cam.transform.position, mouseRay.direction, Color.red, 5);
                break;
            case Device.DAYDREAM:
                if (GvrPointerInputModule.CurrentRaycastResult.gameObject == null)
                    raycast = false;
                else if (GvrPointerInputModule.CurrentRaycastResult.gameObject.layer == LayerMask.NameToLayer("Drawing"))
                    raycast = true;
                else
                    raycast = false;

                hit.point = GvrPointerInputModule.CurrentRaycastResult.worldPosition;
                break;
            case Device.OCULUS_GO:

                break;
        }

        if (raycast)
        {
            if (!drawing)
            {
                drawStartPos = hit.point;
                GameObject newDrawLine = Instantiate(drawLine, hit.transform);
                newDrawLine.transform.position = Vector3.zero;
                currentLine = newDrawLine.GetComponent<LineRenderer>();
                currentLine.SetPosition(0, drawStartPos);

                currentLine.material = lineMaterial;
                drawing = true;
            }
            else if (currentLine != null)
            {
                if (!canDraw)
                    return;

                Vector3[] newPositions = new Vector3[currentLine.positionCount + 1];
                for (int i = 0; i < currentLine.positionCount; i++)
                    newPositions[i] = currentLine.GetPosition(i);

                newPositions[currentLine.positionCount] = hit.point;

                currentLine.positionCount++;

                currentLine.SetPositions(newPositions);

                canDraw = false;
            }
        }
        else if (drawing)
            StopDraw();

    }

    private void StopDraw(){
        drawing = false;
        currentLine = null;
    }

    public void SetColor(int newColorIndex){

        Debug.Log("Line Color Changed to " + newColorIndex);

        lineMaterial = lineColors[newColorIndex];
    }
}