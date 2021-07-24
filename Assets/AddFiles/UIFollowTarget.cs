using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIFollowTarget : MonoBehaviour {
	RectTransform rectTransform = null;
	[SerializeField] Transform target = null;
	public Vector2 offset;

	void Awake() {
		rectTransform = GetComponent<RectTransform>();
	}

	void Update() {
		if (target != null) {
			rectTransform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, target.position) + offset;
        } else {
			Debug.LogError("だめ");
        }
	}
	public void SetTarget(Transform targetTransform) {
		target = targetTransform;
    }
}