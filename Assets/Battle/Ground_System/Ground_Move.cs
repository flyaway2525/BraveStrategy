using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Ground_Move : MonoBehaviour
{
    [SerializeField] Ground _origin_Ground; //移動元のGround
    [SerializeField] Ground _target_Ground;//移動先のGround
    private Ground _ground;                      //このPlayerGround
    private Transform _origin_Transform;
    private Transform _target_Transform;
    private Transform _transform;
    bool trigger = false;//トリガーがオンになったとき、Updateを回す
    public float DEX = 2.0f;      //個別に値を設定するから
    private void Start() {
        //_origin_Ground = GetComponent<Ground>(); //このGroundを動かす
        //_origin_Transform = GetComponent<Transform>();
        _ground = GetComponent<Ground>();
        _transform = GetComponent<Transform>();
    }

    private void Update() {
        if (trigger) {
            float distance = Vector3.Distance(_transform.position, _target_Transform.position);
            if (distance > 0.1f) {
                float step = DEX * Time.deltaTime;
                Vector3.MoveTowards(_transform.position, _target_Transform.position, step);
            } else {
                //現在のGroundをセットしなおすScriptを入れる
                trigger = false;
                if (false/*次の移動マスが存在しているなら*/) {
                    //originとtargetを設定しなおして
                    trigger = true;
                }
            }
        }
    }
    public void MoveAtoB(Ground A , Ground B) {
        trigger = true;
    }

}
