using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DateManager : MonoBehaviour
{
    public TextMeshProUGUI dateText;
    public Button nextDayButton;

    private bool isProcessing = false;

    private void Update()
    {
        dateText.text = "Day: " + StatManager.Instance.현재날짜.ToString();
    }

    public void NextDay()
    {
        if (isProcessing) return;

        isProcessing = true;

        // 날짜 증가
        StatManager.Instance.AdjustDate(1);

        // 허기와 피로도 초기화
        StatManager.Instance.ResetDailyStats();

        StatManager.Instance.CheckForEnding();

        // Wash 스크립트의 버튼 초기화 호출
        Wash washManager = FindObjectOfType<Wash>();
        if (washManager != null)
        {
            washManager.ResetWashButtonUsage();
        }

        // 잠자는 효과음 재생
        SoundManager.instance.PlaySpecialEffect("SleepButton");
        StartCoroutine(ResetProcessingAfterSE(SoundManager.instance.sleepEff.length));
    }


    private IEnumerator ResetProcessingAfterSE(float delay)
    {
        yield return new WaitForSeconds(delay);
        isProcessing = false;
    }
}
