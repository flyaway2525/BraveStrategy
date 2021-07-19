using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_Status : All_Status
{
    public GameObject owner; //効果を発揮しているプレイヤーや敵
    public BasicStatus BasicStatus;
    [SerializeField] TargetController targetController;
    //[SerializeField] int maxhp_up, atk_up;
    //[SerializeField] int maxhp_rate, atk_rate;
    [SerializeField] int _buff_Turn;//0になったらバフが切れる、-1は期限がない
    public GameObject Buff_Object; //buffを表現するためのgameObject
    public virtual void Start() {
        if (owner == null) {//Debug.LogWarning("Attach gameObject on this gameObject!! : " + gameObject);
            owner = transform.gameObject;
        }
        if (BasicStatus == null) {//Debug.LogWarning("Attach status on this gameObject!! : " + gameObject);
            BasicStatus = owner.GetComponent<BasicStatus>();
        }
        if (targetController == null) {//Debug.LogWarning("Attach TargetController on this gameObject!! : " + gameObject);
            targetController = GameObject.Find("Field").GetComponent<TargetController>();
        }
        targetController.Buff_Script = this;
    }
    public int Buff_Turn { 
        set { 
            if(_buff_Turn == -1) {return;}
            value = _buff_Turn; 
            if (_buff_Turn <= 0) {this.OnDisable();} 
        } 
        get{ return(_buff_Turn); } }
    public void OnDisable() {
        Destroy(Buff_Object);
        Destroy(this);
    }
}
