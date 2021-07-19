using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Ground : Ground
{
    [SerializeField] Ground _inputFirstGround;//�z�u�f�[�^�󂯓n���ꏊ(���͋K��l)
    //public GameObject
    public override void Start() {
        base.Start();
        ground_Controller.Enemy_Ground = this;
        if (_inputFirstGround != null) {  //�z�u�f�[�^�󂯓n���ꏊ(���͋K��l)
            position_Ground = _inputFirstGround; 
            ground_Controller.Set_Ground(this, position_Ground); 
            SetPos(position_Ground);
        } else {
            Debug.LogWarning(this.gameObject + " : �̏����ʒu�����Ă��������B");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
