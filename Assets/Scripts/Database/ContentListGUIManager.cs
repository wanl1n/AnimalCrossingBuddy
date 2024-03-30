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
        
        LoadingGUIManager.GetInstance().ShowLoading();

        yield return StartCoroutine(DatabaseManager.GetInstance().GetColumnData("Icon Image Link", this._table.ToLower(), c => iconLinks = c)); ;
        LoadingGUIManager.GetInstance().HideLoading();
        
        yield return StartCoroutine(DatabaseManager.GetInstance().CreatePortraits(iconLinks, _listParent, _table));
    }

    public IEnumerator LoadFishData(FishModel fish)
    {
        VisualElement popupRoot = this._iconPopUpDocument.GetComponent<UIDocument>().rootVisualElement;
        VisualElement transparentBG = popupRoot.Q<VisualElement>("TransparentBG");
        transparentBG.style.display = DisplayStyle.Flex;

        Label iconName = transparentBG.Q<Label>("IconName");
        iconName.text = "<b><line-height=65%>" + fish.Name.ToUpper() + "</line-height></b>";

        VisualElement iconSprite = popupRoot.Q<VisualElement>("IconSprite");
        Texture2D iconTex = new(0, 0);
        yield return StartCoroutine(DatabaseManager.GetInstance().DownloadTexture(fish.IconImage, (tex) => iconTex = tex));
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
        VisualElement popupRoot = this._iconPopUpDocument.GetComponent<UIDocument>().rootVisualElement;
        VisualElement transparentBG = popupRoot.Q<VisualElement>("TransparentBG");
        transparentBG.style.display = DisplayStyle.Flex;

        Label iconName = transparentBG.Q<Label>("IconName");
        iconName.text = "<b><line-height=65%>" + insect.Name.ToUpper() + "</line-height></b>";

        VisualElement iconSprite = popupRoot.Q<VisualElement>("IconSprite");
        Texture2D iconTex = new(0, 0);
        yield return StartCoroutine(DatabaseManager.GetInstance().DownloadTexture(insect.IconImage, (tex) => iconTex = tex));
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
        VisualElement popupRoot = this._iconPopUpDocument.GetComponent<UIDocument>().rootVisualElement;
        VisualElement transparentBG = popupRoot.Q<VisualElement>("TransparentBG");
        transparentBG.style.display = DisplayStyle.Flex;

        Label iconName = transparentBG.Q<Label>("IconName");
        iconName.text = "<b><line-height=65%>" + seaCreature.Name.ToUpper() + "</line-height></b>";

        VisualElement iconSprite = popupRoot.Q<VisualElement>("IconSprite");
        Texture2D iconTex = new(0, 0);
        yield return StartCoroutine(DatabaseManager.GetInstance().DownloadTexture(seaCreature.IconImage, (tex) => iconTex = tex));
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
        VisualElement popupRoot = this._iconPopUpDocument.GetComponent<UIDocument>().rootVisualElement;
        VisualElement transparentBG = popupRoot.Q<VisualElement>("TransparentBG");
        transparentBG.style.display = DisplayStyle.Flex;

        Label iconName = transparentBG.Q<Label>("IconName");
        iconName.text = "<b><line-height=65%>" + villager.Name.ToUpper() + "</line-height></b>";

        VisualElement iconSprite = popupRoot.Q<VisualElement>("IconSprite");
        Texture2D iconTex = new(0, 0);
        yield return StartCoroutine(DatabaseManager.GetInstance().DownloadTexture(villager.PhotoImage, (tex) => iconTex = tex));
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
}
