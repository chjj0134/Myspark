using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemUsageManager : MonoBehaviour
{
    public TextMeshProUGUI[] itemCountTexts; // �� ������ ���� ���� ǥ�� �ؽ�Ʈ
    public Button[] itemButtons; // 1������ 4�������� ��ư
    public int[] itemCounts = new int[4]; // �� �������� ���� ���� �迭 (1�� ��ư���� 4�� ��ư����)

    private void Start()
    {
        // �� ��ư�� Ŭ�� �̺�Ʈ�� �����ϴ� ���� ��ȭ �Լ� ����
        itemButtons[0].onClick.AddListener(() => UseItem(0, 2, 2));  // 1�� ��ư: ��� +2, ���ɵ� +2
        itemButtons[1].onClick.AddListener(() => UseItem(1, -2, 0)); // 2�� ��ư: ��� -2
        itemButtons[2].onClick.AddListener(() => UseItem(2, -5, 0)); // 3�� ��ư: ��� -5
        itemButtons[3].onClick.AddListener(() => UseItem(3, -8, 0)); // 4�� ��ư: ��� -8

        // UI �ʱ�ȭ
        UpdateItemUI();
    }

    // �������� ����ϰ� ������ �����ϴ� �Լ�
    private void UseItem(int itemIndex, int hungerChange, int interestChange)
    {
        if (itemIndex != 0 && itemCounts[itemIndex] <= 0)
        {
            //Debug.Log("�������� �����ϴ�.");
            return;
        }

        // ��� �� ���ɵ� ����
        StatManager.Instance.��� = Mathf.Clamp(StatManager.Instance.��� + hungerChange, 0, 10);
        StatManager.Instance.���ɵ� = Mathf.Clamp(StatManager.Instance.���ɵ� + interestChange, 0, 15);

        // ������ ��� �� ���� ���� ����
        if (itemIndex != 0) itemCounts[itemIndex]--;

        // ���� ���� �� UI ������Ʈ
        StatManager.Instance.SaveStatsToPlayerPrefs();
        UpdateItemUI();
    }

    // ������ ���� �� ��ư ���� ������Ʈ
    public void UpdateItemUI()
    {
        for (int i = 0; i < itemButtons.Length; i++)
        {
            if (i == 0) // 1�� ��ư�� �׻� Ȱ��ȭ
            {
                itemButtons[i].interactable = true;
            }
            else
            {
                if (itemCounts[i] > 0)
                {
                    EnableButton(itemButtons[i]);
                }
                else
                {
                    DisableButton(itemButtons[i]);
                }
            }

            // ������ ���� �ؽ�Ʈ ������Ʈ
            itemCountTexts[i].text = "x" + itemCounts[i].ToString();
        }
    }

    // ��ư Ȱ��ȭ �Լ�
    private void EnableButton(Button button)
    {
        button.interactable = true;
        var colors = button.colors;
        colors.normalColor = Color.white; // Ȱ��ȭ �� ���� ���� (�⺻ ���)
        button.colors = colors;
    }

    // ��ư ��Ȱ��ȭ �Լ�
    private void DisableButton(Button button)
    {
        button.interactable = false;
        var colors = button.colors;
        colors.disabledColor = new Color(0.7f, 0.7f, 0.7f); // ��Ȱ��ȭ �� ȸ������ ����
        button.colors = colors;
    }
}
