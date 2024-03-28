using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LoadingGUIManager : MonoBehaviour
{
    private static LoadingGUIManager _instance;

    private VisualElement _root;

    private Image _loadingSprite;

    [SerializeField]
    private List<Sprite> _animation;
    private int _animationFrame = 0;
    
    private int _degrees = 0;
    
    [SerializeField]
    private int _degreeIncrement = 10;
    
    [SerializeField]
    private int _degreeLimit = 20; 

    // Start is called before the first frame update
    private void Start()
    {
        this._root = this.GetComponent<UIDocument>().rootVisualElement;

        _loadingSprite = _root.Q<Image>("LoadingSprite");
        _loadingSprite.schedule.Execute(AdvanceAnimationFrame).Every(60);
    
        HideLoading();
    }

    public void ShowLoading()
    {
        _root.style.display = DisplayStyle.Flex;
    }

    public void HideLoading()
    {
        _root.style.display = DisplayStyle.None;
    }

    private void AdvanceAnimationFrame()
    {
        if (_root.style.display == DisplayStyle.None)
            return;

        _loadingSprite.style.backgroundImage = new(_animation[_animationFrame]);
        _loadingSprite.style.rotate = new Rotate(_degrees);

        if (_degrees + _degreeIncrement < _degreeLimit)
        {
            _degrees += _degreeIncrement;
        }
        else
            _degrees = -_degreeLimit;

        if (_animationFrame == _animation.Count - 1)
            _animationFrame = 0;
        else
            _animationFrame++;
    }

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public static LoadingGUIManager GetInstance()
    {
        if (_instance == null)
        {
            _instance = new();
        }
        return _instance;
    }
}
