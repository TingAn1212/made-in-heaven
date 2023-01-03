using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TutorialBehavior : MonoBehaviour
{
    public TutorialControl control;
    public Vector2 velocity;
    public GameObject InvisibleEffect;
    public GameObject Bullet1;
    public GameObject Bullet2;
    public Vector2 acc;
    Dictionary<string, float> CD = new Dictionary<string, float>(){
        {"Weapon1",0},
        {"Weapon2",0},
        {"E",0},
        {"Q",0}
    };
    Dictionary<string, float> Buff = new Dictionary<string, float>(){
        {"E",0},
        {"Q",0}
    };
    // Start is called before the first frame update
    void Start()
    {
        velocity = new Vector2(0,0);
        acc = new Vector2(0,0);
    }
    Vector3 GetMouse(){
        Vector3 mousePos = Input.mousePosition;
        mousePos.x -= Screen.width/2;
        mousePos.y -= Screen.height/2;
        return mousePos;
    }
    Vector2 ScaleVector(Vector2 og, float maximum){
        float constant = (Mathf.Sqrt(Mathf.Pow(og.x,2)+Mathf.Pow(og.y,2)))/maximum;
        Vector2 NewVector = new Vector2(og.x/constant,og.y/constant);
        return NewVector;
    }
    void Animate(bool option){
        if (option){
            GetComponent<Animator>().speed = 2;
        }else{
            GetComponent<Animator>().speed = 0;
        }
    }
    void ReadMouse(){
        if (Input.GetMouseButtonDown(0))
        {
            if (CD["Weapon1"] == 0){
                Vector3 MousePos = GetMouse();
                GameObject NewEntity = Instantiate(Bullet1, new Vector3(0,0,0), Quaternion.identity);
                NewEntity.GetComponent<Bullet1Behavior>().SetVelocity(ScaleVector(new Vector2(MousePos.x,MousePos.y),(float)0.65));
                control.entitys.Add(NewEntity);
                GameObject.Find("EffectPlayer").GetComponent<EffectControl>().Play(12);
                CD["Weapon1"] = 10;
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (CD["Weapon2"] == 0){
                Vector3 MousePos = GetMouse();
                GameObject NewEntity = Instantiate(Bullet2, new Vector3(0,0,0), Quaternion.identity);
                NewEntity.GetComponent<Bullet1Behavior>().SetVelocity(ScaleVector(new Vector2(MousePos.x,MousePos.y),(float)0.3));
                control.entitys.Add(NewEntity);
                GameObject.Find("EffectPlayer").GetComponent<EffectControl>().Play(13);
                CD["Weapon2"] = 60;
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (CD["E"] == 0){
                GameObject.Find("EffectPlayer").GetComponent<EffectControl>().Play(11);
                Buff["E"] = (float)300;
                CD["E"] = (float)480;
            }
        }
        if (Input.GetKeyDown(KeyCode.Q)){
            if (CD["Q"] == 0){
                GameObject.Find("EffectPlayer").GetComponent<EffectControl>().Play(11);
                Buff["Q"] = (float)600;
                CD["Q"] = (float)1200;
                GameObject effect = Instantiate(InvisibleEffect, new Vector3(0,0,0), Quaternion.identity);
                control.Particles.Add(effect);
            }
        }
    }
    void ReadWASD(){
        float increase = (float)0.003;
        //W
        if (Input.GetKeyDown(KeyCode.W))
        {
            acc.y += increase;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            acc.y -= increase;
        }
        //S
        if (Input.GetKeyDown(KeyCode.S))
        {
            acc.y += -increase;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            acc.y -= -increase;
        }
        //D
        if (Input.GetKeyDown(KeyCode.D))
        {
            acc.x += increase;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            acc.x -= increase;
        }
        //A
        if (Input.GetKeyDown(KeyCode.A))
        {
            acc.x += -increase;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            acc.x -= -increase;
        }
    }
    void CheckCD(){
        float decrease = 1 * control.TimeConstant;
        foreach(KeyValuePair<string, float> data in new Dictionary<string,float>(CD)){
            if (data.Value - decrease > 0){
                CD[data.Key] -= decrease;
            }else{
                CD[data.Key] = 0;
            }
        }
        foreach(KeyValuePair<string, float> data in new Dictionary<string,float>(Buff)){
            if (data.Value - decrease > 0){
                Buff[data.Key] -= decrease;
            }else{
                Buff[data.Key] = 0;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        ReadWASD();
        if (!control.pause){
            ReadMouse();
            CheckCD();
            //Update Animation
            Animate(true);
            //Apply buff
            float SpeedBoost = (float)1;
            if (Buff["E"] > 0){
                SpeedBoost = (float)1.6;
            }
            int CamSize = 5;
            if (Buff["Q"] > 0){
                CamSize = 10;
                GetComponent<SpriteRenderer>().color = new Color(255,255,255,0);
            }else{
                GetComponent<SpriteRenderer>().color = new Color(255,255,255,255);
            }
            control.SetCamera(CamSize);
            //Velocity setting
            float maxVelocity = (float)0.25 * control.TimeConstant * SpeedBoost;
            float friction = (float)0.0015 * control.TimeConstant;
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
            //rotate to mouse
            Vector3 mousePos = GetMouse();
            Vector2 current = transform.position;
            var direction = new Vector2(mousePos.x,mousePos.y) - current;
            var angle = Mathf.Atan2(direction.y, direction.x)*Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }else{
            //Update Animation
            Animate(false);
        }
    }
}
