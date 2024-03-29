using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class IconPopUpGUIManager : MonoBehaviour
{
    private VisualElement _root;
    private VisualElement _transparentBG;

    private void Start()
    {
        this._root = this.GetComponent<UIDocument>().rootVisualElement;
        this._transparentBG = this._root.Q<VisualElement>("TransparentBG");

        this._transparentBG.RegisterCallback<PointerDownEvent>(this.OnTransparentBGClick);

    }

    void OnTransparentBGClick(PointerDownEvent e)
    {
        this._transparentBG.style.display = DisplayStyle.None;

    }
}
