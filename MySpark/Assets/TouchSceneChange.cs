using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.UI;

public class TouchSceneChange : MonoBehaviour
{
    public string sceneName = "Select/Select";  // 전환할 씬 이름
    public VideoPlayer videoPlayer;            // 비디오 플레이어
    public RawImage rawImage;                  // UI에 비디오를 표시할 RawImage

    private bool sceneChanging = false;        // 씬 전환 중 여부

    private void Start()
    {
        // 비디오 재생 준비
        if (videoPlayer != null && rawImage != null)
        {
            // VideoPlayer의 출력 결과를 RawImage에 연결
            rawImage.texture = videoPlayer.targetTexture;

            // 비디오 재생 완료 이벤트 등록
            videoPlayer.loopPointReached += OnVideoEnd;

            // 비디오 시작
            videoPlayer.Play();
        }
    }

    private void Update()
    {
        // 터치 또는 클릭으로 씬 전환
        if (!sceneChanging && (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0)))
        {
            // 강제 씬 전환 처리
            sceneChanging = true;

            if (videoPlayer.isPlaying) // 비디오가 재생 중이면 중지
            {
                videoPlayer.Stop();
            }

            LoadNextScene();
        }
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        // 비디오가 끝난 경우 씬 전환
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
        // 이벤트 해제
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached -= OnVideoEnd;
        }
    }
}
