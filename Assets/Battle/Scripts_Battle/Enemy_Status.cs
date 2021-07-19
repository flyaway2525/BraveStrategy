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
    [Tooltip("�L�����N�^�ŗL�̃X�e�[�^�X")]
    [SerializeField] int MaxHP, ATK, DEF;
    [Tooltip("�ϓ��X�e�[�^�X")]
    [SerializeField] int MaxHP_Add, MaxHP_Rate;
    [SerializeField] int ATK_Add, ATK_Rate;
    [SerializeField] int DEF_Add, DEF_Rate;
    [Tooltip("�L�����N�^�̑����i")]
    [SerializeField] GameObject ring;
    private bool BattleStartHP = true;//HP�̏����l����

    public virtual void Start() {
        if (status == null) {
            Debug.LogWarning("Attach BasicStatus on this gameObject!! : " + gameObject);
            status = gameObject.GetComponent<BasicStatus>();
        }
        if (_targetController == null) {
            Debug.LogWarning("Attach TargetController on this gameObject!! : " + gameObject);
            _targetController = GameObject.Find("Field").GetComponent<TargetController>();
        }
        //�X�e�[�^�X�̏����l����    (�p�b�V�u�����������x�������)  
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
    public virtual IEnumerator FastSkill() {//�搧�U��(�v���C���[�ړ��s��)
        yield return null;
    }
    public virtual IEnumerator OnTurn() {//�G�̃^�[���̍s��(�v���C���[�ړ��\)
        yield return null;
    }
    public virtual IEnumerator EndSkill() {        //�^�[���C�����̃X�L��(�v���C���[�ړ��s��)
        yield return null;
    }

}

