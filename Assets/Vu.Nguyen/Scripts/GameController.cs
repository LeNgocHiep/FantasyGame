using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Character[] lstenemey1;
    public Character[] lstenemey;
    public Character[] lstCharacters;
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
    ////        lstCharacters[iHerro].OnAttack(lstenemey[iHerro]);
    ////        flagQuanDanh = false;
    ////        iHerro++;
    ////    }

    ////    if (!flagQuanDanh)
    ////    {
    ////        if (iEnemy == 5)
    ////        {
    ////            iEnemy = 0;
    ////        }
    ////        if (lstCharacters[iEnemy].flagDanhRoi)
    ////        {
    ////            // if (lstCharacters[iEnemy].flagDie)

    ////            lstenemey[iEnemy].attack(lstCharacters[iEnemy]);
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
    ////                lstCharacters[i].flagDanhRoi = false;
    ////            }
    ////        }
    ////        lstCharacters[iHerro].OnAttack(lstenemey[iHerro]);
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

    ////private int nhanVatChuaDie(Character[] arr)
    ////{
    ////    for (int i = 0; i < arr.Length; i++)
    ////        if (!arr[i].flagDie)
    ////            return i;
    ////    return -1;
    ////}

    private void Update()
    {
        TurnActive(lstenemey, lstCharacters);
    }

    bool CheckCharactersWin()
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
        for (int i = 0; i < lstCharacters.Length; i++)
        {
            if (!lstCharacters[i].flagDie)
            {
                return false;
            }
        }
        return true;
    }
    int iEnemy = 0;
    int iHeros = 0;
    bool flagTurn = true; //True : Character danh --- False : Enemy danh
    void TurnActive(Character[] arrEnemy, Character[] arrHeros)
    {

        if (flagTurn)// Hero tấn công
        {
            if (iHeros == 0)
            {
                iHeros = CharacterDuocChon(arrHeros);
                if (arrHeros[iHeros].flagDie)
                    iHeros = CharacterDuocChon(arrHeros);
                CharacterTanCong(arrHeros[iHeros], iHeros, arrEnemy);
                ResetFlagDanhRoiCharacter(arrEnemy);
                flagTurn = false;
                Debug.Log("FlagTurn:" + flagTurn);

            }
            else
            {

                if (arrEnemy[iEnemy].flagDanhRoi)
                {
                    iHeros = CharacterDuocChon(arrHeros);
                    if (arrHeros[iHeros].flagDie)
                    {
                        Debug.Log("Character DIE -----------------");
                        return;

                    }
                    CharacterTanCong(arrHeros[iHeros], iHeros, arrEnemy);
                    ResetFlagDanhRoiCharacter(arrEnemy);
                    flagTurn = false;
                    Debug.Log("FlagTurn:" + flagTurn);
                }
            }

        }
        else  //Enemy tấn công
        {
            if (arrHeros[iHeros].flagDanhRoi)
            {
                iEnemy = CharacterDuocChon(arrEnemy);
                if (arrEnemy[iEnemy].flagDie)
                    iEnemy = CharacterDuocChon(arrEnemy);
                if (iHeros == 0)
                {
                    Debug.Log("Reset Vong danh -> Round2:" + iEnemy);
                    iHeros = CharacterDuocChon(arrHeros);
                }
                CharacterTanCong(arrEnemy[iEnemy], iEnemy, arrHeros);
                flagTurn = true;
                Debug.Log("FlagTurn:" + flagTurn);
                ResetFlagDanhRoiCharacter(arrHeros);
            }


        }
    }
    //Chọn Character ra trận
    int CharacterDuocChon(Character[] arrCharacters)
    {
        for (int i = 0; i < arrCharacters.Length; i++)
        {
            if (!arrCharacters[i].flagDie && !arrCharacters[i].flagDanhRoi)
            {
                return i;
            }
        }
        for (int i = 0; i < arrCharacters.Length; i++) //trường hợp chỉ còn 1 Character còn sống
        {
            if (!arrCharacters[i].flagDie)
            {
                return i;
            }
        }
        return 0;
    }
    //vị trí Character sau cùng trong mảng còn sống 
    int ViTriCharacterSauCung(Character[] arrCharacters)
    {
        int vt = -1;
        for (int i = 0; i < arrCharacters.Length; i++)
        {
            if (!arrCharacters[i].flagDie)
            {
                vt = i;
            }
        }
        return vt;
    }
    //Reset trạng thái flagDanhRoi cho enemy
    void ResetFlagDanhRoiCharacter(Character[] arrEnemy)
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
    void CharacterTanCong(Character Characters, int stt, Character[] arrEnemy) //stt : là vị trí của Character trong mảng lstCharacter
    {
        //Kiểm tra trạng thái sống chết của Character
        if (Characters.flagDie)
            return;

        Characters.OnAttack(arrEnemy);

    }
}
