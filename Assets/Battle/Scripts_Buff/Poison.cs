using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : Buff_Status
{
    public override void Start() {
        base.Start();
        //System.Type typefile = typeof(Poison);
        //System.Reflection.MethodInfo mi = typefile.GetMethod("OnEnable");
        //if (mi != null) { Debug.Log(mi + "このメソッドは存在します。"); } else { Debug.Log(mi + "このメソッドは存在しません。"); }
        //メソッドを検索するテスト

        Buff_Object = new GameObject("Poison");
        Buff_Object.transform.parent = this.transform;

    }
    public override void passive_Third() { //テストのためにここで発動させてみる
        Debug.Log($"プレイヤーは毒で30ダメージ");
        BasicStatus.hp -= 30;

        if (owner.GetComponent<Poison_Resistance>()) {
            Debug.Log("毒耐性持ち");
        } else { Debug.Log("毒耐性持ってないぞ"); }
    }
    public void OnTurnEnd() {
        Debug.Log($"プレイヤーは毒で30ダメージ");
        BasicStatus.hp -= 30;
    }
}
