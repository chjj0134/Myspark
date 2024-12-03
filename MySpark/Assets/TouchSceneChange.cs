using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.UI;

public class TouchSceneChange : MonoBehaviour
{
    public string sceneName = "Select/Select";  // ��ȯ�� �� �̸�
    public VideoPlayer videoPlayer;            // ���� �÷��̾�
    public RawImage rawImage;                  // UI�� ������ ǥ���� RawImage

    private bool sceneChanging = false;        // �� ��ȯ �� ����

    private void Start()
    {
        // ���� ��� �غ�
        if (videoPlayer != null && rawImage != null)
        {
            // VideoPlayer�� ��� ����� RawImage�� ����
            rawImage.texture = videoPlayer.targetTexture;

            // ���� ��� �Ϸ� �̺�Ʈ ���
            videoPlayer.loopPointReached += OnVideoEnd;

            // ���� ����
            videoPlayer.Play();
        }
    }

    private void Update()
    {
        // ��ġ �Ǵ� Ŭ������ �� ��ȯ
        if (!sceneChanging && (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0)))
        {
            // ���� �� ��ȯ ó��
            sceneChanging = true;

            if (videoPlayer.isPlaying) // ������ ��� ���̸� ����
            {
                videoPlayer.Stop();
            }

            LoadNextScene();
        }
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        // ������ ���� ��� �� ��ȯ
        if (!sceneChanging)
        {
            sceneChanging = true;
            LoadNextScene();
        }
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    private void OnDestroy()
    {
        // �̺�Ʈ ����
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached -= OnVideoEnd;
        }
    }
}
