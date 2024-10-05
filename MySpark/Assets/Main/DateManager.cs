using TMPro;
using UnityEngine;

public class DateManager : MonoBehaviour
{
    public TextMeshProUGUI dateText; // TextMeshProUGUI�� ����

    private void Update()
    {
        // ���� �Ŵ������� ��¥ ���� �����ͼ� UI ������Ʈ
        dateText.text = "Day: " + StatManager.Instance.���糯¥.ToString();
    }

    public void NextDay()
    {
        // ���� ���� �Ѿ�� ��ư Ŭ�� �� ����
        StatManager.Instance.AdjustDate(1);
    }
}
