using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterAction1 : MonoBehaviour
{
    public float RotationSpeed;
    public float MoveSpeed;
    float Speed;
    Vector2 direction;
    GameControl control;
    public float CD;
    float CoolDown;
    float tick = 1;
    public GameObject Bullet;
    public float BulletSpeed;
    public float Range;
    // Start is called before the first frame update
    void Start()
    {
        control = GameObject.Find("GameControl").GetComponent<GameControl>();
        CoolDown = CD;
    }
    Vector2 ScaleVector(Vector2 og, float maximum){
        float constant = (Mathf.Sqrt(Mathf.Pow(og.x,2)+Mathf.Pow(og.y,2)))/maximum;
        Vector2 NewVector = new Vector2(og.x/constant,og.y/constant);
        return NewVector;
    }
    void Shoot(){
        GameObject.Find("EffectPlayer").GetComponent<EffectControl>().Play(6);
        GameObject bullet = Instantiate(Bullet, transform.position, Quaternion.identity);
        Vector3 target = control.LastPlayer - transform.position;
        bullet.GetComponent<BulletScript>().SetVelocity(ScaleVector(new Vector2(target.x,target.y),BulletSpeed));
        control.entitys.Add(bullet);
    }
    // Update is called once per frame
    void Update()
    {
        if (!control.pause){
            MoveSpeed = MoveSpeed * control.TimeConstant;

            CoolDown -= tick*control.TimeConstant;

            direction = control.LastPlayer - transform.position;
            float angle = Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle,Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, RotationSpeed*Time.deltaTime);

            Vector2 TargetVelo = Vector2.MoveTowards(transform.position, control.LastPlayer, Speed*Time.deltaTime);
            Vector2 PlayerVelo = GetComponent<EnemyAction>().velocity;
            if (Vector3.Distance(control.LastPlayer,transform.position) < Range){
                TargetVelo = new Vector2(0,0); 
                GetComponent<Rigidbody2D>().MovePosition(new Vector2(transform.position.x+TargetVelo.x+PlayerVelo.x, transform.position.y+TargetVelo.y+PlayerVelo.y));

                if (CoolDown <= 0){
                    if (Vector3.Distance(control.LastPlayer, new Vector3(0,0,0)) < 0.5f){
                        CoolDown = CD;
                        Shoot();
                    }else{
                        CoolDown = CD;
                    }
                }
            }else{
                GetComponent<Rigidbody2D>().MovePosition(new Vector2(TargetVelo.x+PlayerVelo.x, TargetVelo.y+PlayerVelo.y));
            }
        }
    }
}
