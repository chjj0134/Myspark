using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.UI;

public class EndingSceneController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public Button returnToTitleButton;

    // ���� ���� Ŭ���� (��������Ʈ 1�� 2 ������ ���� ����)
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
        // PlayerPrefs���� ���õ� ��������Ʈ�� ���� Ÿ�� ��������
        string selectedSprite = PlayerPrefs.GetString("SelectedSprite", "Sprite1");
        string endingType = PlayerPrefs.GetString("EndingType", "Default");

        // ��ư�� ó���� ��Ȱ��ȭ
        returnToTitleButton.gameObject.SetActive(false);
        returnToTitleButton.onClick.AddListener(ReturnToTitle);

        // ���� ���� Ŭ�� ���
        PlayEndingVideo(selectedSprite, endingType);
    }

    private void PlayEndingVideo(string selectedSprite, string endingType)
    {
        // ���õ� ��������Ʈ�� ���� Ÿ�Կ� �´� ���� Ŭ���� ����
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

        // ���� Ŭ���� ����� �α׷� ���
        if (selectedClip != null)
        {
            Debug.Log($"Selected Sprite: {selectedSprite}, Ending Type: {endingType}, Playing Video Clip: {selectedClip.name}");
            videoPlayer.clip = selectedClip;
            videoPlayer.Play();
            videoPlayer.loopPointReached += OnVideoEnd; // ���� ����� ������ ȣ��� �޼��� ����
        }
        else
        {
            Debug.LogError($"No video clip found for Selected Sprite: {selectedSprite}, Ending Type: {endingType}");
        }
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        // ���� ����� ������ Ÿ��Ʋ�� ���ư��� ��ư Ȱ��ȭ
        returnToTitleButton.gameObject.SetActive(true);
    }

    private void ReturnToTitle()
    {
        // Ÿ��Ʋ ������ �̵�
        SceneManager.LoadScene("Title/Title");
    }
}
