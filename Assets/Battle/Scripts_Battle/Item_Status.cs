using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Item_Status : All_Status
{
    [SerializeField] GameObject owner;
    public BasicStatus status;
    [SerializeField] TargetController targetController;
    [SerializeField] int maxhp_up, atk_up;
    [SerializeField] int maxhp_rate, atk_rate;
    public virtual void Start() {
        if (owner == null) {
            //Debug.LogWarning("Attach parrent gameObject on this gameObject!! : " + gameObject);
            owner = transform.parent.gameObject;
        }
        if (status == null) {
            //Debug.LogWarning("Attach parrent status on this gameObject!! : " + gameObject);
            status = owner.GetComponent<BasicStatus>();
        }
        if (targetController == null) {
            //Debug.LogWarning("Attach TargetController on this gameObject!! : " + gameObject);
            targetController = GameObject.Find("Field").GetComponent<TargetController>();
        }
        //targetController.Item_Object = this.gameObject;
        targetController.Item_Script = this;
    }
}







