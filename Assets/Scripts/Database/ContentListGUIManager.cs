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

    private VisualElement _root;
    private VisualElement _listParent;
    // List<FishModel> _fishList = new();


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
}
