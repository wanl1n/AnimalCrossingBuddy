using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AdminIconPopUpManager : MonoBehaviour
{
    private VisualElement _root;
    private Button _deleteButton;


    void Start()
    {
        this._root = this.GetComponent<UIDocument>().rootVisualElement;
        this._deleteButton = this._root.Q<Button>("DeleteButton");

        this._deleteButton.clickable.clicked += this.OnDeleteButtonClicked;
    }

    private void OnDeleteButtonClicked()
    {
        StartCoroutine(DeleteModel());

        this.GetComponent<IconPopUpGUIManager>().Close();
    }

    private IEnumerator DeleteModel()
    {
        DatabaseManager.GetInstance().StopAllCoroutines();
        yield return StartCoroutine(DatabaseManager.GetInstance().DeleteModel(this.GetComponent<IconPopUpGUIManager>().LastLoadedName, this.GetComponent<IconPopUpGUIManager>().LastLoadedType));
        
        GameObject contentListGUIManager = GameObject.FindGameObjectWithTag("Scene Document");
        if (contentListGUIManager != null)
        {
            yield return StartCoroutine(contentListGUIManager.GetComponent<ContentListGUIManager>().ReloadAll());
        }
    }

}
