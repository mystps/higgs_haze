using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialoguePanel;
    public Text dialogueText;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("YourObjectTag"))
                {
                    string comment = "Your comment text";
                    Vector2 dialoguePosition = Camera.main.WorldToScreenPoint(hit.collider.transform.position);
                    OpenDialogueAtPosition(comment, dialoguePosition);
                }
            }
            else if (dialoguePanel.activeInHierarchy && !IsPointerOverUIObject())
            {
                CloseDialogue();
            }
        }
    }

    public void OpenDialogueAtPosition(string comment, Vector2 position)
    {
        dialoguePanel.SetActive(true);
        dialogueText.text = comment;
        RectTransform dialogueRectTransform = dialoguePanel.GetComponent<RectTransform>();
        dialogueRectTransform.anchoredPosition = position;
    }

    public void CloseDialogue()
    {
        dialoguePanel.SetActive(false);
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

        return results.Count > 0;
    }
}