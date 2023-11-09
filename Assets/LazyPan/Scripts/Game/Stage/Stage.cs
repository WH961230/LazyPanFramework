using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace LazyPan {
    public class Stage : MonoBehaviour {
        public Comp loadingUIComp;
        public int StageCountIndex = 0;
        public int StageCount = 0;
        private StageWork work;
        private Queue<StageWork> works = new Queue<StageWork>();

        public void Load(string sceneName) {
            works.Enqueue(new LoadLoadingUI(new LoadLoadingUIParameters() { Description = "加载 Loading 界面", uiRoot = transform }, this));
            works.Enqueue(new LoadScene(new LoadSceneParameters() { Description = "加载场景", sceneName = sceneName }));
            works.Enqueue(new LoadGlobal(new LoadGlobalParameters() { Description = "加载场景物体", sceneName = sceneName }, this));
            StageCount = works.Count;
        }

        public void Update() {
            if (work == null && works.Count > 0) {
                StageCountIndex = StageCount - works.Count;
                work = works.Dequeue();
                work?.OnStart();
            }
            work?.OnUpdate();
            if (work != null) {
                LoadingUI();
                if (work.IsDone) {
                    work.OnComplete();
                    work = null;
                }
            }
        }

        private void LoadingUI() {
            if (loadingUIComp && work != null) {
                Slider loadingSlider = loadingUIComp.Get<Slider>("UI_Loading_Slider");
                TextMeshProUGUI loadingText = loadingUIComp.Get<TextMeshProUGUI>("UI_Loading_Text");
                float eachProgress = (float) 1 / StageCount;
                loadingSlider.value = eachProgress * (StageCountIndex + work.Progress);
                loadingText.text = string.Concat(work.Parameters.Description, " ", Mathf.Round(loadingSlider.value * 100f), "%");
            }
        }
    }

    public class LoadGlobalParameters : StageParameters {
        public string sceneName;
    }

    public class LoadGlobal : StageWork {
        private LoadGlobalParameters Parameters;
        private Stage stage;
        private Game game;

        public LoadGlobal(StageParameters Parameters, Stage stage) : base(Parameters) {
            this.Parameters = (LoadGlobalParameters)Parameters;
            this.stage = stage;
        }

        public override void OnStart() {
            Progress = 0;
        }

        public override void OnUpdate() {
            if (SceneManager.GetActiveScene().name == Parameters.sceneName && game == null) {
                game = Loader.LoadGo("全局", "Global/Global", null, true).GetComponent<Game>();
            }

            if (game != null && game.LoadFinished && !IsDone) {
                Progress = 1;
                IsDone = true;
                ClockUtil.Instance.AlarmAfter(1f, () => { Object.DestroyImmediate(stage.gameObject); });
            }
        }

        public override void OnComplete() {
        }
    }

    public class LoadSceneParameters : StageParameters {
        public string sceneName;
    }

    public class LoadScene : StageWork {
        LoadSceneParameters Parameters;
        public LoadScene(StageParameters Parameters) : base(Parameters) {
            this.Parameters = (LoadSceneParameters)Parameters;
        }

        public override void OnStart() {
            Progress = 0;
            AsyncOperation operation = Loader.LoadSceneAsync(Parameters.sceneName);
            operation.allowSceneActivation = false;

            while (!operation.isDone) {
                if (operation.progress >= 0.9f && Progress < 1) {
                    Progress = 1;
                    ClockUtil.Instance.AlarmAfter(1f, () => {
                        operation.allowSceneActivation = true;
                        IsDone = true;
                    });
                    break;
                } else {
                    Progress = operation.progress;
                }
            }
        }

        public override void OnUpdate() {
        }

        public override void OnComplete() {
        }
    }

    public class LoadLoadingUIParameters : StageParameters {
        public Transform uiRoot;
    }

    public class LoadLoadingUI : StageWork {
        LoadLoadingUIParameters Parameters;
        Stage stage;

        public LoadLoadingUI(StageParameters Parameters, Stage stage) : base(Parameters) {
            this.Parameters = (LoadLoadingUIParameters)Parameters;
            this.stage = stage;
        }

        public override void OnStart() {
            Progress = 0;
            stage.loadingUIComp = Loader.LoadComp("加载界面", "UI/UI_Loading", Parameters.uiRoot, true);
        }

        public override void OnUpdate() {
            if(stage.loadingUIComp != null) {
                stage.loadingUIComp.gameObject.SetActive(true);
                Progress = 1;
                IsDone = true;
            }
        }

        public override void OnComplete() {
        }
    }

    public class StageParameters {
        public string Description;
    }

    public abstract class StageWork {
        public float Progress;
        public bool IsDone;
        public StageParameters Parameters;
        public StageWork(StageParameters Parameters) {
            this.Parameters = Parameters;
        }

        public abstract void OnStart();
        public abstract void OnUpdate();
        public abstract void OnComplete();
    }
}