using UnityEngine;

namespace DefaultNamespace
{
    public static class PlayerData
    {
        public static int ChracterNumber;

        #region CharacterTraits

        public static int Health;

        public static int Speed =>
            SpeedCharacter
            [ChracterNumber,
                PlayerPrefs.GetInt($"SpeedIndex{ChracterNumber}")]; //speed koda entegre olucak

        public static int SwordAttackRange =>
            SwordAttackRangeCharacter[ChracterNumber, PlayerPrefs.GetInt($"AttackIndex{ChracterNumber}")];

        #endregion

        #region CharacterTraitsPropertys

        public static int[,] HealthsCharacter =
        {
            { 100, 105, 110, 120, 125 },
            { 130, 135, 140, 150, 155 },
        };

        public static int[,] SpeedCharacter =
        {
            { 100, 105, 110, 120, 125 },
            { 130, 135, 140, 150, 155 },
        };

        public static int[,] SwordAttackRangeCharacter =
        {
            { 10, 15, 20, 25, 30 },
            { 35, 45, 50, 55, 60 }
        };

        #endregion


        public static bool Save;
        public static bool DamageActive = false;

        public static int[] MoneysLevelUp =
        {
            0,
            1000,
            3000,
            5000,
            5000,
            10000,
            20000,
        };

        public static void HealthAdd(int health)
        {
            if (health < 100)
                Health += health;

            if (Health > 100)
                Health = PlayerData.HealthsCharacter[ChracterNumber,
                    PlayerPrefs.GetInt($"HealthIndex{ChracterNumber}")];
        }
    }
}