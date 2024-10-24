using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;  // 싱글톤 인스턴스

    public AudioSource bgmSource;
    public AudioSource seSource;

    public AudioClip defaultBGM;          // 기본 배경음악
    public AudioClip miniGameBGM;         // 미니게임 씬의 배경음악
    public AudioClip buttonEff;           // 버튼 클릭 소리
    public AudioClip levelUpEff;          // 레벨업 효과음

    public AudioClip bubbleEff;
    public AudioClip buyEff;
    public AudioClip eatEff;
    public AudioClip showerEff;
    public AudioClip sleepEff;

    [Range(0f, 1f)]
    public float bgmVolume = 1f;         // BGM 볼륨
    [Range(0f, 1f)]
    public float seVolume = 1f;          // SE 볼륨

    private Dictionary<string, AudioClip> specialEffects; // 효과음 저장소

    private void Awake()
    {
        // 싱글톤 설정
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // 효과음 초기화
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
            PlayButtonSE();  // 화면 터치 시 버튼 효과음 재생
        }
    }

    // BGM 재생
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

    // 버튼 클릭 소리 재생
    public void PlayButtonSE()
    {
        PlaySE(buttonEff);
    }

    // 특정 효과음 재생
    public void PlaySpecialEffect(string buttonTag)
    {
        if (specialEffects.ContainsKey(buttonTag))
        {
            // 버튼 클릭 효과음 먼저 재생 후, 추가 효과음 재생
            StartCoroutine(PlayButtonThenEffect(specialEffects[buttonTag]));
        }
    }

    // 버튼 소리 재생 후 추가 효과음 재생
    private IEnumerator PlayButtonThenEffect(AudioClip additionalEff)
    {
        PlayButtonSE();
        yield return new WaitForSeconds(buttonEff.length);
        PlaySE(additionalEff);
    }

    // 레벨업 효과음 재생
    public void PlayLevelUpSE()
    {
        PlaySE(levelUpEff);
    }

    // 일반적인 효과음 재생
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

    // 씬이 로드될 때 BGM 갱신
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayBGM();
    }

    // BGM 끄고 켜기
    public void ToggleBGM(bool isOn)
    {
        bgmSource.mute = !isOn;
    }

    // SE 끄고 켜기
    public void ToggleSE(bool isOn)
    {
        seSource.mute = !isOn;
    }
}
