using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectSprite : MonoBehaviour
{
    // �� ���� ��������Ʈ�� ���� ����
    public Sprite sprite1;
    public Sprite sprite2;

    // ��ư Ŭ�� �̺�Ʈ
    public void SelectSprite1()
    {
        PlayerPrefs.SetString("SelectedSprite", "Sprite1");
        SceneManager.LoadScene("Main/Main");
    }

    public void SelectSprite2()
    {
        PlayerPrefs.SetString("SelectedSprite", "Sprite2");
        SceneManager.LoadScene("Main/Main");
    }
}
