using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bite : MonoBehaviour
{
    public float RotationSpeed;
    public float MoveSpeed;
    float Speed;
    Vector2 direction;
    GameControl control;
    // Start is called before the first frame update
    void Start()
    {
        control = GameObject.Find("GameControl").GetComponent<GameControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!control.pause){
            Speed = MoveSpeed * control.TimeConstant;

            direction = control.LastPlayer - transform.position;
            float angle = Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle,Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, RotationSpeed*Time.deltaTime);

            Vector2 TargetVelo = Vector2.MoveTowards(transform.position, control.LastPlayer, Speed*Time.deltaTime);
            Vector2 PlayerVelo = GetComponent<EnemyAction>().velocity;
            GetComponent<Rigidbody2D>().MovePosition(new Vector2(TargetVelo.x+PlayerVelo.x, TargetVelo.y+PlayerVelo.y));
        }
    }
}
