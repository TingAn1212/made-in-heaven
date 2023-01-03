using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    GameControl control;
    public GameObject Explode;
    public Vector2 velocity;
    public Vector2 acc;
    // Start is called before the first frame update
    void Start()
    {
        control = GameObject.Find("GameControl").GetComponent<GameControl>();
        velocity = new Vector2(0,0);
        acc = new Vector2(0,0);
    }

    public void PerUpdate(float maxVelocity, float friction){
        if (control.SurgeDuration > 0){
            control.TimeConstant = control.BaseTime*2f;
        }
        //update velo
        velocity.x += acc.x * control.TimeConstant;
        velocity.y += acc.y * control.TimeConstant;
        //check velo
        if (velocity.x > maxVelocity){
            velocity.x = maxVelocity;
        }
        if (velocity.x < -maxVelocity){
            velocity.x = -maxVelocity;
        }
        if (velocity.y > maxVelocity){
            velocity.y = maxVelocity;
        }
        if (velocity.y < -maxVelocity){
            velocity.y = -maxVelocity;
        }
        //apply friction
        if (acc.x == 0 || (velocity.x > 0 && acc.x < 0) || (velocity.x < 0 && acc.x > 0)){
            if (velocity.x > (float)0){
                velocity.x -= friction;
                if (velocity.x < (float)0){
                        velocity.x = 0;
                }
            }else if(velocity.x < (float)0){
                velocity.x += friction;
                if (velocity.x > (float)0){
                    velocity.x = 0;
                }
            }
        }
        if (acc.y == 0 || (velocity.y > 0 && acc.y < 0) || (velocity.y < 0 && acc.y > 0)){
            if (velocity.y > (float)0){
                velocity.y -= friction;
                if (velocity.y < (float)0){
                        velocity.y = 0;
                }
            }else if(velocity.y < (float)0){
                velocity.y += friction;
                if (velocity.y > (float)0){
                    velocity.y = 0;
                }
            }
        }

        //Check for death
        if (GetComponent<Data>().HP <= 0){
            GameObject.Find("EffectPlayer").GetComponent<EffectControl>().Play(9);
            PlayerPrefs.SetFloat("Point",control.Point);
            Instantiate(Explode, new Vector3(0,0,0), Quaternion.identity);
            Destroy(gameObject);
            control.End();
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<Data>() != null && (col.gameObject.GetComponent<Data>().Type == "Enemy" || col.gameObject.GetComponent<Data>().Type == "EnemyBullet")){
            GameObject.Find("EffectPlayer").GetComponent<EffectControl>().Play(5);
            GetComponent<Data>().HP -= (col.gameObject.GetComponent<Data>().Damage * GetComponent<Data>().Defence) - GetComponent<Data>().Reduction;
            if (col.gameObject.GetComponent<Data>().Type == "EnemyBullet"){
                Destroy(col.gameObject);
            }
        }
        if (col.gameObject.GetComponent<Data>() != null && col.gameObject.GetComponent<Data>().Name == "HealthOrb" && GetComponent<Data>().HP < 100){
            GameObject.Find("EffectPlayer").GetComponent<EffectControl>().Play(14);
            GetComponent<Data>().HP += col.gameObject.GetComponent<Data>().Damage * GetComponent<Data>().Heal * control.TimeConstant;
            Destroy(col.gameObject);
        }
        if (GetComponent<Data>().HP > 100){
            GetComponent<Data>().HP = 100f;
        }
    }
}
