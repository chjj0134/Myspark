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
        StatManager.Instance.AdjustDate(1);

        // Wash 스크립트의 버튼 초기화 호출
        Wash washManager = FindObjectOfType<Wash>();
        if (washManager != null)
        {
            washManager.ResetWashButtonUsage();
        }

        SoundManager.instance.PlaySpecialEffect("SleepButton");
        StartCoroutine(ResetProcessingAfterSE(SoundManager.instance.sleepEff.length));
    }

    private IEnumerator ResetProcessingAfterSE(float delay)
    {
        yield return new WaitForSeconds(delay);
        isProcessing = false;
    }
}
