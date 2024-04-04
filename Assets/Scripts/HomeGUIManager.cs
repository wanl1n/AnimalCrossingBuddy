using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class HomeGUIManager : MonoBehaviour
{
    private VisualElement _root;

    private VisualElement _navBar;
    private VisualElement _dateTimeScreen;
    private VisualElement _featuredScreen;
    private VisualElement _eventsScreen;

    private VisualElement _catchableFish;
    private VisualElement _catchableBugs;
    private VisualElement _catchableSeas;

    private Label _catchableFishText;
    private Label _catchableBugsText;
    private Label _catchableSeasText;

    private Image _adminButton;

    private Toggle _hemisphereToggle;


    // Start is called before the first frame update
    void Start()
    {
        this._root = this.GetComponent<UIDocument>().rootVisualElement;

        this._adminButton = this._root.Q<Image>("AdminButton");

        this._navBar = this._root.Q<VisualElement>("NaviBar");
        this._dateTimeScreen = this._root.Q<VisualElement>("DateTimeScren");

        this._featuredScreen = this._root.Q<VisualElement>("FeaturedScreen");
        this._catchableFish = this._featuredScreen.Q<VisualElement>("CatchableFishList");
        this._catchableBugs = this._featuredScreen.Q<VisualElement>("CatchableBugsList");
        this._catchableSeas = this._featuredScreen.Q<VisualElement>("CatchableSeasList");

        this._catchableFishText = this._featuredScreen.Q<Label>("CatchableFishText");
        this._catchableBugsText = this._featuredScreen.Q<Label>("CatchableBugsText");
        this._catchableSeasText = this._featuredScreen.Q<Label>("CatchableSeasText");

        this._eventsScreen = this._root.Q<VisualElement>("EventsScreen");

        this._hemisphereToggle = this._root.Q<Toggle>("HemisphereToggle");


        this._adminButton.RegisterCallback<ClickEvent>(this.OnAdminButtonClick);

        this._hemisphereToggle.RegisterValueChangedCallback(
            evt =>
            {
                TimeManager.GetInstance().IsInSouthernHemisphere = evt.newValue;
                this.GetComponent<TimeDisplayGUIManager>().UpdateDisplay();
                this.GetComponent<TimeDisplayGUIManager>().UpdateNowList();

            }
        );
        this._hemisphereToggle.value = TimeManager.GetInstance().IsInSouthernHemisphere;

        StartCoroutine(Load());

    }
    private void OnAdminButtonClick(ClickEvent e)
    {
        SceneManager.LoadScene("AdminLoginScene");
    }

    private IEnumerator Load()
    {
        LoadingGUIManager.GetInstance().ShowLoading();
        yield return StartCoroutine(LoadAllCatchable());

    }

    public IEnumerator LoadAllCatchable()
    {

        yield return StartCoroutine(DatabaseManager.GetInstance().CreateNowPortraits(this._catchableFish, "Fish"));
        LoadingGUIManager.GetInstance().HideLoading();
        if (this._catchableFish.childCount != 0)
        {
            this._catchableFishText.style.display = DisplayStyle.None;
        }
        else
        {
            this._catchableFishText.style.display = DisplayStyle.Flex;
        }

        yield return StartCoroutine(DatabaseManager.GetInstance().CreateNowPortraits(this._catchableBugs, "Insects"));
        if (this._catchableBugs.childCount != 0)
        {
            this._catchableBugsText.style.display = DisplayStyle.None;
        }
        else
        {
            this._catchableBugsText.style.display = DisplayStyle.Flex;
        }

        yield return StartCoroutine(DatabaseManager.GetInstance().CreateNowPortraits(this._catchableSeas, "Sea_creatures"));
        if (this._catchableSeas.childCount != 0)
        {
            this._catchableSeasText.style.display = DisplayStyle.None;
        }
        else
        {
            this._catchableSeasText.style.display = DisplayStyle.Flex;
        }



    }
}
