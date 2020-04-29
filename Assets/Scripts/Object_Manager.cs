using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class Object_Manager : MonoBehaviour
{

    public GameObject apple_basket, pot_of_gold, doggo, mouse, avocado, flowers, plant_shelf, mushrooms, pug, bonsai; 
    GameObject inst0, inst1, inst2, inst3, inst4, inst5, inst6, inst7, inst8, inst9;
    Pose pose0, pose1, pose2, pose3, pose4, pose5, pose6, pose7, pose8, pose9;
    Anchor anchor0, anchor1, anchor2, anchor3, anchor4, anchor5, anchor6, anchor7, anchor8, anchor9;
    public static GameObject active_obj;
    bool obj_placed = false;
    public static int tapCount = 0;
    float timeElapsed;
    //Anchor anchor;
    //Pose pose;   
    GameObject[] objArray = new GameObject[10];
    GameObject[] insArray = new GameObject[10];
    Pose[] posArray = new Pose[10];
    Anchor[] ancArray = new Anchor[10];
       
    void Awake(){
        
        timeElapsed = Time.realtimeSinceStartup;

        objArray[0] = apple_basket;
        objArray[1] = pot_of_gold;
        objArray[2] = doggo;
        objArray[3] = mouse;
        objArray[4] = avocado;
        objArray[5] = flowers;
        objArray[6] = plant_shelf;
        objArray[7] = mushrooms;
        objArray[8] = pug;
        objArray[9] = bonsai;

        insArray[0] = inst0;
        insArray[1] = inst1;
        insArray[2] = inst2;
        insArray[3] = inst3;
        insArray[4] = inst4;
        insArray[5] = inst5;
        insArray[6] = inst6;
        insArray[7] = inst7;
        insArray[8] = inst8;
        insArray[9] = inst9;

        posArray[0] = pose0;
        posArray[1] = pose1;
        posArray[2] = pose2;
        posArray[3] = pose3;
        posArray[4] = pose4;
        posArray[5] = pose5;
        posArray[6] = pose6;
        posArray[7] = pose7;
        posArray[8] = pose8;
        posArray[9] = pose9;

        ancArray[0] = anchor0;
        ancArray[1] = anchor1;
        ancArray[2] = anchor2;
        ancArray[3] = anchor3;
        ancArray[4] = anchor4;
        ancArray[5] = anchor5;
        ancArray[6] = anchor6;
        ancArray[7] = anchor7;
        ancArray[8] = anchor8;
        ancArray[9] = anchor9;
        
    }

    void Update() {
        
        /* once the application starts place all objects in the scene.
           this only needs to be executed once thus the boolean flag. */
        if(!obj_placed) placeObjects();

        /* update whether or not the object is visible based on the
           location of the user*/
        updateVisibility();
            
        // check for screen touch each frame
        checkForTouch();

    }

    /* ##### object_placement function #####
       used to set correct positions for objects to be placed at - 
       sets the position based on an offset from the original camera
       position. returns a vector of coordinates. */
    Vector3 objectPlacement(GameObject obj){
        
        /* apple basket -- located at end of 3F jacobs near grad student office
        // pot of gold --- 
        // dog -----------
        // mouse ---------
        // avocado -------
        // flowers -------
        // plant shelf ---
        // mushrooms -----
        // pug (dog2) ----
        // bonsai -------- located in 370 */

        float offset_z = 0.0f, offset_x = 0.0f, offset_y = 0.0f;

        if(obj == apple_basket){
            //actual - upstairs hallway jacobs
            /*offset_x = -18.0f;
            offset_y = 0.0f; 
            offset_z = 5.0f;*/
            //home
            offset_x = 0.0f;
            offset_y = -0.25f;
            offset_z = 4.0f;

        }else if(obj == pot_of_gold){ 
            //home
            offset_x = 0.0f;
            offset_y = -0.25f;
            offset_z = 6.5f;

        }else if (obj == doggo){

            offset_x = 2.0f;
            offset_y = -0.25f;
            offset_z = 6.5f;

        }else if(obj == mouse){

            offset_x = -4.0f;
            offset_y = -0.25f;
            offset_z = 6.5f;

        }else if(obj == avocado){

            offset_x = -10.0f;
            offset_y = -0.25f;
            offset_z = 6.5f;

        }else if(obj == flowers){

            offset_z = 0f;

        }else if(obj == plant_shelf){

            offset_z = 0f;

        }else if(obj == mushrooms){

            offset_z = 0f;

        }else if(obj == pug){

            offset_z = 0f;

        }else if (obj == bonsai){
            
            offset_x = 2.5f;
            offset_y = 0.0f;
            offset_z = 3.0f;

        }

        return new Vector3 ((Camera.main.transform.position.x+offset_x),(Camera.main.transform.position.y+offset_y),
            (Camera.main.transform.position.z+offset_z));

    }

    void placeObjects(){
        
        for(int i = 0; i < objArray.Length; i++){
                posArray[i] = new Pose();
                posArray[i].position = objectPlacement(objArray[i]);
                ancArray[i] = Session.CreateAnchor(posArray[i],null);
                insArray[i] = GameObject.Instantiate(objArray[i], ancArray[i].transform.position,
                    ancArray[i].transform.rotation,ancArray[i].transform);
                if(i > 0) insArray[i].transform.GetChild(0).gameObject.SetActive(false);
        }

        active_obj = insArray[0];
        obj_placed = true;
    }

    void updateVisibility(){
        
        var dist = Vector3.Distance(active_obj.transform.position, Camera.main.transform.position);    
        if(dist > 2.5f) active_obj.transform.GetChild(0).gameObject.SetActive(false);
        else if(dist <= 2.5f){ 
            active_obj.transform.GetChild(0).gameObject.SetActive(true);
            active_obj.transform.Rotate(0,Time.deltaTime*10,0);
        }

        //active_obj.transform.Rotate(0,Time.deltaTime*10,0);

    }

    /* ##### checkForTouch function #####
       this function checks to determine if the user has touched the screen
       once. if the user is within a certain radius of the target object and
       there are still more objects to find, the next object is placed as the
       new target object. if it is the last object, the application quits. */
    void checkForTouch(){

        if(Input.touchCount == 1){ 
            var dist = Vector3.Distance(Camera.main.transform.position, active_obj.transform.position);
            if((tapCount < objArray.Length) && (dist < 1.5f) && (Input.GetTouch(0).phase == TouchPhase.Began)){
                timeElapsed = Time.realtimeSinceStartup - timeElapsed;
                Log_Output.writeTime(active_obj.name, timeElapsed, Hot_Cold.cueOn);
                active_obj = insArray[++tapCount];
                // deactivate the child gameobject -- messes with anchor (or something) and
                // causes lag if you deactivate parent
                insArray[tapCount].transform.GetChild(0).gameObject.SetActive(true);
                insArray[tapCount-1].transform.GetChild(0).gameObject.SetActive(false);
            }else if(tapCount >= objArray.Length && (Input.GetTouch(0).phase == TouchPhase.Began)){
                timeElapsed = Time.realtimeSinceStartup - timeElapsed;
                Log_Output.writeTime(active_obj.name, timeElapsed, Hot_Cold.cueOn);
                Application.Quit();
            } 
        }

    }

}
