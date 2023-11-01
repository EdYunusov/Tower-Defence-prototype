using UnityEngine;

namespace SpaseShooter
{

    [CreateAssetMenu]
    public sealed class EnemyAssets : ScriptableObject
    {
        [Header("Внешний вид")]

        public Color color = Color.white;
        public Vector2 spriteScale = new Vector2(3, 3);
        public RuntimeAnimatorController animations;

        [Header("Параметры")]

        public float moveSpeed;
        public int HP;
        public int armor = 0;
        public Enemy.TypeOfArmor armorType;
        public int Score;
        public float Radius = 0.19f;
        public int damage = 1;
        public int gold = 0;
        
    }
}

