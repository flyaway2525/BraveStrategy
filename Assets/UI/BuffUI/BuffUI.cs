using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

/*
構成
追加と消去のpublic機能
boolで管理して、変更があったら場所を動かす感じ
バッファとして消す前の配列と消した後の配列を記憶する
各オブジェクトをバッファから移動後に動かす

 
 */
public class BuffUI : MonoBehaviour {
    List<GameObject> BuffUIs = new List<GameObject>();           //最新のバフ
    List<GameObject> BuffUIs_Buffer = new List<GameObject>();//変更前のバフ
    public enum STATUS {
        IDLE,
        CHANGE
    }
    public STATUS status;
    public bool atk_up = false;
    public GameObject atk_up_obj;
    public bool atk_down = false;
    public GameObject atk_down_obj;
    public bool poison = false;
    public GameObject poison_obj;
    Vector3[] path = {
        new Vector3(0.25f,-1.0f,-0.42f),
        new Vector3(0.4f,-1.0f,-0.29f),
        new Vector3(0.5f,-1.0f,-0.1f),
        new Vector3(0.5f,-1.0f,0.1f),
        new Vector3(0.4f,-1.0f,0.29f),
        new Vector3(0.25f,-1.0f,0.42f),
    };
    public void Start() {
        status = STATUS.IDLE;
    }
    public void Update() {
        switch (status) {
            case STATUS.IDLE:
                if(BuffUIs.Count != BuffUIs_Buffer.Count) {
                    status = STATUS.CHANGE;
                }
                break;
            case STATUS.CHANGE:

                break;
            default:
                break;
        }
    }


    /*
     新規に作られた場合このコードではエラーが出るので見送り
     
     */
    public void ChangeBuffUIs() {
        BuffUIs.ForEach(item => {
            //BuffUIs.IndexOf(item) から
            //  BuffUIs_Buffer.IndexOf(item)　まで移動するコードを書く
            Vector3[] _path = new Vector3[0];
            int from = BuffUIs_Buffer.IndexOf(item);
            int to = BuffUIs.IndexOf(item);
            int x = 1;//加算方向か、減算方向か指定
            if (from - to < 0) { x = -1; }
            for (int i = BuffUIs_Buffer.IndexOf(item); from == to; i = i + x) {
                Array.Resize(ref _path, _path.Length + 1);
                _path[_path.Length - 1] = path[i];
            }
            transform.DOLocalPath(_path, 2.0f, PathType.CatmullRom)
            .SetEase(Ease.Linear)
            .SetLookAt(0.01f, Vector3.forward)
            .SetOptions(false, AxisConstraint.None);
        });


    }
    public void Toggle_ATKup(bool tf) {
        atk_up = tf;
        if (tf) {
            BuffUIs.Add(atk_up_obj);
            atk_up_obj = Instantiate(atk_up_obj);
        } else {
            BuffUIs.Remove(atk_up_obj);
        }
    }
    public void Toggle_ATKdown(bool tf) {
        atk_down = tf;
        if (tf) {
            BuffUIs.Add(atk_down_obj);
            atk_down_obj = Instantiate(atk_down_obj);
        } else {
            BuffUIs.Remove(atk_down_obj);
        }
    }
    public void Toggle_Poison(bool tf) {
        atk_up = tf;
        if (tf) {
            BuffUIs.Add(poison_obj);
            poison_obj = Instantiate(poison_obj);
        } else {
            BuffUIs.Remove(poison_obj);
        }
    }
}
