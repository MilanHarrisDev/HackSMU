using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw : MonoBehaviour
{

    public string[] drawButtons;
    private bool drawing = false;

    [SerializeField]
    private GameObject drawLine;
    [SerializeField]
    private LineRenderer currentLine;

    [SerializeField]
    private bool mouseTest = false;

    [SerializeField]
    private LayerMask drawingLayer;


    private Vector3 drawStartPos;

    private float drawTimer = 0f;
    private float drawTime = .1f;
    private bool canDraw = false;

    private void Update()
    {
        drawTimer += Time.deltaTime;
        if(drawTimer >= drawTime && !canDraw)
        {
            drawTimer = 0;
            canDraw = true;
        }

        bool drawPressed = false;
        foreach (string str in drawButtons)
        {
            if (Input.GetButton(str))
            {
                drawPressed = true;
                break;
            }
        }

        if (mouseTest)
            drawPressed = Input.GetMouseButton(0);

        if (drawPressed)
            TryDraw();
        else if (drawing)
            StopDraw();
    }

    private void TryDraw(){
        if(mouseTest)
        {
            RaycastHit hit;
            Camera cam = Camera.main;
            Vector2 mousePos = Input.mousePosition;
            Ray mouseRay = cam.ScreenPointToRay(Input.mousePosition);

            bool raycast = Physics.Raycast(cam.transform.position, mouseRay.direction, out hit, 1000f, drawingLayer);

            Debug.DrawRay(cam.transform.position, mouseRay.direction, Color.red, 5);

            if (raycast)
            {
                if (!drawing)
                {
                    drawStartPos = hit.point;
                    GameObject newDrawLine = Instantiate(drawLine, hit.transform);
                    newDrawLine.transform.position = Vector3.zero;
                    currentLine = newDrawLine.GetComponent<LineRenderer>();
                    currentLine.SetPosition(0, drawStartPos);
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
                }
            }
            else if (drawing)
                StopDraw();
        }
    }

    private void StopDraw(){
        drawing = false;
        currentLine = null;
    }
}