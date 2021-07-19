using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[DisallowMultipleComponent]
[RequireComponent(typeof(BasicStatus))]
public class Enemy_Status : All_Status
{

    public BasicStatus status;
    [SerializeField] string type;
    [Range(0, 1000)]
    [Tooltip("キャラクタ固有のステータス")]
    [SerializeField] int MaxHP, ATK, DEF;
    [Tooltip("変動ステータス")]
    [SerializeField] int MaxHP_Add, MaxHP_Rate;
    [SerializeField] int ATK_Add, ATK_Rate;
    [SerializeField] int DEF_Add, DEF_Rate;
    [Tooltip("キャラクタの装備品")]
    [SerializeField] GameObject ring;
    private bool BattleStartHP = true;//HPの初期値を代入

    public virtual void Start() {
        if (status == null) {
            Debug.LogWarning("Attach BasicStatus on this gameObject!! : " + gameObject);
            status = gameObject.GetComponent<BasicStatus>();
        }
        if (_targetController == null) {
            Debug.LogWarning("Attach TargetController on this gameObject!! : " + gameObject);
            _targetController = GameObject.Find("Field").GetComponent<TargetController>();
        }
        //ステータスの初期値を代入    (パッシブ発動後もう一度代入する)  
        status.maxhp = MaxHP;
        status.atk = ATK;
        status.def = DEF;
        status.type = type;
        status.maxhp_add = MaxHP_Add;
        status.atk_add = ATK_Add;
        status.def_add = DEF_Add;
        status.maxhp_rate = MaxHP_Rate;
        status.atk_rate = ATK_Rate;
        status.def_rate = DEF_Rate;
        status.type = type;
    }
    //public override void passive_First() { }
    //public override void passive_Second() { }    
    public override void passive_Third() {
        status.maxhp = (int)((float)(status.maxhp + status.maxhp_add) * status.maxhp_rate);
        status.atk = (int)((float)(status.atk + status.atk_add) * status.atk_rate);
        status.def = (int)((float)(status.def + status.def_add) * status.def_rate);
        if (BattleStartHP) { BattleStartHP = false; status.hp = status.maxhp; }
    }
    public virtual IEnumerator FastSkill() {//先制攻撃(プレイヤー移動不可)
        yield return null;
    }
    public virtual IEnumerator OnTurn() {//敵のターンの行動(プレイヤー移動可能)
        yield return null;
    }
    public virtual IEnumerator EndSkill() {        //ターン修了時のスキル(プレイヤー移動不可)
        yield return null;
    }

}

