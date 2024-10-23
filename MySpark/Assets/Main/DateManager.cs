using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DateManager : MonoBehaviour
{
    public TextMeshProUGUI dateText; // TextMeshProUGUI�� ����
    public Button nextDayButton;  // ��ư ��� ���� Button ������Ʈ �߰�

    private bool isProcessing = false;  // �̹� ó�� ������ Ȯ���ϴ� �÷���

    private void Update()
    {
        // ���� �Ŵ������� ��¥ ���� �����ͼ� UI ������Ʈ
        dateText.text = "Day: " + StatManager.Instance.���糯¥.ToString();
    }

    public void NextDay()
    {
        if (isProcessing)
        {
            // �̹� ó�� ���̸� �ƹ� ���۵� ���� ����
            return;
        }

        // ó�� ������ �÷��� ����
        isProcessing = true;

        // ���� ���� �Ѿ�� ��ư Ŭ�� �� ����
        StatManager.Instance.AdjustDate(1);

        // Sleep ȿ���� ���
        SoundManager.instance.PlaySpecialEffect("SleepButton");

        // Sleep ȿ������ ���� �� �ٽ� ���� �� �ֵ��� �÷��� ����
        StartCoroutine(ResetProcessingAfterSE(SoundManager.instance.sleepEff.length));
    }

    // ȿ���� ����� ���� �� �÷��� ����
    private IEnumerator ResetProcessingAfterSE(float delay)
    {
        yield return new WaitForSeconds(delay);
        isProcessing = false;  // �ٽ� ���� �� �ֵ��� �÷��� �ʱ�ȭ
    }
}
