using System;
using UnityEngine;

public class Note : MonoBehaviour
{
    [TextArea]
    public string noteText;

    [SerializeField] private GameObject noteUI;
    [SerializeField] private TMPro.TextMeshProUGUI noteUIText;

    private bool isOpen = false;

    public void OpenNote()
    {
        noteUI.SetActive(true);
        noteUIText.text = noteText;
        isOpen = true;
        Time.timeScale = 0f; 
    }

    public void CloseNote()
    {
        noteUI.SetActive(false);
        isOpen = false;
        Time.timeScale = 1f;
    }

    private void Start()
    {
        noteUI.SetActive(false);
    }
}
