using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 간 상태 유지
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // 슬라이더의 최대값을 인스펙터에서 설정하고 초기 UI 업데이트
        UpdateUI();
    }

    private void Update()
    {
        // 슬라이더 값을 경험치 값과 동기화
        경험치Slider.value = 현재경험치;

        // 레벨 텍스트 업데이트
        레벨Text.text = "Level: " + 레벨.ToString();
    }

    // 경험치 추가 메서드
    public void AddExperience(int amount)
    {
        현재경험치 += amount;

        // 경험치가 100을 넘으면 레벨업
        if (현재경험치 >= 경험치최대값)
        {
            LevelUp();
        }

        // 경험치가 변경되었으므로 UI 업데이트
        UpdateUI();
    }

    private void LevelUp()
    {
        레벨 += 1;  // 레벨 증가
        현재경험치 -= 경험치최대값;  // 남은 경험치 반영
    }

    // UI를 업데이트하는 메서드
    private void UpdateUI()
    {
        // 슬라이더 값과 텍스트 업데이트
        경험치Slider.value = 현재경험치;
        레벨Text.text = "Level: " + 레벨.ToString();
    }
}
