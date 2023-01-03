using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAction : MonoBehaviour
{
    GameControl control;
    public GameObject Death;
    public GameObject Orb;
    public Vector2 velocity;
    // Start is called before the first frame update
    void Start()
    {
        velocity = new Vector2(0,0);
        control = GameObject.Find("GameControl").GetComponent<GameControl>();
        GetComponent<Data>().HP *= control.BaseTime;
    }
    public void UpdateVelocity(Vector2 NewVelocity){
        velocity = NewVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Data>().HP <=0){
            GameObject.Find("EffectPlayer").GetComponent<EffectControl>().Play(10);
            //Kill
            GameObject death = Instantiate(Death,transform.position,Quaternion.identity);
            control.Particles.Add(death);
            Destroy(gameObject);
            //Spawn Orb
            GameObject orb = Instantiate(Orb,transform.position,Quaternion.identity);
            orb.GetComponent<BulletScript>().SetVelocity(new Vector2(0,0));
            control.entitys.Add(orb);
            //Add points
            float point = GetComponent<Data>().point*control.TimeConstant;
            control.Point += point;
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<Data>() != null && (col.gameObject.GetComponent<Data>().Type == "Player" || col.gameObject.GetComponent<Data>().Type == "Bullet")){
            GameObject.Find("EffectPlayer").GetComponent<EffectControl>().Play(7);
            GetComponent<Data>().HP -= (col.gameObject.GetComponent<Data>().Damage * GetComponent<Data>().Defence) - GetComponent<Data>().Reduction;
            if (col.gameObject.GetComponent<Data>().Type == "Bullet"){
                Destroy(col.gameObject);
            }
        }
    }
}
