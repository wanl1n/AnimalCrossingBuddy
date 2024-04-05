using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AdminUserManager : MonoBehaviour
{
    private VisualElement _root;
    private VisualElement _usersList;
    private Label _usersLabel;

    // Start is called before the first frame update
    void Start()
    {
        this._root = this.GetComponent<UIDocument>().rootVisualElement;
        this._usersList = this._root.Q<VisualElement>("UserList");
        this._usersLabel = this._root.Q<Label>("NoUsersLabel");

        StartCoroutine(LoadUsers());
    }

    public IEnumerator LoadUsers()
    {
        List<StringModel> users = new();

        yield return StartCoroutine(DatabaseManager.GetInstance().GetColumnData("Username", "users", u => users = u));

        foreach (var user in users)
        {
            VisualTreeAsset template = Resources.Load<VisualTreeAsset>("UserDisplay");
            VisualElement userDisplay = template.Instantiate();
            userDisplay.name = user.Data;

            Label username = userDisplay.Q<Label>("UserText");

            Image delete = userDisplay.Q<Image>("DeleteButton");
            delete.RegisterCallback<ClickEvent>(evt => StartCoroutine(DeleteUser(user.Data)));

            username.text = user.Data;

            this._usersList.Add(userDisplay);
        }

        if (this._usersList != null)
        {
            List<VisualElement> childToRemove = new List<VisualElement>();

            foreach (var child in this._usersList.Children())
            {
                bool inStringModel = false;
                foreach (var model in users)
                {
                    string modelIdName = model.Data;
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
                this._usersList.Remove(child);
            }

            if (users.Count >= 1)
            {
                this._usersLabel.style.display = DisplayStyle.None;
            }
        }
    }

    public IEnumerator DeleteUser(string data)
    {
        yield return StartCoroutine(DatabaseManager.GetInstance().DeleteUser(data));
        yield return StartCoroutine(LoadUsers());
    }
}
