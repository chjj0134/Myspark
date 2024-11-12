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
        dateText.text = "Day: " + StatManager.Instance.���糯¥.ToString();
    }

    public void NextDay()
    {
        if (isProcessing) return;

        isProcessing = true;

        // ��¥ ����
        StatManager.Instance.AdjustDate(1);

        // ���� �Ƿε� �ʱ�ȭ
        StatManager.Instance.ResetDailyStats();

        StatManager.Instance.CheckForEnding();

        // Wash ��ũ��Ʈ�� ��ư �ʱ�ȭ ȣ��
        Wash washManager = FindObjectOfType<Wash>();
        if (washManager != null)
        {
            washManager.ResetWashButtonUsage();
        }

        // ���ڴ� ȿ���� ���
        SoundManager.instance.PlaySpecialEffect("SleepButton");
        StartCoroutine(ResetProcessingAfterSE(SoundManager.instance.sleepEff.length));
    }


    private IEnumerator ResetProcessingAfterSE(float delay)
    {
        yield return new WaitForSeconds(delay);
        isProcessing = false;
    }
}
