using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxLogo : MonoBehaviour {

    float time;
    Vector3 scaleLG;
	// Use this for initialization
	void Start () {
        time = 2;
        scaleLG = this.transform.localScale;
        this.transform.localScale = new Vector3(0,0,0);
	}
	
	// Update is called once per frame
	void Update () {
        if (time > 0)
        {
            float scx;
            float scy;
            float scz;
            if (time > 0.5)
            {
                 scx = this.transform.localScale.x + (float)0.02;
                 scy = this.transform.localScale.y + (float)0.02;
                 scz = this.transform.localScale.z + (float)0.02;
            }
            else
            {
                scx = this.transform.localScale.x - (float)0.02;
                scy = this.transform.localScale.y - (float)0.02;
                scz = this.transform.localScale.z - (float)0.02;
            }
            this.transform.localScale = new Vector3(scx, scy, scz);
            time -= Time.deltaTime;
        }
	}
}
