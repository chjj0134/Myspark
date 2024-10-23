using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Wash : MonoBehaviour
{
    public Button washButton1; // 피로도 감소 버튼1
    public Button washButton2; // 피로도 감소 버튼2

    public int fatigueReduction = -2; // 피로도 감소량

    private void Start()
    {
        // 처음에 버튼2는 비활성화 상태로 설정
        DisableButton(washButton2);

        // 버튼 클릭 이벤트에 피로도 감소 함수 연결
        washButton1.onClick.AddListener(ActivateWashButton2WithDelay);
        washButton2.onClick.AddListener(ReduceFatigue);
    }

    // washButton1 클릭 시 Shower Eff 재생 및 버튼1을 비활성화 후 버튼2 활성화
    private void ActivateWashButton2WithDelay()
    {
        // Shower Eff 사운드 재생
        SoundManager.instance.PlaySpecialEffect("ShowerButton");

        // Shower Eff가 끝난 후 버튼2 활성화
        float showerEffDuration = SoundManager.instance.showerEff.length;
        DisableButton(washButton1); // 버튼1 비활성화
        StartCoroutine(EnableButtonAfterDelay(washButton2, showerEffDuration)); // Shower Eff 끝난 후 버튼2 활성화
    }

    // 피로도 감소 기능 (washButton2 클릭 시 호출)
    private void ReduceFatigue()
    {
        if (StatManager.Instance != null)
        {
            // 피로도 스탯 감소 (0 밑으로 내려가지 않도록 제한)
            StatManager.Instance.피로도 = Mathf.Clamp(StatManager.Instance.피로도 + fatigueReduction, 0, 10);

            // 피로도 스탯을 저장
            StatManager.Instance.SaveStatsToPlayerPrefs();

            // Bubble Eff 사운드 재생
            SoundManager.instance.PlaySpecialEffect("BubbleButton");

            // 버튼2를 비활성화
            DisableButton(washButton2);
        }
        else
        {
            Debug.LogError("StatManager 인스턴스를 찾을 수 없습니다.");
        }
    }

    // 버튼 활성화 함수
    private void EnableButton(Button button)
    {
        button.interactable = true; // 버튼 활성화
        var colors = button.colors;
        colors.normalColor = Color.white; // 활성화 시 색상 설정 (기본 흰색)
        button.colors = colors;
    }

    // 버튼 비활성화 함수
    private void DisableButton(Button button)
    {
        button.interactable = false; // 버튼 비활성화
        var colors = button.colors;
        colors.disabledColor = new Color(0.7f, 0.7f, 0.7f); // 비활성화 시 회색으로 설정
        button.colors = colors;
    }

    // 일정 시간 후 버튼을 활성화하는 코루틴
    private IEnumerator EnableButtonAfterDelay(Button button, float delay)
    {
        yield return new WaitForSeconds(delay);
        EnableButton(button);
    }
}
