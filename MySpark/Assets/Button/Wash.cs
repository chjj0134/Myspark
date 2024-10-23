using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Wash : MonoBehaviour
{
    public Button washButton1; // �Ƿε� ���� ��ư1
    public Button washButton2; // �Ƿε� ���� ��ư2

    public int fatigueReduction = -2; // �Ƿε� ���ҷ�

    private void Start()
    {
        // ó���� ��ư2�� ��Ȱ��ȭ ���·� ����
        DisableButton(washButton2);

        // ��ư Ŭ�� �̺�Ʈ�� �Ƿε� ���� �Լ� ����
        washButton1.onClick.AddListener(ActivateWashButton2WithDelay);
        washButton2.onClick.AddListener(ReduceFatigue);
    }

    // washButton1 Ŭ�� �� Shower Eff ��� �� ��ư1�� ��Ȱ��ȭ �� ��ư2 Ȱ��ȭ
    private void ActivateWashButton2WithDelay()
    {
        // Shower Eff ���� ���
        SoundManager.instance.PlaySpecialEffect("ShowerButton");

        // Shower Eff�� ���� �� ��ư2 Ȱ��ȭ
        float showerEffDuration = SoundManager.instance.showerEff.length;
        DisableButton(washButton1); // ��ư1 ��Ȱ��ȭ
        StartCoroutine(EnableButtonAfterDelay(washButton2, showerEffDuration)); // Shower Eff ���� �� ��ư2 Ȱ��ȭ
    }

    // �Ƿε� ���� ��� (washButton2 Ŭ�� �� ȣ��)
    private void ReduceFatigue()
    {
        if (StatManager.Instance != null)
        {
            // �Ƿε� ���� ���� (0 ������ �������� �ʵ��� ����)
            StatManager.Instance.�Ƿε� = Mathf.Clamp(StatManager.Instance.�Ƿε� + fatigueReduction, 0, 10);

            // �Ƿε� ������ ����
            StatManager.Instance.SaveStatsToPlayerPrefs();

            // Bubble Eff ���� ���
            SoundManager.instance.PlaySpecialEffect("BubbleButton");

            // ��ư2�� ��Ȱ��ȭ
            DisableButton(washButton2);
        }
        else
        {
            Debug.LogError("StatManager �ν��Ͻ��� ã�� �� �����ϴ�.");
        }
    }

    // ��ư Ȱ��ȭ �Լ�
    private void EnableButton(Button button)
    {
        button.interactable = true; // ��ư Ȱ��ȭ
        var colors = button.colors;
        colors.normalColor = Color.white; // Ȱ��ȭ �� ���� ���� (�⺻ ���)
        button.colors = colors;
    }

    // ��ư ��Ȱ��ȭ �Լ�
    private void DisableButton(Button button)
    {
        button.interactable = false; // ��ư ��Ȱ��ȭ
        var colors = button.colors;
        colors.disabledColor = new Color(0.7f, 0.7f, 0.7f); // ��Ȱ��ȭ �� ȸ������ ����
        button.colors = colors;
    }

    // ���� �ð� �� ��ư�� Ȱ��ȭ�ϴ� �ڷ�ƾ
    private IEnumerator EnableButtonAfterDelay(Button button, float delay)
    {
        yield return new WaitForSeconds(delay);
        EnableButton(button);
    }
}
