using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonClick : MonoBehaviour
{
    public string target;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void ChangeScene(){
        GameObject.Find("EffectPlayer").GetComponent<EffectControl>().Play(0);
        SceneManager.LoadScene(target);
    }

}
