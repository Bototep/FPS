using UnityEngine;

public class SoundManager : MonoBehaviour
{
	public static SoundManager instance;

	public AudioClip backgroundMusic;
	public AudioClip[] soundEffects;

	private AudioSource musicSource;
	private AudioSource effectsSource;

	public float musicVolume = 0.5f;
	public float effectsVolume = 0.5f;

	void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	void Start()
	{
		if (musicSource == null)
		{
			musicSource = gameObject.AddComponent<AudioSource>();
		}
		if (effectsSource == null)
		{
			effectsSource = gameObject.AddComponent<AudioSource>();
		}

		PlayBackgroundMusic();
	}

	public void PlayBackgroundMusic()
	{
		if (backgroundMusic != null)
		{
			musicSource.clip = backgroundMusic;
			musicSource.loop = true;
			musicSource.volume = musicVolume;
			musicSource.Play();
		}
	}

	public void PlaySoundEffect(int index)
	{
		if (index >= 0 && index < soundEffects.Length)
		{
			effectsSource.clip = soundEffects[index];
			effectsSource.volume = effectsVolume;
			effectsSource.Play();
		}
	}

	public void SetMusicVolume(float volume)
	{
		musicVolume = volume;
		musicSource.volume = volume;
	}

	public void SetEffectsVolume(float volume)
	{
		effectsVolume = volume;
		effectsSource.volume = volume;
	}
}