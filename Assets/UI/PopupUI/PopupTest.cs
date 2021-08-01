using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupTest : MonoBehaviour{
    void Start(){
        
    }
    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            MakePopupUI.instance.PopTest2(transform);
        }
    }
}
