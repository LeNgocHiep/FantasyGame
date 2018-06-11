using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public Enemy[] lstenemey;
    public Heros[] lstHeros;
	// Use this for initialization
	void Start () {
		
	}

    int iHerro = 0;
    int iEnemy = 0;

    bool flagQuanDanh = true; //true: Hiệp đánh, false: Vũ đánh
    int luotHerro = 0;
    int luotEnemy = 0;
	// Update is called once per frame
	void Update () {

        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    lstHeros[iHerro].OnAttack(lstenemey[iHerro]);
        //    flagQuanDanh = false;
        //    iHerro++;
        //    return;
        //}

        //if (luotHerro > 0 && iEnemy == 0 && flagQuanDanh && !lstenemey[4].flagDanhRoi)
        //{
        //    lstHeros[iHerro].OnAttack(lstenemey[iHerro]);
        //    flagQuanDanh = false;
        //    iHerro++;
        //    return;
        //}


        //if (luotEnemy > 0 && iHerro == 0 && !flagQuanDanh && !lstenemey[4].flagDanhRoi)
        //{
        //    lstenemey[iEnemy].attack(lstHeros[iEnemy]);
        //    flagQuanDanh = true;
        //    iEnemy++;
        //    return;
        //}

        ////if (iHerro == 0)
        ////    return;

        //if (!flagQuanDanh && lstHeros[iHerro-1].flagDanhRoi)
        //{
        //    lstenemey[iEnemy].attack(lstHeros[iEnemy]);
        //    flagQuanDanh = true;
        //    iEnemy++;
        //    return;
        //}


        //if (iEnemy == 0)
        //    return;

        //if (flagQuanDanh && lstenemey[iEnemy - 1].flagDanhRoi)//HIệp đánh
        //{
        //    lstHeros[iHerro].OnAttack(lstenemey[iHerro]);
        //    flagQuanDanh = false;
        //    iHerro++;
        //    return;
        //}

        //if (iHerro == 5 && iEnemy == 5)
        //{
        //    iHerro = 0;
        //    luotHerro++;
        //    for (int i = 0; i < lstenemey.Length; i++)
        //        lstenemey[i].flagDanhRoi = false;
        //    iEnemy = 0;
        //    luotEnemy++;
        //    for (int i = 0; i < lstHeros.Length; i++)
        //        lstHeros[i].flagDanhRoi = false;
        //}


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            lstHeros[iHerro].OnAttack(lstenemey[iHerro]);
            flagQuanDanh = false;
            iHerro++;
        }


        if (iHerro == 0)
            return;

        if (!flagQuanDanh) 
        {
            if (iHerro == 6 && lstHeros[iHerro - 2].flagDanhRoi)
            {
                lstenemey[0].attack(lstHeros[0]);
                iHerro = 1;
                flagQuanDanh = true;
                iEnemy++;
                for (int i = 1; i < 5; i++)
                {
                    lstHeros[i].flagDanhRoi = false;
                }
                return;

            }
            if (lstHeros[iHerro - 1].flagDanhRoi)
            { lstenemey[iEnemy].attack(lstHeros[iEnemy]);
            flagQuanDanh = true;
            iEnemy++;
                }
            Debug.Log("hihi");
        }


        if (iEnemy == 0)
            return;

        if (flagQuanDanh && lstenemey[iEnemy - 1].flagDanhRoi)//HIệp đánh
        {
            if (iEnemy == 5)
            {
                lstHeros[0].OnAttack(lstenemey[0]);
                iEnemy = 1;
                for (int i = 1; i < 5; i++)
                {
                    lstenemey[i].flagDanhRoi = false;
                }
            }
            lstHeros[iHerro].OnAttack(lstenemey[iHerro]);
            flagQuanDanh = false;
            iHerro++;
        }
    }
}
