using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public Enemy[] lstenemey;
    public Heros[] lstHeros;
	// Use this for initialization
	void Start () {
		
	}

    int i = 0;


	// Update is called once per frame
	void Update () {
        Debug.Log("Hiep");
		if (Input.GetKeyDown(KeyCode.A))
        {

            lstenemey[Random.Range(0, 4)].attack(lstHeros[ Random.Range(0,4)]);
            i++;
            if (i >= lstenemey.Length)
                i = 0;
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            lstHeros[Random.Range(0, 4)].OnAttack(lstenemey[Random.Range(0, 4)]);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            lstenemey[i].playerAttack(20);
            if (lstenemey[i].flagDie)

            {
                i++;
                if (i >= lstenemey.Length)
                    return;
            }
        }
	}
}
