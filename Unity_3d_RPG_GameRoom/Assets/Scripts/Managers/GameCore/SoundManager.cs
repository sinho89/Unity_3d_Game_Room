using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager
{
    // AudioSource   = Device
    // AudioClip     = Sound
    // AudioListener = Listener

    AudioSource[] _audioSources = new AudioSource[(int)Defines.Sounds.MaxCount];
    Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();

    public void Init()
    {
        GameObject root = GameObject.Find("@Sound");

        if(root == null)
        {
            root = new GameObject { name = "@Sound" };
            Object.DontDestroyOnLoad(root);

            string[] soundNames = System.Enum.GetNames(typeof(Defines.Sounds));
            for(int i = 0; i < soundNames.Length - 1; i++)
            {
                GameObject go = new GameObject { name = soundNames[i] };
                _audioSources[i] = go.AddComponent<AudioSource>();
                go.transform.parent = root.transform;
            }

            _audioSources[(int)Defines.Sounds.Bgm].loop = true;
        }
    }
    AudioClip GetOrAddAudioClip(string path, Defines.Sounds type = Defines.Sounds.Effect)
    {
        if (path.Contains("Sounds/") == false)
            path = $"Sounds/{path}";

        AudioClip audioClip = null;

        if (type == Defines.Sounds.Bgm)
        {
            audioClip = Managers.Resource.Load<AudioClip>(path);
        }
        else
        {
            if (_audioClips.TryGetValue(path, out audioClip) == false)
            {
                audioClip = Managers.Resource.Load<AudioClip>(path);
                _audioClips.Add(path, audioClip);
            }
        }

        if (audioClip == null)
            Debug.Log($"AudioClip Missing : {path}");

        return audioClip;
    }

    public void Play(string path, Defines.Sounds type = Defines.Sounds.Effect, float volume = 1.0f, float pitch = 1.0f)
    {
        AudioClip audioClip = GetOrAddAudioClip(path, type);
        Play(audioClip, type, volume, pitch);
    }

    public void Play(AudioClip audioClip, Defines.Sounds type = Defines.Sounds.Effect, float volume = 1.0f, float pitch = 1.0f)
    {
        if (audioClip == null)
            return;

        if(type == Defines.Sounds.Bgm)
        {
            AudioSource audioSource = _audioSources[(int)Defines.Sounds.Bgm];
            if (audioSource.isPlaying)
                audioSource.Stop();

            audioSource.pitch = pitch;
            audioSource.clip = audioClip;
            audioSource.volume = volume;
            audioSource.Play();
        }
        else
        {
            AudioSource audioSource = _audioSources[(int)Defines.Sounds.Effect];
            audioSource.pitch = pitch;
            audioSource.volume = volume;
            audioSource.PlayOneShot(audioClip);
        }
    }

    public void Clear()
    {
        foreach (AudioSource audioSource in _audioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }

        _audioClips.Clear();
    }
}
