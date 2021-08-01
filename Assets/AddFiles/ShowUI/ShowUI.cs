using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* ‰ñ“]‚ÉŠÖ‚·‚é•û–@‚ÌŽ‘—¿
https://tama-lab.net/2017/06/unity%E3%81%A7%E3%82%AA%E3%83%96%E3%82%B8%E3%82%A7%E3%82%AF%E3%83%88%E3%82%92%E5%9B%9E%E8%BB%A2%E3%81%95%E3%81%9B%E3%82%8B%E6%96%B9%E6%B3%95%E3%81%BE%E3%81%A8%E3%82%81/
 */
public class ShowUI : MonoBehaviour {
    [SerializeField] bool move;
    [SerializeField] Vector3 rotateTo;
    [SerializeField] string[] text;
    void Awake(){
        transform.eulerAngles = new Vector3(90.0f, 0, 0);
        EnterMotion();
    }
    void Update(){
        if (move) {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(rotateTo), Time.deltaTime * 100.0f);
            //Debug.Log("‰ñ‚Á‚Ä‚é‚Í‚¸" + transform.rotation.eulerAngles + "  " + rotateTo);
            if (Mathf.Abs(transform.rotation.eulerAngles.x - rotateTo.x) < 2.0f) {
                transform.eulerAngles = rotateTo;
                move = false;
            }
        }
    }

    public void EnterMotion() {
        move = true;
        rotateTo = new Vector3(0, 0, 0);
    }
    public void ExitMotion() {
        move = true;
        rotateTo = new Vector3(90.0f, 0, 0);
    }
    public void SetText(string[] AddText) {
        text = null;
        text = AddText;
    }
    public void EnterText() {

    }
    public void ExitText() {

    }
}
