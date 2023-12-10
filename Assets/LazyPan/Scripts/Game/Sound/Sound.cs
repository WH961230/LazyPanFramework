using System.Collections.Generic;
using LazyPan;
using UnityEngine;

public class Sound : SingletonMonoBehaviour<Sound> {
    private List<SoundInfo> soundInfos = new List<SoundInfo>();

    public void PlaySound(string soundSign, Vector3 point, bool isLoop) {
        SoundInfo soundInfo = GetSoundInfo(soundSign);
        if (soundInfo == null) {
            soundInfo = new SoundInfo(soundSign, point, isLoop);
            soundInfos.Add(soundInfo);
        }

        soundInfo.Play();
    }

    public void StopSound(string soundSign) {
        SoundInfo soundInfo = GetSoundInfo(soundSign);
        soundInfo?.Stop();
    }

    public SoundInfo GetSoundInfo(string soundSign) {
        foreach (SoundInfo info in soundInfos) {
            if (info.SoundSign == soundSign) {
                return info;
            }
        }

        return null;
    }

    public class SoundInfo {
        public string SoundSign;
        public GameObject SoundGo;
        public AudioSource SoundSource;
        public Vector3 SoundPoint;
        public bool SoundLock;
        public bool SoundLoop;

        public SoundInfo(string soundSign, Vector3 soundPoint, bool soundLoop) {
            SoundSign = soundSign;
            SoundPoint = soundPoint;
            SoundLoop = soundLoop;
            SoundGo = new GameObject(soundSign);
            SoundSource = SoundGo.AddComponent<AudioSource>();
            SoundSource.playOnAwake = false;
            SoundSource.loop = soundLoop;
            SoundSource.clip = Loader.LoadAsset<AudioClip>(Loader.AssetType.SOUND, soundSign);
            DontDestroyOnLoad(SoundGo);
            Data.Instance.OnUpdateEvent.AddListener(OnUpdate);
        }

        public void Play() {
            SoundGo.transform.position = SoundPoint;
            if (SoundLoop && !SoundSource.isPlaying) {
                SoundSource.Play();
            }

            if (!SoundLoop) {
                if (SoundSource.isPlaying) {
                    SoundSource.Stop();
                }
                SoundSource.Play();
            }
        }

        public void Stop() {
            SoundSource.Stop();
        }

        private void OnUpdate() {
            if (SoundLock) {
                SoundGo.transform.position = SoundPoint;
            }
        }
    }
}