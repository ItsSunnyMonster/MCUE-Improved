/*
 *   Copyright (c) 2022 ItsSunnyMonster

 *   This program is free software: you can redistribute it and/or modify
 *   it under the terms of the GNU General Public License as published by
 *   the Free Software Foundation, either version 3 of the License, or
 *   (at your option) any later version.

 *   This program is distributed in the hope that it will be useful,
 *   but WITHOUT ANY WARRANTY; without even the implied warranty of
 *   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *   GNU General Public License for more details.

 *   You should have received a copy of the GNU General Public License
 *   along with this program.  If not, see <https://www.gnu.org/licenses/>.
 */

using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public static class CustomMenus
{
    [MenuItem("GameObject/UI/Minecraft/Button")]
    private static void CreateButton()
    {
        UnityEngine.Debug.LogWarning("Button is not implemented yet!");
        return;

        GameObject button = null;
        // If nothing is selected
        if (Selection.activeTransform == null)
        {
            // Find a canvas object
            var canvas = Object.FindObjectOfType<Canvas>();
            // If no canvas in the scene
            if (canvas == null)
            {
                // Create a new canvas
                var canvasGameObject = new GameObject("Canvas", typeof(RectTransform), typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
                canvas = canvasGameObject.GetComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            }
            // Spawn the button which is parented to the canvas
            button = Object.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/UI/Button.prefab"), canvas.transform);
        }
        // If something is selected
        else
        {
            // Get the selected transform
            var selection = Selection.activeTransform;
            // If the selected object has component canvas or if its parents and grand parents has component canvas
            if (selection.TryGetComponent<Canvas>(out _) || selection.GetComponentInParent<Canvas>() != null)
            {
                // Spawn the button which is parented to the selection
                button = Object.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/UI/Button.prefab"), selection);
            }
            // If the selection doesn't have canvas or its parents doesn't have canvas
            else
            {
                Canvas canvas = null;
                // Check all the children
                for (var i = 0; i < selection.childCount; i++)
                {
                    // If canvas not found then continue searching
                    canvas = selection.GetChild(i).GetComponent<Canvas>();
                    if (canvas == null) continue;
                    // If canvas does get found then spawn button parented to the children
                    button = Object.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/UI/Button.prefab"), canvas.transform);
                    // Stop searching
                    break;
                }
                // If no children has canvas
                if (canvas == null)
                {
                    // Create a new canvas and parent it to the selection
                    var canvasObject = new GameObject("Canvas", typeof(RectTransform), typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
                    canvasObject.transform.SetParent(selection);
                    canvas = canvasObject.GetComponent<Canvas>();
                    // Spawn button parented to the canvas
                    button = Object.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/UI/Button.prefab"), canvas.transform);
                }
            }
        }

        System.Diagnostics.Debug.Assert(button != null, nameof(button) + " != null");
        // Rename button
        button.name = "Button";

        // Select the newly created button
        Selection.activeGameObject = button;


        /*
        var canvas = Selection.activeTransform.GetComponentInChildren<Canvas>(true);
        if (canvas == null)
        {
            var canvasGameObject = new GameObject("Canvas", typeof(RectTransform), typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
            canvas = canvasGameObject.GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        } 
        Object.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/UI/Button.prefab"), canvas.transform).name = "Button";
        */
    }

    [MenuItem("GameObject/UI/Minecraft/Text")]
    // Same thing as CreateButton
    private static void CreateText()
    {
        GameObject text = null;
        if (Selection.activeTransform == null)
        {
            var canvas = Object.FindObjectOfType<Canvas>();
            if (canvas == null)
            {
                var canvasGameObject = new GameObject("Canvas", typeof(RectTransform), typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
                canvas = canvasGameObject.GetComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            }
            text = Object.Instantiate(Resources.Load<GameObject>("Prefabs/UI/Text"), canvas.transform);
        }
        else
        {
            var selection = Selection.activeTransform;
            if (selection.TryGetComponent<Canvas>(out var _) || selection.GetComponentInParent<Canvas>() != null)
            {
                text = Object.Instantiate(Resources.Load<GameObject>("Prefabs/UI/Text"), selection);
            }
            else
            {
                Canvas canvas = null;
                for (var i = 0; i < selection.childCount; i++)
                {
                    canvas = selection.GetChild(i).GetComponent<Canvas>();
                    if (canvas == null) continue;
                    text = Object.Instantiate(Resources.Load<GameObject>("Prefabs/UI/Text"), canvas.transform);
                    break;
                }
                if (canvas == null)
                {
                    var canvasObject = new GameObject("Canvas", typeof(RectTransform), typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
                    canvasObject.transform.SetParent(selection);
                    canvas = canvasObject.GetComponent<Canvas>();
                    text = Object.Instantiate(Resources.Load<GameObject>("Prefabs/UI/Text"), canvas.transform);
                }
            }
        }

        Debug.Assert(text != null, nameof(text) + " != null");
        text.name = "Text";

        Selection.activeGameObject = text;
    }

    [MenuItem("Tools/SunnyMonster/Open Persistent Data Path")]
    private static void OpenPersistentPath()
    {
        EditorUtility.RevealInFinder(Application.persistentDataPath);
    }

    [MenuItem("Tools/SunnyMonster/Open Data Path")]
    private static void OpenDataPath()
    {
        EditorUtility.RevealInFinder(Application.dataPath);
    }
}