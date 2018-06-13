using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersHealthBar : MonoBehaviour {
    public  float max_Health = 100f;
    public  float cur_Health = 0f;
    public GameObject health_bar;
	// Use this for initialization
	void Start () {
        cur_Health = max_Health;
        InvokeRepeating("decreaseHealth",1f,1f);       
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void decreaseHealth()
    {
        //cur_Health -= 2f;
        float calcuator_Health = cur_Health / max_Health;
        setHealthBar(calcuator_Health);
    }
    public void setHealthBar(float health)
    {
        health_bar.transform.localScale = new Vector3(health,health_bar.transform.localScale.y,health_bar.transform.localScale.z);
    }
}
