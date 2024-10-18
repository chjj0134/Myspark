using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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

        // �����̴��� �����ϴ� Ư�� �������� �����̴��� ����ȭ
        if (SceneManager.GetActiveScene().name == "Main") // �����̴��� �ִ� �� �̸����� ����
        {
            if (StatManager.Instance != null)
            {
                StatManager.Instance.�Ƿε�Slider = GameObject.Find("�Ƿε�Slider")?.GetComponent<Slider>();
                StatManager.Instance.���Slider = GameObject.Find("���Slider")?.GetComponent<Slider>();
                StatManager.Instance.���ɵ�Slider = GameObject.Find("���ɵ�Slider")?.GetComponent<Slider>();
                StatManager.Instance.ưư��Slider = GameObject.Find("ưư��Slider")?.GetComponent<Slider>();
                StatManager.Instance.�����ο�Slider = GameObject.Find("�����ο�Slider")?.GetComponent<Slider>();
                StatManager.Instance.������Slider = GameObject.Find("������Slider")?.GetComponent<Slider>();
                StatManager.Instance.Gold = GameObject.Find("Gold")?.GetComponent<TextMeshProUGUI>();

                // �����̴� �� ����ȭ
                StatManager.Instance.UpdateSliders();
            }
        }
    }




    public void HidePopup()
    {
        popupPanel.SetActive(false);
        darkBackground.SetActive(false);
    }
}
