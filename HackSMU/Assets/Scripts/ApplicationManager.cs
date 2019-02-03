using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public enum Device
{
    MOUSE_DEBUG,
    DAYDREAM,
    OCULUS_GO
}

public class ApplicationManager : MonoBehaviour {

    public Microphone mic;

    public static ApplicationManager manager;

    public Device device;

    private void Awake()
    {
        mic = new Microphone();

        if (manager == null)
            manager = this;
        else if(manager != this)
            Destroy(this.gameObject);
    }
}
