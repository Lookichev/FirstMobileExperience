using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairwayManager : MonoBehaviour
{
	/// <summary>
	/// Лестничный объект
	/// </summary>
	[SerializeField]
	private GameObject StairwayCreater;

	/// <summary>
	/// Шаблоны ступенек
	/// </summary>
	/// <remarks>
	/// [0] - полная ступенька с 7 позициями
	/// [1] - 6 - позиций
	/// [2] - 5 - позиций
	/// [3] - 4 позиции
	/// [4] - 3 позиции
	/// [5] - 2 позиции
	/// </remarks>
	[SerializeField]
	private GameObject[] StairwayPrefabs;


	/// <summary>
	/// Путь в Высшую Лигу
	/// </summary>
	/// <remarks>Эн таро Тассадар</remarks>
	private LinkedList<GameObject> _Ladders;


	/// <summary>
	/// Нужно-ли генерировать новую ступеньку
	/// </summary>
	private bool _NeedNewLadderBlock;

	/// <summary>
	/// Возвращает координаты верхней ступени
	/// </summary>
	public Vector3 GetLastStep => _Ladders.Last.Value.transform.position;

	/// <summary>
	/// Создает новый блок лестницы
	/// </summary>
	private void CreateNewBlock()
	{
		//Экземпляр ступеньки
		GameObject block = null;
		//Индекс шаблона ступеньки
		int range = 0;

		//Если, есть повышенный уровень сложности - учитываем координату Z
		//Если : сложность игры равная двум шагам усложнения не превышает пройденный путь - сложность не повышать
		if (MainManager.StepComplications * 2 < MainManager.Interface.NumStep)
			block = Instantiate(StairwayPrefabs[0], StairwayCreater.transform);
		//Добавляются ступеньки длины 6 и 5 шаговые
		else if (MainManager.StepComplications * 3 < MainManager.Interface.NumStep)
		{
			range = Random.Range(0, 4);
			block = Instantiate(StairwayPrefabs[range], StairwayCreater.transform);
		}
		//Добавляются ступеньки 4 и 3 шаговые
		else if (MainManager.StepComplications * 3 > MainManager.Interface.NumStep)
		{
			//range = Random.Range(0, 5);НАДО ДОБАВИТЬ ПРОВЕРКУ, ЧТОБЫ СТУПЕНЬКИ ВСЕГДА ИМЕЛИ ПУТЬ ИХ ПЕРЕШАГИВАНИЯ, ИНАЧЕ ОНИ МОГУТ СТОЯТЬ ДАЛЕКО ДРУГ ОТ ДРУЖКИ
			range = Random.Range(0, 4);
			block = Instantiate(StairwayPrefabs[range], StairwayCreater.transform);
		}

		//Установка координаты Z
		var posZ = 0;
		if (range == 0) { }
		//Шестишажные ступеньки имеют два варианта позиционирования
		else if (range == 1) posZ = Random.Range(0, 2) - 1;
		//Пятишажные ступеньки имеют три варианта позиции
		else if (range == 2) posZ = Random.Range(-1, 2);
		//Четырехшажные ступеньки имеют 4 варианта позиции
		else if(range == 3) posZ = Random.Range(-1, 3) - 1;
		//Трехшажные имеют - 5 позиций
		else posZ = Random.Range(-2, 3);

		//Установка координат X и Y
		//Локальная позиция экземпляра ступеньки
		Vector3 pos = _Ladders.Last.Value.transform.localPosition;
		//Смещение последнего блока на два вверх и вперед (локальные координаты)
		block.transform.localPosition = new Vector3(pos.x + 1, pos.y + 1, posZ);

		_Ladders.AddLast(block);
	}

	void Start()
	{
		_Ladders = new LinkedList<GameObject>();

		_Ladders.AddLast(Instantiate(StairwayPrefabs[0], StairwayCreater.transform));
		_Ladders.Last.Value.transform.position = new Vector3(10, -10, 0);

		//Строим первые ступени
		while (_Ladders.Count != 25)
		{
			var block = Instantiate(StairwayPrefabs[0], StairwayCreater.transform);

			//Координаты предпоследнего блока лестницы
			var position = _Ladders.Last.Value.transform.localPosition;

			//Смещение последнего блока на один вверх и вперед (локальные координаты)
			block.transform.localPosition = new Vector3(position.x + 1, position.y + 1, 0);

			_Ladders.AddLast(block);
		}
	}

	/// <summary>
	/// Генерирует новую ступеньку и удаляет старую
	/// </summary>
	public void CreateNewStep()
	{
		CreateNewBlock();

		Destroy(_Ladders.First.Value);
		_Ladders.RemoveFirst();
	}
}
