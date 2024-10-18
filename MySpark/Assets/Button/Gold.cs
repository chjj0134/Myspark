using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Gold : MonoBehaviour
{
    public TextMeshProUGUI goldText; // 골드 표시 텍스트
    public Button button100; // 100 골드 버튼
    public Button button200; // 200 골드 버튼
    public Button button250; // 250 골드 버튼

    public int cost100 = 100;
    public int cost200 = 200;
    public int cost250 = 250;

    // 아이템 사용 스크립트 참조
    public ItemUsageManager itemUsageManager;

    private void Start()
    {
        // UI 초기화
        UpdateGoldUI();

        // 버튼 클릭 이벤트에 골드 차감 함수 및 아이템 추가 로직 연결
        button100.onClick.AddListener(() => TryPurchaseItem(cost100, button100, 1)); // 1번 아이템 추가
        button200.onClick.AddListener(() => TryPurchaseItem(cost200, button200, 2)); // 2번 아이템 추가
        button250.onClick.AddListener(() => TryPurchaseItem(cost250, button250, 3)); // 3번 아이템 추가
    }

    // 골드 차감 및 아이템 추가 로직
    private void TryPurchaseItem(int itemCost, Button purchaseButton, int itemIndex)
    {
        if (StatManager.Instance != null)
        {
            int currentGold = StatManager.Instance.골드;

            // 소지한 골드가 아이템 비용보다 많거나 같을 때만 구매 가능
            if (currentGold >= itemCost)
            {
                StatManager.Instance.골드 -= itemCost; // 골드 차감

                // 아이템 소지 갯수 증가
                if (itemUsageManager != null && itemIndex < itemUsageManager.itemCounts.Length)
                {
                    itemUsageManager.itemCounts[itemIndex]++; // 구매한 아이템 소지 갯수 증가
                    itemUsageManager.UpdateItemUI(); // UI 업데이트
                }

                StatManager.Instance.SaveStatsToPlayerPrefs(); // 차감된 골드와 소지한 아이템 갯수 저장
                UpdateGoldUI(); // UI 업데이트
            }
            else
            {
                Debug.Log("골드가 부족합니다.");
            }
        }
        else
        {
            Debug.LogError("StatManager 인스턴스를 찾을 수 없습니다.");
        }
    }

    // 골드 텍스트와 버튼 상태 업데이트
    private void UpdateGoldUI()
    {
        if (StatManager.Instance != null)
        {
            int currentGold = StatManager.Instance.골드;
            goldText.text = "Gold: " + currentGold.ToString();

            // 각각의 버튼에 대해 골드가 충분하지 않으면 비활성화하고 회색으로 설정
            UpdateButtonState(button100, cost100, currentGold);
            UpdateButtonState(button200, cost200, currentGold);
            UpdateButtonState(button250, cost250, currentGold);
        }
    }

    // 버튼의 상태를 업데이트하는 메서드
    private void UpdateButtonState(Button button, int itemCost, int currentGold)
    {
        if (currentGold < itemCost)
        {
            button.interactable = false;
            ColorBlock colorBlock = button.colors;
            colorBlock.disabledColor = new Color(0.7f, 0.7f, 0.7f); // 회색으로 설정
            button.colors = colorBlock;
        }
        else
        {
            button.interactable = true;
        }
    }
}
