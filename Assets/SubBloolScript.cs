using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SubBloolScript : MonoBehaviour {

    public TextMeshProUGUI meshSubBlood;
    float timeShowSubBlood;
    Vector3 defSubBloodPosition;
    // Use this for initialization
	void Start () {
            meshSubBlood.text = " ";
        defSubBloodPosition = meshSubBlood.transform.position;
        timeShowSubBlood = 0;
	}	
	// Update is called once per frame
	void Update () {
        if (timeShowSubBlood > 0)
        {
            double t = this.transform.position.y + 0.5;
            this.transform.position = new Vector3(this.transform.position.x, (float)t, this.transform.position.z);
            timeShowSubBlood -= Time.deltaTime;
        }
        else
        {
   
            meshSubBlood.text = " ";
        }
	}
    void resetSubBlood(int mauBiTru)
    {
        timeShowSubBlood = 2;
        meshSubBlood.text = "-" + mauBiTru.ToString();
        this.transform.position = defSubBloodPosition;       
    }
}
