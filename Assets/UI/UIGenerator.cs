using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGenerator : MonoBehaviour{
    public static UIGenerator instance;
    public GameObject popupUI;
    public GameObject scrollUI;
    public ScrollUI scrollScript;
    public GameObject infomationUI;
    public InfomationUI infomationScript;
    public Transform targetTransform;
    public Canvas canvas;
    private void Awake() {
        instance = this;
    }
    private void Start() {
        GeneratScrollUI();
        GeneratInfomationUI();
    }
    public void GeneratPopupUI(Transform _target) {
        //ターゲットを選択して、それにフォローするポップアップUIを生成する
        GameObject popupUIInstance = Instantiate(popupUI, canvas.transform);
        popupUIInstance.GetComponent<UIFollowTarget>().SetTarget(_target);
    }
    public void GeneratPopupUI(Transform _target,string _str) {
        //ターゲットを選択して、それにフォローするポップアップUIを生成して特定の文字列を表示
        GameObject popupUIInstance = Instantiate(popupUI, canvas.transform);
        popupUIInstance.GetComponent<UIFollowTarget>().SetTarget(_target);
        popupUIInstance.GetComponent<UIFollowTarget>().SetTextValue(_str);
    }
    public void GeneratScrollUI() {
        scrollUI = Instantiate(scrollUI, canvas.transform);
        scrollScript = scrollUI.GetComponent<ScrollUI>();
    }
    public void GeneratInfomationUI() {
        infomationUI = Instantiate(infomationUI, canvas.transform);
        infomationScript = infomationUI.GetComponent<InfomationUI>();
    }
    public void AddScrollText() {
        scrollScript.AddScrollText("test");
    }
    public void AddScrollText(string _str) {
        scrollScript.AddScrollText(_str);
    }
    public void AddInfomationText() {
        infomationScript.AddText("addText Test");
    }
    public void AddInfomationText(string _str) {
        infomationScript.AddText(_str);
    }
}
