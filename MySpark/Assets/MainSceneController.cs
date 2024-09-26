using UnityEngine;
using UnityEngine.UI;

public class MainSceneController : MonoBehaviour
{
    public Image characterImage; // �߾��� ���׶�� �̹���
    public Sprite sprite1;       // ��������Ʈ 1
    public Sprite sprite2;       // ��������Ʈ 2

    void Start()
    {
        // PlayerPrefs���� ���õ� ��������Ʈ ������ �����ͼ� �߾ӿ� ǥ��
        string selectedSprite = PlayerPrefs.GetString("SelectedSprite");

        if (selectedSprite == "Sprite1")
        {
            characterImage.sprite = sprite1;
        }
        else if (selectedSprite == "Sprite2")
        {
            characterImage.sprite = sprite2;
        }
    }
}
