using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LoadingGUIManager : MonoBehaviour
{
    private VisualElement _root;

    private Image _loadingSprite;

    [SerializeField]
    private List<Sprite> _animation;
    private int[] degrees;
    private int _animationFrame = 0;

    // Start is called before the first frame update
    void Start()
    {
        this._root = this.GetComponent<UIDocument>().rootVisualElement;

        _loadingSprite = _root.Q<Image>("LoadingSprite");
        _loadingSprite.schedule.Execute(AdvanceAnimationFrame).Every(60);
    }

    private void AdvanceAnimationFrame()
    {
        _loadingSprite.style.backgroundImage = new(_animation[_animationFrame]);

        if (_animationFrame == _animation.Count - 1)
            _animationFrame = 0;
        else
            _animationFrame++;
    }

    // Update is called once per frame
    void Update()
    {

    }


}
