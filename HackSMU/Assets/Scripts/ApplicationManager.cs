using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Device
{
    MOUSE_DEBUG,
    DAYDREAM,
    OCULUS_GO
}

public class ApplicationManager : MonoBehaviour {

    public static Device device;
}
