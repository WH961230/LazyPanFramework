using LazyPan;
using UnityEngine;

public class Sound : SingletonMonoBehaviour<Sound> {
    public void PlaySound(string soundSign, SoundInfo soundInfo) {
        GameObject soundGo = new GameObject(soundSign);
        AudioSource soundSource = soundGo.AddComponent<AudioSource>();
        soundSource.clip = Loader.LoadAsset<AudioClip>(Loader.AssetType.SOUND, soundSign);
        soundGo.transform.position = soundInfo.SoundPoint;
        soundSource.Play();
        DontDestroyOnLoad(soundGo);
    }

    public class SoundInfo {
        public Vector3 SoundPoint;

        public SoundInfo(Vector3 soundPoint) {
            SoundPoint = soundPoint;
        }
    }
}