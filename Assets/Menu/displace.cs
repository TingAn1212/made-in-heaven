using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class displace : MonoBehaviour
{
    public int corner;
    private Vector2 Initial;
    private int width;
    private int height;
    // Start is called before the first frame update
    void Start()
    {
        Initial = GetComponent<RectTransform>().anchoredPosition;
        width = Screen.width;
        height = Screen.height;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        int range;
        if (mousePos.x >= 0 && mousePos.y >= 0 && mousePos.x <= width && mousePos.y <= height){
            float xDisplacement = 0;
            float yDisplacement = 0;
            switch(corner){
                case 1:
                    range = 10;
                    xDisplacement = (width - mousePos.x)/width;
                    yDisplacement = (mousePos.y)/height;
                    GetComponent<RectTransform>().anchoredPosition = new Vector2(Initial.x + (range*xDisplacement),Initial.y - (range*yDisplacement));
                    break;
                case 2:
                    range = 40;
                    xDisplacement = (mousePos.x)/width;
                    yDisplacement = (mousePos.y)/height;
                    GetComponent<RectTransform>().anchoredPosition = new Vector2(Initial.x + (range*xDisplacement),Initial.y + (range*yDisplacement));
                    break;
                case 3:
                    range = 60;
                    xDisplacement = (width - mousePos.x)/width;
                    yDisplacement = (height - mousePos.y)/height;
                    GetComponent<RectTransform>().anchoredPosition = new Vector2(Initial.x - (range*xDisplacement),Initial.y - (range*yDisplacement));
                    break;
                case 4:
                    range = 20;
                    xDisplacement = mousePos.x/width;
                    yDisplacement = (height - mousePos.y)/height;
                    GetComponent<RectTransform>().anchoredPosition = new Vector2(Initial.x + (range*xDisplacement),Initial.y - (range*yDisplacement));
                    break;
            }
        }else{
            GetComponent<RectTransform>().anchoredPosition = Initial;
        }
    }
}
