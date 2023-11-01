using UnityEngine;

namespace SpaseShooter
{

    [CreateAssetMenu]
    public sealed class UpgradeAsset : ScriptableObject
    {
        [Header("������� ���")]

        public Sprite sprite;

        [Header("���������")]

        public int[] costByLevel = { 3 };
    }
}

