using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupController : MonoBehaviour
{
    public GameObject popupPanel; // 팝업 패널 (배경 어두운 패널 포함)
    public GameObject darkBackground; // 어두운 배경

    void Start()
    {
        popupPanel.SetActive(false);
        darkBackground.SetActive(false);
    }

    public void ShowPopup()
    {
        popupPanel.SetActive(true);
        darkBackground.SetActive(true);
    }

    public void HidePopup()
    {
        popupPanel.SetActive(false);
        darkBackground.SetActive(false);
    }
}

