using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/***
 *  �^�[�Q�b�g�R���g���[���[�̎g���� 
 *  GameData��1�쐬�����TargetController����Q�Ƃ��邱��
 *  �v���p�e�B��p���ėl�X�Ȏ�ނ̃Q�[���I�u�W�F�N�g���Q�Ƃ��邱�Ƃ��ł���
 *  �Q�Ƃ������Q�[���I�u�W�F�N�g��_allObjects�Ɏ����œ���
 * _allObjects�͑����X�g�ɃI�u�W�F�N�g�����邱�Ƃ�ۏ؂��Ȃ�
 * �P���`�ŗp�����<GameObject>
 * ��set�̂݉\
 * �����`�ŗp�����Lsit<GameObject>
 * ��set,get���\
 * ����remove�@�\�͎�������ĂȂ��B
 * 
 * UPDATE : Passive�𔭓����邵�Ȃ���GameObject�͗v��Ȃ��Ǝv����B
 * �͈͂�ALLOBJECTS�ɂ����ꍇ�A���ǑS����Object��Passive_1~3�����邩��B
 * �ł���Ȃ��TC�͌��ʔ����́h�Ώە��h�ɍi��ׂ��A
 * ��:�ł��󂯂�\���̂��閡���L�����N�^
 * Player�ɑ����� && �ŏ�ԂłȂ�(�v����) && !�őϐ��������Ă���
 * _Players               _Poison                        _Resist_Poison
 * �̂悤�Ȍ`���]�܂����B
 * 
 * �őϐ��X�L���𕕈󂳂ꂽ�ꍇ�Aremove�@�\���g���A�����������Start�ɂ���悤��TC�ɓo�^���Ȃ����B(��p�̊֐������ׂ�)
 * 
 * 
***/
public class TargetController : MonoBehaviour
{     [SerializeField] List<Player_Status> _player_Scripts = new List<Player_Status>();    //�S�Ă�Player�I�u�W�F�N�g������(Player(Player_Status)����������ɃV�t�g����)
        [SerializeField] List<Enemy_Status> _enemy_Scripts = new List<Enemy_Status>();  //�S�Ă�Enemy�I�u�W�F�N�g������(Player(Enemy_Status)����������ɃV�t�g����)
        [SerializeField] List<Item_Status> _item_Scripts = new List<Item_Status>();      //�S�Ă�Items�I�u�W�F�N�g������(Player(Item_Status)����������ɃV�t�g����)
        [SerializeField] List<Buff_Status> _buff_Scripts = new List<Buff_Status>();        //�S�Ă�Buffs�I�u�W�F�N�g������(Player(Buff_Status)����������ɃV�t�g����)
    //_player_Scripts
    private void setLists<T>(List<T> gameObjects, List<T> _gameObjects) {
        foreach (T gameObject in gameObjects) {
            setList(gameObject, _gameObjects);
        }
    }
    private void setList<T>(T gameObject, List<T> _gameObjects) {
        _gameObjects.Add(gameObject);
        //Debug.Log(gameObject + " : was added to : " + _gameObjects);
    }
    public List<Player_Status> Player_Scripts { set { setLists(value, _player_Scripts); } get { return _player_Scripts; } }
    public Player_Status Player_Script { set { setList(value, _player_Scripts); } }
    public List<Enemy_Status> Enemy_Scripts { set { setLists(value, _enemy_Scripts); } get { return _enemy_Scripts; } }
    public Enemy_Status Enemy_Script { set { setList(value, _enemy_Scripts); } }
    public List<Item_Status> Item_Scripts { set { setLists(value, _item_Scripts); } get { return _item_Scripts; } }
    public Item_Status Item_Script { set { setList(value, _item_Scripts); } }
    public List<Buff_Status> Buff_Scripts { set { setLists<Buff_Status>(value, _buff_Scripts); } get { return _buff_Scripts; } }
    public Buff_Status Buff_Script { set { setList<Buff_Status>(value, _buff_Scripts); } }

    public List<All_Status> Initialize_TargetController() {//gameObject �̃��X�g������Script�̃��X�g��Ԃ�
        List<All_Status> allScripts = new List<All_Status>();
        foreach (All_Status _allScripts in _player_Scripts) { allScripts.Add(_allScripts); }
        foreach (All_Status _allScripts in _enemy_Scripts) { allScripts.Add(_allScripts); }
        foreach (All_Status _allScripts in _item_Scripts) { allScripts.Add(_allScripts); }
        foreach (All_Status _allScripts in _buff_Scripts) { allScripts.Add(_allScripts); }
        return allScripts;
    }
}







/*   �@�ߋ��̈╨ : �S�Ă�GameObject�����锠������Ă݂���
 *   �@�S���̃Q�[���I�u�W�F�N�g��Ώۂɂ������͍�ꂽ�B����ɂ���đS�ẴI�u�W�F�N�g����p�b�V�u�𔭓������邱�Ƃ͉\�ɂȂ����Ǝv��ꂽ��
 *   �@�v���C���[�Ƀo�t���t�^����Ă���Ȃǂ����ꍇ�A�v���C���[�̃I�u�W�F�N�g�ƃo�t�̑ΏۃI�u�W�F�N�g���v���C���[�ɂȂ邽�߁A���s�������B
 *   �@�o�t�̃I�u�W�F�N�g���쐬���ăv���C���[�ɃA�^�b�`���邱�Ƃ��l�������A�o�t�͑����ƈ���ăQ�[�����ɂ�����������肷��B
 *   �@�v���C���[�̒����ɂ͑����i�Ȃǂ̃X�e�[�^�X��z�u�������̂ŕ���킵���Ȃ��悤�ɑS�ăX�N���v�g�ŊǗ��������B
 *   �@�o�t�I�u�W�F�N�g�𐶐�����ꍇ�A�v���n�u���C���X�^���X�����悤�Ǝv��
 *   �@
 *   �@_allScripts�ł`�k�k�n�a�i�d�b�s��S���i�[���邱�Ƃ��l�������A�Ⴄ�N���X(Buff_Status��Chracter_Status�Ȃ�)��List������͕̂s�\
 *   �@�Ƃ������]�v�ɂ݂��炭�Ȃ�̂�
 *   �@���ꂼ��̃N���X�Ń��X�g����邱�Ƃɂ����B
 *   �@�ȏ�
 *   �@
 *   �@            
    //[SerializeField] List<GameObject> _allObjects = new List<GameObject>();//�S�ẴQ�[���I�u�W�F�N�g������(����͎g��Ȃ������ɂ����Ă�������)
    //    [SerializeField] List<ALLOBJECTS> _allScripts = new List<ALLOBJECTS>();//�S�Ă̑Ώۂ��Ƃ�X�N���v�g������(Player(Character_Status),Enemy(Character_Status),Buff,Item_Status) 
    //[SerializeField] List<GameObject> _player_Objects = new List<GameObject>();    //�S�Ă�Player�I�u�W�F�N�g������(Player(Player_Status)����������ɃV�t�g����)
    //[SerializeField] List<GameObject> _enemy_Objects = new List<GameObject>();  //�S�Ă�Enemy�I�u�W�F�N�g������(Player(Enemy_Status)����������ɃV�t�g����)
    //[SerializeField] List<GameObject> _item_Objects = new List<GameObject>();      //�S�Ă�Items�I�u�W�F�N�g������(Player(Item_Status)����������ɃV�t�g����)
   
           ********************************************************
            
    //public List<GameObject> Player_Objects { set { setLists<GameObject>(value, _player_Objects); } get { return _player_Objects; } }
    //public GameObject Player_Object { set { setList<GameObject>(value, _player_Objects); } }
    //public List<GameObject> Enemy_Objects { set { setLists<GameObject>(value, _enemy_Objects); } get { return _enemy_Objects; } }
    //public GameObject Enemy_Object { set { setList<GameObject>(value, _enemy_Objects); } }
    //public List<GameObject> Item_Objets { set { setLists<GameObject>(value, _item_Objects); } get { return _item_Objects; } }
    //public GameObject Item_Object { set { setList<GameObject>(value, _item_Objects); } }

           ********************************************************
         
    //public List<GameObject> GameObjects { set { setLists<GameObject>(value, _allObjects); } get { return _allObjects; } }
    //public GameObject GameObject { set { setList<GameObject>(value, _allObjects); } }
    //public List<ALLOBJECTS> Scripts { set { setLists<ALLOBJECTS>(value, _allScripts); } get { return _allScripts; } }
    //public ALLOBJECTS Script { set { setList<ALLOBJECTS>(value, _allScripts); } }


private void setList<T>(T gameObject, List<T> _gameObjects) {
    if (true) {
        Debug.Log(gameObject.Equals(typeof(GameObject)));
        if (gameObject.Equals(typeof(GameObject)) && !_allObjects.Contains(gameObject as GameObject)) { //_allObjects�ɓ����ꍇ
            _allObjects.Add(gameObject as GameObject);
            Debug.Log(gameObject + ": new _allObjects");
        } else if (gameObject.Equals(typeof(GameObject))) {
            Debug.LogWarning(gameObject + " : is already in : " + _gameObjects);
        }
        if (gameObject.Equals(typeof(ALLOBJECTS)) && !_allScripts.Contains(gameObject as ALLOBJECTS)) { //_allScripts�ɓ����ꍇ
            _allScripts.Add(gameObject as ALLOBJECTS);
            Debug.Log(gameObject + ": new _allScripts");
        } else if (gameObject.Equals(typeof(ALLOBJECTS))) {
            Debug.LogWarning(gameObject + " : is already in : " + _gameObjects);
        }
    }
    if (!_gameObjects.Contains(gameObject)) {
        if (!_allObjects.Contains(gameObject as GameObject)) {                //_allObjects�̃��X�g�ɒǉ�
            _allObjects.Add(gameObject as GameObject);
            Debug.Log(gameObject + ": new _allObjects");
        } else if (!_allScripts.Contains(gameObject as ALLOBJECTS)) {       //_allScripts�̃��X�g�ɒǉ�
            _allScripts.Add(gameObject as ALLOBJECTS);
            Debug.Log(gameObject + ": new _allScripts");
        }
        _gameObjects.Add(gameObject);                                                 //�{���@�Ώۂ̃��X�g�ɒǉ�
        Debug.Log(gameObject + " : was added to : " + _gameObjects);
    } else {
        Debug.LogWarning(gameObject + " : is already in : " + _gameObjects);
    }
}
   */
/*
private void setLists(List<ALLOBJECTS> gameObjects, List<ALLOBJECTS> _gameObjects) {
    foreach (Buff_Status gameObject in gameObjects) {
        setList(gameObject, _gameObjects);
    }
}
private void setList(ALLOBJECTS gameObject, List<ALLOBJECTS> _gameObjects) {
    if (_gameObjects == _allScripts) { //�����Ώی����S�̃��X�g�ɓ����ĂȂ�������
        if (!_allScripts.Contains(gameObject)) {
            _allScripts.Add(gameObject);
            //Debug.Log("new Object put in the _allObjects" + gameObject);
        } else {
            Debug.LogWarning(gameObject + "is already in" + _gameObjects);
        }
    } else {                                        //�����Ώی����S�̃��X�g�ɓ����Ă�����
        if (!_gameObjects.Contains(gameObject)) {
            if (!_allScripts.Contains(gameObject)) {
                _allScripts.Add(gameObject);
                //Debug.Log("new Object put in the _allObjects" + gameObject);
            }
            _gameObjects.Add(gameObject);
            //Debug.Log("add " + gameObject + " in " + _gameObjects);
        } else {
            Debug.LogWarning(gameObject + "is already in" + _gameObjects);
        }
    }
}
*/