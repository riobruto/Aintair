using System;
using System.Collections;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class PlayerData
    {
        public delegate void CommitDelegate(PlayerData data);
        public static event CommitDelegate CommitEvent;
        
        public int KillCount;

        private static PlayerData _cache;
        public static PlayerData Current
        {
            get
            {
                if (_cache != null) return _cache;

                if (!PlayerPrefs.HasKey(nameof(PlayerData))) return _cache = new PlayerData();
                return _cache = JsonUtility.FromJson<PlayerData>(PlayerPrefs.GetString(nameof(PlayerData)));

            }
        }
        public static void Commit(Func<PlayerData,PlayerData> predicate)
        {
            predicate(Current).Write();
            CommitEvent?.Invoke(Current);
        }
        public void Write()
        {
            PlayerPrefs.SetString(nameof(PlayerData), JsonUtility.ToJson(this));
            PlayerPrefs.Save();
        }
    }
}