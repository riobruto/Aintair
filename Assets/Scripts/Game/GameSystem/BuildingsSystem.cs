using Buildings;
using Data;
using UnityEngine;

namespace GameSystem
{
	public delegate void BuldingLossDelegate(bool state);

	public class BuildingsSystem : MonoBehaviour
	{
		private Building[] _buildings;
		public Building[] Buildings { get => _buildings; }

		private static BuildingsSystem _instance;
		public static BuildingsSystem Instance { get => _instance; }

		private int _buildingsCount;

		public event BuldingLossDelegate OnBuildingLossEvent;

		private void Awake()
		{
			if (_instance == null) _instance = this;
			OnBuildingLossEvent?.Invoke(false);
		}

		private void Start()
		{
			_buildings = FindObjectsOfType<Building>();
			int count = 0;
			foreach (Building building in _buildings)
			{
				count++;
				building.BuildingStateEvent += BuildingStateEvent;
			}
			_buildingsCount = count;
		}

		private void BuildingStateEvent(BuildingState state)
		{
			switch (state)
			{
				case BuildingState.DESTROYED:

					_buildingsCount--;
					if (_buildingsCount == 0)
					{
						OnBuildingLossEvent?.Invoke(true);
						if (PlayerData.Current.KillCount < JetsSytem.Instance.JetCount)
						{
							PlayerData.Commit(data => { data.KillCount = JetsSytem.Instance.JetCount; return data; });
						}
					}
					break;

				case BuildingState.RECIEVING_DAMAGE:

					break;
			}
		}
	}
}