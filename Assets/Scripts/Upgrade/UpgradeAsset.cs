using UnityEngine;

namespace SpaseShooter
{

    [CreateAssetMenu]
    public sealed class UpgradeAsset : ScriptableObject
    {
        [Header("Внешний вид")]

        public Sprite sprite;

        [Header("Параметры")]

        public int[] costByLevel = { 3 };
    }
}

