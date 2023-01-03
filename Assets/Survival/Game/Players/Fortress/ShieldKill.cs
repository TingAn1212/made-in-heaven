using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldKill : MonoBehaviour
{
    public float life;
    GameControl control;
    Vector2 velocity;
    // Start is called before the first frame update
    void Start()
    {
        velocity = new Vector2(0,0);
        control = GameObject.Find("GameControl").GetComponent<GameControl>();
    }
    public void UpdateVelocity(Vector2 NewVelocity){
        velocity = NewVelocity;
    }
    // Update is called once per frame
    void Update()
    {
        if (!control.pause){
            transform.position = new Vector3(transform.position.x + velocity.x,transform.position.y + velocity.y,0);
            life -= control.TimeConstant;
            if (life<0){
                GameObject.Find("EffectPlayer").GetComponent<EffectControl>().Play(19);
                Destroy(gameObject);
            }
        }
    }
}
