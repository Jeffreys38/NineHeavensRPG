using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TMP_LinkHandler : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private string idName = default;
    
    [Header("Broadcasting on")]
    [SerializeField] private VoidEventChannelSO _onAction = default;
    
    private TMP_Text tmpText;

    void Awake()
    {
        tmpText = GetComponent<TMP_Text>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        int linkIndex = TMP_TextUtilities.FindIntersectingLink(tmpText, Input.mousePosition, null);
        if (linkIndex != -1)
        {
            TMP_LinkInfo linkInfo = tmpText.textInfo.linkInfo[linkIndex];
            string linkID = linkInfo.GetLinkID();
            
            HandleLinkClick(linkID);
        }
    }

    private void HandleLinkClick(string linkID)
    {
        if (linkID == idName)
        {
            _onAction.RaiseEvent();
        }
    }
}