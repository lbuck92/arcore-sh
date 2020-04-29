using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Text_Display : MonoBehaviour
{
    Text displayText;

    void Awake(){
        displayText = this.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        showText(Object_Manager.active_obj);
    }

    void showText(GameObject obj){

        displayText.text = "find the ";
        if(obj.name == "apples(Clone)") displayText.text += "apple basket";
        else if (obj.name == "gold(Clone)") displayText.text += "pot of gold";
        else if (obj.name == "avocado(Clone)") displayText.text += "avocado";
        else if (obj.name == "bonsai(Clone)") displayText.text += "bonsai";
        else if (obj.name == "doggo(Clone)") displayText.text += "dog";
        else if (obj.name == "flowers(Clone)") displayText.text += "flowers";
        else if (obj.name == "pug(Clone)") displayText.text += "pug";
        else if (obj.name == "mouse(Clone)") displayText.text += "mouse";
        else if (obj.name == "mushrooms(Clone)") displayText.text += "mushrooms";
        else if (obj.name == "plant-shelf(Clone)") displayText.text += "shelf of plants";

    }
}
