using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    public bool flagBuffHoiMau;
    public GameObject health_bar;
    public bool flagDanhRoi = false;
    public bool flagDie = false;
    Animation anim;
    Vector3 default_location;
    Quaternion default_rotation;
    string state = "Idle";
    float stateTime = 0;  //time animation 
    //SubBlood start
    public TextMeshProUGUI meshSubBlood;
    float timeShowSubBlood;
    Color defColor = Color.red;
    Vector3 defSubBloodPosition;
    //SubBlood end
    void Start()
    {
        curHealth = maxHealth;
        InvokeRepeating("decreaseHealth", 1f, 1f);
        anim = GetComponent<Animation>();
        default_location = this.transform.position;
        default_rotation = this.transform.rotation;
        //SubBlood start
        meshSubBlood.color = defColor;
        defSubBloodPosition = meshSubBlood.transform.position;
        timeShowSubBlood = 0;
        meshSubBlood.text = "";
        //SubBlood end
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
                //this.transform.rotation = default_rotation;
                //if (flagBuffHoiMau)
                //    health_bar.transform.rotation = default_rotation;
            }
        }
        else
        {
            switchAnimation(state);
        }
        this.checkCharacterDie();
        //SubBlood start
        if (timeShowSubBlood > 0)
        {
            //if (timeShowSubBlood ==2 && meshSubBlood.color == Color.green)
            //    meshSubBlood.color = defColor;
            double t = meshSubBlood.transform.position.y + 0.5;
            meshSubBlood.transform.position = new Vector3(this.transform.position.x, (float)t, this.transform.position.z);
            timeShowSubBlood -= Time.deltaTime;
        }
        else
        {
            meshSubBlood.text = "";
            meshSubBlood.color = defColor;
        }
        //SubBlood end

        //this.OnStay(state);
        //this.OnMove(new Vector3(transform.position.x,transform.position.y, transform.position.z + speed * Time.deltaTime));

    }
    void resetSubBlood(int mauBiTru)
    {
        timeShowSubBlood = 1;
        if (mauBiTru > 0)
            meshSubBlood.text = "+" + mauBiTru.ToString();
        else meshSubBlood.text = mauBiTru.ToString();
        meshSubBlood.transform.position = defSubBloodPosition;
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
                int mflagHvsE = HeroTrue ? -200 : 200;
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
                int mflagHvsE = HeroTrue ? -200 : 200;
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
                        int mflagHvsE = HeroTrue ? -200 : 200;
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
                    int mflagHvsE = HeroTrue ? -200 : 200;
                    float znew = arrEnemy[0].transform.position.z + mflagHvsE;
                    this.transform.position = new Vector3(xnew, transform.position.y, znew); // di chuyển nhân vật
                    for (int i = 0; i < lstElife.Count; i++)
                    {
                        XuLyDame(hero, arrEnemy[lstElife[i]]);
                    }
                    //đánh thêm số lượng mục tiêu còn thiếu
                    int t = positionAttack.Length - lstElife.Count;
                    List<int> lstEnemyConSongMaChuaBiDanh = new List<int>(); ///XEM LAI
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
    private void DanhMucTieuThapMau(Character hero, Character[] arrEnemy) // Chọn mục tiêu thấp máu nhất trong team địch để đánh
    {
        List<int> lstMinHealth = GetDsEnemyMauTangDan(arrEnemy);
        int t = lstMinHealth.Count > positionAttack.Length ? positionAttack.Length : lstMinHealth.Count;
        Debug.Log("HIEP KIEM TRA RIVEN DANH THAP MAU NHAT" + t);
        // if (t == 0)
        //    return;
        if (t == 1)
        {
            //Nếu mục tiêu còn sống
            int sflagHvsE = HeroTrue ? -100 : 100;
            this.transform.position = new Vector3(arrEnemy[lstMinHealth[0]].transform.position.x, transform.position.y, arrEnemy[lstMinHealth[0]].transform.position.z + sflagHvsE);//di chuyển nhân vật
            XuLyDame(hero, arrEnemy[lstMinHealth[0]]);
            Debug.Log("HIEP KIEM TRA RIVEN DANH THAP MAU NHAT");
        }
        else
        {
            float xnew = (arrEnemy[0].transform.position.x + arrEnemy[1].transform.position.x) / 2;
            int mflagHvsE = HeroTrue ? -200 : 200;
            float znew = arrEnemy[0].transform.position.z + mflagHvsE;
            this.transform.position = new Vector3(xnew, transform.position.y, znew); // di chuyển nhân vật
            for (int i = 0; i < t; i++)
            {
                XuLyDame(hero, arrEnemy[lstMinHealth[i]]);
            }
        }
    }
    private void BuffMucTieuThapMau(Character hero, Character[] arrHeros) // Chọn mục tiêu thấp máu nhất trong team địch để đánh
    {
        List<int> lstMinHealth = GetDsEnemyMauTangDan(arrHeros);
        int t = lstMinHealth.Count > positionAttack.Length ? positionAttack.Length : lstMinHealth.Count;
        Debug.Log("HIEP KIEM TRA RIVEN DANH THAP MAU NHAT" + t);
        // if (t == 0)
        //    return;
        if (t == 1)
        {
            //Nếu mục tiêu còn sống
            //int sflagHvsE = HeroTrue ? 100 : -100;
            //this.transform.position = new Vector3(arrHeros[lstMinHealth[0]].transform.position.x, transform.position.y, arrHeros[lstMinHealth[0]].transform.position.z + sflagHvsE);//di chuyển nhân vật
            //this.transform.rotation = new Quaternion(0, 180, 0,0);
            XuLyBuff(hero, arrHeros[lstMinHealth[0]]);
            Debug.Log("HIEP KIEM TRA RIVEN DANH THAP MAU NHAT");
        }
        else
        {
           // float xnew = (arrHeros[0].transform.position.x + arrHeros[1].transform.position.x) / 2;
           // int mflagHvsE = HeroTrue ? 200 : -200;
           // float znew = arrHeros[0].transform.position.z + mflagHvsE;
           // this.transform.position = new Vector3(xnew, transform.position.y, znew); // di chuyển nhân vật
           // this.transform.rotation = new Quaternion(0, 180, 0, 0);
            for (int i = 0; i < t; i++)
            {
                XuLyBuff(hero, arrHeros[lstMinHealth[i]]);
            }
        }
    }
    private void DanhMucTieuNhieuMau(Character hero, Character[] arrEnemy) // Chọn mục tiêu nhiều máu nhất trong team địch để đánh
    {
        List<int> lstMaxHealth = GetDsEnemyMauTangDan(arrEnemy);
        int t = lstMaxHealth.Count > positionAttack.Length ? positionAttack.Length : lstMaxHealth.Count;
        if (t == 0)
            return;
        if (t == 1)
        {
            //Nếu mục tiêu còn sống
            int sflagHvsE = HeroTrue ? -100 : 100;
            this.transform.position = new Vector3(arrEnemy[lstMaxHealth[lstMaxHealth.Count - 1]].transform.position.x, transform.position.y, arrEnemy[lstMaxHealth[lstMaxHealth.Count - 1]].transform.position.z + sflagHvsE);//di chuyển nhân vật
            XuLyDame(hero, arrEnemy[lstMaxHealth[lstMaxHealth.Count - 1]]);
            Debug.Log("HIEP KIEM TRA RIVEN DANH THAP MAU NHAT" + (lstMaxHealth.Count - 1));
        }
        else
        {
            float xnew = (arrEnemy[0].transform.position.x + arrEnemy[1].transform.position.x) / 2;
            int mflagHvsE = HeroTrue ? -200 : 200;
            float znew = arrEnemy[0].transform.position.z + mflagHvsE;
            this.transform.position = new Vector3(xnew, transform.position.y, znew); // di chuyển nhân vật
            for (int i = t - 1; i > 0; i--)
            {
                XuLyDame(hero, arrEnemy[lstMaxHealth[i]]);
            }
        }
    }
    private List<int> GetDsEnemyMauTangDan(Character[] arrEnemy)
    {
        List<int> lst = TimViTriDSMucTieuConSong(arrEnemy);
        for (int i = 0; i < lst.Count - 1; i++)
        {
            if (!arrEnemy[lst[i]].flagDie)
            {
                for (int j = i + 1; j < lst.Count; j++)
                {
                    if (!arrEnemy[lst[j]].flagDie && arrEnemy[lst[i]].curHealth > arrEnemy[lst[j]].curHealth)
                    {
                        int t = lst[i];
                        lst[i] = lst[j];
                        lst[j] = t;
                    }
                }
            }
        }
        Debug.Log("DS THAP MAU NHAT::");
        for (int i = 0; i < lst.Count; i++)
        {
            Debug.Log("-" + lst[i]);
        }
        return lst;
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
    public void OnAttack(Character[] arrEnemy, Character[] arrHeros)
    {
        if (flagCoDinh)
            DanhMucTieuCoDinh(this, arrEnemy);
        if (flagNgauNhien)
            DanhMucTieuNgauNhien(this, arrEnemy);
        if (flagNhieuMauNhat)
            DanhMucTieuNhieuMau(this, arrEnemy);
        if (flagThapMauNhat)
            DanhMucTieuThapMau(this, arrEnemy);
        if (flagBuffHoiMau)
            BuffMucTieuThapMau(this, arrHeros);
        this.switchAnimation("Attack");
    }
    public void XuLyDame(Character CharacterCong, Character CharacterThu)
    {
        CharacterThu.curHealth -= CharacterCong.dame; // xử lý tính sát thương tại đây
        CharacterThu.resetSubBlood(-CharacterCong.dame);
    }
    public void XuLyBuff(Character characterCong, Character characterDuocBuff)
    {
        float t = characterDuocBuff.curHealth + characterCong.dame;
        if (t > characterDuocBuff.maxHealth)
            characterDuocBuff.curHealth = characterDuocBuff.maxHealth;
        else characterDuocBuff.curHealth += characterCong.dame;
        characterDuocBuff.meshSubBlood.color = Color.green;
        characterDuocBuff.resetSubBlood(characterCong.dame);
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
