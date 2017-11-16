using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
        float ratio = (float)Screen.width / (float)Screen.height;//Y/X
        this.GetComponent<Camera>().rect = new Rect((1 - 0.2f), (1 - 0.2f * ratio), 0.2f, 0.2f * ratio);
        //Rect(x position, y position, x scale, y scale)
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
