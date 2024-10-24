using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;  // �̱��� �ν��Ͻ�

    public AudioSource bgmSource;
    public AudioSource seSource;

    public AudioClip defaultBGM;          // �⺻ �������
    public AudioClip miniGameBGM;         // �̴ϰ��� ���� �������
    public AudioClip buttonEff;           // ��ư Ŭ�� �Ҹ�
    public AudioClip levelUpEff;          // ������ ȿ����

    public AudioClip bubbleEff;
    public AudioClip buyEff;
    public AudioClip eatEff;
    public AudioClip showerEff;
    public AudioClip sleepEff;

    [Range(0f, 1f)]
    public float bgmVolume = 1f;         // BGM ����
    [Range(0f, 1f)]
    public float seVolume = 1f;          // SE ����

    private Dictionary<string, AudioClip> specialEffects; // ȿ���� �����

    private void Awake()
    {
        // �̱��� ����
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // ȿ���� �ʱ�ȭ
        specialEffects = new Dictionary<string, AudioClip>()
        {
            { "BubbleButton", bubbleEff },
            { "BuyButton", buyEff },
            { "EatButton", eatEff },
            { "ShowerButton", showerEff },
            { "SleepButton", sleepEff }
        };
    }

    private void Start()
    {
        PlayBGM();
    }

    private void Update()
    {
        bgmSource.volume = bgmVolume;
        seSource.volume = seVolume;

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            PlayButtonSE();  // ȭ�� ��ġ �� ��ư ȿ���� ���
        }
    }

    // BGM ���
    public void PlayBGM()
    {
        if (SceneManager.GetActiveScene().name == "MiniGameScene")
        {
            if (bgmSource.clip != miniGameBGM)
            {
                bgmSource.clip = miniGameBGM;
                bgmSource.Play();
            }
        }
        else
        {
            if (bgmSource.clip != defaultBGM)
            {
                bgmSource.clip = defaultBGM;
                bgmSource.Play();
            }
        }
    }

    // ��ư Ŭ�� �Ҹ� ���
    public void PlayButtonSE()
    {
        PlaySE(buttonEff);
    }

    // Ư�� ȿ���� ���
    public void PlaySpecialEffect(string buttonTag)
    {
        if (specialEffects.ContainsKey(buttonTag))
        {
            // ��ư Ŭ�� ȿ���� ���� ��� ��, �߰� ȿ���� ���
            StartCoroutine(PlayButtonThenEffect(specialEffects[buttonTag]));
        }
    }

    // ��ư �Ҹ� ��� �� �߰� ȿ���� ���
    private IEnumerator PlayButtonThenEffect(AudioClip additionalEff)
    {
        PlayButtonSE();
        yield return new WaitForSeconds(buttonEff.length);
        PlaySE(additionalEff);
    }

    // ������ ȿ���� ���
    public void PlayLevelUpSE()
    {
        PlaySE(levelUpEff);
    }

    // �Ϲ����� ȿ���� ���
    private void PlaySE(AudioClip clip)
    {
        if (clip != null)
        {
            seSource.PlayOneShot(clip);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // ���� �ε�� �� BGM ����
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayBGM();
    }

    // BGM ���� �ѱ�
    public void ToggleBGM(bool isOn)
    {
        bgmSource.mute = !isOn;
    }

    // SE ���� �ѱ�
    public void ToggleSE(bool isOn)
    {
        seSource.mute = !isOn;
    }
}
