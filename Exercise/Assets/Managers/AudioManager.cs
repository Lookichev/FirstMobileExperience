using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	/// <summary>
	/// Озвучка меню
	/// </summary>
	[SerializeField]
	private AudioClip MenuSound;

	/// <summary>
	/// Озвучка движения в сторону
	/// </summary>
	[SerializeField]
	private AudioClip SideMoveSound;

	/// <summary>
	/// Озвучка движения вперед
	/// </summary>
	[SerializeField]
	private AudioClip FrontMoveSound;

	/// <summary>
	/// Озвучка поражения
	/// </summary>
	[SerializeField]
	private AudioClip GameOverSound;

	/// <summary>
	/// Cандтрек
	/// </summary>
	[SerializeField]
	private AudioClip Soundtrack;

	/// <summary>
	/// Источник звука
	/// </summary>
	private AudioSource _Source;

	/// <summary>
	/// Настройка громкости
	/// </summary>
	public float SoundVolume
	{
		get => AudioListener.volume;
		set
		{
			AudioListener.volume = value;
			RecordTable.SoundValue = value;
		}
	}

	/// <summary>
	/// Включение звука
	/// </summary>
	public bool SoundMute
	{
		get => AudioListener.pause;
		set
		{
			AudioListener.pause = value;
			RecordTable.Mute = value;
		}
	}

	void Start()
	{
		//Восстановление предыдущих настроек
		AudioListener.volume = RecordTable.SoundValue;
		AudioListener.pause = RecordTable.Mute;

		//Инициализация источника звука в кода
		_Source = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
		//Загрузка клипа в источник
		_Source.clip = Soundtrack;
		_Source.Play();
		//ЗАцикливание
		_Source.loop = true;
		//Чисто 2D звук
		_Source.spatialBlend = 0;
	}

	/// <summary>
	/// Воспроизведение звуков меню
	/// </summary>
	/// <param name="clip">Клипак</param>
	public void PlayMenuSound()
	{
		_Source.PlayOneShot(MenuSound);
	}

	/// <summary>
	/// Воспроизведение звуков движения в сторону
	/// </summary>
	public void PlaySideMoveSound()
	{
		_Source.PlayOneShot(SideMoveSound, SoundVolume / 4);
	}

	/// <summary>
	/// Воспроизведение звуков движения вперед
	/// </summary>
	public void PlayFrontMoveSound()
	{
		_Source.PlayOneShot(FrontMoveSound, SoundVolume / 4);
	}

	/// <summary>
	/// Воспроизведение барабанов при поражении
	/// </summary>
	public void PlayGameOverSound()
	{
		_Source.PlayOneShot(GameOverSound);
	}

	/// <summary>
	/// Остановка сандтрека
	/// </summary>
	public void StopSoundtrack()
	{
		_Source.Stop();
	}
}
