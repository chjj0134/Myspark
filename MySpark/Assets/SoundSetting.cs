using UnityEngine;
using UnityEngine.UI;

public class SoundSetting : MonoBehaviour
{
    public Toggle bgmToggle;
    public Toggle seToggle;

    private void Start()
    {
        // ����� �⺻���� true�� ���� (�Ҹ��� ���� ����)
        bgmToggle.isOn = true;
        seToggle.isOn = true;

        // ��� ���� ��ȭ�� ���� �̺�Ʈ ���
        bgmToggle.onValueChanged.AddListener(SoundManager.instance.ToggleBGM);
        seToggle.onValueChanged.AddListener(SoundManager.instance.ToggleSE);
    }
}
