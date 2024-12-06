using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class SelectSprite : MonoBehaviour
{
    public Sprite[] sprite1Levels = new Sprite[4];
    public Sprite[] sprite2Levels = new Sprite[4];
    public VideoPlayer videoPlayer; // VideoPlayer ����
    public RawImage videoOverlay; // UI�� Raw Image ���� (���� ��¿�)

    private int currentDay;
    private int selectedSpriteIndex;
    private bool canSelect = false; // �ʱ�ȭ �� ���� �Ұ��� ����

    private void Start()
    {
        // PlayerPrefs���� ���� ��¥ �ҷ����� (�⺻���� 1)
        currentDay = PlayerPrefs.GetInt("���糯¥", 1);

        // ��¥�� ���� ��������Ʈ �ε��� ����
        if (currentDay <= 2)
            selectedSpriteIndex = 0;
        else if (currentDay <= 4)
            selectedSpriteIndex = 1;
        else if (currentDay <= 6)
            selectedSpriteIndex = 2;
        else
            selectedSpriteIndex = 3;

        // 0.5�� �� ���� �����ϵ��� ����
        Invoke("EnableSelection", 0.5f);

        // VideoPlayer �ʱ�ȭ
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += OnVideoFinished; // ���� ���� �̺�Ʈ ����
        }

        // Video Overlay�� �ʱ⿡�� ��Ȱ��ȭ
        if (videoOverlay != null)
        {
            videoOverlay.gameObject.SetActive(false);

            // Raycast Target ��Ȱ��ȭ
            videoOverlay.raycastTarget = false;
        }
    }

    private void EnableSelection()
    {
        canSelect = true; // ���� ���� ���·� ����
    }

    public void SelectSprite1()
    {
        if (!canSelect) return; // ���� �Ұ����� ��� �Լ� ����

        PlayerPrefs.SetString("SelectedSprite", "Sprite1");
        PlayerPrefs.SetInt("SelectedSpriteLevel", selectedSpriteIndex);
        PlayerPrefs.SetString("SelectedSpriteImage", sprite1Levels[selectedSpriteIndex].name);

        PlayVideoBeforeSceneLoad(); // ���� ��� �� �� ��ȯ
    }

    public void SelectSprite2()
    {
        if (!canSelect) return; // ���� �Ұ����� ��� �Լ� ����

        PlayerPrefs.SetString("SelectedSprite", "Sprite2");
        PlayerPrefs.SetInt("SelectedSpriteLevel", selectedSpriteIndex);
        PlayerPrefs.SetString("SelectedSpriteImage", sprite2Levels[selectedSpriteIndex].name);

        PlayVideoBeforeSceneLoad(); // ���� ��� �� �� ��ȯ
    }

    private void PlayVideoBeforeSceneLoad()
    {
        if (videoPlayer != null && videoOverlay != null)
        {
            videoOverlay.gameObject.SetActive(true); // ���� �������� Ȱ��ȭ
            videoPlayer.Play(); // ���� ���
        }
        else
        {
            // VideoPlayer�� ���� ��� �ٷ� �� ��ȯ
            SceneManager.LoadScene("Main/Main");
        }
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
        // ���� ������ ��� ��ȯ
        SceneManager.LoadScene("Main/Main");
    }

    private void OnDestroy()
    {
        // VideoPlayer �̺�Ʈ ����
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached -= OnVideoFinished;
        }
    }
}
