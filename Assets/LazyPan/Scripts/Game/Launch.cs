using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LazyPan {
    public class Launch : MonoBehaviour {
        private Transform uiRoot;
        private Game game;

        private void Start() {
            Loader.Load("主相机", "Camera/Camera_Main", null);
            Loader.Load("灯光", "Light/Light_Directional", null);

            uiRoot = Loader.Load("画布", "Global/Global_UI_Root", null).transform;
            GameComp uiBeginComp = Loader.LoadComp("主界面", "UI/UI_Begin", uiRoot);

            Listener.AddListener(uiBeginComp.Get<Button>("UI_Begin_Start"), StartGame);
            Listener.AddListener(uiBeginComp.Get<Button>("UI_Begin_Quit"), Act.QuitGame);
        }

        private void StartGame() {
            GameComp uiLoadingComp = Loader.LoadComp("加载界面", "UI/UI_Loading", uiRoot);
            StartCoroutine(LoadScene("Fight", uiLoadingComp.Get<Slider>("UI_Loading_Slider"), uiLoadingComp.Get<TextMeshProUGUI>("UI_Loading_Text")));
        }

        IEnumerator<string> LoadScene(string name, Slider slider, TextMeshProUGUI text) {
            yield return null;

            AsyncOperation operation = Loader.LoadSceneAsync(name);
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
                text.text = string.Concat("Loading ", Mathf.Round(slider.value * 100f) , "%");
                yield return null;
            }
        }
    }
}