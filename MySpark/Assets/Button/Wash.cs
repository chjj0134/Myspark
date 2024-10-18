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
        washButton1.onClick.AddListener(ActivateWashButton2);
        washButton2.onClick.AddListener(ReduceFatigue);
    }

    // washButton1 Ŭ�� �� ��ư1�� ��Ȱ��ȭ�ϰ� ��ư2�� Ȱ��ȭ�ϴ� �Լ�
    private void ActivateWashButton2()
    {
        DisableButton(washButton1);
        EnableButton(washButton2);
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

            // ��ư2�� ��Ȱ��ȭ
            DisableButton(washButton2);

            //Debug.Log($"�Ƿε��� {fatigueReduction}��ŭ ���ҵǾ����ϴ�. ���� �Ƿε�: {StatManager.Instance.�Ƿε�}");
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
}
