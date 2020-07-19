using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Stream_AFK_Text_Game
{
    static class IO
    {
        #region Additional Functions

        public static void Player(Player Player)
        {
            PlayerStats(Player);
            PlayerWeapon(Player.Weapon);
            PlayerArmour(Player.Armour);
        }

        public static void PlayerStats(Player Player)
        {
            PlayerName(Player.GetName());
            PlayerLevel(Player.GetLevel());
            PlayerHP(Player.GetHP());
            PlayerMaxHP(Player.GetMaxHP());
            PlayerAC(Player.GetAC());
            PlayerStr(Player.GetStr());
            PlayerDex(Player.GetDex());
            PlayerCon(Player.GetCon());
            PlayerStrMod(Player.GetStrMod());
            PlayerDexMod(Player.GetDexMod());
            PlayerConMod(Player.GetConMod());
            PlayerXP(Player.GetXP());
            PlayerLU(Player.GetLU());
            PlayerStamina(Player.GetStamina());
            PlayerStaminaMax(Player.GetStaminaMax());
        }

        public static void PlayerWeapon(Weapon Weapon)
        {
            PlayerWeaponName(Weapon.Name);
            PlayerWeaponDamage(Weapon.Damage);
            PlayerWeaponTwoHanded(Weapon.TwoHanded);
            PlayerWeaponVersatile(Weapon.Versatile);
        }

        public static void PlayerArmour(Armour Armour)
        {
            PlayerArmourName(Armour.Name);
            PlayerArmourWeight(Armour.Weight);
            PlayerArmourAC(Armour.AC);
        }

        #endregion

        #region Game Mechanics

        public static void GameUpdate(string Text)
        {
            File.WriteAllText("Stream Files\\Game Mechanics\\Game Updates.txt", Text);
        }

        public static void Events(List<string> Events)
        {
            string EveString = "";
            foreach (string Event in Events)
                EveString += Event + "\n";
            File.WriteAllText("Stream Files\\Game Mechanics\\Events.txt", EveString);
        }

        public static void Options(List<string> Options)
        {
            string OptString = "";
            foreach (string Option in Options)
                OptString += Option + "\n";
            File.WriteAllText("Stream Files\\Game Mechanics\\Player Options.txt", OptString);
        }

        public static void NPCs(List<EnemyNPC> NPCList)
        {
            string NPCString = "";
            foreach (EnemyNPC NPC in NPCList)
                NPCString += NPC.Name + "\n";
            File.WriteAllText("Stream Files\\Game Mechanics\\NPCs.txt", NPCString);
        }

        #endregion

        #region Player

        #region Stats

        public static void PlayerName(string Name)
        {
            File.WriteAllText("Stream Files\\Player\\1. Stats\\1. Name.txt", "Name: " + Name);
        }

        public static string GetPlayerName()
        {
            return File.ReadAllText("Stream FIles\\Player\\1. Stats\\1. Name.txt");
        }

        public static void PlayerLevel(int Level)
        {
            File.WriteAllText("Stream Files\\Player\\1. Stats\\2. Level.txt", "Level: " + Convert.ToString(Level));
        }

        public static void PlayerHP(int HP)
        {
            File.WriteAllText("Stream Files\\Player\\1. Stats\\3. HP.txt", "HP: " + Convert.ToString(HP) + "/");
        }

        public static void PlayerMaxHP(int MaxHP)
        {
            File.WriteAllText("Stream Files\\Player\\1. Stats\\4. MaxHP.txt", Convert.ToString(MaxHP));
        }

        public static void PlayerAC(int AC)
        {
            File.WriteAllText("Stream Files\\Player\\1. Stats\\5. AC.txt", "AC: " + Convert.ToString(AC));
        }

        public static void PlayerStr(int Str)
        {
            File.WriteAllText("Stream Files\\Player\\1. Stats\\6. Str.txt", Convert.ToString(Str));
        }

        public static void PlayerDex(int Dex)
        {
            File.WriteAllText("Stream Files\\Player\\1. Stats\\7. Dex.txt", Convert.ToString(Dex));
        }

        public static void PlayerCon(int Con)
        {
            File.WriteAllText("Stream Files\\Player\\1. Stats\\8. Con.txt", Convert.ToString(Con));
        }

        public static void PlayerStrMod(int StrMod)
        {
            File.WriteAllText("Stream Files\\Player\\1. Stats\\9. StrMod.txt", Convert.ToString(StrMod));
        }

        public static void PlayerDexMod(int DexMod)
        {
            File.WriteAllText("Stream Files\\Player\\1. Stats\\10. DexMod.txt", Convert.ToString(DexMod));
        }

        public static void PlayerConMod(int ConMod)
        {
            File.WriteAllText("Stream Files\\Player\\1. Stats\\11. ConMod.txt", Convert.ToString(ConMod));
        }

        public static void PlayerXP(int XP)
        {
            File.WriteAllText("Stream Files\\Player\\1. Stats\\12. XP.txt", Convert.ToString(XP));
        }

        public static void PlayerLU(int LU)
        {
            File.WriteAllText("Stream Files\\Player\\1. Stats\\13. LevelUp.txt", Convert.ToString(LU));
        }

        public static void PlayerStamina(int Stamina)
        {
            File.WriteAllText("Stream Files\\Player\\1. Stats\\14. Stamina.txt", Convert.ToString(Stamina));
        }

        public static void PlayerStaminaMax(int StaminaMax)
        {
            File.WriteAllText("Stream Files\\Player\\1. Stats\\15. StaminaMax.txt", Convert.ToString(StaminaMax));
        }

        #endregion

        #region Equipment

        #region Weapon

        public static void PlayerWeaponName(string Name)
        {
            File.WriteAllText("Stream Files\\Player\\2. Equipment\\1. Weapon\\1. Name.txt", Name);
        }

        public static void PlayerWeaponDamage(int Damage)
        {
            File.WriteAllText("Stream Files\\Player\\2. Equipment\\1. Weapon\\2. Damage.txt", Convert.ToString(Damage));
        }

        public static void PlayerWeaponTwoHanded(bool TwoHanded)
        {
            File.WriteAllText("Stream Files\\Player\\2. Equipment\\1. Weapon\\3. TwoHanded.txt", Convert.ToString(TwoHanded));
        }

        public static void PlayerWeaponVersatile(bool Versatile)
        {
            File.WriteAllText("Stream Files\\Player\\2. Equipment\\1. Weapon\\4. Versatile.txt", Convert.ToString(Versatile));
        }

        #endregion

        #region Armour

        public static void PlayerArmourName(string Name)
        {
            File.WriteAllText("Stream Files\\Player\\2. Equipment\\2. Armour\\1. Name.txt", Name);
        }

        public static void PlayerArmourWeight(string Weight)
        {
            File.WriteAllText("Stream Files\\Player\\2. Equipment\\2. Armour\\2. Weight.txt", Weight);
        }

        public static void PlayerArmourAC(int AC)
        {
            File.WriteAllText("Stream Files\\Player\\2. Equipment\\2. Armour\\3. AC.txt", Convert.ToString(AC));
        }

        #endregion

        #region Inventory

        public static void PlayerInventory(Inventorys Inventory)
        {
            string PotString = "";
            foreach (Potions Potion in Inventory.Potions)
                PotString += Potion.Name + "\n";
            File.WriteAllText("Stream Files\\Player\\2. Equipment\\3. Inventory\\Inventory.txt", PotString);
        }

        #endregion

        #endregion

        #endregion
    }
}
