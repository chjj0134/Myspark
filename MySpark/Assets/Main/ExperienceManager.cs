using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExperienceManager : MonoBehaviour
{
    public static ExperienceManager Instance { get; private set; }

    public int �������ġ = 0;
    public int ���� = 1;
    public int ����ġ�ִ밪 = 100;

    public Slider ����ġSlider;
    public TextMeshProUGUI ����Text;

    private SoundManager soundManager;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadExperienceData(); // ����� ����ġ ������ �ҷ�����
            UpdateUI();           // �ʱ� UI ������Ʈ
            SceneManager.sceneLoaded += OnSceneLoaded;  // �� �ε� �̺�Ʈ ����
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Main")
        {
            ����ġSlider = GameObject.Find("Ex")?.GetComponent<Slider>();
            ����Text = GameObject.Find("Level")?.GetComponent<TextMeshProUGUI>();

            if (����ġSlider != null)
            {
                ����ġSlider.maxValue = ����ġ�ִ밪;
                ����ġSlider.value = �������ġ;
            }
            else
            {
                Debug.LogError("����ġSlider�� ã�� �� �����ϴ�.");
            }

            if (����Text != null)
            {
                ����Text.text = "Level: " + ����;
            }
            else
            {
                Debug.LogError("����Text�� ã�� �� �����ϴ�.");
            }

            UpdateUI();
        }
    }

    private void Start()
    {
        soundManager = SoundManager.instance; // SoundManager �ν��Ͻ� �ʱ�ȭ
        UpdateUI();
    }

    private void Update()
    {
        if (����ġSlider != null)
            ����ġSlider.value = �������ġ;

        if (����Text != null)
            ����Text.text = "Level: " + ����;
    }

    public void AddExperience(int amount)
    {
        �������ġ += amount;

        if (�������ġ >= ����ġ�ִ밪)
        {
            LevelUp();
        }

        SaveExperienceData(); // ����ġ ���� ��� ����
        UpdateUI(); // UI ������Ʈ
    }

    private void LevelUp()
    {
        ���� += 1;
        �������ġ -= ����ġ�ִ밪;

        if (soundManager != null)
        {
            soundManager.PlayLevelUpSE();
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        // �����̴� ���� �ؽ�Ʈ ������Ʈ
        if (����ġSlider != null)
            ����ġSlider.value = �������ġ;
        else
            Debug.LogError("����ġ �����̴��� �Ҵ���� �ʾҽ��ϴ�.");

        if (����Text != null)
            ����Text.text = "Level: " + ����;
        else
            Debug.LogError("���� �ؽ�Ʈ�� �Ҵ���� �ʾҽ��ϴ�.");
    }

    private void SaveExperienceData()
    {
        PlayerPrefs.SetInt("�������ġ", �������ġ);
        PlayerPrefs.SetInt("����", ����);
        PlayerPrefs.Save(); // ���� ����
    }
    private void LoadExperienceData()
    {
        �������ġ = PlayerPrefs.GetInt("�������ġ", 0);  // �⺻�� 0
        ���� = PlayerPrefs.GetInt("����", 1);  // �⺻�� 1
        UpdateUI(); // �ҷ��� �����ͷ� UI ������Ʈ
    }


    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
