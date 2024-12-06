using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class SelectSprite : MonoBehaviour
{
    public Sprite[] sprite1Levels = new Sprite[4];
    public Sprite[] sprite2Levels = new Sprite[4];
    public VideoPlayer videoPlayer; // VideoPlayer 연결
    public RawImage videoOverlay; // UI의 Raw Image 연결 (영상 출력용)

    private int currentDay;
    private int selectedSpriteIndex;
    private bool canSelect = false; // 초기화 시 선택 불가능 상태

    private void Start()
    {
        // PlayerPrefs에서 현재 날짜 불러오기 (기본값은 1)
        currentDay = PlayerPrefs.GetInt("현재날짜", 1);

        // 날짜에 따라 스프라이트 인덱스 설정
        if (currentDay <= 2)
            selectedSpriteIndex = 0;
        else if (currentDay <= 4)
            selectedSpriteIndex = 1;
        else if (currentDay <= 6)
            selectedSpriteIndex = 2;
        else
            selectedSpriteIndex = 3;

        // 0.5초 후 선택 가능하도록 설정
        Invoke("EnableSelection", 0.5f);

        // VideoPlayer 초기화
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += OnVideoFinished; // 영상 종료 이벤트 연결
        }

        // Video Overlay를 초기에는 비활성화
        if (videoOverlay != null)
        {
            videoOverlay.gameObject.SetActive(false);

            // Raycast Target 비활성화
            videoOverlay.raycastTarget = false;
        }
    }

    private void EnableSelection()
    {
        canSelect = true; // 선택 가능 상태로 변경
    }

    public void SelectSprite1()
    {
        if (!canSelect) return; // 선택 불가능할 경우 함수 종료

        PlayerPrefs.SetString("SelectedSprite", "Sprite1");
        PlayerPrefs.SetInt("SelectedSpriteLevel", selectedSpriteIndex);
        PlayerPrefs.SetString("SelectedSpriteImage", sprite1Levels[selectedSpriteIndex].name);

        PlayVideoBeforeSceneLoad(); // 영상 재생 후 씬 전환
    }

    public void SelectSprite2()
    {
        if (!canSelect) return; // 선택 불가능할 경우 함수 종료

        PlayerPrefs.SetString("SelectedSprite", "Sprite2");
        PlayerPrefs.SetInt("SelectedSpriteLevel", selectedSpriteIndex);
        PlayerPrefs.SetString("SelectedSpriteImage", sprite2Levels[selectedSpriteIndex].name);

        PlayVideoBeforeSceneLoad(); // 영상 재생 후 씬 전환
    }

    private void PlayVideoBeforeSceneLoad()
    {
        if (videoPlayer != null && videoOverlay != null)
        {
            videoOverlay.gameObject.SetActive(true); // 영상 오버레이 활성화
            videoPlayer.Play(); // 영상 재생
        }
        else
        {
            // VideoPlayer가 없을 경우 바로 씬 전환
            SceneManager.LoadScene("Main/Main");
        }
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
        // 메인 씬으로 즉시 전환
        SceneManager.LoadScene("Main/Main");
    }

    private void OnDestroy()
    {
        // VideoPlayer 이벤트 해제
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached -= OnVideoFinished;
        }
    }
}
