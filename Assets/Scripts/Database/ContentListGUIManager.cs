using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;

public class ContentListGUIManager : MonoBehaviour
{
    [SerializeField]
    private string _table;
    public string Table
    {
        get { return this._table; }
    }

    private VisualElement _root;
    private VisualElement _listParent;
    // List<FishModel> _fishList = new();

    [SerializeField]
    private GameObject _iconPopUpDocument;

    // Start is called before the first frame update
    public void Start()
    {
        this._root = this.GetComponent<UIDocument>().rootVisualElement;
        this._listParent = _root.Q<VisualElement>(className: "catchable-list");

        StartCoroutine(LoadIcons());
    }

    private IEnumerator LoadIcons()
    {
        List<StringModel> iconLinks = new();
        // List<VisualElement> icons = new();
        
        LoadingGUIManager.GetInstance().ShowLoading();

        yield return StartCoroutine(DatabaseManager.GetInstance().GetColumnData("Icon Image Link", _table, c => iconLinks = c));
        LoadingGUIManager.GetInstance().HideLoading();
        
        yield return StartCoroutine(DatabaseManager.GetInstance().CreatePortraits(iconLinks, _listParent, _table));
        

        // foreach (var icon in icons)
        // {
        //     // Debug.Log(icon.name);
        //     _listParent.Add(icon);
        // }
    }

    public IEnumerator LoadIconData(string icon)
    {
        WWWForm form = new();
        form.AddField("table", _table);
        form.AddField("iconID", Int32.Parse(icon));

        using UnityWebRequest handler = UnityWebRequest.Post("http://localhost/sqlconnect/AnimalCrossingBuddy/getIconData.php", form);
        yield return handler.SendWebRequest();

        Debug.Log(handler.downloadHandler.text);

        string[] result = handler.downloadHandler.text.Split('\t');

        if (result[0] == "0")
        {
            VisualElement popupRoot = this._iconPopUpDocument.GetComponent<UIDocument>().rootVisualElement;
            VisualElement transparentBG = popupRoot.Q<VisualElement>("TransparentBG");
            transparentBG.style.display = DisplayStyle.Flex;

            Label iconName = transparentBG.Q<Label>("IconName");
            iconName.text = result[1].ToUpper();
        }
        else { Debug.LogError("GetIconData failed. [ERROR] : " + handler.error); }
    }
}
