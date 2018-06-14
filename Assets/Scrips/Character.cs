using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public bool HeroTrue; //phân biệt Hero và Enemy - Hero = true
    public float maxHealth;
    public float curHealth;
    public int dame;
    public int[] positionAttack;            //vị trí enemy bị tấn công
    public bool flagCoDinh;          //chỉ đánh mục tiêu được chỉ định  - mục tiêu bị đánh đã chết => không đánh ai khác;
    public bool flagNgauNhien;       //Đánh mục tiêu ngẫu nhiên
    public bool flagThapMauNhat;     //Đánh mục tiêu thấp máu nhất
    public bool flagNhieuMauNhat;    //Đánh mục tiêu nhiều máu nhất
    public GameObject health_bar;
    public bool flagDanhRoi = false;
    public bool flagDie = false;
    Animation anim;
    Vector3 default_location;
    string state = "Idle";
    float stateTime = 0;  //time animation 
    void Start()
    {
        curHealth = maxHealth;
        InvokeRepeating("decreaseHealth", 1f, 1f);

        anim = GetComponent<Animation>();
        default_location = this.transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        if (!state.Equals("Idle") && stateTime > 0)
        {
            this.stateTime -= Time.deltaTime;

            if (stateTime <= 0)
            {
                if (state.Equals("Attack"))
                {
                    flagDanhRoi = true;
                }
                if (state.Equals("Die"))
                {


                    this.gameObject.SetActive(false); //Destroy(this.gameObject, 0f);//

                    Debug.Log("Character ----------DIE");
                }
                this.switchAnimation("Idle");
                this.transform.position = default_location;
            }
        }
        else
        {
            switchAnimation(state);
        }
        this.checkCharacterDie();
        //this.OnStay(state);
        //this.OnMove(new Vector3(transform.position.x,transform.position.y, transform.position.z + speed * Time.deltaTime));

    }
    void checkCharacterDie()
    {
        if (curHealth <= 0)
        {
            flagDie = true;
            this.switchAnimation("Die");
        }
    }
    public void OnStay(string action)
    {
        anim.clip = anim.GetClip(action);
        anim.Play();
        //Debug.Log("Time Idle:  "+ anim.clip.length);
    }
    private void DanhMucTieuCoDinh(Character hero, Character[] arrEnemy)
    {
        if (positionAttack.Length == 1) //đánh đơn mục tiêu
        {
            if (!arrEnemy[positionAttack[0]].flagDie) //Mục tiêu còn sống
            {
                //Nếu mục tiêu còn sống
                int sflagHvsE = HeroTrue ? -100 : 100;
                this.transform.position = new Vector3(arrEnemy[positionAttack[0]].transform.position.x, transform.position.y, arrEnemy[positionAttack[0]].transform.position.z + sflagHvsE);//di chuyển nhân vật
                XuLyDame(hero, arrEnemy[positionAttack[0]]);
            }
            else //mục tiêu đã chết - ko đánh ai hết
            {
                return;
            }
        }
        else //đánh đa mục tiêu
        {
            List<int> lst = new List<int>();
            for (int i = 0; i < positionAttack.Length; i++)
            {
                if (!arrEnemy[positionAttack[i]].flagDie) //Đánh những mục tiêu còn sống
                {
                    lst.Add(positionAttack[i]);
                }
            }
            if (lst.Count > 0) //có mục tiêu còn sống
            {
                //đánh mục tiêu còn sống
                float xnew = (arrEnemy[0].transform.position.x + arrEnemy[1].transform.position.x) / 2;
                int mflagHvsE = HeroTrue ? -300 : 300;
                float znew = arrEnemy[0].transform.position.z + mflagHvsE;
                this.transform.position = new Vector3(xnew, transform.position.y, znew); // di chuyển nhân vật
                for (int i = 0; i < lst.Count; i++)
                {
                    XuLyDame(hero, arrEnemy[lst[i]]);
                }
            }
        }
    }
    private void DanhMucTieuNgauNhien(Character hero, Character[] arrEnemy) //đánh mục tiêu được chỉ định, sau đó đánh ngẫu nhiên
    {
        if (positionAttack.Length == 1) //đánh đơn mục tiêu
        {
            if (!arrEnemy[positionAttack[0]].flagDie) //Mục tiêu còn sống
            {
                //Nếu mục tiêu còn sống
                int sflagHvsE = HeroTrue ? -100 : 100;
                this.transform.position = new Vector3(arrEnemy[positionAttack[0]].transform.position.x, transform.position.y, arrEnemy[positionAttack[0]].transform.position.z + sflagHvsE);//di chuyển nhân vật
                XuLyDame(hero, arrEnemy[positionAttack[0]]);
            }
            else //mục tiêu đã chết - chọn ngẫu nhiên 1 enemy để đánh
            {
                List<int> lst = TimViTriDSMucTieuConSong(arrEnemy);
                if (lst.Count >= 1)
                {
                    //Nếu mục tiêu còn sống
                    int sflagHvsE = HeroTrue ? -100 : 100;
                    int rd = Random.Range(0, lst.Count - 1);
                    Debug.Log("RANDOM----------------" + rd);
                    this.transform.position = new Vector3(arrEnemy[lst[rd]].transform.position.x, transform.position.y, arrEnemy[lst[rd]].transform.position.z + sflagHvsE);//di chuyển nhân vật
                    XuLyDame(hero, arrEnemy[lst[rd]]);
                }
                else //lst.Count = 0  => Tất cả mục tiêu đều chết
                {
                    return;
                }

            }
        }
        else //đánh đa mục tiêu  ======================CHƯA VIẾT XONG ==================================
        {
            List<int> lstElife = new List<int>(); //Danh  sách enemy mặc định đưa  vào còn sống
            for (int i = 0; i < positionAttack.Length; i++)
            {
                if (!arrEnemy[positionAttack[i]].flagDie) //Đánh những mục tiêu còn sống
                {
                    lstElife.Add(positionAttack[i]);
                }
            }
            if (lstElife.Count == positionAttack.Length) //kt = true => tất cả mục tiêu đều sống
            {
                //đánh mục tiêu còn sống
                float xnew = (arrEnemy[0].transform.position.x + arrEnemy[1].transform.position.x) / 2;
                int mflagHvsE = HeroTrue ? -300 : 300;
                float znew = arrEnemy[0].transform.position.z + mflagHvsE;
                this.transform.position = new Vector3(xnew, transform.position.y, znew); // di chuyển nhân vật
                for (int i = 0; i < positionAttack.Length; i++)
                {
                    XuLyDame(hero, arrEnemy[positionAttack[i]]);
                }
            }
            else //có enemy đã chết - xử lý thay thế bằng enemy khác
            {
                if (lstElife.Count == 0) //toàn bộ enemy đã chết
                {
                    //chọn ra các vị trí mới 
                    List<int> lst = TimViTriDSMucTieuConSong(arrEnemy);  //danh sách enemy còn sống trong mảng arrEnemy
                    if (lst.Count <= positionAttack.Length && lst.Count > 0)
                    {
                        float xnew = (arrEnemy[0].transform.position.x + arrEnemy[1].transform.position.x) / 2;
                        int mflagHvsE = HeroTrue ? -300 : 300;
                        float znew = arrEnemy[0].transform.position.z + mflagHvsE;
                        this.transform.position = new Vector3(xnew, transform.position.y, znew); // di chuyển nhân vật
                        for (int i = 0; i < lst.Count; i++)
                        {
                            XuLyDame(hero, arrEnemy[lst[i]]);
                        }
                    }
                }
                else
                {
                    // đánh những enemy đưa vào mặc định còn sống
                    float xnew = (arrEnemy[0].transform.position.x + arrEnemy[1].transform.position.x) / 2;
                    int mflagHvsE = HeroTrue ? -300 : 300;
                    float znew = arrEnemy[0].transform.position.z + mflagHvsE;
                    this.transform.position = new Vector3(xnew, transform.position.y, znew); // di chuyển nhân vật
                    for (int i = 0; i < lstElife.Count; i++)
                    {
                        XuLyDame(hero, arrEnemy[lstElife[i]]);
                    }
                    //đánh thêm số lượng mục tiêu còn thiếu
                    int t = positionAttack.Length - lstElife.Count;
                    List<int> lstEnemyConSongMaChuaBiDanh=new List<int>(); ///XEM LAI
                    for (int i = 0; i < arrEnemy.Length; i++)
                    {
                        for (int j = 0; j < lstElife.Count; j++)
                        {
                            if (!arrEnemy[i].flagDie && !KTMotSoCoTrongMang(i, lstElife))
                            {
                                lstEnemyConSongMaChuaBiDanh.Add(i);
                                t--;
                            }
                        }
                        if (t == 0)
                        {
                            for (int k = 0; k < lstEnemyConSongMaChuaBiDanh.Count; k++)
                            {
                                XuLyDame(hero, arrEnemy[lstEnemyConSongMaChuaBiDanh[k]]);
                            }
                            break;
                        }                         
                    }
                }
            }
        }
    }
    bool KTMotSoCoTrongMang(int a, List<int> lst)
    {
        for (int i = 0; i < lst.Count; i++)
        {
            if (lst[i] == a)
                return true;
        }
        return false;
    }
    List<int> TimViTriDSMucTieuConSong(Character[] arrEnemy)
    {
        List<int> lst = new List<int>();
        for (int i = 0; i < arrEnemy.Length; i++)
        {
            if (!arrEnemy[i].flagDie)
            {
                //tìm được mục tiêu còn sống mới 
                lst.Add(i);
            }
        }
        return lst;
    }
    public void OnAttack(Character[] arrEnemy)
    {
        if (flagCoDinh)
            DanhMucTieuCoDinh(this, arrEnemy);
        if (flagNgauNhien)
            DanhMucTieuNgauNhien(this, arrEnemy);
        this.switchAnimation("Attack");
    }
    public void XuLyDame(Character CharacterCong, Character CharacterThu)
    {
        CharacterThu.curHealth -= CharacterCong.dame; // xử lý tính sát thương tại đây
    }
    public void OnMove(Vector3 vector3)
    {
        this.transform.position = vector3;
        anim.clip = anim.GetClip("Run");
        anim.Play();
    }
    void switchAnimation(string state)
    {
        if (!this.state.Equals(state))
        {
            this.state = state;
            anim.clip = anim.GetClip(state);
            this.stateTime = anim[state].length; //anim.clip.length; 
        }
        else
        {
            anim.clip = anim.GetClip(state);
        }
        anim.Play();
    }
    void decreaseHealth() // Xử lý giao diện thanh máu
    {
        //curHealth -= 2f;
        float calcuator_Health = curHealth / maxHealth;
        if (calcuator_Health < 0)
            calcuator_Health = 0;
        setHealthBar(calcuator_Health);
    }
    public void setHealthBar(float health)
    {
        health_bar.transform.localScale = new Vector3(health, health_bar.transform.localScale.y, health_bar.transform.localScale.z);
    }
}
