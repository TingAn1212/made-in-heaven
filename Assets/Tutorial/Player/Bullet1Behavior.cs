using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet1Behavior : MonoBehaviour
{
    // Start is called before the first frame update
    TutorialControl control;
    Vector2 velocity;
    Vector2 SelfVelocity;
    // Start is called before the first frame update
    void Start()
    {
        velocity = new Vector2(0,0);
        control = GameObject.Find("control").GetComponent<TutorialControl>();
    }
    void Animate(bool option){
        if (option){
            GetComponent<Animator>().speed = 1;
        }else{
            GetComponent<Animator>().speed = 0;
        }
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
            Animate(true);
            transform.position = new Vector3(transform.position.x + velocity.x + SelfVelocity.x*control.TimeConstant,transform.position.y + velocity.y + SelfVelocity.y*control.TimeConstant,0);
        }else{
            Animate(false);
        }
        
    }
}
