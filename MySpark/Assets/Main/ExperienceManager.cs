using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExperienceManager : MonoBehaviour
{
    public static ExperienceManager Instance { get; private set; }

    // 경험치와 레벨 변수
    public int 현재경험치 = 0;
    public int 레벨 = 1;
    public int 경험치최대값 = 100;  // 경험치 최대 값

    // 경험치 UI 요소
    public Slider 경험치Slider;
    public TextMeshProUGUI 레벨Text;

    private SoundManager soundManager;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 간 상태 유지
            LoadExperienceData(); // 저장된 경험치 데이터 불러오기
            SceneManager.sceneLoaded += OnSceneLoaded;  // 씬 로드 이벤트 연결
        }
        else
        {
            Destroy(gameObject); // 중복된 인스턴스 방지
        }
    }

    // 씬이 로드될 때 호출되는 메서드
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Main")  // 메인 씬으로 돌아왔을 때만 실행
        {
            // 경험치 슬라이더와 레벨 텍스트 다시 찾기
            경험치Slider = GameObject.Find("Ex")?.GetComponent<Slider>();
            레벨Text = GameObject.Find("Level")?.GetComponent<TextMeshProUGUI>();

            if (경험치Slider != null)
            {
                경험치Slider.maxValue = 경험치최대값;
                경험치Slider.value = 현재경험치;  // 경험치 값 동기화
            }
            else
            {
                Debug.LogError("경험치Slider를 찾을 수 없습니다.");
            }

            if (레벨Text != null)
            {
                레벨Text.text = "Level: " + 레벨.ToString();  // 레벨 텍스트 동기화
            }
            else
            {
                Debug.LogError("레벨Text를 찾을 수 없습니다.");
            }

            UpdateUI(); // UI 동기화
        }
    }

    private void Start()
    {
        UpdateUI(); // 초기 UI 업데이트
    }

    private void Update()
    {
        // 슬라이더 값을 경험치 값과 동기화
        if (경험치Slider != null)
            경험치Slider.value = 현재경험치;

        // 레벨 텍스트 업데이트
        if (레벨Text != null)
            레벨Text.text = "Level: " + 레벨.ToString();
    }

    // 경험치 추가 메서드
    public void AddExperience(int amount)
    {
        현재경험치 += amount;

        // 경험치가 최대값을 넘으면 레벨업
        if (현재경험치 >= 경험치최대값)
        {
            LevelUp();
        }

        UpdateUI();
        SaveExperienceData(); // 데이터 저장
    }

    private void LevelUp()
    {
        레벨 += 1;  // 레벨 증가
        현재경험치 -= 경험치최대값;  // 남은 경험치 반영

        if (soundManager != null)
        {
            soundManager.PlayLevelUpSE();  // 레벨업 효과음 재생
        }
    }

    // UI를 업데이트하는 메서드
    private void UpdateUI()
    {
        // 슬라이더 값과 텍스트 업데이트
        if (경험치Slider != null)
            경험치Slider.value = 현재경험치;

        if (레벨Text != null)
            레벨Text.text = "Level: " + 레벨.ToString();
    }

    // 경험치와 레벨 데이터를 저장하는 메서드
    private void SaveExperienceData()
    {
        PlayerPrefs.SetInt("현재경험치", 현재경험치);
        PlayerPrefs.SetInt("레벨", 레벨);
        PlayerPrefs.Save(); // 저장
    }

    // 저장된 경험치와 레벨 데이터를 불러오는 메서드
    private void LoadExperienceData()
    {
        현재경험치 = PlayerPrefs.GetInt("현재경험치", 0);  // 기본값 0
        레벨 = PlayerPrefs.GetInt("레벨", 1);  // 기본값 1
        UpdateUI(); // 불러온 데이터로 UI 업데이트
    }

    private void OnEnable()
    {
        // 씬이 다시 로드될 때 이벤트를 재등록
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // 메모리 누수를 방지하기 위해 이벤트 해제
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
