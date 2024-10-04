using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupController : MonoBehaviour
{
    public GameObject popupPanel; // �˾� �г� (��� ��ο� �г� ����)
    public GameObject darkBackground; // ��ο� ���

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

