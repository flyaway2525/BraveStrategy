using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class All_Status : MonoBehaviour
{
    public TargetController _targetController;
    public GameData gameData;
    public SO_ShowInfo showInfo;
    public EnemyBullets enemyBullets;
    //���ׂĂ𓝊�����I�u�W�F�N�g�N���X�I
    /***
     *  Character_Status��Item_Status�������������N���X
     *  �p�b�V�u�����^�C�~���O�Ȃǂ�override���邱�ƂŃA�C�e���ɔ\�͂�t�^�ł���
     *  �\�� ALLOBJECTS | Character_Status | ���ꂼ��̃L�����N�^�X�e�[�^�X
     *                            | Item_Status        | ���ꂼ��̃A�C�e���X�e�[�^�X
     *  TargetController��ALLOBJECTS��GameObject�����Ă������Ƃŏ�ɂ���GameObject�̖��������ʂ���(�������Tag�ŗǂ��Ȃ���?)                            
     ***/
    public void Awake() {
        if(showInfo == null) {
            showInfo = Resources.Load("ShowInfo") as SO_ShowInfo;
            if(showInfo == null) {
                Debug.LogError("Nothing showInfo!");
            }
        }
    }
    public virtual void passive_First() { }
    public virtual void passive_Second() { }
    public virtual void passive_Third() { }
}
