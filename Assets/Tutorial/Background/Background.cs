using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    GameObject control;
    Vector2 velocity;
    // Start is called before the first frame update
    void Start()
    {
        velocity = new Vector2(0,0);
        control = GameObject.Find("control");
    }
    public void UpdateVelocity(Vector2 NewVelocity){
        velocity = NewVelocity;
    }
    // Update is called once per frame
    void Update()
    {
        if (!control.GetComponent<TutorialControl>().pause){
            transform.position = new Vector3(transform.position.x + velocity.x,transform.position.y + velocity.y,0);
        }
    }
}
