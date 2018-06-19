using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject[] vitri;
    public Transform transHerro;
    //public Character[] allHerro;
    public Character[] lstenemey;
    public Character[] lstHero;

    private void Start()
    {
        lstHero = new Character[ChonTuongTrongDanhSach.sceneChonHero.danhdauDaCo.Length];
        for (int i = 0; i < 5; i++)
        {
            GameObject g = Instantiate(Preload.Global.listTuong[Preload.Global.vitriTuongDangCo[ChonTuongTrongDanhSach.sceneChonHero.danhdauDaCo[i]]], transHerro);
            lstHero[i] = g.GetComponent<Character>();
            lstHero[i].transform.localPosition = vitri[i].transform.localPosition;
        }
    }

    private void Update()
    {
        TurnActive(lstenemey, lstHero);
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
        for (int i = 0; i < lstHero.Length; i++)
        {
            if (!lstHero[i].flagDie)
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
