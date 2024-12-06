using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;

public class VideoPopupManager : MonoBehaviour
{
    public GameObject jobPopup; // 알바하기 메인 팝업
    public GameObject videoPopup; // 비디오 재생 팝업
    public GameObject resultPopup; // 결과 팝업
    public GameObject black; // 뒤에 있는 검은 배경 창
    public VideoPlayer videoPlayer; // 비디오 플레이어 컴포넌트
    public TextMeshProUGUI resultBalloonText; // 결과 말풍선 텍스트
    public TextMeshProUGUI statChangeText1; // 첫 번째 스탯 변화 텍스트
    public TextMeshProUGUI statChangeText2; // 두 번째 스탯 변화 텍스트
    public TextMeshProUGUI statChangeText3; // 세 번째 스탯 변화 텍스트

    public VideoClip[] videoClips; // 비디오 클립 배열

    private int currentLevel; // 날짜에 따른 현재 레벨
    private int selectedSpriteIndex; // 선택된 스프라이트 인덱스
    private int jobIndex; // 선택된 알바의 인덱스

    private void OnEnable()
    {
        if (StatManager.Instance != null)
        {
            // 날짜 변경 이벤트 등록
            StatManager.Instance.OnDateChanged.AddListener(ResetStateForNewDay);
        }

        if (StatManager.Instance != null)
        {
            currentLevel = StatManager.Instance.GetSpriteLevelByDay();
            Debug.Log($"OnEnable에서 초기화된 레벨: {currentLevel}");

            selectedSpriteIndex = PlayerPrefs.GetString("SelectedSprite") == "Sprite1" ? 0 : 1;

            if (videoPlayer != null)
            {
                videoPlayer.loopPointReached += OnVideoFinished;
            }
        }
        else
        {
            Debug.LogError("StatManager.Instance가 초기화되지 않았습니다.");
        }
    }

    private void OnDisable()
    {
        if (StatManager.Instance != null)
        {
            StatManager.Instance.OnDateChanged.RemoveListener(ResetStateForNewDay);
        }

        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached -= OnVideoFinished;
        }
    }

    private void ResetStateForNewDay()
    {
        // 날짜 변경 시 레벨 재계산
        currentLevel = StatManager.Instance.GetSpriteLevelByDay();
        Debug.Log($"날짜 변경으로 초기화된 레벨: {currentLevel}");
    }

    public void OnJobButtonClicked(int selectedJobIndex)
    {
        if (StatManager.Instance != null)
        {
            if (StatManager.Instance.피로도 >= 10)
            {
                resultBalloonText.text = "너무 피곤해서 알바를 할 수 없습니다!";
                jobPopup.SetActive(false);
                resultPopup.SetActive(true);
                return;
            }

            if (StatManager.Instance.허기 >= 10)
            {
                resultBalloonText.text = "배가 고파서 알바를 할 수 없습니다!";
                jobPopup.SetActive(false);
                resultPopup.SetActive(true);
                return;
            }
        }

        jobPopup.SetActive(false);
        videoPopup.SetActive(true);
        jobIndex = selectedJobIndex;

        int videoClipIndex = selectedSpriteIndex * 12 + currentLevel * 3 + jobIndex;
        Debug.Log($"VideoClipIndex: {videoClipIndex}, Sprite: {selectedSpriteIndex}, Level: {currentLevel}, Job: {jobIndex}");

        if (videoClipIndex >= 0 && videoClipIndex < videoClips.Length)
        {
            videoPlayer.clip = videoClips[videoClipIndex];
            videoPlayer.Play();
        }
        else
        {
            Debug.LogError($"Invalid VideoClipIndex: {videoClipIndex}. Check videoClips array.");
        }
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
        videoPopup.SetActive(false);
        resultPopup.SetActive(true);

        AdjustStatsForJob();
        SetResultText();
    }

    private void AdjustStatsForJob()
    {
        if (StatManager.Instance == null)
        {
            Debug.LogError("StatManager 인스턴스를 찾을 수 없습니다.");
            return;
        }

        switch (jobIndex)
        {
            case 0: // Kids
                StatManager.Instance.AdjustStat("피로도", 3);
                StatManager.Instance.AdjustStat("허기", 1);
                StatManager.Instance.AdjustStat("튼튼함", 3);
                StatManager.Instance.AdjustStat("지혜로움", -1);
                StatManager.Instance.AdjustStat("도덕성", -1);
                break;
            case 1: // English
                StatManager.Instance.AdjustStat("피로도", 3);
                StatManager.Instance.AdjustStat("허기", 1);
                StatManager.Instance.AdjustStat("튼튼함", -1);
                StatManager.Instance.AdjustStat("지혜로움", 2);
                StatManager.Instance.AdjustStat("도덕성", -1);
                break;
            case 2: // Volunteer
                StatManager.Instance.AdjustStat("피로도", 3);
                StatManager.Instance.AdjustStat("허기", 1);
                StatManager.Instance.AdjustStat("튼튼함", -1);
                StatManager.Instance.AdjustStat("지혜로움", -1);
                StatManager.Instance.AdjustStat("도덕성", 3);
                break;
        }

        StatManager.Instance.AdjustStat("골드", 150);
    }

    private void SetResultText()
    {
        string selectedSprite = PlayerPrefs.GetString("SelectedSprite");
        resultBalloonText.text = selectedSprite == "Sprite1" ? "힘들었다옹!" : "힘들었다멍!";

        switch (jobIndex)
        {
            case 0:
                statChangeText1.text = "튼튼함 +3";
                statChangeText2.text = "지혜로움 -1";
                statChangeText3.text = "도덕성 -1";
                break;
            case 1:
                statChangeText1.text = "튼튼함 -1";
                statChangeText2.text = "지혜로움 +2";
                statChangeText3.text = "도덕성 -1";
                break;
            case 2:
                statChangeText1.text = "튼튼함 -1";
                statChangeText2.text = "지혜로움 -1";
                statChangeText3.text = "도덕성 +3";
                break;
        }
    }

    public void CloseResultPopup()
    {
        resultPopup.SetActive(false);
        if (black != null)
        {
            black.SetActive(false);
        }
    }
}
