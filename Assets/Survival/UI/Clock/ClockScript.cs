using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockScript : MonoBehaviour
{
    public GameControl control;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!control.pause){
            Quaternion old = transform.rotation;
            transform.Rotate(0, 0, old.z + (speed*control.TimeConstant*-2), Space.Self);
        }
    }
}
