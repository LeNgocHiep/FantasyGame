using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KhungTuong : MonoBehaviour {

    public int IndexTuong;

	// Use this for initialization
	void Start () {
		
	}
	

    public void click()
    {
        ChonTuongTrongDanhSach.sceneChonHero.clickChonTuong(IndexTuong);
    }




	// Update is called once per frame
	void Update () {
		
	}
}
