using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public Enemy[] lstenemey;
    public Heros[] lstHeros;
    // Use this for initialization
    ////void Start()
    ////{

    ////}

    ////int iHerro = 0;
    ////int iEnemy = 0;

    ////bool flagQuanDanh = true; //true: Hiệp đánh, false: Vũ đánh
    ////int luotHerro = 0;
    ////int luotEnemy = 0;
    ////// Update is called once per frame
    ////void Update()
    ////{

    ////    if (Input.GetKeyDown(KeyCode.Escape))
    ////    {
    ////        lstHeros[iHerro].OnAttack(lstenemey[iHerro]);
    ////        flagQuanDanh = false;
    ////        iHerro++;
    ////    }

    ////    if (!flagQuanDanh)
    ////    {
    ////        if (iEnemy == 5)
    ////        {
    ////            iEnemy = 0;
    ////        }
    ////        if (lstHeros[iEnemy].flagDanhRoi)
    ////        {
    ////            // if (lstHeros[iEnemy].flagDie)

    ////            lstenemey[iEnemy].attack(lstHeros[iEnemy]);
    ////            flagQuanDanh = true;
    ////            iEnemy++;
    ////        }
    ////    }


    ////    if (iHerro == 0)
    ////        return;

    ////    if (flagQuanDanh && lstenemey[iHerro - 1].flagDanhRoi)
    ////    {
    ////        if (iHerro == 5)
    ////        {
    ////            iHerro = 0;
    ////            for (int i = 0; i < lstenemey.Length; i++)
    ////            {
    ////                lstenemey[i].flagDanhRoi = false;
    ////                lstHeros[i].flagDanhRoi = false;
    ////            }
    ////        }
    ////        lstHeros[iHerro].OnAttack(lstenemey[iHerro]);
    ////        flagQuanDanh = false;
    ////        iHerro++;
    ////    }

    ////}

    ////private int nhanVatChuaDie(Enemy[] arr)
    ////{
    ////    for (int i = 0; i < arr.Length; i++)
    ////        if (!arr[i].flagDie)
    ////            return i;
    ////    return -1;
    ////}

    ////private int nhanVatChuaDie(Heros[] arr)
    ////{
    ////    for (int i = 0; i < arr.Length; i++)
    ////        if (!arr[i].flagDie)
    ////            return i;
    ////    return -1;
    ////}

    private void Update()
    {
        TurnActive(lstenemey, lstHeros);
    }

    bool CheckHerosWin()
    {
        for (int i = 0; i < lstenemey.Length; i++)
        {
            if (!lstenemey[i].flagDie)
            {
                return false;
            }
        }
        return true;
    }
    bool CheckEnemyWin()
    {
        for (int i = 0; i < lstHeros.Length; i++)
        {
            if (!lstHeros[i].flagDie)
            {
                return false;
            }
        }
        return true;
    }
    int iEnemy = 0;
    int iHero = 0;
    bool flagTurn = true; //True : Hero danh --- False : Enemy danh
    void TurnActive(Enemy[] arrEnemy, Heros[] arrHeros)
    {

        if (flagTurn)
        {
            if (iHero == 0)
            {
                //if (iEnemy == 0)
                //{
                //    iEnemy = EnemyDuocChon(arrEnemy);
                //}
                iHero = HeroDuocChon(arrHeros);
                if (arrHeros[iHero].flagDie)
                    iHero = HeroDuocChon(arrHeros);
                HeroTanCong(arrHeros[iHero], iHero, arrEnemy);
                //if (iHero == ViTriHeroSauCung(arrHeros))
                // ResetFlagDanhRoiHeros(arrHeros);
                ResetFlagDanhRoiEnemy(arrEnemy);
                flagTurn = false;
                Debug.Log("FlagTurn:" + flagTurn);

            }
            else
            {

                if (arrEnemy[iEnemy].flagDanhRoi)
                {

                    //if (iEnemy == 0)
                    //{
                    //    iEnemy = EnemyDuocChon(arrEnemy);
                    //}
                    iHero = HeroDuocChon(arrHeros);
                    if (arrHeros[iHero].flagDie)
                    {
                        Debug.Log("HERO DIE -----------------");
                        return;

                    }
                    HeroTanCong(arrHeros[iHero], iHero, arrEnemy);

                    //if (iHero == ViTriHeroSauCung(arrHeros))
                    ResetFlagDanhRoiEnemy(arrEnemy);
                    flagTurn = false;
                    Debug.Log("FlagTurn:" + flagTurn);
                }
            }

        }
        else
        {
            if (arrHeros[iHero].flagDanhRoi)
            {
                iEnemy = EnemyDuocChon(arrEnemy);
                if (arrEnemy[iEnemy].flagDie)
                    iEnemy = EnemyDuocChon(arrEnemy);
                if (iHero == 0)
                {
                    Debug.Log("Reset Vong danh -> Round2:" + iEnemy);
                    iHero = HeroDuocChon(arrHeros);
                }
                EnemyTanCong(arrEnemy[iEnemy], iEnemy, arrHeros);

                //if (iEnemy == ViTriEnemySauCung(arrEnemy))

                flagTurn = true;
                Debug.Log("FlagTurn:" + flagTurn);
                ResetFlagDanhRoiHeros(arrHeros);
            }


        }
    }
    //Chọn Hero ra trận
    int HeroDuocChon(Heros[] arrHeros)
    {
        for (int i = 0; i < arrHeros.Length; i++)
        {
            if (!arrHeros[i].flagDie && !arrHeros[i].flagDanhRoi)
            {
                return i;
            }
        }
        for (int i = 0; i < arrHeros.Length; i++) //trường hợp chỉ còn 1 Hero còn sống
        {
            if (!arrHeros[i].flagDie)
            {
                return i;
            }
        }
        return 0;
    }
    //Chọn Enemy ra trận
    int EnemyDuocChon(Enemy[] arrEnemy)
    {
        for (int i = 0; i < arrEnemy.Length; i++)
        {
            if (!arrEnemy[i].flagDie && !arrEnemy[i].flagDanhRoi)
            {
                return i;
            }
        }
        for (int i = 0; i < arrEnemy.Length; i++) //trường hợp chỉ còn 1 Enemy còn sống
        {
            if (!arrEnemy[i].flagDie)
            {
                return i;
            }
        }
        return 0;
    }
    //vị trí Enemy sau cùng trong mảng còn sống 
    int ViTriEnemySauCung(Enemy[] arrEnemy)
    {
        int vt = -1;
        for (int i = 0; i < arrEnemy.Length; i++)
        {
            if (!arrEnemy[i].flagDie && !arrEnemy[i].flagDanhRoi)
            {
                vt = i;
            }
        }
        return vt;
    }
    //vị trí Hero sau cùng trong mảng còn sống 
    int ViTriHeroSauCung(Heros[] arrHeros)
    {
        int vt = -1;
        for (int i = 0; i < arrHeros.Length; i++)
        {
            if (!arrHeros[i].flagDie)
            {
                vt = i;
            }
        }
        return vt;
    }
    //Reset trạng thái flagDanhRoi cho enemy
    void ResetFlagDanhRoiEnemy(Enemy[] arrEnemy)
    {
        for (int i = 0; i < arrEnemy.Length; i++)
        {
            if (!arrEnemy[i].flagDanhRoi && !arrEnemy[i].flagDie)
            {
                return;
            }
        }
        Debug.Log("Tat cac cac ENEMY danh xong");
        for (int i = 0; i < arrEnemy.Length; i++)
        {
            arrEnemy[i].flagDanhRoi = false;
        }
        Debug.Log("Reset tranng thai ENEMY");
    }
    //Reset trạng thái flagDanhRoi cho Heros
    void ResetFlagDanhRoiHeros(Heros[] arrHeros)
    {
        for (int i = 0; i < arrHeros.Length; i++)
        {
            if (!arrHeros[i].flagDanhRoi && !arrHeros[i].flagDie)
                return;
        }
        Debug.Log("Tat cac cac HERO danh xong");
        for (int i = 0; i < arrHeros.Length; i++)
        {
            arrHeros[i].flagDanhRoi = false;
        }
        Debug.Log("Reset tranng thai HERO");
    }
    void HeroTanCong(Heros heros, int stt, Enemy[] arrEnemy) //stt : là vị trí của hero trong mảng lstHero
    {
        //Kiểm tra trạng thái sống chết của Hero
        if (heros.flagDie)
            return;
        if (arrEnemy[stt].flagDie) //Mục tiêu đã chết 
        {
            for (int i = 0; i < arrEnemy.Length; i++)
            {
                if (!arrEnemy[i].flagDie)
                {
                    Debug.Log("Lượt Hero =" + stt + " Danh Enemy" + i);
                    heros.OnAttack(arrEnemy[i]);
                    break;
                }
            }
        }
        else  //Mục tiêu còn sống
        {
            Debug.Log("Lượt Hero =" + stt + " Danh Enemy" + stt);
            heros.OnAttack(arrEnemy[stt]);
        }
    }
    void EnemyTanCong(Enemy enemy, int stt, Heros[] arrHeros) //stt : là vị trí của enemy trong mảng lstEnemy
    {
        //Kiểm tra trạng thái sống chết của Enemy
        if (enemy.flagDie)
            return;
        if (arrHeros[stt].flagDie) //Mục tiêu đã chết 
        {
            for (int i = 0; i < arrHeros.Length; i++)
            {
                if (!arrHeros[i].flagDie)
                {
                    Debug.Log("Lượt Enemy =" + stt + " Danh Hero" + i);
                    enemy.attack(arrHeros[i]);
                    break;
                }
            }
        }
        else  //Mục tiêu còn sống
        {
            Debug.Log("Lượt Enemy =" + stt + " Danh Hero" + stt);
            enemy.attack(arrHeros[stt]);
        }
    }

}