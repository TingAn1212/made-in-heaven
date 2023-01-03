using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceControl : MonoBehaviour
{
    List<GameObject> backgrounds;
    public GameObject background;
    public Button StartButton;
    List<Vector3> BackgroundCoord;
    public string Tag;
    Vector3 velocity;
    // Start is called before the first frame update
    void Start()
    {
        velocity = new Vector3(Random.Range(-0.1f, 0.1f),Random.Range(-0.1f, 0.1f),0);
        backgrounds = new List<GameObject>();
        BackgroundCoord = new List<Vector3>();
        GameObject StartBackground = Instantiate(background, new Vector3(0,0,0), Quaternion.identity);
        backgrounds.Add(StartBackground);
        StartButton.Select();
    }
    bool include(List<Vector3> src, Vector3 check){
        foreach(var item in src){
            if (Vector3.Distance(item, check) < 6){
                return true;
            }
        }
        return false;
    }
    void UpdateBackgroundCoord(){
        List<Vector3> newB = new List<Vector3>();
        foreach (var b in backgrounds){
            newB.Add(b.transform.position);
        }
        BackgroundCoord = newB;
    }
    // Update is called once per frame
    void Update()
    {
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
                        GameObject NewBackground = Instantiate(background, new Vector3(coord.x-velocity.x/2,coord.y-velocity.y/2,0), Quaternion.identity);
                        backgrounds.Add(NewBackground);
                        UpdateBackgroundCoord();
                    }
                }
            }
        }
        foreach (var b in new List<GameObject>(backgrounds)){
            b.GetComponent<ChoiceBackground>().UpdateVelocity(new Vector2(-(velocity.x/2),-(velocity.y/2)));
            if (Mathf.Abs(b.transform.position.x) > 2*28 || Mathf.Abs(b.transform.position.y) > 2*28){
                backgrounds.Remove(b);
                Destroy(b);
            }
        }
    }
}
