using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DateManager : MonoBehaviour
{
    public TextMeshProUGUI dateText;
    public Button nextDayButton;
    public Image fadeImage; // 검정색 화면을 위한 Image (Canvas에 추가 필요)

    private bool isProcessing = false;

    private void Update()
    {
        dateText.text = "Day: " + StatManager.Instance.현재날짜.ToString();
    }

    public void NextDay()
    {
        if (isProcessing) return;

        isProcessing = true;

        // 화면 페이드인 (검정색으로 전환)
        StartCoroutine(FadeToBlack(() =>
        {
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
        }));
    }

    private IEnumerator FadeToBlack(System.Action onComplete)
    {
        fadeImage.gameObject.SetActive(true);

        // 화면을 서서히 검정색으로 (알파 값 증가)
        for (float t = 0; t < 1; t += Time.deltaTime)
        {
            fadeImage.color = new Color(0, 0, 0, t);
            yield return null;
        }

        fadeImage.color = Color.black;

        // 검정색으로 전환 후 이벤트 실행
        onComplete?.Invoke();

        // 잠시 대기 후 페이드아웃 (화면 복귀)
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
