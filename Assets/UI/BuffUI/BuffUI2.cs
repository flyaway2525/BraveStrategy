using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 各BuffUIにIDを割り振っておく
IDの若い順に処理？
 
 
 */
public class BuffUI2 : MonoBehaviour{
    [SerializeField]List<GameObject> BuffUIs = new List<GameObject>();           //最新のバフ
    [SerializeField] List<GameObject> BuffUIs_Buffer = new List<GameObject>();//変更前のバフ
    public enum STATUS {
        IDLE,
        CHANGE
    }
    public STATUS status;
    public GameObject atk_up_obj;
    Vector3[] path = {
        new Vector3(0.25f,-1.0f,-0.42f),
        new Vector3(0.4f,-1.0f,-0.29f),
        new Vector3(0.5f,-1.0f,-0.1f),
        new Vector3(0.5f,-1.0f,0.1f),
        new Vector3(0.4f,-1.0f,0.29f),
        new Vector3(0.25f,-1.0f,0.42f),
    };

    public void Add_ATKup() {
        GameObject instance = Instantiate(atk_up_obj);
        instance.transform.parent = this.transform;
        instance.transform.localPosition = path[0];
        BuffUIs.Add(instance);
        BuffUIs_Buffer.Add(instance);

    }
    public void Remove_ATKup() {
        BuffUIs_Buffer.Remove(atk_up_obj);
        //Destroy(atk_up_obj);
    }
}
