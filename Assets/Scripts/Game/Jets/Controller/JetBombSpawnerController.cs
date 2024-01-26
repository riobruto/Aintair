using Buildings;
using Entities;
using GameSystem;
using Invaders;
using Jets.Controller;
using UnityEngine;

namespace Jets

{
	public class JetBombSpawnerController : JetController
	{
		[SerializeField] private Transform _hatchPoint;

		private Vector3 _bombSite;
		private int _bombCount;
		private int _bombCountMax = 4;
		private Vector3 _lastPosition;

		private float _timer;
		private float _burstTimer = .25f;

		internal override void SetUp()
		{
			Building[] targetBuildings = FindObjectsOfType<Building>();
			Vector3 pos = Vector3.zero;

			foreach(Building b in targetBuildings)
			{
				pos += b.transform.position;
			}
			_bombSite = pos / targetBuildings.Length;
		}

		internal override void OnJetChangeState(JetState state)
		{
			if (state == JetState.RESETTED)
			{
				_bombCount = _bombCountMax;

				_timer = _burstTimer;
			}
		}

		internal override void UpdateController()
		{
			if (Vector3.Distance(_lastPosition, transform.position) > 1)
			{
				_lastPosition = transform.position;
			}
			if (GetOwner().IsDestroyed) return;

			if (IsNearBuilding(_bombSite))
			{
				_timer += Time.deltaTime;

				if (_bombCount > 0 && _timer > _burstTimer)
				{
					BombEntity missile = BombBufferSystem.Instance.BombCircularBuffer.GetNext().GetComponent<BombEntity>();
					missile.transform.position = _hatchPoint.position;
					missile.transform.rotation = _hatchPoint.rotation;

					missile.Initialize();

					Debug.DrawRay(_lastPosition, (transform.position - _lastPosition).normalized * 100, Color.red, 10);

					_bombCount--;
					_timer = 0;
				}
			}
		}

		private bool IsNearBuilding(Vector3 pos)
		{
			return (Vector3.Distance(transform.position, pos) <= GameConfigurationsSystem.Instance.BombingDistance);
		}

		private void OnDrawGizmos()
		{
			Gizmos.DrawWireSphere(_bombSite, GameConfigurationsSystem.Instance.BombingDistance);
		}
	}
}