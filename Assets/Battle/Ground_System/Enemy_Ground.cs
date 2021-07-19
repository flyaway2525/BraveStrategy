using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Ground : Ground
{
    [SerializeField] Ground _inputFirstGround;//配置データ受け渡し場所(今は規定値)
    //public GameObject
    public override void Start() {
        base.Start();
        ground_Controller.Enemy_Ground = this;
        if (_inputFirstGround != null) {  //配置データ受け渡し場所(今は規定値)
            position_Ground = _inputFirstGround; 
            ground_Controller.Set_Ground(this, position_Ground); 
            SetPos(position_Ground);
        } else {
            Debug.LogWarning(this.gameObject + " : の初期位置を入れてください。");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
