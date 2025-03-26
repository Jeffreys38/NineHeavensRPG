using UnityEngine;
using UnityEngine.EventSystems;

public class BtnAction : MonoBehaviour, IPointerClickHandler
{
    public GameObject uiObject;

    void Start()
    {
        uiObject.SetActive(false);
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        bool currentStatus = uiObject.activeSelf;
        uiObject.SetActive(!currentStatus);
    
        FlipHorizontal();
    }
    
    private void FlipHorizontal()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}