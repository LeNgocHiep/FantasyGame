using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heros : MonoBehaviour
{

    public float maxHealth;
    public float curHealth;
    public GameObject health_bar;

    public int dame;
    public int vitri; //vị trí đứng trong team 
    //public int[] location_attack; //vi tri quan dich Hero nay tan cong
    Animation anim;
    Vector3 default_location;
    Rigidbody rigidbody;
    float enemySpeed = 30f;
    string state = "Idle";
    float stateTime = 0;  //time animation 
    //public GameObject[] ObjTaget; //Danh sách mục tiêu được chọn 
    // Use this for initialization
    void Start()
    {
        curHealth = maxHealth;
        InvokeRepeating("decreaseHealth", 1f, 1f);

        anim = GetComponent<Animation>();
        rigidbody = GetComponent<Rigidbody>();
        default_location = this.transform.position;
    }
    float speed = 300f;
    // Update is called once per frame
    void Update()
    {
        if (!state.Equals("Idle") && stateTime > 0)
        {
            this.stateTime -= Time.deltaTime;
            if (stateTime <= 0)
            {
                if(state.Equals("Die"))
                    Destroy(this.gameObject, 2.3f);
                this.switchAnimation("Idle");
                this.transform.position = default_location;
                Debug.Log("Auto Hiep:" );
            }
        }
        switchAnimation(state);
        this.checkHeroDie();
        //this.OnStay(state);
        //this.OnMove(new Vector3(transform.position.x,transform.position.y, transform.position.z + speed * Time.deltaTime));

    }
    void checkHeroDie()
    {
        if (curHealth <= 0)
        {
            this.switchAnimation("Die");
        }
    }
    public void OnStay(string action)
    {
        anim.clip = anim.GetClip(action);
        anim.Play();
        //Debug.Log("Time Idle:  "+ anim.clip.length);
    }
    //public void OnAttack(GameObject[] lstObjTaget)
    //{
    //    for (int i = 0; i < lstObjTaget.Length; i++)
    //    {      
    //        Vector3 vecterLenght = transform.position - lstObjTaget[i].transform.position;
    //        float lenght = vecterLenght.magnitude; // xác định chiều dài vector
    //        vecterLenght.Normalize();
    //        Vector3 velocity = vecterLenght * enemySpeed;
    //        if (lenght > 0)
    //        {
    //            //Move Hero
    //            rigidbody.velocity = new Vector3(velocity.x, rigidbody.velocity.y, velocity.z);


    //            transform.velocity = new Vector3(velocity.x, enemyRB.velocity.y, velocity.z);
    //            transform.LookAt()
    //            anim.clip = anim.GetClip("Attack");
    //            anim.Play();
    //            anim.Play();
    //        }

    //    }
    //}
    public void OnAttack(Object obj)
    {
        Enemy enemy = (Enemy)obj;
        //this.transform.position = enemy.transform.position;
        this.transform.position = new Vector3(enemy.transform.position.x, enemy.transform.position.y + 30, enemy.transform.position.z - 50);
        enemy.playerAttack(this.dame);
        this.switchAnimation("Attack");
        //this.BeingDamage(1);

    }
    //public void BeingDamage(float damage)
    //{
    //    HerosHealthBar.cur_Health -= damage;
    //}
    public void OnMove(Vector3 vector3)
    {
        this.transform.position = vector3;
        anim.clip = anim.GetClip("Run");
        anim.Play();
    }
    void switchAnimation(string state)
    {
        if(this.state!= state)
            this.stateTime = anim.clip.length;
        this.state = state;
        anim.clip = anim.GetClip(state);
        anim.Play();      
    }
    void decreaseHealth()
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
