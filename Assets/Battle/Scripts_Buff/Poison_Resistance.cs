using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison_Resistance : Buff_Status
{
    // Start is called before the first frame update
    public override void Start() {
        base.Start();
        Buff_Object = new GameObject("Poison_Resistance");
        Buff_Object.transform.parent = this.transform;

    }
    public override void passive_Third() { 
    }
 }
