using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceManager : MonoBehaviour
{
    public static ExperienceManager Instance { get; private set; }

    // ����ġ�� ���� ����
    public int �������ġ = 0;
    public int ���� = 1;
    public int ����ġ�ִ밪 = 100;  // ����ġ �ִ� ��

    // ����ġ UI ���
    public Slider ����ġSlider;
    public TextMeshProUGUI ����Text;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �� �� ���� ����
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // �����̴��� �ִ밪�� �ν����Ϳ��� �����ϰ� �ʱ� UI ������Ʈ
        UpdateUI();
    }

    private void Update()
    {
        // �����̴� ���� ����ġ ���� ����ȭ
        ����ġSlider.value = �������ġ;

        // ���� �ؽ�Ʈ ������Ʈ
        ����Text.text = "Level: " + ����.ToString();
    }

    // ����ġ �߰� �޼���
    public void AddExperience(int amount)
    {
        �������ġ += amount;

        // ����ġ�� 100�� ������ ������
        if (�������ġ >= ����ġ�ִ밪)
        {
            LevelUp();
        }

        // ����ġ�� ����Ǿ����Ƿ� UI ������Ʈ
        UpdateUI();
    }

    private void LevelUp()
    {
        ���� += 1;  // ���� ����
        �������ġ -= ����ġ�ִ밪;  // ���� ����ġ �ݿ�
    }

    // UI�� ������Ʈ�ϴ� �޼���
    private void UpdateUI()
    {
        // �����̴� ���� �ؽ�Ʈ ������Ʈ
        ����ġSlider.value = �������ġ;
        ����Text.text = "Level: " + ����.ToString();
    }
}
