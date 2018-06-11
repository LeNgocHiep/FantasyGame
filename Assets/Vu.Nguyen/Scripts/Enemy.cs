using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

    string tenEnemy;
    public int ViTriDung; //Vị trí đứng trong đội hình
    public int HP; // số máu
    public int Dam; // lực đánh mạnh nhất
    public int mucTieu; // số lượng mục tiêu
    public int somauDoiPhuongMat; //Số máu đối phương bị mất
    public int somauDoiPhuongMatDam;//số máu khi đối phương mất khi dùng Dam
    Animation anim;
    bool flagAttack, flagRun;
    public bool flagDie;
    Image maucon, mauhet;
    GameObject Healthbar;
    public bool flagDanhRoi = false;
	// Use this for initialization
	void Start () {
        tenEnemy = this.name;
         anim = GetComponent<Animation>();
        vitriCu = this.transform.position;
        Healthbar = transform.GetChild(3).gameObject;
        maucon = Healthbar.transform.GetChild(1).GetComponent<Image>();
        mauhet = Healthbar.transform.GetChild(0).GetComponent<Image>();
    }  

    public float timeAttack;
    public float moveSpeed;
    Vector3 vitriCu;
	// Update is called once per frame
	void Update () {
       if (!flagAttack && !flagRun && !flagDie)
        {
            this.MovementEnemy("Idle");
         }
       if (flagRun)
        {
            if (transform.localPosition.x >= 150f)
            {
                transform.position = new Vector3(transform.position.x - transform.position.x * Time.deltaTime * moveSpeed, transform.position.y, transform.position.z);
                this.MovementEnemy("Run");
            }
            else
            {
                flagAttack = true;
                flagRun = false;
            }
        }
       if (flagAttack)
        {
            if (timeAttack >= 0)
            {
                timeAttack -= Time.deltaTime;
                this.MovementEnemy("Attack");
            }
            else
            {
                //Trừ máu đối phương
                transform.position = vitriCu;
                flagAttack = false;
                timeAttack = 3;
                flagDanhRoi = true;
            }
        }

        if (HP <= 0)
        {
            flagDie = true;
            this.MovementEnemy("Die");
            Healthbar.SetActive(false);
            Destroy(this.gameObject,2.3f);
        }
    }
    
    private void MovementEnemy(string trangthai)
    {
        if (this.tenEnemy.Equals("DragonFire") && trangthai == "Run")
            return;
        anim.clip = anim.GetClip(trangthai);
        anim.Play();
    }

    public void attack(Object ObjHero)
    {
        Heros heros = (Heros)ObjHero;
        heros.curHealth -= this.Dam;
        if (this.name.Equals("DragonFire"))
            flagAttack = true;
        else
        {
            flagRun = true;
        }
    }

    public void playerAttack(int somau)
    {
        this.HP -= somau;
        maucon.rectTransform.offsetMin = new Vector2(maucon.rectTransform.offsetMin.x + somau, 0);
        maucon.rectTransform.offsetMax = new Vector2(0, 0);
    }


}
