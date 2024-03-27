using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class NavBarGUIManager : MonoBehaviour
{
    private VisualElement _root;
    private List<RadioButton> _navButtons = new();

    // Start is called before the first frame update
    void Start()
    {
        this._root = GetComponent<UIDocument>().rootVisualElement;
        this._navButtons.Add(this._root.Q<RadioButton>("NBVillagersButton"));
        this._navButtons.Add(this._root.Q<RadioButton>("NBFishButton"));
        this._navButtons.Add(this._root.Q<RadioButton>("NBBugsButton"));
        this._navButtons.Add(this._root.Q<RadioButton>("NBSeasButton"));
        this._navButtons.Add(this._root.Q<RadioButton>("NBHomeButton"));
        this._navButtons.Add(this._root.Q<RadioButton>("NBUserButton"));

        if (this.gameObject.name.Contains("Home"))
            this._navButtons.Find(x => x.name == "NBHomeButton").value = true;

        this._navButtons.Find(x => x.name == "NBVillagersButton").RegisterValueChangedCallback(evt => { if (evt.newValue) this.LoadVillagersScene(); });
        this._navButtons.Find(x => x.name == "NBFishButton").RegisterValueChangedCallback(evt => { if (evt.newValue) this.LoadFishScene(); });
        this._navButtons.Find(x => x.name == "NBBugsButton").RegisterValueChangedCallback(evt => { if (evt.newValue) this.LoadBugsScene(); });
        this._navButtons.Find(x => x.name == "NBSeasButton").RegisterValueChangedCallback(evt => { if (evt.newValue) this.LoadSeasScene(); });
        this._navButtons.Find(x => x.name == "NBHomeButton").RegisterValueChangedCallback(evt => { if (evt.newValue) this.LoadHomeScene(); });
        this._navButtons.Find(x => x.name == "NBUserButton").RegisterValueChangedCallback(evt => { if (evt.newValue) this.LoadUserScene(); });
    }
    private void LoadVillagersScene()
    {
         Debug.Log("LoadVillagersScene");
         SceneManager.LoadScene("VillagerScene");
    }

    private void LoadFishScene()
    {
        Debug.Log("LoadFishScene");
    }

    private void LoadBugsScene()
    {
         Debug.Log("LoadBugsScene");
         SceneManager.LoadScene("BugScene");    
    }
    private void LoadSeasScene()
    {
        Debug.Log("LoadSeasScene");
    }
    private void LoadHomeScene()
    {
         Debug.Log("LoadHomeScene");
         SceneManager.LoadScene("HomeScene");

    }
    private void LoadUserScene()
    {
         Debug.Log("LoadUserScene");
         SceneManager.LoadScene("UserScene");
    }
}
