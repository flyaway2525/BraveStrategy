using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakePopupUI : MonoBehaviour{
    public static MakePopupUI instance;
    public GameObject textUI;
    public Transform targetTransform;
    public Canvas canvas;
    private void Awake() {
        instance = this;
    }
    private void Start() {
        // textUI = ~~~~
    }
    public void PopTest() {
        GameObject UIObj = Instantiate(textUI, canvas.transform);
        UIObj.GetComponent<UIFollowTarget>().SetTarget(targetTransform);
    }
    public void PopTest2(Transform _target) {
        //ターゲットを選択して、それにフォローするポップアップUIを生成する
        GameObject UIObj = Instantiate(textUI, canvas.transform);
        UIObj.GetComponent<UIFollowTarget>().SetTarget(_target);
    }
    //public void ScrollUI 
}
