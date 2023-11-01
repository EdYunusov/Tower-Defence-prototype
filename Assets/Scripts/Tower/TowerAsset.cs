using UnityEngine;
using SpaseShooter;

[CreateAssetMenu]
public class TowerAsset: ScriptableObject
{
    [SerializeField] private Tower m_TowerPrefab;
    public Tower TowerPrefab => m_TowerPrefab;

    public int goldCost = 15;
    public Sprite towerIcone;
    public Sprite towerVisualModel;
    public TurretProp turretProperties;

    [SerializeField] private UpgradeAsset requiredUpgrade;
    [SerializeField] private int requiredUpgradeLevel;
    public bool IsAvaible() => !requiredUpgrade ||
            requiredUpgradeLevel <= Upgrades.GetUpgradeLevel(requiredUpgrade);
    public TowerAsset[] m_UpgradesTo;
}
