using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class StatManager : MonoBehaviour
{
    public static StatManager Instance { get; private set; }

    // 스탯 변수
    public int 피로도 = 0;
    public int 허기 = 0;
    public int 관심도 = 0;
    public int 튼튼함 = 0;
    public int 지혜로움 = 0;
    public int 도덕성 = 0;
    public int 골드 = 0;
    public int 현재날짜 = 1;  // 기본적으로 1일부터 시작

    // 슬라이더 참조
    public Slider 피로도Slider;
    public Slider 피로도2Slider;
    public Slider 허기Slider;
    public Slider 관심도Slider;
    public Slider 튼튼함Slider;
    public Slider 지혜로움Slider;
    public Slider 도덕성Slider;
    public TextMeshProUGUI Gold;

    // 날짜 UI 텍스트 참조
    public TextMeshProUGUI dateText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        // 슬라이더 값을 스탯 값과 동기화
        피로도Slider.value = 피로도;
        피로도2Slider.value = 피로도;
        허기Slider.value = 허기;
        관심도Slider.value = 관심도;
        튼튼함Slider.value = 튼튼함;
        지혜로움Slider.value = 지혜로움;
        도덕성Slider.value = 도덕성;

        // 날짜 UI 업데이트
        if (dateText != null)
        {
            dateText.text = "Day: " + 현재날짜.ToString();
        }

        if (Gold != null)
        {
            Gold.text = "Gold: " + 골드.ToString();
        }
    }

    // 스탯 값을 변경하는 메서드
    public void AdjustStat(string statName, int amount)
    {
        switch (statName)
        {
            case "피로도":
                피로도 = Mathf.Clamp(피로도 + amount, 0, 10); // 예시: 최대 값이 10일 경우
                break;
            case "허기":
                허기 = Mathf.Clamp(허기 + amount, 0, 10);
                break;
            case "관심도":
                관심도 = Mathf.Clamp(관심도 + amount, 0, 15);
                break;
            case "튼튼함":
                튼튼함 = Mathf.Clamp(튼튼함 + amount, 0, 15);
                break;
            case "지혜로움":
                지혜로움 = Mathf.Clamp(지혜로움 + amount, 0, 15);
                break;
            case "도덕성":
                도덕성 = Mathf.Clamp(도덕성 + amount, 0, 15);
                break;
        }
    }

    // 날짜를 증가시키는 메서드
    public void AdjustDate(int days)
    {
        ResetDailyStats();

        현재날짜 += days;

        // 현재날짜가 7일을 넘으면 게임 엔딩 처리
        if (현재날짜 > 7)
        {
            // 엔딩 처리 로직 
            SceneManager.LoadScene("Ending/Endging");

            // 스탯 및 날짜 초기화
            ResetStats();
        }
    }
    
    //날짜가 지나면 초기화되는 메서드
    private void ResetDailyStats()
    {
        피로도 = 0; // 피로도 초기화
    }

    // 초기화 메서드
    public void ResetStats()
    {
        피로도 = 0;
        허기 = 0;
        관심도 = 0;
        튼튼함 = 0;
        지혜로움 = 0;
        도덕성 = 0;
        골드 = 0;
        현재날짜 = 1;
    }
}
