using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;
    private bool shouldPlay = true;

    // Start is called before the first frame update
    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.playOnAwake = false;
        }
    }

    public void Play(string name, Vector3 location)
    {
        if (shouldPlay)
        {
            Sound s = System.Array.Find(sounds, sound => sound.name == name);
            AudioSource.PlayClipAtPoint(s.source.clip, location);
        }
    }

    private void OnApplicationQuit()
    {
        shouldPlay = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
