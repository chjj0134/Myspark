using TMPro;
using UnityEngine;

public class DateManager : MonoBehaviour
{
    public TextMeshProUGUI dateText; // TextMeshProUGUI로 변경

    private void Update()
    {
        // 스텟 매니저에서 날짜 정보 가져와서 UI 업데이트
        dateText.text = "Day: " + StatManager.Instance.현재날짜.ToString();
    }

    public void NextDay()
    {
        // 다음 날로 넘어가는 버튼 클릭 시 실행
        StatManager.Instance.AdjustDate(1);
    }
}
