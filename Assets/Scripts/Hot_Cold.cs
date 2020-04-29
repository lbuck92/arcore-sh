using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hot_Cold : MonoBehaviour
{
    public static GameObject currentObject;
    public static bool cueOn = true, evenOn = false;
    public Image sprite;
    float curAngle, curDistance = 0.0f;
    Vector3 targetDirection;
    Color emission;
    public Color red, blue, ltred, ltblue;

    void Update()
    {
        currentObject = Object_Manager.active_obj;
        //get current angle from camera to desired object
        targetDirection = currentObject.transform.position - Camera.main.transform.position;
        curAngle = Vector3.Angle(targetDirection,Camera.main.transform.forward);
        //get current distance from camera to desired object
        //curDistance = Vector3.Distance(Camera.main.transform.position,currentObject.transform.position);
        
        setCue(Object_Manager.tapCount);

        //change the color of the object based on the orientation of the camera
        if(cueOn) setColor(curAngle);
        else sprite.color = new Color32(255,255,255,0);

    }

    void setColor(float angle){

        if(curAngle <= 35f) emission = red;
        else if(curAngle <= 50f) emission = ltred;
        else if (curAngle <= 75f) emission = ltblue;
        else if (curAngle > 85f) emission = blue;
        
        sprite.color = Color.Lerp(sprite.color, emission, 0.1f);

    }

    public static void setCue(int trial){

        if(evenOn && (trial % 2 == 0)) cueOn = true;
        else if (!evenOn && (trial % 2 != 0)) cueOn = true;
        else cueOn = false;     

    }

}
