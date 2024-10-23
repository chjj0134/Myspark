using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DateManager : MonoBehaviour
{
    public TextMeshProUGUI dateText; // TextMeshProUGUI로 변경
    public Button nextDayButton;  // 버튼 제어를 위한 Button 컴포넌트 추가

    private bool isProcessing = false;  // 이미 처리 중인지 확인하는 플래그

    private void Update()
    {
        // 스텟 매니저에서 날짜 정보 가져와서 UI 업데이트
        dateText.text = "Day: " + StatManager.Instance.현재날짜.ToString();
    }

    public void NextDay()
    {
        if (isProcessing)
        {
            // 이미 처리 중이면 아무 동작도 하지 않음
            return;
        }

        // 처리 중으로 플래그 설정
        isProcessing = true;

        // 다음 날로 넘어가는 버튼 클릭 시 실행
        StatManager.Instance.AdjustDate(1);

        // Sleep 효과음 재생
        SoundManager.instance.PlaySpecialEffect("SleepButton");

        // Sleep 효과음이 끝난 후 다시 눌릴 수 있도록 플래그 리셋
        StartCoroutine(ResetProcessingAfterSE(SoundManager.instance.sleepEff.length));
    }

    // 효과음 재생이 끝난 후 플래그 리셋
    private IEnumerator ResetProcessingAfterSE(float delay)
    {
        yield return new WaitForSeconds(delay);
        isProcessing = false;  // 다시 눌릴 수 있도록 플래그 초기화
    }
}
