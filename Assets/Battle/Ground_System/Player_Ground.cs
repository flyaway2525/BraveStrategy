using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems; // using��ǉ�
using UnityEngine;
using System.Linq;
public class Player_Ground : Ground, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    private bool _isPushed = false; // �}�E�X��������Ă��邩������Ă��Ȃ���    
    public List<Ground> pass_grounds;//�ʂ�������            pass_gorunds��[0]��player_Ground�̈ʒu�ɌŒ肷��H���͂��Ȃ�
    public bool onLastGround = false;
    [SerializeField] Ground _inputFirstGround; //�z�u�f�[�^�󂯓n���ꏊ(���͋K��l)
    Vector3 _inputPosition;     //�^�b�`���n�߂�PlayerGround��Posision�ŕύX����Ȃ�
    private int _mobility = 0;
    private float _DEX = 2.0f;
    private float _Time = 0;//1�ɂȂ����烊�Z�b�g���邱��
    private bool testTrigger = true;//���ł������Ă���
    [SerializeField] GameObject pt;
    public override void Start() {
        base.Start();
        pt = (GameObject)Resources.Load("PlayerParticle");
        _mobility = GetComponent<Player_Status>().Mobility;
        ground_Controller.Player_Ground = this;
        if (_inputFirstGround != null) {            //�z�u�f�[�^�󂯓n���ꏊ(���͋K��l)
            pass_grounds.Clear();//Debug.Log(string.Join(",",pass_grounds) + " : �N���A�����̂ŉ����Ȃ��͂�");
            pass_grounds.Add(_inputFirstGround);//Debug.Log(string.Join(",", pass_grounds) + " : _inputFirstGround�@���ꂽ�̂łȂ񂩓����Ă�͂�");
            ground_Controller.Set_Ground(this, _inputFirstGround);
            SetPos(_inputFirstGround);
        } else {
            Debug.LogWarning(this.gameObject + "�����ʒu���ݒ肳��Ă܂���B");
        }
    }
    void Update() {// �}�E�X����������Ă��鎞�A�I�u�W�F�N�g�𓮂���
        if (_isPushed) {
            Vector3 inputTouch;  //���ۂɐG���Ă���ꏊ
            Vector3 objectPosition; //�v���C���[������ׂ��ꏊ
            inputTouch = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            inputTouch.z = 0;
            _inputPosition.z = 0;
            float d = Vector3.Distance(_inputPosition, inputTouch);
            if (d < 1.3f) { //Transform.position = objectPosition;     //�ړ��̃R�[�h��g�ނ̂ň�UOFF 
                objectPosition = inputTouch;
                if (position_Ground != null && position_Ground != pass_grounds.Last() && pass_grounds.Count <= _mobility) { //Ground�I�u�W�F�N�g�ɏ�������̏���
                    pass_grounds.Add(position_Ground);
                    _inputPosition = pass_grounds.Last().gameObject.transform.position;
                    ground_Controller.Remove_Ground(this);
                    ground_Controller.Set_Ground(this, pass_grounds.Last());
                    pass_grounds.Last().renderer.material.color = Color.red;
                }
            } else { 
                //Player�̕\���ʒu��1�s���ł̈ړ��������E�ɐ�������
                //�g�p�����Player�̈ʒu���ς�邽�ߌ��݂͔p�~�A�ړ��������E�̉����̂��߂ɍ���K�v�Ȃ̂łƂ��Ă���
                /*
                objectPosition = _inputPosition + (inputTouch - _inputPosition) * 1.3f / d;
                Transform.position = objectPosition;
                */
            }
            if (pass_grounds.Count >= 2 && !gameData.player_Turn) {//�����i��
                gameData.player_Turn = true;
                gameData.StartCoroutine("Player_Turn");//�R���[�`���Ȃ̂ł���ȉ��ɏ����������Ƃ��͏��Ԃɒ���
            }                                                             
        }
        if (pass_grounds.Count >= 1) {   //Pass�����������Ɉړ����鏈��              
            if (testTrigger) {
                //StartCoroutine("Particle");
                //testTrigger =false;
            }
            float distance = Vector3.Distance(transform.position, pass_grounds[0].transform.position);
            if (0.01f < distance) {
                transform.position = Vector3.MoveTowards(transform.position, pass_grounds[0].transform.position, _DEX * Time.deltaTime * 0.5f);
            } else {
                gameObject.transform.position = pass_grounds[0].transform.position;
                if (pass_grounds.Count > 1) {
                    pass_grounds.Remove(pass_grounds[0]);
                    StartCoroutine("Particle");
                    testTrigger = false;
                }
            }
        }
    }
    // �N���b�N���m���A�R�[���o�b�N�����֐�
    public void OnPointerClick(PointerEventData eventData) { }
    public void OnPointerDown(PointerEventData eventData) {//�G������
        _isPushed = true;
        _inputPosition = pass_grounds.Last().transform.position;
        //Collider.enabled = false;//�G���Ă�L������Collider��OFF
        gameObject.layer = 2;
        ground_Controller.Selected_Ground = this;//�G���Ă���PlayerGround
      IEnumerable<Ground[]> _turnOn_Ground = ground_Controller.Ground.Where(i => i[1] == null); //��������Ă��Ȃ�Ground��Collider��true�ɂ���B
        foreach (Ground[] g in _turnOn_Ground) {
            if (g[0] != null) {
                g[0].collider.enabled = true;
                //g[0].gameObject.layer = 0;
            }
        }
    }
    public void OnPointerUp(PointerEventData eventData) {
        OnPointerUp();
    }
    public void OnPointerUp() {
        _isPushed = false;
        _inputPosition = Vector3.zero;
        //Collider.enabled = true;
        gameObject.layer = 0;
        foreach (Ground ground in ground_Controller.Normal_Grounds) {
            ground.collider.enabled = false; 
            //ground.gameObject.layer = 2;
        }
        if (pass_grounds.Count > 0) {     //�w��b�����Ƃ��ړ����Ă���ړ��A���ĂȂ�������o�H�����Z�b�g����(���߂Ƀ��Z�b�g����̂Ń��Z�b�g���Ȃ�)�����ʒu��
            SetPos(pass_grounds.Last());
        }
        foreach (Ground _pass_grounds in pass_grounds) {
            Debug.Log(_pass_grounds);
            _pass_grounds.renderer.material.color = Color.blue;
        }
    }

    private IEnumerator Particle() {
        GameObject particles = Instantiate(pt) as GameObject;
        particles.GetComponent<Transform>().parent = this.gameObject.transform;
        yield return new WaitForSeconds(0.2f);
        testTrigger = true;
    }
}