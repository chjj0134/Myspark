using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    private SoundManager soundManager;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �� �� ���� ����
            LoadExperienceData(); // ����� ����ġ ������ �ҷ�����
            SceneManager.sceneLoaded += OnSceneLoaded;  // �� �ε� �̺�Ʈ ����
        }
        else
        {
            Destroy(gameObject); // �ߺ��� �ν��Ͻ� ����
        }
    }

    // ���� �ε�� �� ȣ��Ǵ� �޼���
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Main")  // ���� ������ ���ƿ��� ���� ����
        {
            // ����ġ �����̴��� ���� �ؽ�Ʈ �ٽ� ã��
            ����ġSlider = GameObject.Find("Ex")?.GetComponent<Slider>();
            ����Text = GameObject.Find("Level")?.GetComponent<TextMeshProUGUI>();

            if (����ġSlider != null)
            {
                ����ġSlider.maxValue = ����ġ�ִ밪;
                ����ġSlider.value = �������ġ;  // ����ġ �� ����ȭ
            }
            else
            {
                Debug.LogError("����ġSlider�� ã�� �� �����ϴ�.");
            }

            if (����Text != null)
            {
                ����Text.text = "Level: " + ����.ToString();  // ���� �ؽ�Ʈ ����ȭ
            }
            else
            {
                Debug.LogError("����Text�� ã�� �� �����ϴ�.");
            }

            UpdateUI(); // UI ����ȭ
        }
    }

    private void Start()
    {
        UpdateUI(); // �ʱ� UI ������Ʈ
    }

    private void Update()
    {
        // �����̴� ���� ����ġ ���� ����ȭ
        if (����ġSlider != null)
            ����ġSlider.value = �������ġ;

        // ���� �ؽ�Ʈ ������Ʈ
        if (����Text != null)
            ����Text.text = "Level: " + ����.ToString();
    }

    // ����ġ �߰� �޼���
    public void AddExperience(int amount)
    {
        �������ġ += amount;

        // ����ġ�� �ִ밪�� ������ ������
        if (�������ġ >= ����ġ�ִ밪)
        {
            LevelUp();
        }

        UpdateUI();
        SaveExperienceData(); // ������ ����
    }

    private void LevelUp()
    {
        ���� += 1;  // ���� ����
        �������ġ -= ����ġ�ִ밪;  // ���� ����ġ �ݿ�

        if (soundManager != null)
        {
            soundManager.PlayLevelUpSE();  // ������ ȿ���� ���
        }
    }

    // UI�� ������Ʈ�ϴ� �޼���
    private void UpdateUI()
    {
        // �����̴� ���� �ؽ�Ʈ ������Ʈ
        if (����ġSlider != null)
            ����ġSlider.value = �������ġ;

        if (����Text != null)
            ����Text.text = "Level: " + ����.ToString();
    }

    // ����ġ�� ���� �����͸� �����ϴ� �޼���
    private void SaveExperienceData()
    {
        PlayerPrefs.SetInt("�������ġ", �������ġ);
        PlayerPrefs.SetInt("����", ����);
        PlayerPrefs.Save(); // ����
    }

    // ����� ����ġ�� ���� �����͸� �ҷ����� �޼���
    private void LoadExperienceData()
    {
        �������ġ = PlayerPrefs.GetInt("�������ġ", 0);  // �⺻�� 0
        ���� = PlayerPrefs.GetInt("����", 1);  // �⺻�� 1
        UpdateUI(); // �ҷ��� �����ͷ� UI ������Ʈ
    }

    private void OnEnable()
    {
        // ���� �ٽ� �ε�� �� �̺�Ʈ�� ����
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // �޸� ������ �����ϱ� ���� �̺�Ʈ ����
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
