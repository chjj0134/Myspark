using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;

public class VideoPopupManager : MonoBehaviour
{
    public GameObject jobPopup; // �˹��ϱ� ���� �˾�
    public GameObject videoPopup; // ���� ��� �˾�
    public GameObject resultPopup; // ��� �˾�
    public GameObject black; // �ڿ� �ִ� ���� ��� â
    public VideoPlayer videoPlayer; // ���� �÷��̾� ������Ʈ
    public TextMeshProUGUI resultBalloonText; // ��� ��ǳ�� �ؽ�Ʈ
    public TextMeshProUGUI statChangeText1; // ù ��° ���� ��ȭ �ؽ�Ʈ
    public TextMeshProUGUI statChangeText2; // �� ��° ���� ��ȭ �ؽ�Ʈ
    public TextMeshProUGUI statChangeText3; // �� ��° ���� ��ȭ �ؽ�Ʈ

    public VideoClip[] videoClips; // ���� Ŭ�� �迭

    private int currentLevel; // ��¥�� ���� ���� ����
    private int selectedSpriteIndex; // ���õ� ��������Ʈ �ε���
    private int jobIndex; // ���õ� �˹��� �ε���

    private void OnEnable()
    {
        if (StatManager.Instance != null)
        {
            // ��¥ ���� �̺�Ʈ ���
            StatManager.Instance.OnDateChanged.AddListener(ResetStateForNewDay);
        }

        if (StatManager.Instance != null)
        {
            currentLevel = StatManager.Instance.GetSpriteLevelByDay();
            Debug.Log($"OnEnable���� �ʱ�ȭ�� ����: {currentLevel}");

            selectedSpriteIndex = PlayerPrefs.GetString("SelectedSprite") == "Sprite1" ? 0 : 1;

            if (videoPlayer != null)
            {
                videoPlayer.loopPointReached += OnVideoFinished;
            }
        }
        else
        {
            Debug.LogError("StatManager.Instance�� �ʱ�ȭ���� �ʾҽ��ϴ�.");
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
        // ��¥ ���� �� ���� ����
        currentLevel = StatManager.Instance.GetSpriteLevelByDay();
        Debug.Log($"��¥ �������� �ʱ�ȭ�� ����: {currentLevel}");
    }

    public void OnJobButtonClicked(int selectedJobIndex)
    {
        if (StatManager.Instance != null)
        {
            if (StatManager.Instance.�Ƿε� >= 10)
            {
                resultBalloonText.text = "�ʹ� �ǰ��ؼ� �˹ٸ� �� �� �����ϴ�!";
                jobPopup.SetActive(false);
                resultPopup.SetActive(true);
                return;
            }

            if (StatManager.Instance.��� >= 10)
            {
                resultBalloonText.text = "�谡 ���ļ� �˹ٸ� �� �� �����ϴ�!";
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
            Debug.LogError("StatManager �ν��Ͻ��� ã�� �� �����ϴ�.");
            return;
        }

        switch (jobIndex)
        {
            case 0: // Kids
                StatManager.Instance.AdjustStat("�Ƿε�", 3);
                StatManager.Instance.AdjustStat("���", 1);
                StatManager.Instance.AdjustStat("ưư��", 3);
                StatManager.Instance.AdjustStat("�����ο�", -1);
                StatManager.Instance.AdjustStat("������", -1);
                break;
            case 1: // English
                StatManager.Instance.AdjustStat("�Ƿε�", 3);
                StatManager.Instance.AdjustStat("���", 1);
                StatManager.Instance.AdjustStat("ưư��", -1);
                StatManager.Instance.AdjustStat("�����ο�", 2);
                StatManager.Instance.AdjustStat("������", -1);
                break;
            case 2: // Volunteer
                StatManager.Instance.AdjustStat("�Ƿε�", 3);
                StatManager.Instance.AdjustStat("���", 1);
                StatManager.Instance.AdjustStat("ưư��", -1);
                StatManager.Instance.AdjustStat("�����ο�", -1);
                StatManager.Instance.AdjustStat("������", 3);
                break;
        }

        StatManager.Instance.AdjustStat("���", 150);
    }

    private void SetResultText()
    {
        string selectedSprite = PlayerPrefs.GetString("SelectedSprite");
        resultBalloonText.text = selectedSprite == "Sprite1" ? "������ٿ�!" : "������ٸ�!";

        switch (jobIndex)
        {
            case 0:
                statChangeText1.text = "ưư�� +3";
                statChangeText2.text = "�����ο� -1";
                statChangeText3.text = "������ -1";
                break;
            case 1:
                statChangeText1.text = "ưư�� -1";
                statChangeText2.text = "�����ο� +2";
                statChangeText3.text = "������ -1";
                break;
            case 2:
                statChangeText1.text = "ưư�� -1";
                statChangeText2.text = "�����ο� -1";
                statChangeText3.text = "������ +3";
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
