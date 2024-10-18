using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
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
    public int 현재날짜 = 1;

    // 슬라이더 참조
    public Slider 피로도Slider;
    public Slider 피로도2Slider;
    public Slider 허기Slider;
    public Slider 관심도Slider;
    public Slider 튼튼함Slider;
    public Slider 지혜로움Slider;
    public Slider 도덕성Slider;
    public TextMeshProUGUI Gold;
    public TextMeshProUGUI dateText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // 처음 시작 여부를 판단
            if (PlayerPrefs.GetInt("게임시작됨", 0) == 0)
            {
                // 처음 실행 시 스탯 초기화
                ResetStats();
                PlayerPrefs.SetInt("게임시작됨", 1);  // 게임이 시작됨을 기록
                PlayerPrefs.Save();
            }
            else
            {
                // 저장된 데이터 불러오기
                LoadStatsFromPlayerPrefs();
            }

            SceneManager.sceneLoaded += OnSceneLoaded;  // 씬 로드 이벤트 연결
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("씬 이름: " + scene.name);

        if (scene.name == "Main")  // Main 씬일 때만 실행
        {
            // 피로도2Slider와 dateText 연결
            피로도2Slider = GameObject.Find("피로도2Slider")?.GetComponent<Slider>();
            dateText = GameObject.Find("Date")?.GetComponent<TextMeshProUGUI>();

            if (피로도2Slider != null)
            {
                피로도2Slider.maxValue = 10;
                피로도2Slider.value = 피로도;  // 피로도 값 동기화
            }
            else
            {
                Debug.LogError("피로도2Slider를 찾을 수 없습니다.");
            }

            if (dateText != null)
            {
                dateText.text = "Day: " + 현재날짜.ToString();  // 날짜 텍스트 동기화
            }
            else
            {
                Debug.LogError("dateText를 찾을 수 없습니다.");
            }
        }
        else if (scene.name == "Title")  // Title 씬에서만 초기화
        {
            ResetStats();  // Title 씬에서 스탯을 초기화
        }
    }

    private void OnEnable()
    {
        // 씬이 다시 로드될 때 이벤트를 재등록
        SceneManager.sceneLoaded += OnSceneLoaded;

        // 씬 이동 후 UI 요소들을 다시 참조
        피로도Slider = GameObject.Find("피로도Slider")?.GetComponent<Slider>();
        허기Slider = GameObject.Find("허기Slider")?.GetComponent<Slider>();
        관심도Slider = GameObject.Find("관심도Slider")?.GetComponent<Slider>();
        튼튼함Slider = GameObject.Find("튼튼함Slider")?.GetComponent<Slider>();
        지혜로움Slider = GameObject.Find("지혜로움Slider")?.GetComponent<Slider>();
        도덕성Slider = GameObject.Find("도덕성Slider")?.GetComponent<Slider>();
        Gold = GameObject.Find("Gold")?.GetComponent<TextMeshProUGUI>();

        UpdateSliders(); // OnEnable에서 슬라이더 값 동기화
    }

    private void OnDisable()
    {
        // 메모리 누수를 방지하기 위해 이벤트 해제
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        UpdateSliders(); // Start에서 슬라이더 값 동기화
    }

    private void Update()
    {
        UpdateSliders(); // 매 프레임마다 슬라이더 값 업데이트
    }

    public void UpdateSliders()
    {
        // 슬라이더 값을 스탯 값과 동기화
        if (피로도Slider != null) 피로도Slider.value = 피로도;
        if (피로도2Slider != null) 피로도2Slider.value = 피로도;
        if (허기Slider != null) 허기Slider.value = 허기;
        if (관심도Slider != null) 관심도Slider.value = 관심도;
        if (튼튼함Slider != null) 튼튼함Slider.value = 튼튼함;
        if (지혜로움Slider != null) 지혜로움Slider.value = 지혜로움;
        if (도덕성Slider != null) 도덕성Slider.value = 도덕성;

        // 날짜 UI 업데이트
        if (dateText != null)
        {
            dateText.text = "Day: " + 현재날짜.ToString();
        }

        // 골드 UI 업데이트
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
                피로도 = Mathf.Clamp(피로도 + amount, 0, 10);
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
        SaveStatsToPlayerPrefs(); // 스탯이 변경될 때마다 저장
    }

    // 날짜를 증가시키는 메서드
    public void AdjustDate(int days)
    {
        ResetDailyStats();

        현재날짜 += days;

        if (현재날짜 > 7)
        {
            // 7일차를 넘으면 Title 씬으로 이동
            SceneManager.LoadScene("Title");
        }
        else
        {
            SaveStatsToPlayerPrefs(); // 날짜 변경 시에도 저장
        }
    }

    private void ResetDailyStats()
    {
        피로도 = 0;
    }

    // 스탯 초기화 메서드 (Title 씬에서만 실행)
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
        SaveStatsToPlayerPrefs(); // 초기화 후 저장
    }

    public void SaveStatsToPlayerPrefs()
    {
        PlayerPrefs.SetInt("피로도", 피로도);
        PlayerPrefs.SetInt("허기", 허기);
        PlayerPrefs.SetInt("관심도", 관심도);
        PlayerPrefs.SetInt("튼튼함", 튼튼함);
        PlayerPrefs.SetInt("지혜로움", 지혜로움);
        PlayerPrefs.SetInt("도덕성", 도덕성);
        PlayerPrefs.SetInt("골드", 골드);
        PlayerPrefs.SetInt("현재날짜", 현재날짜);
        PlayerPrefs.Save();
    }

    public void LoadStatsFromPlayerPrefs()
    {
        피로도 = PlayerPrefs.GetInt("피로도", 0);
        허기 = PlayerPrefs.GetInt("허기", 0);
        관심도 = PlayerPrefs.GetInt("관심도", 0);
        튼튼함 = PlayerPrefs.GetInt("튼튼함", 0);
        지혜로움 = PlayerPrefs.GetInt("지혜로움", 0);
        도덕성 = PlayerPrefs.GetInt("도덕성", 0);
        골드 = PlayerPrefs.GetInt("골드", 0);
        현재날짜 = PlayerPrefs.GetInt("현재날짜", 1);
    }
}
