using System.Collections.Generic;
using System.Linq;
using Data.ScriptableObjects;
using Other;
using UnityEditor;
using UnityEngine;

namespace Data
{
    public class ClientItemDatabase : Singleton<ClientItemDatabase>
    {
        public List<RewardSO> items;
        private Dictionary<int, Sprite> lookup;

        void Awake()
        {
            lookup = new Dictionary<int, Sprite>();
            foreach (var item in items)
                lookup[(int)item.reward.id] = item.sprite;
        }

        public Sprite GetItem(int id)
        {
            if (lookup.TryGetValue(id, out var item))
                return item;

            Debug.LogError("Item not found: " + id);
            return null;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            string[] guids = AssetDatabase.FindAssets("t:RewardSO");
            items = guids.Select(guid => AssetDatabase.LoadAssetAtPath<RewardSO>(AssetDatabase.GUIDToAssetPath(guid))).ToList();
            Debug.Log($"ClientItemDatabase is filled automatically! ({items.Count} piece RewardSO)");
        }
#endif
    }
}
