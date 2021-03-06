﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChonTuongTrongDanhSach : MonoBehaviour {
    public static ChonTuongTrongDanhSach sceneChonHero;
    public GameObject prefabsKhung;
    public Transform transListTuong;
    public Transform[] listOTuong;
    public GameObject maskBtnStartGame;
    public int[] danhdauDaCo = new int[] { -1,-1,-1,-1,-1};

    private void Awake()
    {
        sceneChonHero = this;
    }

    private void Start()
    {
        for (int i = 0; i < Preload.Global.vitriTuongDangCo.Count;i++)
        {
           GameObject g =  Instantiate(prefabsKhung, transListTuong);
            g.transform.GetChild(0).GetComponent<Image>().sprite = Preload.Global.listImageTuong[Preload.Global.vitriTuongDangCo[i]];
            g.GetComponent<KhungTuong>().IndexTuong = i;
        }
    }

    public void clickChonTuong(int indexTuong)
    {
        GameObject g = transListTuong.GetChild(indexTuong).gameObject;
        if (g.transform.GetChild(1).gameObject.activeSelf)
        {
            //không làm chi hết
            return;
        }
        else
        {
            //Gán hình lên các ô chưa có.          
            if (!ktraDu5Con(danhdauDaCo, danhdauDaCo.Length))
            {
                g.transform.GetChild(1).gameObject.SetActive(true);
                GanHinhLenO(g, indexTuong);
            }
        }
    }

    void GanHinhLenO(GameObject g, int indexTuong)
    {
        for (int i = 0; i < listOTuong.Length; i++)
        {
            if (danhdauDaCo[i] == -1)
            {
                listOTuong[i].gameObject.transform.GetChild(0).GetComponent<Image>().enabled = true;
                listOTuong[i].gameObject.transform.GetChild(0).GetComponent<Image>().sprite = g.transform.GetChild(0).GetComponent<Image>().sprite;
                danhdauDaCo[i] = indexTuong;
                break;
            }
        }
        if (ktraDu5Con(danhdauDaCo, danhdauDaCo.Length))
            maskBtnStartGame.SetActive(false);

    }

    public bool ktraDu5Con(int[] a, int n)
    {
        for (int i = 0; i < n; i++)
            if (a[i] == -1)
                return false;
        return true;
    }

    public void clickHuyTuong(int indexOTuong)
    {
        if (danhdauDaCo[indexOTuong] == -1)
            return;
        transListTuong.GetChild(danhdauDaCo[indexOTuong]).transform.GetChild(1).gameObject.SetActive(false);
        listOTuong[indexOTuong].gameObject.transform.GetChild(0).GetComponent<Image>().enabled = false;
        danhdauDaCo[indexOTuong] = -1;
        maskBtnStartGame.SetActive(true);
    }

    public void clickStartTranDau()
    {
        for(int i = 0; i < danhdauDaCo.Length; i++)
        {
            Debug.Log(danhdauDaCo[i]);
        }

        if (maskBtnStartGame.activeSelf)
            return;
        else
            UnityEngine.SceneManagement.SceneManager.LoadScene("Match");
    }

    public void clickBack()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }

}
