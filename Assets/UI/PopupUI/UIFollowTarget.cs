using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class UIFollowTarget : MonoBehaviour {
	RectTransform rectTransform = null;
	[SerializeField] Transform target = null; //UIを表示する対象物
	[SerializeField] TextMeshProUGUI text;
	[SerializeField] float fadeOutSpeed = 1.0f;
	[SerializeField] Vector2 goUp;
	public Vector2 offset;

	void Start() {
		rectTransform = GetComponent<RectTransform>();
		text = GetComponent<TextMeshProUGUI>();
		goUp = new Vector2(0.0f,0.0f);
	}

	void Update() {
		if (target != null) {
			SetTarget(target.transform);
			rectTransform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, target.position) + offset + goUp;
			goUp += new Vector2(0.0f, 0.5f);
			text.color = Color.Lerp(text.color,new Color(1.0f,0.0f,0.0f,0.0f), fadeOutSpeed * Time.deltaTime);
			if (text.color.a <= 0.1f) {
				Destroy(gameObject);
			}
		} else {
			Debug.LogError("だめ");
        }
	}
	public void SetTarget(Transform targetTransform) {
		target = targetTransform;
    }
}