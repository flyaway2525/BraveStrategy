using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 プレイヤーの動いた経路に線を引くScript
 */
public class PlayerParticleScript : MonoBehaviour
{
    public Player_Ground Player_Ground;
    [SerializeField] ParticleSystem particle;
    [SerializeField] List<Ground> _pass_Grounds;
    void Start() {
        Player_Ground = transform.parent.GetComponent<Player_Ground>();
        transform.position = Player_Ground.transform.position;
        _pass_Grounds.Clear();
        _pass_Grounds.AddRange(Player_Ground.pass_grounds);
        transform.parent = transform.parent.parent;
    }

    // Update is called once per frame
    void Update() {
        if(1 <= _pass_Grounds.Count) {
            float distance = Vector3.Distance(transform.position, _pass_Grounds[0].transform.position);
            if (0.01f < distance) {
                transform.position = Vector3.MoveTowards(transform.position, _pass_Grounds[0].transform.position, 2.0f * Time.deltaTime * 6.0f);
            } else {
                gameObject.transform.position = _pass_Grounds[0].transform.position;
                if (1 < _pass_Grounds.Count) {
                    _pass_Grounds.Remove(_pass_Grounds[0]);
                } else {
                    StartCoroutine("waitTime");
                }
            }
        }
    }
    private IEnumerator waitTime() {
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }
    
}
