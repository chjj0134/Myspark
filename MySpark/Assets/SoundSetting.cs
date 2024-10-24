using UnityEngine;
using UnityEngine.UI;

public class SoundSetting : MonoBehaviour
{
    public Toggle bgmToggle;
    public Toggle seToggle;

    private void Start()
    {
        // 토글의 기본값을 true로 설정 (소리가 켜진 상태)
        bgmToggle.isOn = true;
        seToggle.isOn = true;

        // 토글 상태 변화에 따른 이벤트 등록
        bgmToggle.onValueChanged.AddListener(SoundManager.instance.ToggleBGM);
        seToggle.onValueChanged.AddListener(SoundManager.instance.ToggleSE);
    }
}
