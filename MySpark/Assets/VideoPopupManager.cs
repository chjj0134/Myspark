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

    // �� 24���� ���� Ŭ�� �迭 (2���� ĳ���� x 4���� x 3�˹� ���� = 24)
    public VideoClip[] videoClips;

    private int currentLevel; // ��¥�� ���� ���� ����
    private int selectedSpriteIndex; // ���õ� ��������Ʈ �ε���
    private int jobIndex; // ���õ� �˹��� �ε���

    private void Start()
    {
        // ���� ��¥ ������ ���� ���
        int currentDay = PlayerPrefs.GetInt("���糯¥", 1);
        currentLevel = (currentDay <= 2) ? 0 : (currentDay <= 4) ? 1 : (currentDay <= 6) ? 2 : 3;

        // ��������Ʈ �ε��� ���� (PlayerPrefs�� ����� ���õ� ��������Ʈ�� ����)
        selectedSpriteIndex = PlayerPrefs.GetString("SelectedSprite") == "Sprite1" ? 0 : 1;

        // ���� �÷��̾ ��� ���� �̺�Ʈ �߰�
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    // �� ��ư Ŭ�� �� ���� ���
    // �� ��ư Ŭ�� �� ���� ���
    public void OnJobButtonClicked(int selectedJobIndex)
    {
        // �Ƿε��� ��� ���� Ȯ��
        if (StatManager.Instance != null)
        {
            if (StatManager.Instance.�Ƿε� >= 10)
            {
                resultBalloonText.text = "�ʹ� �ǰ��ؼ� �˹ٸ� �� �� �����ϴ�!";
                jobPopup.SetActive(false); // �˹��ϱ� �˾� �ݱ�
                resultPopup.SetActive(true); // ��� �˾� ���� (�ȳ� �޽��� ǥ��)
                return;
            }
            if (StatManager.Instance.��� >= 10)
            {
                resultBalloonText.text = "�谡 ���ļ� �˹ٸ� �� �� �����ϴ�!";
                jobPopup.SetActive(false); // �˹��ϱ� �˾� �ݱ�
                resultPopup.SetActive(true); // ��� �˾� ���� (�ȳ� �޽��� ǥ��)
                return;
            }
        }

        // �˹��ϱ� �˾� �ݰ� ���� �˾� ����
        jobPopup.SetActive(false);
        videoPopup.SetActive(true);
        jobIndex = selectedJobIndex; // ���õ� �˹� �ε��� ����

        // ���� ������ ���õ� ��������Ʈ�� ���� ������ ���� Ŭ�� ����
        int videoClipIndex = selectedSpriteIndex * 12 + currentLevel * 3 + jobIndex;
        videoPlayer.clip = videoClips[videoClipIndex];
        videoPlayer.Play(); // ���� ���
    }


    // ���� ����� �Ϸ�Ǹ� ȣ��Ǵ� �޼���
    private void OnVideoFinished(VideoPlayer vp)
    {
        videoPopup.SetActive(false); // ���� �˾� �ݱ�
        resultPopup.SetActive(true); // ��� �˾� ����

        // ���� ����
        AdjustStatsForJob();

        // ��� �ؽ�Ʈ ����
        SetResultText();
    }

    // �˹ٿ� ���� ���� ���� �޼���
    private void AdjustStatsForJob()
    {
        if (StatManager.Instance == null)
        {
            Debug.LogError("StatManager �ν��Ͻ��� ã�� �� �����ϴ�.");
            return;
        }

        // �˹ٿ� ���� ���� ��ȭ
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

        // �˹� �Ϸ� �� ��� 150 �߰�
        StatManager.Instance.AdjustStat("���", 150);
    }

    // ��������Ʈ�� �˹ٿ� ���� ��� �ؽ�Ʈ ����
    private void SetResultText()
    {
        Debug.Log("Current jobIndex: " + jobIndex); // jobIndex �� Ȯ��

        string selectedSprite = PlayerPrefs.GetString("SelectedSprite");
        resultBalloonText.text = selectedSprite == "Sprite1" ? "������ٿ�!" : "������ٸ�!";

        // ���� ��ȭ �ؽ�Ʈ ����
        switch (jobIndex)
        {
            case 0: // Kids
                statChangeText1.text = "ưư�� +3";
                statChangeText2.text = "�����ο� -1";
                statChangeText3.text = "������ -1";
                break;

            case 1: // English
                statChangeText1.text = "ưư�� -1";
                statChangeText2.text = "�����ο� +2";
                statChangeText3.text = "������ -1";
                break;

            case 2: // Volunteer
                statChangeText1.text = "ưư�� -1";
                statChangeText2.text = "�����ο� -1";
                statChangeText3.text = "������ +3";
                break;
        }
    }

    // ��� �˾� �ݱ�
    public void CloseResultPopup()
    {
        resultPopup.SetActive(false); // ��� �˾� �ݱ�
        if (black != null)
        {
            black.SetActive(false); // ���� ��� â �ݱ�
        }
    }
}
