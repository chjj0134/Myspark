using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackToMain : MonoBehaviour
{
    public Button backButton;  // 메인으로 돌아가는 버튼
    public GameObject resultPopup;  // 결과 팝업 오브젝트
    public GameObject blackOverlay;  // Black 오버레이 오브젝트

    private SelectSprite selectSpriteScript; // SelectSprite 스크립트 참조 변수
    private SpriteRenderer playerSpriteRenderer; // 스프라이트 렌더러

    void Start()
    {
        // SelectSprite 스크립트 컴포넌트와 SpriteRenderer 참조
        selectSpriteScript = FindObjectOfType<SelectSprite>();
        playerSpriteRenderer = GameObject.Find("Spark").GetComponent<SpriteRenderer>(); // Player 스프라이트 오브젝트 이름에 맞게 변경

        // 버튼 클릭 이벤트에 팝업 닫기 및 메인 씬 로드 기능 연결
        backButton.onClick.AddListener(ClosePopupAndLoadMainScene);
    }

    // 팝업을 닫고 "Main/Main" 씬을 로드하는 함수
   public void ClosePopupAndLoadMainScene()
    {
        // 팝업과 오버레이 닫기
        if (resultPopup != null) resultPopup.SetActive(false);
        if (blackOverlay != null) blackOverlay.SetActive(false);

        // 기본 날짜별 스프라이트로 복원
        ResetToDefaultSprite();

        // 메인 씬으로 이동
        SceneManager.LoadScene("Main/Main");
    }

    // 기본 날짜별 스프라이트로 복원하는 메서드
    private void ResetToDefaultSprite()
    {
        if (selectSpriteScript == null || playerSpriteRenderer == null)
        {
            Debug.LogError("SelectSprite 스크립트나 SpriteRenderer를 찾을 수 없습니다.");
            return;
        }

        // PlayerPrefs에서 선택된 스프라이트 정보와 날짜에 맞는 레벨 정보 가져오기
        string selectedSprite = PlayerPrefs.GetString("SelectedSprite");
        int selectedSpriteIndex = PlayerPrefs.GetInt("SelectedSpriteLevel");

        // 스프라이트를 날짜에 맞게 설정
        if (selectedSprite == "Sprite1")
        {
            playerSpriteRenderer.sprite = selectSpriteScript.sprite1Levels[selectedSpriteIndex];
        }
        else if (selectedSprite == "Sprite2")
        {
            playerSpriteRenderer.sprite = selectSpriteScript.sprite2Levels[selectedSpriteIndex];
        }
    }
}
