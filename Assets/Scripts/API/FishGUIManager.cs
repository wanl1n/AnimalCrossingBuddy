using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;

public class FishGUIManager : MonoBehaviour
{
    private VisualElement _root;
    private VisualElement _listView;
    List<FishModel> _fishList = new();


    // Start is called before the first frame update
    public void Start()
    {
        this._root = this.GetComponent<UIDocument>().rootVisualElement;
        this._listView = _root.Q<VisualElement>(className: "catchable-list");


        StartCoroutine(StartFillList());
    }

    private IEnumerator StartFillList()
    {

        WWWForm form = new();

        using UnityWebRequest handler = UnityWebRequest.Post("http://localhost/sqlconnect/AnimalCrossingBuddy/fish.php", form);
        yield return handler.SendWebRequest();

        string[] result = handler.downloadHandler.text.Split('\t');

        if (result[0] == "0")
        {
            for (int i = 1; i < result.Length - 1; i++)
            {
                FishModel fish = JsonConvert.DeserializeObject<FishModel>(result[i]);
                _fishList.Add(fish);
            }

            StartCoroutine(CreateIconList());
        }
        else { Debug.LogError("php failed. [ERROR] : " + handler.error); }
    }

    private IEnumerator DownloadTexture(string url, System.Action<Texture2D> callback)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {

        }
        else
        {
            DownloadHandlerTexture response = (DownloadHandlerTexture)request.downloadHandler;
            callback(response.texture);
        }
    }

    private IEnumerator CreateIconList()
    {
        foreach (var fish in _fishList)
        {
            Texture2D icon = new(0, 0);
            
            yield return StartCoroutine(DownloadTexture(fish.IconImage, (tex) => icon = tex));

            VisualElement newIcon = new();
            newIcon.AddToClassList("portraits");
            newIcon.style.backgroundImage = new StyleBackground(icon);
            // newIcon.RegisterCallback<ClickEvent>(evt => OpenFishPopup());
            _listView.Add(newIcon);
        }

    }
}
