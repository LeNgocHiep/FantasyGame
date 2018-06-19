using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class allHeroController : MonoBehaviour {

    public GameObject[] lstOTuong;
    public Vector3 vtSpawn;
    public Transform transSpawn;
    public GameObject luulai;
    public GameObject popUpDetail;
    public Text mau, dame, muctieu;

    // Use this for initialization
    void Start () {
        //if (Preload.Global == null)
        //{
        //    UnityEngine.SceneManagement.SceneManager.LoadScene("preload");
        //    return;
        //}

        for (int i = 0; i < lstOTuong.Length;i++)
        {
            if (Preload.Global.duLieu.tuongDaCo[i] == 1)
            {
                lstOTuong[i].transform.GetChild(1).gameObject.SetActive(false);
            }
        }
	}
	
    public void clickShowTuong(int i)
    {
        if (lstOTuong[i].transform.GetChild(1).gameObject.activeSelf)
            return;

        if (luulai != null)
            Destroy(luulai);
        GameObject g = Instantiate(Preload.Global.listTuong[i], transSpawn);
        g.transform.localPosition = vtSpawn;
        g.transform.localScale = new Vector3(100, 100, 100);
        g.transform.localRotation = new Quaternion(0, 180, 0, 0);
        luulai = g;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray,out hit))
            {
                if (hit.collider != null)
                {
                    Character c = luulai.GetComponent<Character>();

                    mau.text = c.maxHealth.ToString();
                    dame.text = c.dame.ToString();
                    if (c.flagCoDinh)
                        muctieu.text = "Cố định";
                    if (c.flagNgauNhien)
                        muctieu.text = "Ngẫu nhiên";
                    popUpDetail.SetActive(true);
                }
            }
        }
    }

    public void clickExitDetail()
    {
        popUpDetail.SetActive(false);
    }

    public void clickBack()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }

}
