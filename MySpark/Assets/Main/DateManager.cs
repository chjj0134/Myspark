using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DateManager : MonoBehaviour
{
    public TextMeshProUGUI dateText;
    public Button nextDayButton;
    public Image fadeImage; // ������ ȭ���� ���� Image (Canvas�� �߰� �ʿ�)

    private bool isProcessing = false;

    private void Update()
    {
        dateText.text = "Day: " + StatManager.Instance.���糯¥.ToString();
    }

    public void NextDay()
    {
        if (isProcessing) return;

        isProcessing = true;

        // ȭ�� ���̵��� (���������� ��ȯ)
        StartCoroutine(FadeToBlack(() =>
        {
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
        }));
    }

    private IEnumerator FadeToBlack(System.Action onComplete)
    {
        fadeImage.gameObject.SetActive(true);

        // ȭ���� ������ ���������� (���� �� ����)
        for (float t = 0; t < 1; t += Time.deltaTime)
        {
            fadeImage.color = new Color(0, 0, 0, t);
            yield return null;
        }

        fadeImage.color = Color.black;

        // ���������� ��ȯ �� �̺�Ʈ ����
        onComplete?.Invoke();

        // ��� ��� �� ���̵�ƿ� (ȭ�� ����)
        yield return new WaitForSeconds(SoundManager.instance.sleepEff.length);

        for (float t = 1; t > 0; t -= Time.deltaTime)
        {
            fadeImage.color = new Color(0, 0, 0, t);
            yield return null;
        }

        fadeImage.color = new Color(0, 0, 0, 0);
        fadeImage.gameObject.SetActive(false);
    }

    private IEnumerator ResetProcessingAfterSE(float delay)
    {
        yield return new WaitForSeconds(delay);
        isProcessing = false;
    }
}
