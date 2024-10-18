using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
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
    public int ���糯¥ = 1;

    // �����̴� ����
    public Slider �Ƿε�Slider;
    public Slider �Ƿε�2Slider;
    public Slider ���Slider;
    public Slider ���ɵ�Slider;
    public Slider ưư��Slider;
    public Slider �����ο�Slider;
    public Slider ������Slider;
    public TextMeshProUGUI Gold;
    public TextMeshProUGUI dateText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // ó�� ���� ���θ� �Ǵ�
            if (PlayerPrefs.GetInt("���ӽ��۵�", 0) == 0)
            {
                // ó�� ���� �� ���� �ʱ�ȭ
                ResetStats();
                PlayerPrefs.SetInt("���ӽ��۵�", 1);  // ������ ���۵��� ���
                PlayerPrefs.Save();
            }
            else
            {
                // ����� ������ �ҷ�����
                LoadStatsFromPlayerPrefs();
            }

            SceneManager.sceneLoaded += OnSceneLoaded;  // �� �ε� �̺�Ʈ ����
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("�� �̸�: " + scene.name);

        if (scene.name == "Main")  // Main ���� ���� ����
        {
            // �Ƿε�2Slider�� dateText ����
            �Ƿε�2Slider = GameObject.Find("�Ƿε�2Slider")?.GetComponent<Slider>();
            dateText = GameObject.Find("Date")?.GetComponent<TextMeshProUGUI>();

            if (�Ƿε�2Slider != null)
            {
                �Ƿε�2Slider.maxValue = 10;
                �Ƿε�2Slider.value = �Ƿε�;  // �Ƿε� �� ����ȭ
            }
            else
            {
                Debug.LogError("�Ƿε�2Slider�� ã�� �� �����ϴ�.");
            }

            if (dateText != null)
            {
                dateText.text = "Day: " + ���糯¥.ToString();  // ��¥ �ؽ�Ʈ ����ȭ
            }
            else
            {
                Debug.LogError("dateText�� ã�� �� �����ϴ�.");
            }
        }
        else if (scene.name == "Title")  // Title �������� �ʱ�ȭ
        {
            ResetStats();  // Title ������ ������ �ʱ�ȭ
        }
    }

    private void OnEnable()
    {
        // ���� �ٽ� �ε�� �� �̺�Ʈ�� ����
        SceneManager.sceneLoaded += OnSceneLoaded;

        // �� �̵� �� UI ��ҵ��� �ٽ� ����
        �Ƿε�Slider = GameObject.Find("�Ƿε�Slider")?.GetComponent<Slider>();
        ���Slider = GameObject.Find("���Slider")?.GetComponent<Slider>();
        ���ɵ�Slider = GameObject.Find("���ɵ�Slider")?.GetComponent<Slider>();
        ưư��Slider = GameObject.Find("ưư��Slider")?.GetComponent<Slider>();
        �����ο�Slider = GameObject.Find("�����ο�Slider")?.GetComponent<Slider>();
        ������Slider = GameObject.Find("������Slider")?.GetComponent<Slider>();
        Gold = GameObject.Find("Gold")?.GetComponent<TextMeshProUGUI>();

        UpdateSliders(); // OnEnable���� �����̴� �� ����ȭ
    }

    private void OnDisable()
    {
        // �޸� ������ �����ϱ� ���� �̺�Ʈ ����
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        UpdateSliders(); // Start���� �����̴� �� ����ȭ
    }

    private void Update()
    {
        UpdateSliders(); // �� �����Ӹ��� �����̴� �� ������Ʈ
    }

    public void UpdateSliders()
    {
        // �����̴� ���� ���� ���� ����ȭ
        if (�Ƿε�Slider != null) �Ƿε�Slider.value = �Ƿε�;
        if (�Ƿε�2Slider != null) �Ƿε�2Slider.value = �Ƿε�;
        if (���Slider != null) ���Slider.value = ���;
        if (���ɵ�Slider != null) ���ɵ�Slider.value = ���ɵ�;
        if (ưư��Slider != null) ưư��Slider.value = ưư��;
        if (�����ο�Slider != null) �����ο�Slider.value = �����ο�;
        if (������Slider != null) ������Slider.value = ������;

        // ��¥ UI ������Ʈ
        if (dateText != null)
        {
            dateText.text = "Day: " + ���糯¥.ToString();
        }

        // ��� UI ������Ʈ
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
                �Ƿε� = Mathf.Clamp(�Ƿε� + amount, 0, 10);
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
        SaveStatsToPlayerPrefs(); // ������ ����� ������ ����
    }

    // ��¥�� ������Ű�� �޼���
    public void AdjustDate(int days)
    {
        ResetDailyStats();

        ���糯¥ += days;

        if (���糯¥ > 7)
        {
            // 7������ ������ Title ������ �̵�
            SceneManager.LoadScene("Title");
        }
        else
        {
            SaveStatsToPlayerPrefs(); // ��¥ ���� �ÿ��� ����
        }
    }

    private void ResetDailyStats()
    {
        �Ƿε� = 0;
    }

    // ���� �ʱ�ȭ �޼��� (Title �������� ����)
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
        SaveStatsToPlayerPrefs(); // �ʱ�ȭ �� ����
    }

    public void SaveStatsToPlayerPrefs()
    {
        PlayerPrefs.SetInt("�Ƿε�", �Ƿε�);
        PlayerPrefs.SetInt("���", ���);
        PlayerPrefs.SetInt("���ɵ�", ���ɵ�);
        PlayerPrefs.SetInt("ưư��", ưư��);
        PlayerPrefs.SetInt("�����ο�", �����ο�);
        PlayerPrefs.SetInt("������", ������);
        PlayerPrefs.SetInt("���", ���);
        PlayerPrefs.SetInt("���糯¥", ���糯¥);
        PlayerPrefs.Save();
    }

    public void LoadStatsFromPlayerPrefs()
    {
        �Ƿε� = PlayerPrefs.GetInt("�Ƿε�", 0);
        ��� = PlayerPrefs.GetInt("���", 0);
        ���ɵ� = PlayerPrefs.GetInt("���ɵ�", 0);
        ưư�� = PlayerPrefs.GetInt("ưư��", 0);
        �����ο� = PlayerPrefs.GetInt("�����ο�", 0);
        ������ = PlayerPrefs.GetInt("������", 0);
        ��� = PlayerPrefs.GetInt("���", 0);
        ���糯¥ = PlayerPrefs.GetInt("���糯¥", 1);
    }
}
