using System.Collections;
using UnityEngine;
using TMPro;

namespace JeffreyInc.UI.Screens
{
    public class CreditScene : MonoBehaviour
    {
        public TMP_Text creditTextTemplate;
        public TMP_FontAsset creditFont;
        public GameObject creditsPanel;
        public float scrollSpeed = 50f;

        private RectTransform panelRect;

        void Start()
        {
            panelRect = creditsPanel.GetComponent<RectTransform>();

            string[] titles =
            {
                "Jeffrey Studio",
                "A Game of Cultivation, Power, and Immortality",
            };

            StartCoroutine(DisplayTitlesSequentially(titles));
        }

        IEnumerator DisplayTitlesSequentially(string[] titles)
        {
            foreach (string title in titles)
            {
                AddTitle(title);
                yield return new WaitForSeconds(3f);
            }

            StartScroll();
        }

        void AddTitle(string title)
        {
            TMP_Text titleText = Instantiate(creditTextTemplate, transform.parent);
            titleText.text = title;
            titleText.fontSize = 45;
            titleText.fontStyle = FontStyles.Bold;

            RectTransform rectTransform = titleText.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = Vector2.zero;

            StartCoroutine(FadeIn(titleText));
        }

        void StartScroll()
        {
            AddCreditLine("Director", "Jeffrey");
            AddCreditLine("Producer", "Jeffrey");
            AddCreditLine("Lead Developer", "Jeffrey");
            AddCreditLine("Art Director", "Jeffrey");
            AddCreditLine("Lead Artist", "Jeffrey");
            AddCreditLine("Game Designer", "Jeffrey");
            AddCreditLine("Sound Designer", "Jeffrey");
            AddCreditLine("Music Composer", "Jeffrey");

            StartCoroutine(ScrollCredits());
        }

        IEnumerator ScrollCredits()
        {
            float panelHeight = panelRect.rect.height;
            float viewportHeight = Screen.height;

            while (panelRect.anchoredPosition.y < Screen.height + panelHeight + 270)
            {
                panelRect.anchoredPosition += new Vector2(0, scrollSpeed * Time.deltaTime);
                yield return null;
            }
            
            // Logic after scrolling
        }

        IEnumerator FadeIn(TMP_Text titleText)
        {
            Color initialColor = titleText.color;
            titleText.color = new Color(initialColor.r, initialColor.g, initialColor.b, 0);

            float duration = 2f;
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                float alpha = Mathf.Lerp(0, 1, elapsedTime / duration);
                titleText.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);
                elapsedTime += Time.deltaTime;

                yield return null;
            }

            titleText.color = new Color(initialColor.r, initialColor.g, initialColor.b, 1);
            Destroy(titleText.gameObject);
        }

        void AddCreditLine(string leftText, string rightText)
        {
            TMP_Text left = Instantiate(creditTextTemplate, creditsPanel.transform);
            TMP_Text right = Instantiate(creditTextTemplate, creditsPanel.transform);

            left.text = leftText + ":";
            right.text = rightText;

            left.fontStyle = FontStyles.Bold;
            left.fontSize = 33;
            right.fontStyle = FontStyles.Normal;
            right.fontSize = 33;
        }
    }
}