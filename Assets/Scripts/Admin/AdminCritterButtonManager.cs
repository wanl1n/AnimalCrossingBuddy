using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AdminCritterButtonManager : MonoBehaviour
{
    private VisualElement _root;
    
    private Button _restore;
    private Button _clear;

    [SerializeField]
    private string _table;

    // Start is called before the first frame update
    void Start()
    {
        this._root = this.GetComponent<UIDocument>().rootVisualElement;

        this._restore = this._root.Q<Button>("Restore");
        this._clear = this._root.Q<Button>("Clear");

        this._restore.clickable.clicked += this.OnRestoreButtonClicked;
        this._clear.clickable.clicked += this.OnClearButtonClicked;
    }

    private void OnRestoreButtonClicked()
    {
        StartCoroutine(RestoreModels());
    }

    private void OnClearButtonClicked()
    {
        StartCoroutine(DeleteTable());
    }

    private IEnumerator DeleteTable()
    {
        List<StringModel> iconLinks = new();
        DatabaseManager.GetInstance().StopAllCoroutines();

        yield return StartCoroutine(DatabaseManager.GetInstance().GetNameColumnData("Icon Image Link", this._table.ToLower(), c => iconLinks = c));


        string type = this._table.ToLower();

        if (type == "sea_creatures")
        {
            type = "Sea Creatures";
        }

        foreach (StringModel link in iconLinks)
        {
            GameObject iconPopUpGUIManager = GameObject.FindGameObjectWithTag("Icon Manager");
            if (iconPopUpGUIManager != null)
                yield return StartCoroutine(DatabaseManager.GetInstance().DeleteModel(link.Name, type));
        }

        GameObject contentListGUIManager = GameObject.FindGameObjectWithTag("Scene Document");
        if (contentListGUIManager != null)
        {
            yield return StartCoroutine(contentListGUIManager.GetComponent<ContentListGUIManager>().ReloadAll());
        }
    }

    private IEnumerator RestoreModels()
    {
        DatabaseManager.GetInstance().StopAllCoroutines();
        yield return StartCoroutine(DatabaseManager.GetInstance().RestoreModels(this._table.ToLower()));

        GameObject contentListGUIManager = GameObject.FindGameObjectWithTag("Scene Document");
        if (contentListGUIManager != null)
        {
            yield return StartCoroutine(contentListGUIManager.GetComponent<ContentListGUIManager>().ReloadAll());
        }
    }
}
