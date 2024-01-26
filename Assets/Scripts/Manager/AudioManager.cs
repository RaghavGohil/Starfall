using UnityEngine;
using UnityEngine.Audio;

namespace Game.Sound
{
    [System.Serializable]
    sealed public class Sound
    {
        public string name;
        public AudioClip clip;
        public float volume;
        public bool mute;
        public bool playOnAwake;
        public bool loop;
        
        [HideInInspector]
        public AudioSource source;
    }

    sealed public class AudioManager : MonoBehaviour
    {

        public static AudioManager instance;

        [SerializeField]
        Sound[] gameSounds;

        void Awake()
        {

            if(instance == null)
                instance = this;
            else
                Destroy(instance);

            foreach(Sound s in gameSounds)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.volume = s.volume; 
                s.source.mute = s.mute;
                s.source.loop = s.loop;
                s.source.playOnAwake = s.playOnAwake;
                s.source.clip = s.clip;
            }
        }

        public void PlayInGame(string sound)
        {
            bool hasSound = false;
            
            if(gameSounds == null)
            {
                Debug.LogError("No sounds found to play");
                return;
            }
            
            foreach(Sound s in gameSounds)
            {
                if(s.name == sound)
                {
                    hasSound = true;
                    s.source.Play();
                    return;
                }
            }

            if(!hasSound)
            {
                Debug.LogError("Requested audio not found");
            }
        }

        public AudioSource GetAudioSource(string sound)
        {
            foreach(Sound s in gameSounds)
            {
                if(s.name == sound)
                {
                    return s.source;
                }
            }
            Debug.LogError("Unable to get audio source of "+sound);
            return null;
        }

        public void StopInGameAudio()
        {
            if(gameSounds != null)
            {
                foreach(Sound s in gameSounds)
                {
                    if(s.source.isPlaying)
                        s.source.Stop();
                }
            }
            else
                Debug.LogWarning("No audio to stop");
        }
    }
}


