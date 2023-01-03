using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    GameControl control;
    Vector2 velocity;
    public Vector2 SelfVelocity;
    // Start is called before the first frame update
    void Start()
    {
        velocity = new Vector2(0,0);
        control = GameObject.Find("GameControl").GetComponent<GameControl>();
    }
    public void UpdateVelocity(Vector2 NewVelocity){
        velocity = NewVelocity;
    }
    public void SetVelocity(Vector2 NewVelocity){
        SelfVelocity = NewVelocity;
    }
    // Update is called once per frame
    void Update()
    {
        if (!control.pause){
            transform.position = new Vector3(transform.position.x + velocity.x + SelfVelocity.x*control.TimeConstant,transform.position.y + velocity.y + SelfVelocity.y*control.TimeConstant,0);
        }
    }
    void OnTriggerEnter2D(Collider2D col){
        if (GetComponent<Data>().Type == "EnemyBullet" && col.GetComponent<Data>().Type == "Shield"){
            if (col.GetComponent<Data>().Name == "Shield1"){
                Destroy(gameObject);
            }
            if (col.GetComponent<Data>().Name == "Shield2"){
                GetComponent<Data>().Type = "Bullet";
                SelfVelocity *= -1f;
            }
        }
    }
}
