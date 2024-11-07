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

    // 총 24개의 비디오 클립 배열 (2명의 캐릭터 x 4레벨 x 3알바 유형 = 24)
    public VideoClip[] videoClips;

    private int currentLevel; // 날짜에 따른 현재 레벨
    private int selectedSpriteIndex; // 선택된 스프라이트 인덱스
    private int jobIndex; // 선택된 알바의 인덱스

    private void Start()
    {
        // 현재 날짜 가져와 레벨 계산
        int currentDay = PlayerPrefs.GetInt("현재날짜", 1);
        currentLevel = (currentDay <= 2) ? 0 : (currentDay <= 4) ? 1 : (currentDay <= 6) ? 2 : 3;

        // 스프라이트 인덱스 결정 (PlayerPrefs에 저장된 선택된 스프라이트로 결정)
        selectedSpriteIndex = PlayerPrefs.GetString("SelectedSprite") == "Sprite1" ? 0 : 1;

        // 비디오 플레이어에 재생 종료 이벤트 추가
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    // 각 버튼 클릭 시 비디오 재생
    // 각 버튼 클릭 시 비디오 재생
    public void OnJobButtonClicked(int selectedJobIndex)
    {
        // 피로도와 허기 상태 확인
        if (StatManager.Instance != null)
        {
            if (StatManager.Instance.피로도 >= 10)
            {
                resultBalloonText.text = "너무 피곤해서 알바를 할 수 없습니다!";
                jobPopup.SetActive(false); // 알바하기 팝업 닫기
                resultPopup.SetActive(true); // 결과 팝업 열기 (안내 메시지 표시)
                return;
            }
            if (StatManager.Instance.허기 >= 10)
            {
                resultBalloonText.text = "배가 고파서 알바를 할 수 없습니다!";
                jobPopup.SetActive(false); // 알바하기 팝업 닫기
                resultPopup.SetActive(true); // 결과 팝업 열기 (안내 메시지 표시)
                return;
            }
        }

        // 알바하기 팝업 닫고 비디오 팝업 열기
        jobPopup.SetActive(false);
        videoPopup.SetActive(true);
        jobIndex = selectedJobIndex; // 선택된 알바 인덱스 저장

        // 현재 레벨과 선택된 스프라이트에 따라 적절한 비디오 클립 설정
        int videoClipIndex = selectedSpriteIndex * 12 + currentLevel * 3 + jobIndex;
        videoPlayer.clip = videoClips[videoClipIndex];
        videoPlayer.Play(); // 비디오 재생
    }


    // 비디오 재생이 완료되면 호출되는 메서드
    private void OnVideoFinished(VideoPlayer vp)
    {
        videoPopup.SetActive(false); // 비디오 팝업 닫기
        resultPopup.SetActive(true); // 결과 팝업 열기

        // 스탯 변경
        AdjustStatsForJob();

        // 결과 텍스트 설정
        SetResultText();
    }

    // 알바에 따른 스탯 변경 메서드
    private void AdjustStatsForJob()
    {
        if (StatManager.Instance == null)
        {
            Debug.LogError("StatManager 인스턴스를 찾을 수 없습니다.");
            return;
        }

        // 알바에 따른 스탯 변화
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

        // 알바 완료 후 골드 150 추가
        StatManager.Instance.AdjustStat("골드", 150);
    }

    // 스프라이트와 알바에 따른 결과 텍스트 설정
    private void SetResultText()
    {
        Debug.Log("Current jobIndex: " + jobIndex); // jobIndex 값 확인

        string selectedSprite = PlayerPrefs.GetString("SelectedSprite");
        resultBalloonText.text = selectedSprite == "Sprite1" ? "힘들었다옹!" : "힘들었다멍!";

        // 스탯 변화 텍스트 설정
        switch (jobIndex)
        {
            case 0: // Kids
                statChangeText1.text = "튼튼함 +3";
                statChangeText2.text = "지혜로움 -1";
                statChangeText3.text = "도덕성 -1";
                break;

            case 1: // English
                statChangeText1.text = "튼튼함 -1";
                statChangeText2.text = "지혜로움 +2";
                statChangeText3.text = "도덕성 -1";
                break;

            case 2: // Volunteer
                statChangeText1.text = "튼튼함 -1";
                statChangeText2.text = "지혜로움 -1";
                statChangeText3.text = "도덕성 +3";
                break;
        }
    }

    // 결과 팝업 닫기
    public void CloseResultPopup()
    {
        resultPopup.SetActive(false); // 결과 팝업 닫기
        if (black != null)
        {
            black.SetActive(false); // 검은 배경 창 닫기
        }
    }
}
