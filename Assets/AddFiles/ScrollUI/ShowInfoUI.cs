using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowInfoUI : MonoBehaviour{
    public TextMeshProUGUI textInfo;
    private float fadeOutSpeed = 0.8f;
    private float moveSpeed = 30.0f;
    private float moveUpDistance = 0.0f;
    public RectTransform rectTransform = null;
    public Transform parentTransform;
    public Canvas canvas;
    void Start(){
        textInfo = GetComponentInChildren<TextMeshProUGUI>();
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponent<Canvas>();
        parentTransform = transform.parent.gameObject.transform;
    }
    void Update() {
        transform.rotation = Camera.main.transform.rotation; //ÉJÉÅÉâÇÃï˚Çå¸Ç≠
        moveUpDistance += moveSpeed * Time.deltaTime;
        textInfo.color = Color.Lerp(textInfo.color, new Color(1f, 0f, 0f, 0f), fadeOutSpeed * Time.deltaTime);
        if (textInfo.color.a <= 0.3f) {
            Destroy(this.gameObject);
        }
        rectTransform.position = RectTransformUtility.WorldToScreenPoint(Camera.main,parentTransform.position);
        transform.position += Vector3.up * moveUpDistance;
    }
}
