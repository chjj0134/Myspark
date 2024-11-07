using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExperienceManager : MonoBehaviour
{
    public static ExperienceManager Instance { get; private set; }

    public int 현재경험치 = 0;
    public int 레벨 = 1;
    public int 경험치최대값 = 100;

    public Slider 경험치Slider;
    public TextMeshProUGUI 레벨Text;

    private SoundManager soundManager;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadExperienceData(); // 저장된 경험치 데이터 불러오기
            UpdateUI();           // 초기 UI 업데이트
            SceneManager.sceneLoaded += OnSceneLoaded;  // 씬 로드 이벤트 연결
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Main")
        {
            경험치Slider = GameObject.Find("Ex")?.GetComponent<Slider>();
            레벨Text = GameObject.Find("Level")?.GetComponent<TextMeshProUGUI>();

            if (경험치Slider != null)
            {
                경험치Slider.maxValue = 경험치최대값;
                경험치Slider.value = 현재경험치;
            }
            else
            {
                Debug.LogError("경험치Slider를 찾을 수 없습니다.");
            }

            if (레벨Text != null)
            {
                레벨Text.text = "Level: " + 레벨;
            }
            else
            {
                Debug.LogError("레벨Text를 찾을 수 없습니다.");
            }

            UpdateUI();
        }
    }

    private void Start()
    {
        soundManager = SoundManager.instance; // SoundManager 인스턴스 초기화
        UpdateUI();
    }

    private void Update()
    {
        if (경험치Slider != null)
            경험치Slider.value = 현재경험치;

        if (레벨Text != null)
            레벨Text.text = "Level: " + 레벨;
    }

    public void AddExperience(int amount)
    {
        현재경험치 += amount;

        if (현재경험치 >= 경험치최대값)
        {
            LevelUp();
        }

        SaveExperienceData(); // 경험치 변경 즉시 저장
        UpdateUI(); // UI 업데이트
    }

    private void LevelUp()
    {
        레벨 += 1;
        현재경험치 -= 경험치최대값;

        if (soundManager != null)
        {
            soundManager.PlayLevelUpSE();
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        // 슬라이더 값과 텍스트 업데이트
        if (경험치Slider != null)
            경험치Slider.value = 현재경험치;
        else
            Debug.LogError("경험치 슬라이더가 할당되지 않았습니다.");

        if (레벨Text != null)
            레벨Text.text = "Level: " + 레벨;
        else
            Debug.LogError("레벨 텍스트가 할당되지 않았습니다.");
    }

    private void SaveExperienceData()
    {
        PlayerPrefs.SetInt("현재경험치", 현재경험치);
        PlayerPrefs.SetInt("레벨", 레벨);
        PlayerPrefs.Save(); // 저장 실행
    }
    private void LoadExperienceData()
    {
        현재경험치 = PlayerPrefs.GetInt("현재경험치", 0);  // 기본값 0
        레벨 = PlayerPrefs.GetInt("레벨", 1);  // 기본값 1
        UpdateUI(); // 불러온 데이터로 UI 업데이트
    }


    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
