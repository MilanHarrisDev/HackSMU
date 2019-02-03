using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum Device
{
    MOUSE_DEBUG,
    DAYDREAM,
    OCULUS_GO
}

public class ApplicationManager : MonoBehaviour {

    public static ApplicationManager manager;

    public Device device;

    private void Awake()
    {
        if (manager == null)
            manager = this;
        else if(manager != this)
            Destroy(this.gameObject);
    }
}
