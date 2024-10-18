using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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

        // 슬라이더가 존재하는 특정 씬에서만 슬라이더를 동기화
        if (SceneManager.GetActiveScene().name == "Main") // 슬라이더가 있는 씬 이름으로 변경
        {
            if (StatManager.Instance != null)
            {
                StatManager.Instance.피로도Slider = GameObject.Find("피로도Slider")?.GetComponent<Slider>();
                StatManager.Instance.허기Slider = GameObject.Find("허기Slider")?.GetComponent<Slider>();
                StatManager.Instance.관심도Slider = GameObject.Find("관심도Slider")?.GetComponent<Slider>();
                StatManager.Instance.튼튼함Slider = GameObject.Find("튼튼함Slider")?.GetComponent<Slider>();
                StatManager.Instance.지혜로움Slider = GameObject.Find("지혜로움Slider")?.GetComponent<Slider>();
                StatManager.Instance.도덕성Slider = GameObject.Find("도덕성Slider")?.GetComponent<Slider>();
                StatManager.Instance.Gold = GameObject.Find("Gold")?.GetComponent<TextMeshProUGUI>();

                // 슬라이더 값 동기화
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
