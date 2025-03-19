using UnityEngine;
using TMPro;

public class UIExpTextEffect : MonoBehaviour
{
    [SerializeField] public Canvas canvas;
    [SerializeField] private TextMeshProUGUI expText;
    [SerializeField] private float moveSpeed = 0.8f;
    [SerializeField] private float fadeDuration = 1.5f;

    private RectTransform _rectTransform;
    private float _timer;
    private Color _textColor;

    void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    void Start()
    {
        _rectTransform.rotation = Quaternion.Euler(0f, 180, 0f);
    }
    
    public void Initialize(int expAmount)
    {
        expText.text = $"+{expAmount} EXP";
        _textColor = expText.color; 
        _timer = 0;
        
        if (canvas.renderMode == RenderMode.WorldSpace && canvas.worldCamera == null)
        {
            canvas.worldCamera = Camera.main;
        }
    }

    void Update()
    {
        _timer += Time.deltaTime;
        
        // Làm mờ dần
        _textColor.a = Mathf.Lerp(1, 0, _timer / fadeDuration);
        expText.color = _textColor;
        
        if (_timer >= fadeDuration)
        {
           //  Destroy(gameObject);
        }
    }
}