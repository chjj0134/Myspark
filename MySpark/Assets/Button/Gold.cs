using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Gold : MonoBehaviour
{
    public TextMeshProUGUI goldText; // ��� ǥ�� �ؽ�Ʈ
    public Button button100; // 100 ��� ��ư
    public Button button200; // 200 ��� ��ư
    public Button button250; // 250 ��� ��ư

    public int cost100 = 100;
    public int cost200 = 200;
    public int cost250 = 250;

    // ������ ��� ��ũ��Ʈ ����
    public ItemUsageManager itemUsageManager;

    private void Start()
    {
        // UI �ʱ�ȭ
        UpdateGoldUI();

        // ��ư Ŭ�� �̺�Ʈ�� ��� ���� �Լ� �� ������ �߰� ���� ����
        button100.onClick.AddListener(() => TryPurchaseItem(cost100, button100, 1)); // 1�� ������ �߰�
        button200.onClick.AddListener(() => TryPurchaseItem(cost200, button200, 2)); // 2�� ������ �߰�
        button250.onClick.AddListener(() => TryPurchaseItem(cost250, button250, 3)); // 3�� ������ �߰�
    }

    // ��� ���� �� ������ �߰� ����
    private void TryPurchaseItem(int itemCost, Button purchaseButton, int itemIndex)
    {
        if (StatManager.Instance != null)
        {
            int currentGold = StatManager.Instance.���;

            // ������ ��尡 ������ ��뺸�� ���ų� ���� ���� ���� ����
            if (currentGold >= itemCost)
            {
                StatManager.Instance.��� -= itemCost; // ��� ����

                // ������ ���� ���� ����
                if (itemUsageManager != null && itemIndex < itemUsageManager.itemCounts.Length)
                {
                    itemUsageManager.itemCounts[itemIndex]++; // ������ ������ ���� ���� ����
                    itemUsageManager.UpdateItemUI(); // UI ������Ʈ
                }

                StatManager.Instance.SaveStatsToPlayerPrefs(); // ������ ���� ������ ������ ���� ����
                UpdateGoldUI(); // UI ������Ʈ
            }
            else
            {
                Debug.Log("��尡 �����մϴ�.");
            }
        }
        else
        {
            Debug.LogError("StatManager �ν��Ͻ��� ã�� �� �����ϴ�.");
        }
    }

    // ��� �ؽ�Ʈ�� ��ư ���� ������Ʈ
    private void UpdateGoldUI()
    {
        if (StatManager.Instance != null)
        {
            int currentGold = StatManager.Instance.���;
            goldText.text = "Gold: " + currentGold.ToString();

            // ������ ��ư�� ���� ��尡 ������� ������ ��Ȱ��ȭ�ϰ� ȸ������ ����
            UpdateButtonState(button100, cost100, currentGold);
            UpdateButtonState(button200, cost200, currentGold);
            UpdateButtonState(button250, cost250, currentGold);
        }
    }

    // ��ư�� ���¸� ������Ʈ�ϴ� �޼���
    private void UpdateButtonState(Button button, int itemCost, int currentGold)
    {
        if (currentGold < itemCost)
        {
            button.interactable = false;
            ColorBlock colorBlock = button.colors;
            colorBlock.disabledColor = new Color(0.7f, 0.7f, 0.7f); // ȸ������ ����
            button.colors = colorBlock;
        }
        else
        {
            button.interactable = true;
        }
    }
}
