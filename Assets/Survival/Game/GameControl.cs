using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;

public class GameControl : MonoBehaviour
{
    public List<GameObject> Players;
    Dictionary<string, int> PlayerLookup = new Dictionary<string, int>(){
        {"Vision",0},
        {"Angelic",1},
        {"Doom",2},
        {"Zip",3},
        {"Fortress",4}
    };
    GameObject player;
    public Vector3 LastPlayer = new Vector3(0,0,0);
    public List<GameObject> InitEnemy;
    public List<GameObject> InitBackground;
    List<GameObject> enemy; 
    List<GameObject> background; 
    public bool pause;
    public GameObject cam;
    public float TimeConstant;
    public float BaseTime;
    public List<GameObject> enemys;
    public List<GameObject> entitys;
    public List<GameObject> Particles;
    public List<GameObject> backgrounds;
    List<GameObject> deco;
    List<Vector3> BackgroundCoord;
    public Vector2 PlayerVelocity;
    float SpawnCoolDown = 120f;
    float SurgeCoolDown = 1200f;
    public float SurgeDuration = 0;
    public GameObject SurgeSign;
    public float Point = 0;
    public ExitScene end;
    float Beat = 0f;
    // Start is called before the first frame update
    void Start()
    {
        //Init
        player = Players[PlayerLookup[PlayerPrefs.GetString("Choice")]];
        BaseTime = PlayerPrefs.GetFloat("StartSpeed");
        TimeConstant = BaseTime;

        //enemys = new List<GameObject>();
        entitys = new List<GameObject>();
        backgrounds = new List<GameObject>();
        Particles = new List<GameObject>();
        deco = new List<GameObject>();
        BackgroundCoord = new List<Vector3>();

        background = new List<GameObject>(){
            InitBackground[0],
            InitBackground[1],InitBackground[1],InitBackground[1],
            InitBackground[2],InitBackground[2],InitBackground[2],
            InitBackground[3],InitBackground[3],InitBackground[3],
            InitBackground[4],InitBackground[4],InitBackground[4],
        };
        enemy = new List<GameObject>(){
            InitEnemy[0],InitEnemy[0],
            InitEnemy[1],
            InitEnemy[2],
            InitEnemy[3],
            InitEnemy[4],
            InitEnemy[0],InitEnemy[1],InitEnemy[2],InitEnemy[3],
            InitEnemy[5],
            InitEnemy[6],InitEnemy[6],
            InitEnemy[7],
            InitEnemy[8],InitEnemy[8],
            InitEnemy[9],

        };
        //Spawn
        player = Instantiate(player, new Vector3(0,0,0), Quaternion.identity);
        GameObject StartBackground = Instantiate(RandomItem(background), new Vector3(0,0,0), Quaternion.identity);
        backgrounds.Add(StartBackground);
        PlayerVelocity = new Vector2(0,0);
    }
    void Calibrate(){
        if (player != null){
            if (!(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))){
                player.GetComponent<PlayerAction>().acc = new Vector2(0,0);
            }
        }
    }
    public Data GetPlayer(){
        if (player != null){
            return player.GetComponent<Data>();
        }else{
            return GetComponent<Data>();
        }
        
    }
    public void UpdatePlayerVelocity(){
        if (player != null){
            PlayerVelocity = player.GetComponent<PlayerAction>().velocity;
        }
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
    GameObject RandomItem(List<GameObject> src){
        int start = 0;
        int end = src.Count;
        int item = Random.Range(start, end);
        return src[item];
    }
    public void SetCamera(int size){
        cam.GetComponent<Camera>().orthographicSize = size;
    }
    public void GroupDisplace(Vector3 Displacement){
        foreach (var e in new List<GameObject>(enemys)){
            if (e != null){
                e.transform.position -= Displacement; 
            }else{
                enemys.Remove(e);
            }
        }
        foreach (var e in new List<GameObject>(entitys)){
            if (e != null){
                e.transform.position -= Displacement; 
            }else{
                entitys.Remove(e);
            }
        }
        foreach (var b in new List<GameObject>(backgrounds)){
            b.transform.position -= (Displacement/2f); 
        }
        foreach (var p in new List<GameObject>(Particles)){
            if (p == null){
                Particles.Remove(p);
            }else{
                p.transform.position -= Displacement; 
            }
        }
    }
    public void End(){
        end.Started = true;
    }
    // Update is called once per frame.
    void Update()
    {
        if (!pause){
            //Make sure no bugged button drift
            Calibrate();
            //Timing updates
            BaseTime += (float)0.1/60/60 * PlayerPrefs.GetFloat("TimeAcc")*2;
            Beat += TimeConstant;
            if (Beat > 120){
                GameObject.Find("EffectPlayer").GetComponent<EffectControl>().Play(21);
                Beat = 0f;
            }
            //Time Surge
            if (SurgeDuration > 0){
                SurgeDuration -= 1f;
                SurgeSign.GetComponent<TextMeshProUGUI>().color = new Color(1,0,0,1);;
            }else{
                SurgeSign.GetComponent<TextMeshProUGUI>().color = new Color(1,0,0,0);;
            }  
            if (SurgeCoolDown > 0){
                SurgeCoolDown -= BaseTime;
            }
            if (SurgeCoolDown <= 0){
                if (Random.Range(0,3601) == 0){
                    GameObject.Find("EffectPlayer").GetComponent<EffectControl>().Play(8);
                    SurgeCoolDown = 3600f;
                    SurgeDuration = 600f;
                }
            }
            //Spawn enemy
            if (SpawnCoolDown > 0){
                SpawnCoolDown -= TimeConstant*TimeConstant;
            }
            if (Random.Range(0, 1201) < TimeConstant*10f && (enemys.Count < 10*TimeConstant) && SpawnCoolDown <= 0){
                SpawnCoolDown = 120f;
                Vector3 Location = new Vector3(0,0,0);
                //Finding Coordinate
                switch(Random.Range(0,4)){
                case 0:
                    Location.x = -24f;
                    Location.y = (float)Random.Range(-11,11);
                    break;
                case 1:
                    Location.x = 24f;
                    Location.y = (float)Random.Range(-11,11);
                    break;
                case 2:
                    Location.x = (float)Random.Range(-24,24);
                    Location.y = 11f;
                    break;
                case 3:
                    Location.x = (float)Random.Range(-24,24);
                    Location.y = -11f;
                    break;
                }
                if (TimeConstant < 1.5f){
                    GameObject Adding = enemy[Random.Range(0,6)];
                    GameObject NewEnemy = Instantiate(Adding, Location, Quaternion.identity);
                    enemys.Add(NewEnemy);
                }else{
                    GameObject NewEnemy = Instantiate(RandomItem(enemy), Location, Quaternion.identity);
                    enemys.Add(NewEnemy);
                }
            }
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
                            GameObject NewBackground = Instantiate(RandomItem(background), new Vector3(coord.x-PlayerVelocity.x/2,coord.y-PlayerVelocity.y/2,0), Quaternion.identity);
                            backgrounds.Add(NewBackground);
                            UpdateBackgroundCoord();
                        }
                    }
                }
            }
            //update velocity
            foreach (var e in new List<GameObject>(enemys)){
                if (e != null){
                    e.GetComponent<EnemyAction>().UpdateVelocity(new Vector2(-PlayerVelocity.x,-PlayerVelocity.y));
                    if (Mathf.Abs(e.transform.position.x) > 2*28 || Mathf.Abs(e.transform.position.y) > 2*28){
                        enemys.Remove(e);
                        Destroy(e);
                    }
                }else{
                    enemys.Remove(e);
                }
                
            }
            foreach (var e in new List<GameObject>(entitys)){
                if (e != null){
                    e.GetComponent<BulletScript>().UpdateVelocity(new Vector2(-PlayerVelocity.x,-PlayerVelocity.y));
                    if (Mathf.Abs(e.transform.position.x) > 2*28 || Mathf.Abs(e.transform.position.y) > 2*28){
                        entitys.Remove(e);
                        Destroy(e);
                    }
                }else{
                    entitys.Remove(e);
                }
                
            }
            foreach (var b in new List<GameObject>(backgrounds)){
                b.GetComponent<SurvivalBackground>().UpdateVelocity(new Vector2(-(PlayerVelocity.x/2),-(PlayerVelocity.y/2)));
                if (Mathf.Abs(b.transform.position.x) > 2*28 || Mathf.Abs(b.transform.position.y) > 2*28){
                    backgrounds.Remove(b);
                    Destroy(b);
                }
            }
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
