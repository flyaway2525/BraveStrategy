using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : Buff_Status
{
    public override void Start() {
        base.Start();
        //System.Type typefile = typeof(Poison);
        //System.Reflection.MethodInfo mi = typefile.GetMethod("OnEnable");
        //if (mi != null) { Debug.Log(mi + "���̃��\�b�h�͑��݂��܂��B"); } else { Debug.Log(mi + "���̃��\�b�h�͑��݂��܂���B"); }
        //���\�b�h����������e�X�g

        Buff_Object = new GameObject("Poison");
        Buff_Object.transform.parent = this.transform;

    }
    public override void passive_Third() { //�e�X�g�̂��߂ɂ����Ŕ��������Ă݂�
        Debug.Log($"�v���C���[�͓ł�30�_���[�W");
        BasicStatus.hp -= 30;

        if (owner.GetComponent<Poison_Resistance>()) {
            Debug.Log("�őϐ�����");
        } else { Debug.Log("�őϐ������ĂȂ���"); }
    }
    public void OnTurnEnd() {
        Debug.Log($"�v���C���[�͓ł�30�_���[�W");
        BasicStatus.hp -= 30;
    }
}
