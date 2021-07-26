using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakePopupUI : MonoBehaviour{
    public GameObject textUI;
    public Canvas canvas;
    public void PopTest() {
        GameObject UIObj = Instantiate(textUI, canvas.transform);
        UIObj.GetComponent<UIFollowTarget>().SetTarget(transform);
    }
}
