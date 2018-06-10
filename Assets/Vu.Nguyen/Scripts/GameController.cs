using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public Enemy[] lstenemey;

	// Use this for initialization
	void Start () {
		
	}

    int i = 0;


	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.A))
        {
            lstenemey[i].attack();
            i++;
            if (i >= lstenemey.Length)
                i = 0;
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
