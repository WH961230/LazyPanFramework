using System.Collections.Generic;
using LazyPan.Scripts.Core.Component;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace LazyPan.Scripts.Game {
    public class Launch : MonoBehaviour {
        private Transform uiRoot;
        private Game game;
        private void Start() {
            Loader.Loader.Load("主相机", "Camera/Camera_Main", null);
            Loader.Loader.Load("灯光", "Light/Light_Directional", null);

            GameObject uiCanvas = Loader.Loader.Load("画布", "Interface/UI_Canvas", null);
            uiRoot = uiCanvas.transform;
            GameComp uiBeginComp = Loader.Loader.LoadComp("主界面", "Interface/UI_Begin", uiRoot);

            Button startBtn = uiBeginComp.Get<Button>("UI_Begin_Start");
            Button quitBtn = uiBeginComp.Get<Button>("UI_Begin_Quit");

            Listener.Listener.AddListener(startBtn, StartGame);
            Listener.Listener.AddListener(quitBtn, QuitGame);
        }

        private void StartGame() {
            GameComp uiLoadingComp = Loader.Loader.LoadComp("加载界面", "Interface/UI_Loading", uiRoot);
            Slider uiLoadingSlider = uiLoadingComp.Get<Slider>("UI_Loading_Slider");
            TextMeshProUGUI uiLoadingText = uiLoadingComp.Get<TextMeshProUGUI>("UI_Loading_Text");
            StartCoroutine(LoadScene("Fight", uiLoadingSlider, uiLoadingText));
        }

        private void QuitGame() {
            Debug.Log("QuitGame");
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }

        IEnumerator<string> LoadScene(string name, Slider slider, TextMeshProUGUI text) {
            yield return null;

            AsyncOperation operation = Loader.Loader.LoadSceneAsync(name);
            operation.completed += asyncOperation => Debug.Log("Load scene completed!");
            operation.allowSceneActivation = false;
            float progress = 0;
            while (!operation.isDone) {
                progress = Mathf.MoveTowards(progress, operation.progress, Time.deltaTime);
                if (progress >= 0.9f) {
                    slider.value = Mathf.MoveTowards(slider.value, 1, Time.deltaTime);
                    if (slider.value == 1) {
                        operation.allowSceneActivation = true;
                    }
                } else {
                    slider.value = progress;
                }
                text.text = string.Concat("Loading ", slider.value * 100, "%");
                yield return null;
            }
        }
    }
}