using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Resume : MonoBehaviour {

    private GameObject resume;

    public void ResumePopup()
    {
        resume.SetActive(true);
    }

	// Use this for initialization
	void Start () {
		Instantiate(resume, new Vector3(0, 0, 0), Quaternion.identity);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
