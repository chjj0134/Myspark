using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.UI;

public class EndingSceneController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public Button returnToTitleButton;

    // 엔딩 비디오 클립들 (스프라이트 1과 2 각각에 대해 설정)
    public VideoClip runawaySprite1Clip;
    public VideoClip runawaySprite2Clip;
    public VideoClip ceoSprite1Clip;
    public VideoClip ceoSprite2Clip;
    public VideoClip policeSprite1Clip;
    public VideoClip policeSprite2Clip;
    public VideoClip scientistSprite1Clip;
    public VideoClip scientistSprite2Clip;
    public VideoClip politicianSprite1Clip;
    public VideoClip politicianSprite2Clip;

    private void Start()
    {
        // PlayerPrefs에서 선택된 스프라이트와 엔딩 타입 가져오기
        string selectedSprite = PlayerPrefs.GetString("SelectedSprite", "Sprite1");
        string endingType = PlayerPrefs.GetString("EndingType", "Default");

        // 버튼을 처음엔 비활성화
        returnToTitleButton.gameObject.SetActive(false);
        returnToTitleButton.onClick.AddListener(ReturnToTitle);

        // 엔딩 비디오 클립 재생
        PlayEndingVideo(selectedSprite, endingType);
    }

    private void PlayEndingVideo(string selectedSprite, string endingType)
    {
        // 선택된 스프라이트와 엔딩 타입에 맞는 비디오 클립을 설정
        VideoClip selectedClip = null;

        if (selectedSprite == "Sprite1")
        {
            switch (endingType)
            {
                case "Runaway": selectedClip = runawaySprite1Clip; break;
                case "CEO": selectedClip = ceoSprite1Clip; break;
                case "Police": selectedClip = policeSprite1Clip; break;
                case "Scientist": selectedClip = scientistSprite1Clip; break;
                case "Politician": selectedClip = politicianSprite1Clip; break;
            }
        }
        else if (selectedSprite == "Sprite2")
        {
            switch (endingType)
            {
                case "Runaway": selectedClip = runawaySprite2Clip; break;
                case "CEO": selectedClip = ceoSprite2Clip; break;
                case "Police": selectedClip = policeSprite2Clip; break;
                case "Scientist": selectedClip = scientistSprite2Clip; break;
                case "Politician": selectedClip = politicianSprite2Clip; break;
            }
        }

        // 비디오 클립을 디버그 로그로 출력
        if (selectedClip != null)
        {
            Debug.Log($"Selected Sprite: {selectedSprite}, Ending Type: {endingType}, Playing Video Clip: {selectedClip.name}");
            videoPlayer.clip = selectedClip;
            videoPlayer.Play();
            videoPlayer.loopPointReached += OnVideoEnd; // 비디오 재생이 끝나면 호출될 메서드 연결
        }
        else
        {
            Debug.LogError($"No video clip found for Selected Sprite: {selectedSprite}, Ending Type: {endingType}");
        }
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        // 비디오 재생이 끝나면 타이틀로 돌아가는 버튼 활성화
        returnToTitleButton.gameObject.SetActive(true);
    }

    private void ReturnToTitle()
    {
        // 타이틀 씬으로 이동
        SceneManager.LoadScene("Title/Title");
    }
}
