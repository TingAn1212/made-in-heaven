using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterBullet3Extra : MonoBehaviour
{
    public float OriginalSpeed;
    float CurrentSpeed;
    // Start is called before the first frame update
    void Start(){
        CurrentSpeed = OriginalSpeed;
    }
    Vector2 ScaleVector(Vector2 og, float maximum){
        float constant = (Mathf.Sqrt(Mathf.Pow(og.x,2)+Mathf.Pow(og.y,2)))/maximum;
        Vector2 NewVector = new Vector2(og.x/constant,og.y/constant);
        return NewVector;
    }

    // Update is called once per frame
    void Update()
    {
        CurrentSpeed += 0.01f;
        Vector2 Start = GetComponent<BulletScript>().SelfVelocity; 
        GetComponent<BulletScript>().SelfVelocity = ScaleVector(Start,CurrentSpeed);
    }
}
