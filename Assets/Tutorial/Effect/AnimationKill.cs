using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationKill : MonoBehaviour
{
    public int life;
    Vector2 velocity;
    // Start is called before the first frame update
    void Start()
    {
        velocity = new Vector2(0,0);
    }
    public void UpdateVelocity(Vector2 NewVelocity){
        velocity = NewVelocity;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x + velocity.x,transform.position.y + velocity.y,0);
        life--;
        if (life<0){
            Destroy(gameObject);
        }
    }
}
