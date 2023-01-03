using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZipAction : MonoBehaviour
{
    GameControl control;
    public GameObject TimeEffect;
    public GameObject Bullet1;
    public GameObject Bullet2;
    public GameObject GunPoint1;
    public GameObject GunPoint2;
    Vector3 Front = new Vector3(0,1,0);
    Dictionary<string, float> CD = new Dictionary<string, float>(){
        {"Weapon1",0},
        {"Weapon2",0},
        {"E",0},
        {"Q",0}
    };
    Dictionary<string, float> Buff = new Dictionary<string, float>(){
        {"Q",0}
    };
    // Start is called before the first frame update
    void Start()
    {
        control = GameObject.Find("GameControl").GetComponent<GameControl>();
    }
    Vector3 GetMouse(){
        Vector3 mousePos = Input.mousePosition;
        // mousePos.x -= Screen.width/2;
        // mousePos.y -= Screen.height/2;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
    void UpdateFront(){
        Vector2 velo = GetComponent<PlayerAction>().velocity;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)){
            Front = new Vector3(velo.x,velo.y,0);
        }
    }
    Vector2 ScaleVector(Vector2 og, float maximum){
        float constant = (Mathf.Sqrt(Mathf.Pow(og.x,2)+Mathf.Pow(og.y,2)))/maximum;
        Vector2 NewVector = new Vector2(og.x/constant,og.y/constant);
        return NewVector;
    }
    void Animate(bool option){
        if (option){
            GetComponent<Animator>().speed = 1;
        }else{
            GetComponent<Animator>().speed = 0;
        }
    }
    void ReadMouse(){
        if (Input.GetMouseButtonDown(0))
        {
            if (CD["Weapon1"] == 0){
                GameObject.Find("EffectPlayer").GetComponent<EffectControl>().Play(16);
                GameObject NewEntity = Instantiate(Bullet1, GunPoint1.transform.position, Quaternion.identity);
                NewEntity.GetComponent<BulletScript>().SetVelocity(ScaleVector(new Vector2(Front.x,Front.y),(float)0.8));
                control.entitys.Add(NewEntity);
                NewEntity = Instantiate(Bullet1, GunPoint2.transform.position, Quaternion.identity);
                NewEntity.GetComponent<BulletScript>().SetVelocity(ScaleVector(new Vector2(Front.x,Front.y),(float)0.8));
                control.entitys.Add(NewEntity);
                CD["Weapon1"] = 10;
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (CD["Weapon2"] == 0){
                GameObject.Find("EffectPlayer").GetComponent<EffectControl>().Play(13);
                GameObject NewEntity = Instantiate(Bullet2, GunPoint1.transform.position+((GunPoint2.transform.position-GunPoint1.transform.position)/2f), Quaternion.identity);
                NewEntity.GetComponent<BulletScript>().SetVelocity(ScaleVector(new Vector2(Front.x,Front.y),(float)0.8));
                control.entitys.Add(NewEntity);
                CD["Weapon2"] = 20;
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (CD["E"] == 0){
                GameObject.Find("EffectPlayer").GetComponent<EffectControl>().Play(11);
                GameObject effect = Instantiate(TimeEffect, new Vector3(0,0,0), Quaternion.identity);
                control.Particles.Add(effect);
                //Code for teleportation
                Vector3 Target = GetMouse();
                control.GroupDisplace(Target);
                effect = Instantiate(TimeEffect, new Vector3(0,0,0), Quaternion.identity);
                control.Particles.Add(effect);
                CD["E"] = (float)300;
            }
        }
        if (Input.GetKeyDown(KeyCode.Q)){
            if (CD["Q"] == 0){
                GameObject.Find("EffectPlayer").GetComponent<EffectControl>().Play(11);
                GameObject effect = Instantiate(TimeEffect, new Vector3(0,0,0), Quaternion.identity);
                control.Particles.Add(effect);
                Buff["Q"] = (float)600;
                CD["Q"] = (float)1200;
            }
        }
    }
    void ReadWASD(){
        float increase = (float)0.007;
        //W
        if (Input.GetKeyDown(KeyCode.W))
        {
            GetComponent<PlayerAction>().acc.y += increase;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            GetComponent<PlayerAction>().acc.y -= increase;
        }
        //S
        if (Input.GetKeyDown(KeyCode.S))
        {
            GetComponent<PlayerAction>().acc.y += -increase;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            GetComponent<PlayerAction>().acc.y -= -increase;
        }
        //D
        if (Input.GetKeyDown(KeyCode.D))
        {
            GetComponent<PlayerAction>().acc.x += increase;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            GetComponent<PlayerAction>().acc.x -= increase;
        }
        //A
        if (Input.GetKeyDown(KeyCode.A))
        {
            GetComponent<PlayerAction>().acc.x += -increase;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            GetComponent<PlayerAction>().acc.x -= -increase;
        }
    }
    void CheckCD(){
        float decrease = 1 * control.BaseTime;
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
            UpdateFront();
            ReadMouse();
            CheckCD();
            control.SetCamera(8);
            //Update Animation
            Animate(true);
            //Apply buff
            if (Buff["Q"] > 0){
                if (control.SurgeDuration <= 0){
                    control.TimeConstant = control.BaseTime * 0.5f;
                }
            }else{
                if (control.SurgeDuration <= 0){
                    control.TimeConstant = control.BaseTime;
                }
            }
            //Velocity setting
            float maxVelocity = (float)0.225 * control.TimeConstant;
            float friction = (float)0.004 * control.TimeConstant;
            //update action
            GetComponent<PlayerAction>().PerUpdate(maxVelocity,friction);
            //rotate to mouse
            Vector3 TargetPos = ScaleVector(Front,1f);
            Vector2 current = transform.position;
            var direction = new Vector2(TargetPos.x,TargetPos.y) - current;
            var angle = Mathf.Atan2(direction.y, direction.x)*Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }else{
            //Update Animation
            Animate(false);
        }
    }
}
