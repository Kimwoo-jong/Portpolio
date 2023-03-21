using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum StatModType
{
	Flat = 100,
	PercentAdd = 200,
	PercentMult = 300,
}

public class StatModifier
{
	public readonly float Value;
	public readonly StatModType Type;
	public readonly int Order;
	public readonly object Source;

	public StatModifier(float value, StatModType type, int order, object source)
	{
		Value = value;
		Type = type;
		Order = order;
		Source = source;
	}
	//메소드 오버로딩
	//같은 함수 이름에 파라미터가 다를 경우를 생각하여 정의한다.
	public StatModifier(float value, StatModType type) : this(value, type, (int)type, null) { }

	public StatModifier(float value, StatModType type, int order) : this(value, type, order, null) { }

	public StatModifier(float value, StatModType type, object source) : this(value, type, (int)type, source) { }
}
