using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ONE
{
    public class LogUI : UIBase
    {
        public static LogUI Instance => UIManager.Singleton.GetUI<LogUI>(UIList.LogUI);

        public GameObject logPrefab;
        public Transform logContent;
        public ScrollRect scrollRect;

        public TextMeshProUGUI logText;

        private void OnGUI()
        {
            GUI.skin.box.wordWrap = true;
        }

        public void AddLogMessage(string type, string message)
        {
            // 대화 프리팹을 인스턴스화
            GameObject newChat = Instantiate(logPrefab, logContent);

            // 대화 프리팹의 Text 컴포넌트를 설정
            TextMeshProUGUI chatText = newChat.GetComponent<TextMeshProUGUI>();
            chatText.text = $"[{type}] : {message}";

            // 스크롤을 가장 아래로 고정
            Canvas.ForceUpdateCanvases();
            scrollRect.verticalNormalizedPosition = 0f;
        }

    }

}
