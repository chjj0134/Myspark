using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class StatManager : MonoBehaviour
{
    public static StatManager Instance { get; private set; }

    //Ư�� ��Ȳ ����(������,�Ƿε�)
    public UnityEvent OnDirtyState;
    public UnityEvent OnHungryState;

    //���� ����
    public UnityEvent OnNormalFatigueState;
    public UnityEvent OnNormalHungerState;

    //��¥ ���� �̺�Ʈ
    public UnityEvent OnDateChanged;

    // ���� ����
    public int �Ƿε� = 0;
    public int ��� = 0;
    public int ���ɵ� = 0;
    public int ưư�� = 0;
    public int �����ο� = 0;
    public int ������ = 0;
    public int ��� = 0;
    public int ���糯¥ = 1;
    private int ����ִ밪 = 10;
    private int �Ƿε��ִ밪 = 10;

    // ���� ���� üũ �޼���
    public void CheckForEnding()
    {
        if (���糯¥ > 7)
        {
            string endingType = DetermineEndingType();
            PlayerPrefs.SetString("EndingType", endingType);
            SceneManager.LoadScene("Ending/Ending");
        }
    }

    private string DetermineEndingType()
    {
        if (���ɵ� < 10)
            return "Runaway"; // ���� ����
        else if (���ɵ� >= 10 && ưư�� >= 11 && �����ο� >= 11 && ������ >= 11)
            return "CEO"; // ���� CEO ����
        else if (���ɵ� >= 10)
        {
            if (ưư�� >= �����ο� && ưư�� >= ������)
                return "Police";
            else if (�����ο� >= ưư�� && �����ο� >= ������)
                return "Scientist";
            else
                return "Politician";
        }
        return "Default";
    }

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
            OnDirtyState = new UnityEvent();
            OnHungryState = new UnityEvent();
            OnNormalFatigueState = new UnityEvent();
            OnNormalHungerState = new UnityEvent();
            OnDateChanged = new UnityEvent();

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

    public bool CanPerformActivity()
    {
        return ��� < ����ִ밪 && �Ƿε� < �Ƿε��ִ밪;
    }

    public void PerformActivity()
    {
        if (!CanPerformActivity())
        {
            Debug.Log("��� �Ǵ� �Ƿε��� �ִ�ġ�̹Ƿ� Ȱ���� �� �� �����ϴ�.");
            // ���⿡�� Ȱ�� �Ұ� UI �Ǵ� �˸� ǥ��
            return;
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

        // ���� ��¥�� ����ȭ
        if (dateText != null)
        {
            dateText.text = "Day: " + StatManager.Instance.���糯¥.ToString();
            //Debug.Log($"DateManager Update: Day {StatManager.Instance.���糯¥}");
        }
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

        CheckStates();
    }

    private void CheckStates()
    {
        // �Ƿε� ���� üũ
        if (�Ƿε� >= 10)
        {
            OnDirtyState.Invoke();
        }
        else if (�Ƿε� < 10)
        {
            OnNormalFatigueState.Invoke();
        }

        // ��� ���� üũ
        if (��� >= 10)
        {
            OnHungryState.Invoke();
        }
        else if (��� < 10)
        {
            OnNormalHungerState.Invoke();
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
            case "���":
                ��� += amount;
                break;
        }
        SaveStatsToPlayerPrefs(); // ����� ���� ��� ����
        UpdateSliders(); // UI ��� ����
    }


    // ��¥�� ������Ű�� �޼���
    public void AdjustDate(int days)
    {
        ���糯¥ += days;

        // PlayerPrefs ������Ʈ
        PlayerPrefs.SetInt("CurrentDay", ���糯¥);
        PlayerPrefs.SetInt("���糯¥", ���糯¥); // �ߺ����� ����Ǿ� �ִ��� Ȯ��
        PlayerPrefs.Save();

        //Debug.Log($"AdjustDate ȣ��: ���� ��¥ = {���糯¥}");

        if (���糯¥ > 7)
        {
            SceneManager.LoadScene("Title");
        }
        else
        {
            OnDateChanged.Invoke(); // ��¥ ���� �̺�Ʈ ȣ��
        }
    }

    public int GetCurrentDay()
    {
        int currentDay = PlayerPrefs.GetInt("CurrentDay", 1); // �⺻�� 1
        //Debug.Log($"PlayerPrefs���� ������ ���� ��¥: {currentDay}");
        return currentDay;
    }


    public void ResetDailyStats()
    {
        �Ƿε� = 0;
        ��� = 0;
        SaveStatsToPlayerPrefs(); // ���� ���� ����
        UpdateSliders(); // UI ��� ����
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

    public int GetSpriteLevelByDay()
    {
        int currentDay = ���糯¥; // PlayerPrefs ��� ���� �޸� ���� ���
       //Debug.Log($"���糯¥(StatManager): {currentDay}");

        // ���� ���
        int level = (currentDay - 1) / 2;
        level = Mathf.Clamp(level, 0, 3);

        //Debug.Log($"���� ����: {level}");
        return level;
    }

}
