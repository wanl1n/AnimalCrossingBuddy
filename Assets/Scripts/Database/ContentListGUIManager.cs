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

    private VisualElement _currentParent;
    private Label _currentText;

    private TextField _searchBarText;
    public string SearchBarText
    {
        get { return this._searchBarText.value.Trim();  }
    }

    [SerializeField]
    private GameObject _iconPopUpDocument;

    // Start is called before the first frame update
    private void Start()
    {
        this._root = this.GetComponent<UIDocument>().rootVisualElement;
        this._listParent = this._root.Q<VisualElement>(className: "catchable-list");
        
        this._currentParent = this._root.Q<VisualElement>("CurrentList");
        this._currentText = this._root.Q<Label>("CurrentText");

        this._searchBarText = this._root.Q<TextField>("SearchBar");

        this._searchBarText.RegisterValueChangedCallback(this.OnSearchBarValueChanged);

        StartCoroutine(LoadIcons());
    }

    private IEnumerator LoadIcons()
    {
        List<StringModel> iconLinks = new();
        
        LoadingGUIManager.GetInstance().ShowLoading();

        yield return StartCoroutine(DatabaseManager.GetInstance().GetColumnData("Icon Image Link", this._table.ToLower(), c => iconLinks = c)); 

        yield return StartCoroutine(this.LoadNowPortrait());
        LoadingGUIManager.GetInstance().HideLoading();

        yield return StartCoroutine(DatabaseManager.GetInstance().CreatePortraits(iconLinks, this._listParent, this._table));
    }

    public IEnumerator LoadNowPortrait()
    {
        yield return StartCoroutine(DatabaseManager.GetInstance().CreateNowPortraits(this._currentParent, this._table));
        if (this._currentParent.childCount != 0)
        {   
            this._currentText.style.display = DisplayStyle.None;
        }
        else
        {
            this._currentText.style.display = DisplayStyle.Flex;
        }
    }

    public IEnumerator LoadFishData(FishModel fish)
    {
        Texture2D iconTex = new(0, 0);
        yield return StartCoroutine(DatabaseManager.GetInstance().DownloadTexture(fish.IconImage, (tex) => iconTex = tex));
        VisualElement popupRoot = this._iconPopUpDocument.GetComponent<UIDocument>().rootVisualElement;
        VisualElement transparentBG = popupRoot.Q<VisualElement>("TransparentBG");
        transparentBG.style.display = DisplayStyle.Flex;

        Label iconName = transparentBG.Q<Label>("IconName");
        iconName.text = "<b><line-height=65%>" + fish.Name.ToUpper() + "</line-height></b>";

        VisualElement iconSprite = popupRoot.Q<VisualElement>("IconSprite");
        iconSprite.style.backgroundImage = new StyleBackground(iconTex);

        Label details = transparentBG.Q<Label>("Details");
        details.text = "";

        string monthAvailability = "";
        if (TimeManager.GetInstance().IsInSouthernHemisphere)
            monthAvailability = fish.SHAvailability;
        else
            monthAvailability = fish.NHAvailability;


        details.text = "<line-height=65%>" +
                        "<b>Sell Price: </b>" + fish.SellPrice +
                        "</line-height>\n" +

                        "<line-height=65%>" +
                        "<b>Location: </b>" + fish.Location +
                        "</line-height>\n" +

                        "<line-height=65%>" +
                        "<b>Shadow Size: </b>" + fish.ShadowSize +
                        "</line-height>\n" +

                        "<line-height=65%>" +
                        "<b>Catch Difficulty: </b>" + fish.CatchDifficulty +
                        "</line-height>\n" +

                        "<line-height=65%>" +
                        "<b>Total Catches: </b>" + fish.TotalCatches +
                        "</line-height>\n" +

                        "<line-height=65%>" +
                        "<b>Time: </b>" + fish.TimeAvailability +
                        "</line-height>\n" +


                        "<line-height=65%>" +
                        "<b>Months: </b>" + monthAvailability +
                        "</line-height>\n";

    }

    public IEnumerator LoadInsectData(InsectModel insect)
    {
        Texture2D iconTex = new(0, 0);
        yield return StartCoroutine(DatabaseManager.GetInstance().DownloadTexture(insect.IconImage, (tex) => iconTex = tex));
        VisualElement popupRoot = this._iconPopUpDocument.GetComponent<UIDocument>().rootVisualElement;
        VisualElement transparentBG = popupRoot.Q<VisualElement>("TransparentBG");
        transparentBG.style.display = DisplayStyle.Flex;

        Label iconName = transparentBG.Q<Label>("IconName");
        iconName.text = "<b><line-height=65%>" + insect.Name.ToUpper() + "</line-height></b>";

        VisualElement iconSprite = popupRoot.Q<VisualElement>("IconSprite");
        iconSprite.style.backgroundImage = new StyleBackground(iconTex);

        Label details = transparentBG.Q<Label>("Details");
        details.text = "";

        string monthAvailability = "";
        if (TimeManager.GetInstance().IsInSouthernHemisphere)
            monthAvailability = insect.SHAvailability;
        else
            monthAvailability = insect.NHAvailability;


        details.text = "<line-height=65%>" +
                        "<b>Sell Price: </b>" + insect.SellPrice +
                        "</line-height>\n" +

                        "<line-height=65%>" +
                        "<b>Location: </b>" + insect.Location +
                        "</line-height>\n" +

                        "<line-height=65%>" +
                        "<b>Weather: </b>" + insect.Weather +
                        "</line-height>\n" +

                        "<line-height=65%>" +
                        "<b>Total Catches: </b>" + insect.TotalCatches +
                        "</line-height>\n" +

                        "<line-height=65%>" +
                        "<b>Time: </b>" + insect.TimeAvailability +
                        "</line-height>\n" +


                        "<line-height=65%>" +
                        "<b>Months: </b>" + monthAvailability +
                        "</line-height>\n";

    }

    public IEnumerator LoadSeaCreatureData(SeaCreatureModel seaCreature)
    {
        Texture2D iconTex = new(0, 0);
        yield return StartCoroutine(DatabaseManager.GetInstance().DownloadTexture(seaCreature.IconImage, (tex) => iconTex = tex));
        VisualElement popupRoot = this._iconPopUpDocument.GetComponent<UIDocument>().rootVisualElement;
        VisualElement transparentBG = popupRoot.Q<VisualElement>("TransparentBG");
        transparentBG.style.display = DisplayStyle.Flex;

        Label iconName = transparentBG.Q<Label>("IconName");
        iconName.text = "<b><line-height=65%>" + seaCreature.Name.ToUpper() + "</line-height></b>";

        VisualElement iconSprite = popupRoot.Q<VisualElement>("IconSprite");
        iconSprite.style.backgroundImage = new StyleBackground(iconTex);

        Label details = transparentBG.Q<Label>("Details");
        details.text = "";

        string monthAvailability = "";
        if (TimeManager.GetInstance().IsInSouthernHemisphere)
            monthAvailability = seaCreature.SHAvailability;
        else
            monthAvailability = seaCreature.NHAvailability;


        details.text = "<line-height=65%>" +
                        "<b>Sell Price: </b>" + seaCreature.SellPrice +
                        "</line-height>\n" +

                        "<line-height=65%>" +
                        "<b>Shadow Size: </b>" + seaCreature.ShadowSize +
                        "</line-height>\n" +

                        "<line-height=65%>" +
                        "<b>Movement Speed: </b>" + seaCreature.MovementSpeed +
                        "</line-height>\n" +

                        "<line-height=65%>" +
                        "<b>Total Catches: </b>" + seaCreature.TotalCatches +
                        "</line-height>\n" +

                        "<line-height=65%>" +
                        "<b>Time: </b>" + seaCreature.TimeAvailability +
                        "</line-height>\n" +


                        "<line-height=65%>" +
                        "<b>Months: </b>" + monthAvailability +
                        "</line-height>\n";

    }

    public IEnumerator LoadVillagerData(VillagerModel villager)
    {
        Texture2D iconTex = new(0, 0);
        yield return StartCoroutine(DatabaseManager.GetInstance().DownloadTexture(villager.PhotoImage, (tex) => iconTex = tex));
        VisualElement popupRoot = this._iconPopUpDocument.GetComponent<UIDocument>().rootVisualElement;
        VisualElement transparentBG = popupRoot.Q<VisualElement>("TransparentBG");
        transparentBG.style.display = DisplayStyle.Flex;

        Label iconName = transparentBG.Q<Label>("IconName");
        iconName.text = "<b><line-height=65%>" + villager.Name.ToUpper() + "</line-height></b>";

        VisualElement iconSprite = popupRoot.Q<VisualElement>("IconSprite");
        iconSprite.style.backgroundImage = new StyleBackground(iconTex);

        Label details = transparentBG.Q<Label>("Details");
        details.text = "";


        details.text = "<line-height=65%>" +
                        "<b>Species: </b>" + villager.Species +
                        "</line-height>\n" +

                        "<line-height=65%>" +
                        "<b>Gender: </b>" + villager.Gender +
                        "</line-height>\n" +

                        "<line-height=65%>" +
                        "<b>Personality: </b>" + villager.Personality +
                        "</line-height>\n" +

                        "<line-height=65%>" +
                        "<b>Subtype: </b>" + villager.Subtype +
                        "</line-height>\n" +

                        "<line-height=65%>" +
                        "<b>Hobby: </b>" + villager.Hobby +
                        "</line-height>\n" +

                        "<line-height=65%>" +
                        "<b>Catchphrase: </b>" + villager.Catchphrase +
                        "</line-height>\n" +

                        "<line-height=65%>" +
                        "<b>Birthday: </b>" + villager.Birthday +
                        "</line-height>\n";

    }

    private IEnumerator LoadSearchedName()
    {
        List<StringModel> iconLinks = new();
        yield return StartCoroutine(DatabaseManager.GetInstance().GetModelData(this._table.ToLower(), c => iconLinks = c));
        yield return StartCoroutine(DatabaseManager.GetInstance().CreatePortraits(iconLinks, this._listParent, this._table));

        List<VisualElement> childToRemove = new List<VisualElement>();

        foreach (var child in this._listParent.Children())
        {
            bool inStringModel = false;
            foreach (var model in iconLinks)
            {
                string modelIdName = model.Id + "\t" + model.Name;
                if (child.name == modelIdName)
                {
                    inStringModel = true;
                    break;
                }
            }
            if (!inStringModel)
            {
                childToRemove.Add(child);
            }
        }

        foreach (var child in childToRemove)
        {
            this._listParent.Remove(child);
        }

    }

    private void OnSearchBarValueChanged(ChangeEvent<string> e)
    {
        this._listParent.Clear();
        StopCoroutine(LoadIcons());
        DatabaseManager.GetInstance().StopAllCoroutines();
        StartCoroutine(LoadSearchedName());
    }


}
