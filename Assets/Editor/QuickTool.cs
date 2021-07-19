using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class QuickTool : EditorWindow
{
    [MenuItem("QuickTool/Open _%#T")]
    public static void ShowWindow() {
        // Opens the window, otherwise focuses it if itÅfs already open.
        var window = GetWindow<QuickTool>();

        // Adds a title to the window.
        window.titleContent = new GUIContent("QuickTool");

        // Sets a minimum size to the window.
        window.minSize = new Vector2(250, 50);
    }
    private void OnEnable() {
        // Reference to the root of the window.
        var root = rootVisualElement;
        // Creates our button and sets its Text property.
        var myButton = new Button() { text = "My Button" };

        // Gives it some style.
        myButton.style.width = 160;
        myButton.style.height = 30;

        // Adds it to the root.
        root.Add(myButton);
    }
}