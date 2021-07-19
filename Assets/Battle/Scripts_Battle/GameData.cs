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
    /* �f�[�^�ۑ��Z�p
    public void ChangePTHP(string PTname, int PTHpAdd) {
        if (PlayerPrefs.HasKey(PTname)) {
            string name = PlayerPrefs.GetString(PTname);
            int Hp = PlayerPrefs.GetInt("HP") + PTHpAdd;
            PlayerPrefs.SetInt("HP", Hp);

            Debug.Log(PlayerPrefs.GetString("PT") + "  " + PlayerPrefs.GetInt("HP"));
        } else {
            Debug.Log(PTname + " is Nothing");
        }
    }
    */
    public void PreAction() {
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
        _ground_Controller.touchOffCollider.enabled = true;
        _ground_Controller.turnPlayer = false;
        StartCoroutine("Enemy_FastSkill");
        _ground_Controller.touchOffCollider.enabled =false;
        _ground_Controller.turnPlayer = true;
        canMove = true;
        //playerTrun���n�܂�      �����@Player��G������

    }
    public IEnumerator Player_Turn() {
        if (canMove) {
            canMove = false;
            Debug.Log("3");
            yield return new WaitForSeconds(1.0f);
            Debug.Log("2");
            yield return new WaitForSeconds(1.0f);
            Debug.Log("1");
            yield return new WaitForSeconds(1.0f);
            Debug.Log("0");
            player_Turn = false;
            _ground_Controller.Selected_Ground.OnPointerUp();
            _ground_Controller.turnPlayer = false;
            Debug.Log("Enemy_Trun");
            StartCoroutine("Enemy_Trun");
        }
    }
    public IEnumerator Enemy_Trun() {
        foreach(Enemy_Status enemy_script in _targetController.Enemy_Scripts) {
            IEnumerator c = enemy_script.OnTurn();
            StartCoroutine(c);
        }
        _ground_Controller.touchOffCollider.enabled = true;
        StartCoroutine("Enemy_EndSkill");
        canMove = true;
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
        foreach (Enemy_Status enemy_script in _targetController.Enemy_Scripts) {
            IEnumerator c = enemy_script.EndSkill();
            StartCoroutine(c);
        }
        _ground_Controller.touchOffCollider.enabled = false;
        yield return null;
    }
}
