using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TutorialControl : MonoBehaviour
{
    public TutorialBehavior player;
    public TextBoxScript TextBox;
    public GameObject enemy;
    public GameObject background;
    public bool pause;
    public GameObject cam;
    public float TimeConstant = (float)1;
    List<GameObject> enemys;
    public List<GameObject> entitys;
    public List<GameObject> Particles;
    List<GameObject> backgrounds;
    List<GameObject> deco;
    List<Vector3> BackgroundCoord;
    Vector2 PlayerVelocity;
    // Start is called before the first frame update
    void Start()
    {
        //Init
        enemys = new List<GameObject>();
        entitys = new List<GameObject>();
        backgrounds = new List<GameObject>();
        Particles = new List<GameObject>();
        deco = new List<GameObject>();
        BackgroundCoord = new List<Vector3>();
        GameObject StartBackground = Instantiate(background, new Vector3(0,0,0), Quaternion.identity);
        backgrounds.Add(StartBackground);
        PlayerVelocity = new Vector2(0,0);
    }
    public void UpdatePlayerVelocity(){
        PlayerVelocity = player.velocity;
    }
    public TutorialBehavior GetPlayer(){
        return player;
    }
    public void SpawnDummy(){
        GameObject.Find("EffectPlayer").GetComponent<EffectControl>().Play(2);
        Vector3 NewCoord = new Vector3(player.velocity.x*30,player.velocity.y*30,0);
        GameObject NewEnemy = Instantiate(enemy, NewCoord, Quaternion.identity);
        GameObject Spawn = NewEnemy.GetComponent<DummyBehavior>().Spawn;
        enemys.Add(NewEnemy);
        GameObject Effect = Instantiate(Spawn, NewCoord, Quaternion.identity);
        Particles.Add(Effect);
    }
    void UpdateBackgroundCoord(){
        List<Vector3> newB = new List<Vector3>();
        foreach (var b in backgrounds){
            newB.Add(b.transform.position);
        }
        BackgroundCoord = newB;
    }
    bool include(List<Vector3> src, Vector3 check){
        foreach(var item in src){
            if (Vector3.Distance(item, check) < 6){
                return true;
            }
        }
        return false;
    }
    int nearest(int val,int multiple){
        int rem = val % multiple;
        int result = val - rem;
        if (rem >= (multiple / 2))
            result += multiple;
        return result;
    }
    public void SetCamera(int size){
        cam.GetComponent<Camera>().orthographicSize = size;
    }
    // Update is called once per frame.
    void Update()
    {
        if (!pause){
            //testing
            
            //Run updates
            UpdatePlayerVelocity();
            UpdateBackgroundCoord();
            //Spawn background
            foreach (var b in new List<GameObject>(backgrounds)){
                Vector3 current = b.transform.position;
                int size = 28;//*4
                List<Vector3> checklist = new List<Vector3>();

                checklist.Add(new Vector3(current.x-size,current.y+size,0));
                checklist.Add(new Vector3(current.x,current.y+size,0));
                checklist.Add(new Vector3(current.x+size,current.y+size,0));

                checklist.Add(new Vector3(current.x-size,current.y,0));
                checklist.Add(new Vector3(current.x+size,current.y,0));

                checklist.Add(new Vector3(current.x-size,current.y-size,0));
                checklist.Add(new Vector3(current.x,current.y-size,0));
                checklist.Add(new Vector3(current.x+size,current.y-size,0));

                foreach (var coord in checklist){
                    if (Mathf.Abs(coord.x) < (size*1.5) && Mathf.Abs(coord.y) < (size*1.5)){
                        if (!include(BackgroundCoord,coord)){
                            GameObject NewBackground = Instantiate(background, new Vector3(coord.x-PlayerVelocity.x/2,coord.y-PlayerVelocity.y/2,0), Quaternion.identity);
                            backgrounds.Add(NewBackground);
                            UpdateBackgroundCoord();
                        }
                    }
                }
            }
            //update velocity
            foreach (var e in new List<GameObject>(enemys)){
                if (e != null){
                    e.GetComponent<DummyBehavior>().UpdateVelocity(new Vector2(-PlayerVelocity.x,-PlayerVelocity.y));
                    if (Mathf.Abs(e.transform.position.x) > 2*28 || Mathf.Abs(e.transform.position.y) > 2*28){
                        enemys.Remove(e);
                        Destroy(e);
                        SpawnDummy();
                    }
                }else{
                    enemys.Remove(e);
                }
                
            }
            foreach (var e in new List<GameObject>(entitys)){
                if (e != null){
                    e.GetComponent<Bullet1Behavior>().UpdateVelocity(new Vector2(-PlayerVelocity.x,-PlayerVelocity.y));
                    if (Mathf.Abs(e.transform.position.x) > 2*28 || Mathf.Abs(e.transform.position.y) > 2*28){
                        entitys.Remove(e);
                        Destroy(e);
                    }
                }else{
                    entitys.Remove(e);
                }
                
            }
            foreach (var b in new List<GameObject>(backgrounds)){
                b.GetComponent<Background>().UpdateVelocity(new Vector2(-(PlayerVelocity.x/2),-(PlayerVelocity.y/2)));
                if (Mathf.Abs(b.transform.position.x) > 2*28 || Mathf.Abs(b.transform.position.y) > 2*28){
                    backgrounds.Remove(b);
                    Destroy(b);
                }
            }
            // foreach (var b in new List<GameObject>(deco)){
            //     b.GetComponent<Background>().UpdateVelocity(new Vector2(-(PlayerVelocity.x/2),-(PlayerVelocity.y/2)));
            //     if (Mathf.Abs(b.transform.position.x) > 2*28 || Mathf.Abs(b.transform.position.y) > 2*28){
            //         backgrounds.Remove(b);
            //         Destroy(b);
            //     }
            // }
            foreach (var p in new List<GameObject>(Particles)){
                if (p == null){
                    Particles.Remove(p);
                }else{
                    p.GetComponent<AnimationKill>().UpdateVelocity(new Vector2(-(PlayerVelocity.x),-(PlayerVelocity.y)));
                }
            }
        }
    }
}
