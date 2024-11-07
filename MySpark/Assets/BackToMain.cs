using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackToMain : MonoBehaviour
{
    public Button backButton;  // �������� ���ư��� ��ư
    public GameObject resultPopup;  // ��� �˾� ������Ʈ
    public GameObject blackOverlay;  // Black �������� ������Ʈ

    private SelectSprite selectSpriteScript; // SelectSprite ��ũ��Ʈ ���� ����
    private SpriteRenderer playerSpriteRenderer; // ��������Ʈ ������

    void Start()
    {
        // SelectSprite ��ũ��Ʈ ������Ʈ�� SpriteRenderer ����
        selectSpriteScript = FindObjectOfType<SelectSprite>();
        playerSpriteRenderer = GameObject.Find("Spark").GetComponent<SpriteRenderer>(); // Player ��������Ʈ ������Ʈ �̸��� �°� ����

        // ��ư Ŭ�� �̺�Ʈ�� �˾� �ݱ� �� ���� �� �ε� ��� ����
        backButton.onClick.AddListener(ClosePopupAndLoadMainScene);
    }

    // �˾��� �ݰ� "Main/Main" ���� �ε��ϴ� �Լ�
   public void ClosePopupAndLoadMainScene()
    {
        // �˾��� �������� �ݱ�
        if (resultPopup != null) resultPopup.SetActive(false);
        if (blackOverlay != null) blackOverlay.SetActive(false);

        // �⺻ ��¥�� ��������Ʈ�� ����
        ResetToDefaultSprite();

        // ���� ������ �̵�
        SceneManager.LoadScene("Main/Main");
    }

    // �⺻ ��¥�� ��������Ʈ�� �����ϴ� �޼���
    private void ResetToDefaultSprite()
    {
        if (selectSpriteScript == null || playerSpriteRenderer == null)
        {
            Debug.LogError("SelectSprite ��ũ��Ʈ�� SpriteRenderer�� ã�� �� �����ϴ�.");
            return;
        }

        // PlayerPrefs���� ���õ� ��������Ʈ ������ ��¥�� �´� ���� ���� ��������
        string selectedSprite = PlayerPrefs.GetString("SelectedSprite");
        int selectedSpriteIndex = PlayerPrefs.GetInt("SelectedSpriteLevel");

        // ��������Ʈ�� ��¥�� �°� ����
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
