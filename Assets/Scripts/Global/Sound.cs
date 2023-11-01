public enum Sound 
{
   BGM = 0,
   ArrowShoot = 1,
   MagicShoot = 2,
   WinSound = 3,
   LoseSound = 4,
   PlayerTakeDamage = 5,
   ButtonSound = 6,
   BuyUpgradeSound = 7,
   EnemyDeath = 8,
   ArrowHit = 9,
   SpeerShoot = 10,
   SpeerHit = 11,
   BattelThemeLVL1 = 12,
   BattelThemeLVL2 = 13,
   BattelThemeLVL3 = 14,
   MagicHit = 15,

}

public static class SoundExtensions
{
    public static void Play(this Sound sound)
    {
        SoundPlayer.Instance.Play(sound);
    }
}
