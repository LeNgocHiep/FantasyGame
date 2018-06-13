using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public Enemy[] lstenemey;
    public Heros[] lstHeros;
    // Use this for initialization
    void Start()
    {

    }

    int iHerro = 0;
    int iEnemy = 0;

    bool flagQuanDanh = true; //true: Hiệp đánh, false: Vũ đánh
    int luotHerro = 0;
    int luotEnemy = 0;
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            lstHeros[iHerro].OnAttack(lstenemey[iHerro]);
            flagQuanDanh = false;
            iHerro++;
        }

        if (!flagQuanDanh)
        {
            if (iEnemy == 5)
            {
                iEnemy = 0;
            }
            if (lstHeros[iEnemy].flagDanhRoi)
            {
                // if (lstHeros[iEnemy].flagDie)

                lstenemey[iEnemy].attack(lstHeros[iEnemy]);
                flagQuanDanh = true;
                iEnemy++;
            }
        }


        if (iHerro == 0)
            return;

        if (flagQuanDanh && lstenemey[iHerro - 1].flagDanhRoi)
        {
            if (iHerro == 5)
            {
                iHerro = 0;
                for (int i = 0; i < lstenemey.Length; i++)
                {
                    lstenemey[i].flagDanhRoi = false;
                    lstHeros[i].flagDanhRoi = false;
                }
            }
            lstHeros[iHerro].OnAttack(lstenemey[iHerro]);
            flagQuanDanh = false;
            iHerro++;
        }

    }

    private int nhanVatChuaDie(Enemy[] arr)
    {
        for (int i = 0; i < arr.Length; i++)
            if (!arr[i].flagDie)
                return i;
        return -1;
    }

    private int nhanVatChuaDie(Heros[] arr)
    {
        for (int i = 0; i < arr.Length; i++)
            if (!arr[i].flagDie)
                return i;
        return -1;
    }

}
