using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyBehavior : MonoBehaviour
{
    TutorialControl control;
    public GameObject Spawn;
    public GameObject Explode;
    TextBoxScript Text;
    Vector2 velocity;
    // Start is called before the first frame update
    void Start()
    {
        velocity = new Vector2(0,0);
        control = GameObject.Find("control").GetComponent<TutorialControl>();
        Text = GameObject.Find("TextBox").GetComponent<TextBoxScript>();
    }
    void Animate(bool option){
        if (option){
            GetComponent<Animator>().speed = 1;
        }else{
            GetComponent<Animator>().speed = 0;
        }
    }
    public void UpdateVelocity(Vector2 NewVelocity){
        velocity = NewVelocity;
    }
    // Update is called once per frame
    void Update()
    {
        if (!control.pause){
            Animate(true);
            GetComponent<Rigidbody2D>().MovePosition(new Vector3(transform.position.x + velocity.x,transform.position.y + velocity.y,0)); 
            if (GetComponent<Data>().HP <= 0){
                GameObject.Find("EffectPlayer").GetComponent<EffectControl>().Play(9);
                Destroy(gameObject);
                GameObject Death = Instantiate(Explode, transform.position, Quaternion.identity);
                control.Particles.Add(Death);
                Text.Next = true;
                Text.Index++;
                Text.update();
            }
        }else{
            Animate(false);
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<Data>() != null){
            if (col.gameObject.GetComponent<Data>().Name == "Bullet1" || col.gameObject.GetComponent<Data>().Name == "Bullet2"){
                GetComponent<Data>().HP -= col.gameObject.GetComponent<Data>().Damage;
                GameObject.Find("EffectPlayer").GetComponent<EffectControl>().Play(5);
                Destroy(col.gameObject);
            }
        }
    }
}
