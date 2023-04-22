using UnityEngine;
public class StatPanel : MonoBehaviour
{
	[SerializeField] StatDisplay[] statDisplays;
	[SerializeField] string[] statNames;
	
	public string[] statDescriptions;

	private CharacterStat[] stats;

	private void OnValidate()
	{
		statDisplays = GetComponentsInChildren<StatDisplay>();
		//유니티 에디터에서만 호출되는 함수(확인용)
		//UpdateStatNames();
		//UpdateStatDescription();
	}

	public void SetStats(params CharacterStat[] charStats)
	{
		stats = charStats;

		if (stats.Length > statDisplays.Length)
		{
			Debug.LogError("Not Enough Stat Displays!");
			return;
		}

		for (int i = 0; i < statDisplays.Length; i++)
		{
			statDisplays[i].gameObject.SetActive(i < stats.Length);

			if (i < stats.Length)
			{
				statDisplays[i].Stat = stats[i];
			}
		}
	}
	//스탯의 값
	public void UpdateStatValues()
	{
		for (int i = 0; i < stats.Length; i++)
		{
			statDisplays[i].UpdateStatValue();
		}
	}
	//스탯 이름
	public void UpdateStatNames()
	{
		for (int i = 0; i < statNames.Length; i++)
		{
			statDisplays[i].Name = statNames[i];
		}
	}
	//스탯에 대한 설명
	public void UpdateStatDescription()
    {
		for (int i = 0; i < statDescriptions.Length; i++)
		{
			statDisplays[i].Description = statDescriptions[i];
		}
	}
}