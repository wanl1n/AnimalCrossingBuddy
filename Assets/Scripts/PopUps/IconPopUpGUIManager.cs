using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class IconPopUpGUIManager : MonoBehaviour
{

    private VisualElement _root;
    private VisualElement _transparentBG;

    private Toggle _collectedToggle;

    private string _lastLoadedType;
    public string LastLoadedType
    {
        get { return this._lastLoadedType; }
    }

    private string _lastLoadedName;
    public string LastLoadedName
    {
        get { return this._lastLoadedName; }
    }

    private void Start()
    {
        this._root = this.GetComponent<UIDocument>().rootVisualElement;
        this._transparentBG = this._root.Q<VisualElement>("TransparentBG");

        this._collectedToggle = this._root.Q<Toggle>("CollectedToggle");

        this._collectedToggle.RegisterCallback<ClickEvent>(this.OnToggleChangeValue);
        this._transparentBG.RegisterCallback<PointerDownEvent>(this.OnTransparentBGClick);
    }

    private void OnTransparentBGClick(PointerDownEvent e)
    {
        this._transparentBG.style.display = DisplayStyle.None;

    }

    private void OnToggleChangeValue(EventBase evt)
    {
        StartCoroutine(DatabaseManager.GetInstance().UpdateUserData(this._lastLoadedName, this._collectedToggle.value));
    }

    public void Close()
    {
        this._transparentBG.style.display = DisplayStyle.None;
    }

    public IEnumerator LoadFishData(FishModel fish)
    {
        Texture2D iconTex = new(0, 0);
        yield return StartCoroutine(DatabaseManager.GetInstance().DownloadTexture(fish.IconImage, (tex) => iconTex = tex));
        this._transparentBG.style.display = DisplayStyle.Flex;

        Label iconName = this._transparentBG.Q<Label>("IconName");
        iconName.text = "<b><line-height=65%>" + fish.Name.ToUpper() + "</line-height></b>";

        VisualElement iconSprite = this._root.Q<VisualElement>("IconSprite");
        iconSprite.style.backgroundImage = new StyleBackground(iconTex);

        Label details = this._transparentBG.Q<Label>("Details");
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

        this._lastLoadedType = "Fish";
        this._lastLoadedName = fish.Name;
    }


    public IEnumerator LoadInsectData(InsectModel insect)
    {
        Texture2D iconTex = new(0, 0);
        yield return StartCoroutine(DatabaseManager.GetInstance().DownloadTexture(insect.IconImage, (tex) => iconTex = tex));
        this._transparentBG.style.display = DisplayStyle.Flex;

        Label iconName = this._transparentBG.Q<Label>("IconName");
        iconName.text = "<b><line-height=65%>" + insect.Name.ToUpper() + "</line-height></b>";

        VisualElement iconSprite = this._root.Q<VisualElement>("IconSprite");
        iconSprite.style.backgroundImage = new StyleBackground(iconTex);

        Label details = this._transparentBG.Q<Label>("Details");
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

        this._lastLoadedType = "Insects";
        this._lastLoadedName = insect.Name;
    }

    public IEnumerator LoadSeaCreatureData(SeaCreatureModel seaCreature)
    {
        Texture2D iconTex = new(0, 0);
        yield return StartCoroutine(DatabaseManager.GetInstance().DownloadTexture(seaCreature.IconImage, (tex) => iconTex = tex));
        this._transparentBG.style.display = DisplayStyle.Flex;

        Label iconName = this._transparentBG.Q<Label>("IconName");
        iconName.text = "<b><line-height=65%>" + seaCreature.Name.ToUpper() + "</line-height></b>";

        VisualElement iconSprite = this._root.Q<VisualElement>("IconSprite");
        iconSprite.style.backgroundImage = new StyleBackground(iconTex);

        Label details = this._transparentBG.Q<Label>("Details");
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

        this._lastLoadedType = "Sea Creatures";
        this._lastLoadedName = seaCreature.Name;
    }

    public IEnumerator LoadVillagerData(VillagerModel villager)
    {
        Texture2D iconTex = new(0, 0);
        yield return StartCoroutine(DatabaseManager.GetInstance().DownloadTexture(villager.PhotoImage, (tex) => iconTex = tex));
        this._transparentBG.style.display = DisplayStyle.Flex;

        Label iconName = this._transparentBG.Q<Label>("IconName");
        iconName.text = "<b><line-height=65%>" + villager.Name.ToUpper() + "</line-height></b>";

        VisualElement iconSprite = this._root.Q<VisualElement>("IconSprite");
        iconSprite.style.backgroundImage = new StyleBackground(iconTex);

        Label details = this._transparentBG.Q<Label>("Details");
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

        this._lastLoadedType = "Villagers";
        this._lastLoadedName = villager.Name;
    }

}
