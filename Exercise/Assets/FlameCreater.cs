using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameCreater : MonoBehaviour
{
	/// <summary>
	/// Система частиц пламени
	/// </summary>
	[SerializeField]
	private GameObject FlamePrefab;

	/// <summary>
	/// Радиус установки пламени
	/// </summary>
	[SerializeField]
	private float Radius = 1f;

	/// <summary>
	/// Время на смещение пламени
	/// </summary>
	[SerializeField]
	private int Timer = 5;

    void Start()
    {
		//Генерирует 30 потоков пламени
        for(var i = 0; i < 10; i++)	StartCoroutine(CreaterFlame());
	}

	/// <summary>
	/// Смена позиции пламени
	/// </summary>
	private IEnumerator CreaterFlame()
	{
		var flame = Instantiate(FlamePrefab, gameObject.transform);
		Vector3 pos = Vector3.zero;

		//Через каждые 5 секунд пламя смещается в другую точку
		while(true)
		{
			pos = new Vector3(Random.Range(-Radius, Radius) + 3, 0, Random.Range(-Radius, Radius) - 1);

			flame.transform.localPosition = pos;

			yield return new WaitForSeconds(Timer);
		}
	}
}
