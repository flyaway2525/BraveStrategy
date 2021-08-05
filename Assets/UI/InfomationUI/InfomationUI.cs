using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/* 回転に関する方法の資料
https://tama-lab.net/2017/06/unity%E3%81%A7%E3%82%AA%E3%83%96%E3%82%B8%E3%82%A7%E3%82%AF%E3%83%88%E3%82%92%E5%9B%9E%E8%BB%A2%E3%81%95%E3%81%9B%E3%82%8B%E6%96%B9%E6%B3%95%E3%81%BE%E3%81%A8%E3%82%81/
 */
public class InfomationUI : MonoBehaviour {
    public enum STATUS {
        IDLE,
        OPEN,
        WAIT,
        CLOSE
    }
    public STATUS status;
    [SerializeField] bool move;
    [SerializeField] bool execution;
    [SerializeField] Vector3 rotateTo;
    [SerializeField] TextMeshProUGUI textMeshProUGUI;
    [SerializeField] List<string> text = new List<string>();
    Coroutine coroutine;
    void Awake(){
        status = STATUS.IDLE;
        transform.eulerAngles = new Vector3(90.0f, 0, 0);
        rotateTo = new Vector3(90.0f, 0, 0);//初期の目標回転角度を閉じておく
        textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>();
        if(textMeshProUGUI == null) { Debug.LogError("TextMeshProUGUI is None"); }
    }
    private void Start() {
    }
    void Update(){
        switch (status) {
            case STATUS.IDLE:   //監視
                if(text.Count > 0) {
                    EnterMotion(text[0]);
                    status = STATUS.OPEN;
                }
                break;
            case STATUS.OPEN: //開く処理
                if (!move) {
                    status = STATUS.WAIT;
                }
                break;
            case STATUS.WAIT:  //開いてる状態
                if (Input.GetMouseButtonDown(0)) {
                    ExitMotion();
                    status = STATUS.CLOSE;
                }
                //3秒したらCLOSEを実行するとか
                break;
            case STATUS.CLOSE://閉じる処理
                if (!move) {
                    text.RemoveAt(0);
                    status = STATUS.IDLE;
                }
                break;
            default:
                break;
        }
        if (move) {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(rotateTo), Time.deltaTime * 100.0f);
            //Debug.Log("回ってるはず" + transform.rotation.eulerAngles + "  " + rotateTo);
            if (Mathf.Abs(transform.rotation.eulerAngles.x - rotateTo.x) < 2.0f) {
                transform.eulerAngles = rotateTo;
                move = false;
            }
        }
    }

    public void EnterMotion(string _msg) {
        textMeshProUGUI.text = _msg;
        move = true;
        rotateTo = new Vector3(0, 0, 0);
    }
    /*
    public void ChangeText() {
        if (text.Count > 0) {
            Debug.Log("text.Count > 0  true" + text[0]);
            string _text = text[0];
            text.RemoveAt(0);                   //これが複雑になる要因
            textMeshProUGUI.text = _text;
        } else {
            //Debug.LogError("Text List Don't have Context");
            ExitMotion();
        }
    }
    */
    public void ExitMotion() {
        move = true;
        rotateTo = new Vector3(90.0f, 0, 0);
    }
    public void AddText(string _text) {
        text.Add(_text);
    }
    public void AddTextTest() {
        string _test = "textAddText : " + Time.time;
        //Debug.Log(text[0]);
        AddText(_test);
    }
}
