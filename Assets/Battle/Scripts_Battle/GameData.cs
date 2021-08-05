using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{

    [SerializeField] private TargetController _targetController;
    [SerializeField] Ground_Controller _ground_Controller;
    private bool TF = true;
    public bool canMove = false;
    public bool player_Turn = false;
    public bool enemy_Turn = false;         
    public void Start() {
        UIGenerator.instance.AddScrollText("�X�N���[���ɒǉ�������");
        UIGenerator.instance.AddInfomationText("1�^�[���ڂƂ��o����");
        Debug.Log("�Ԃ��v���C���[�A�΂��G�l�~�[�A���O�����h");
        Debug.Log("�Ԃ𓮂����ăO�����h���ړ�����Q�[��");

    }
    public void Update() {    //�S�Ă�Start���I�����Update�̃^�C�~���O�ɂȂ�����combat���J�n����
        if (TF) {
            TF = false;
            PreAction();
            combat();
        }
    }
    public void PreAction() {
        UIGenerator.instance.AddInfomationText("PreAction");
        UIGenerator.instance.AddScrollText("PreAction���s");
        //Passive�̃��[��
        /***
         * passive_First        : �L�����N�^�̃X�e�[�^�X���グ�������(HP+100�Ƃ�)
         * passice_Second    : �L�����N�^�̃X�e�[�^�X�{���̕ω�   
         * passice_Third       : �L�����N�^�̃X�e�[�^�X�{�����f          
         * ***/
        //TargetController�̏����� : _allGameObjects�ɑS�Ă�Chara��Item�����݂���K�v������(�v�C��) 
        //�p�b�V�u�X�L���𔭓�������(����̓p�b�V�u���ω����邽�тɍs����Ɨǂ�)
        List<All_Status> _allStatus = new List<All_Status>();
        _allStatus = _targetController.Initialize_TargetController();
        foreach (All_Status _script in _allStatus) { _script.passive_First(); }
        foreach (All_Status _script in _allStatus) { _script.passive_Second(); }
        foreach (All_Status _script in _allStatus) { _script.passive_Third(); }
    }
    public void combat() {
        CanMove(false);
        _ground_Controller.turnPlayer = false;
        UIGenerator.instance.AddScrollText("�퓬�J�n");
        StartCoroutine("Enemy_FastSkill");
        _ground_Controller.turnPlayer = true;
        CanMove(true);
        //playerTrun���n�܂�      �����@Player��G������

    }
    public IEnumerator Player_Turn() {
        if (canMove) {
            Debug.Log("3");
            UIGenerator.instance.AddScrollText("�G�s���܂�3�b");
            yield return new WaitForSeconds(1.0f);
            Debug.Log("2");
            UIGenerator.instance.AddScrollText("�G�s���܂�2�b");
            yield return new WaitForSeconds(1.0f);
            Debug.Log("1");
            UIGenerator.instance.AddScrollText("�G�s���܂�1�b");
            yield return new WaitForSeconds(1.0f);
            Debug.Log("0");
            UIGenerator.instance.AddScrollText("�G�s���J�n");
            StartCoroutine("Enemy_Trun");
        }
    }
    public IEnumerator Enemy_Trun() {
        player_Turn = false;
        _ground_Controller.turnPlayer = false;
        Debug.Log("Enemy_Trun");
        UIGenerator.instance.AddScrollText("�G�̃^�[���J�n");
        foreach (Enemy_Status enemy_script in _targetController.Enemy_Scripts) {
            IEnumerator c = enemy_script.OnTurn();
            StartCoroutine(c);
        }
        Debug.Log("�����Ƀv���C���[�̈ړ���҂���������K�v������");
        UIGenerator.instance.AddInfomationText("�^�[���I��/n�G���h�t�F�C�Y�Ɉڍs");
        StartCoroutine("Enemy_EndSkill");
        yield return null;
    }
    public IEnumerator Enemy_FastSkill() {
        foreach (Enemy_Status enemy_script in _targetController.Enemy_Scripts) {
            IEnumerator c = enemy_script.FastSkill();
            StartCoroutine(c);
        }
        yield return null;
    }
    public IEnumerator Enemy_EndSkill() {    
        CanMove(false);
        foreach (Enemy_Status enemy_script in _targetController.Enemy_Scripts) {
            IEnumerator c = enemy_script.EndSkill();
            StartCoroutine(c);
        }
        CanMove(true);
        yield return null;
    }

    public void CanMove(bool TF) {
        _ground_Controller.touchOffCollider.enabled = !TF;
        if (!TF && _ground_Controller.Selected_Ground) {
            _ground_Controller.Selected_Ground.OnPointerUp();//�����I�Ƀ^�b�`������
        }
        canMove = TF;
    }
}
