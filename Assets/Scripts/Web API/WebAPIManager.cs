using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Networking;

public class WebAPIManager : MonoBehaviour
{
    private static WebAPIManager Instance;

    /* Holds a reference to the API's base URL. */
    [SerializeField]
    private string _baseURL = "https://deckofcardsapi.com/api/deck/";

    [SerializeField]
    private string _apiKey = "abd2d0e756mshf3943921a275760p10386bjsn2ac6a39f7fd2";

    [SerializeField]
    private string _apiHost = "omgvamp-hearthstone-v1.p.rapidapi.com";

    private Event _event;

    public void GetEvents()
    {
        Debug.Log("Get Events");

        this.StartCoroutine(this.RequestEvents());
    }

    private IEnumerator RequestEvents()
    {
        string url = "";

        using (UnityWebRequest request = new UnityWebRequest(url, "GET"))
        {
            request.downloadHandler = new DownloadHandlerBuffer();
            yield return request.SendWebRequest();
            Debug.Log($"Response CODE : {request.responseCode}");

            if (string.IsNullOrEmpty(request.error))
            {
                Event response = JsonConvert.DeserializeObject<Event>(request.downloadHandler.text);
                this._event = response;
            }
            else Debug.Log($"Error CODE : {request.error}");
        }
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    public static WebAPIManager GetInstance()
    {
        if (Instance == null)
        {
            Instance = new WebAPIManager();
        }
        return Instance;
    }
}
