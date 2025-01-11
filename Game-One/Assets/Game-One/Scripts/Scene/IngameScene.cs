using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ONE
{
    public class IngameScene : SceneBase
    {

        public override IEnumerator OnStart()
        {
            // Ingame 씬을 비동기로 로드한다.
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Ingame", LoadSceneMode.Single);

            // 로드가 완료될 때 까지 yield return null 을 하면서 기다린다
            while (!asyncLoad.isDone)
            {
                yield return null;
            }

            UIManager.Show<IngameUI>(UIList.IngameUI);
            UIManager.Show<LogUI>(UIList.LogUI);
            // ONE.InputSystem.Singleton.OnEscapeInput += OnEscapeExecute;
        }
        public override IEnumerator OnEnd()
        {
            yield return null;

            // ONE.InputSystem.Singleton.OnEscapeInput -= OnEscapeExecute;
            UIManager.Hide<IngameUI>(UIList.IngameUI);
            UIManager.Hide<LogUI>(UIList.LogUI);
        }

        void OnEscapeExecute()
        {
            // Time.timeScale = 0f;
            // UIManager.Show<PausePopupUI>(UIList.PausePopupUI);
        }
    }
}
