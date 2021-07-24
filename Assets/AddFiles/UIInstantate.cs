using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInstantate : MonoBehaviour{
    public GameObject textUI;
    public Canvas canvas;
    void Start(){

        GameObject UIObj = Instantiate(textUI,canvas.transform);
        UIObj.GetComponent<UIFollowTarget>().SetTarget(transform);
    }
}
