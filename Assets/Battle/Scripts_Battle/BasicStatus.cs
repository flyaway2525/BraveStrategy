using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicStatus : MonoBehaviour {
    [SerializeField] string _type;
    [SerializeField] int _hp;
    [SerializeField] int _maxhp;
    [SerializeField] int _maxhp_add;
    [SerializeField] float _maxhp_rate = 1.0f;
    [SerializeField] int _atk;
    [SerializeField] int _atk_add;
    [SerializeField] float _atk_rate = 1.0f;
    [SerializeField] int _def;
    [SerializeField] int _def_add;
    [SerializeField] float _def_rate = 1.0f;
    [SerializeField] bool _life = true;


    //最終的なスキルの値を入れる。
    public void def_status_maxhp(int MaxHP) {
        maxhp = (int)((float)MaxHP * _maxhp_rate + _maxhp_add);
    }
    public void def_status_ATK(int ATK) {
        atk = (int)((float)ATK * _atk_rate + _atk_add);
    }
    public void def_status(int DEF) {
        def = (int)((float)DEF* _def_rate + _def_add);
    }

    public string type {
        set {
            _type = value;
        }
        get {
            return _type;
        }
    }
    public int hp {
        set {
            if (value <= 0) {
                Dead();
            } else if (value > _maxhp) {
                _hp = _maxhp;
            } else {
                _hp = value;
            }
        }
        get {
            return _hp;
        }
    }
    public int maxhp {
        set {
            if (value <= 0) {
                Dead();
            } else {
                _maxhp = value;
            }
        }
        get {
            return _maxhp;
        }
    }
    public float maxhp_rate {
        set {
            _maxhp_rate = value;
        }
        get {
            return _maxhp_rate;
        }
    }
    public int maxhp_add {
        set {
            _maxhp_add = value;
        }
        get {
            return _maxhp_add;
        }
    }
    public int atk {
        set {
            if (value <= 0) {
                _atk = 0;
            } else {
                _atk = value;
            }
        }
        get {
            return _atk;
        }
    }
    public int atk_add {
        set {
            _atk_add = value;
        }
        get {
            return _atk_add;
        }
    }
    public float atk_rate {
        set {
            _atk_rate = value;
        }
        get {
            return _atk_rate;
        }
    }
    public int def {
        set {
            if (value <= 0) {
                _def = 0;
            } else {
                _def = value;
            }
        }
        get {
            return _def;
        }
    }
    public int def_add {
        set {
            _def_add = value;
        }
        get {
            return _def_add;
        }
    }
    public float def_rate {
        set {
            _def_rate = value;
        }
        get {
            return _def_rate;
        }
    }
    public bool life {
        set {
            _life = value;
        }
        get {
            return _life;
        }
    }
    public void Dead() {
        life = false;
        this.gameObject.GetComponent<Renderer>().material.color = new Color(0.5f,0.2f,0.2f,1f);
        Debug.Log(this.gameObject + " is DEAD!");
        Ground_Controller.Instance.CheckPlayersLife();

    }

}
