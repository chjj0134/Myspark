using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class StatManager : MonoBehaviour
{
    public static StatManager Instance { get; private set; }

    // ���� ����
    public int �Ƿε� = 0;
    public int ��� = 0;
    public int ���ɵ� = 0;
    public int ưư�� = 0;
    public int �����ο� = 0;
    public int ������ = 0;
    public int ��� = 0;
    public int ���糯¥ = 1;  // �⺻������ 1�Ϻ��� ����

    // �����̴� ����
    public Slider �Ƿε�Slider;
    public Slider �Ƿε�2Slider;
    public Slider ���Slider;
    public Slider ���ɵ�Slider;
    public Slider ưư��Slider;
    public Slider �����ο�Slider;
    public Slider ������Slider;
    public TextMeshProUGUI Gold;

    // ��¥ UI �ؽ�Ʈ ����
    public TextMeshProUGUI dateText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        // �����̴� ���� ���� ���� ����ȭ
        �Ƿε�Slider.value = �Ƿε�;
        �Ƿε�2Slider.value = �Ƿε�;
        ���Slider.value = ���;
        ���ɵ�Slider.value = ���ɵ�;
        ưư��Slider.value = ưư��;
        �����ο�Slider.value = �����ο�;
        ������Slider.value = ������;

        // ��¥ UI ������Ʈ
        if (dateText != null)
        {
            dateText.text = "Day: " + ���糯¥.ToString();
        }

        if (Gold != null)
        {
            Gold.text = "Gold: " + ���.ToString();
        }
    }

    // ���� ���� �����ϴ� �޼���
    public void AdjustStat(string statName, int amount)
    {
        switch (statName)
        {
            case "�Ƿε�":
                �Ƿε� = Mathf.Clamp(�Ƿε� + amount, 0, 10); // ����: �ִ� ���� 10�� ���
                break;
            case "���":
                ��� = Mathf.Clamp(��� + amount, 0, 10);
                break;
            case "���ɵ�":
                ���ɵ� = Mathf.Clamp(���ɵ� + amount, 0, 15);
                break;
            case "ưư��":
                ưư�� = Mathf.Clamp(ưư�� + amount, 0, 15);
                break;
            case "�����ο�":
                �����ο� = Mathf.Clamp(�����ο� + amount, 0, 15);
                break;
            case "������":
                ������ = Mathf.Clamp(������ + amount, 0, 15);
                break;
        }
    }

    // ��¥�� ������Ű�� �޼���
    public void AdjustDate(int days)
    {
        ResetDailyStats();

        ���糯¥ += days;

        // ���糯¥�� 7���� ������ ���� ���� ó��
        if (���糯¥ > 7)
        {
            // ���� ó�� ���� 
            SceneManager.LoadScene("Ending/Endging");

            // ���� �� ��¥ �ʱ�ȭ
            ResetStats();
        }
    }
    
    //��¥�� ������ �ʱ�ȭ�Ǵ� �޼���
    private void ResetDailyStats()
    {
        �Ƿε� = 0; // �Ƿε� �ʱ�ȭ
    }

    // �ʱ�ȭ �޼���
    public void ResetStats()
    {
        �Ƿε� = 0;
        ��� = 0;
        ���ɵ� = 0;
        ưư�� = 0;
        �����ο� = 0;
        ������ = 0;
        ��� = 0;
        ���糯¥ = 1;
    }
}
