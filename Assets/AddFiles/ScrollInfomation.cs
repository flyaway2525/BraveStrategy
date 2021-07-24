using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class ScrollInfomation : MonoBehaviour{
    public Scrollbar scrollbar;
    public TextMeshProUGUI textMeshProUGUI;
    [SerializeField] int maxLine = 30;
    [SerializeField] float scrollbarValue = 0.0f;
    [SerializeField] List<string> contents = new List<string>();
    private int testInt = 0;

    private void Start() {
        if(scrollbar == null) {
            Debug.LogError("Scrollbar���Ȃ�");
        }
        if(textMeshProUGUI == null) {
            Debug.LogError("textMeshProUGUI���Ȃ�");
        }
    }
    public void ChangeInfomation() {
        string content;
        content = string.Join("\n",contents);
        textMeshProUGUI.text = content;
        scrollbarValue = 0;
        scrollbar.value = scrollbarValue;
    }
    public void AddInfomation(string addInfo) {
        while (contents.Count > maxLine) {
            contents.RemoveAt(0);//�����Ȃ�����1�v�f�ڂ��폜����
        }
        contents.Add(addInfo);
        ChangeInfomation();
    }

    public void AddInfoTest() {
        AddInfomation("�e�X�g�R�[�h�����s����܂���" + testInt);
        testInt++;
    }
}
