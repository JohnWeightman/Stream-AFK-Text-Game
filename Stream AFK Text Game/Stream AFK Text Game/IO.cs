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

        public static void ResetFiles(Player PlayerObj)
        {
            Player(PlayerObj);
            GameUpdate("Game Starting...");
            Events(null);
            Options(null);
            File.WriteAllText("Stream Files\\Game Mechanics\\NPCs.txt", "");
            VoteTimer(30);
        }

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
            PlayerHP(Player.GetHP(), Player.GetMaxHP());
            PlayerAC(Player.GetAC());
            PlayerXP(Player.GetXP());
            PlayerLU(Player.GetLU());
            PlayerStamina(Player.GetStamina(), Player.GetStaminaMax());
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
            if(Events != null)
                foreach (string Event in Events)
                    EveString += Event + "\n";
            File.WriteAllText("Stream Files\\Game Mechanics\\Events.txt", EveString);
        }

        public static void Options(List<string> Options)
        {
            string OptString = "";
            int Count = 0;
            if(Options != null)
                foreach (string Option in Options)
                {
                    Count++;
                    OptString += Count + ". " + Option + "\n";
                }
            File.WriteAllText("Stream Files\\Game Mechanics\\Player Options.txt", OptString);
        }

        public static void NPCs(List<EnemyNPC> NPCList)
        {
            string NPCString = "";
            int Count = 0;
            foreach (EnemyNPC NPC in NPCList)
            {
                Count++;
                NPCString += Count + ". " + NPC.Name + "\n";
            }
            File.WriteAllText("Stream Files\\Game Mechanics\\NPCs.txt", NPCString);
        }

        public static void VoteTimer(int Seconds)
        {
            File.WriteAllText("Stream Files\\Game Mechanics\\Vote Timer.txt", Convert.ToString(Seconds) + " Secs");
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
            string Name = File.ReadAllText("Stream FIles\\Player\\1. Stats\\1. Name.txt").Substring(6);
            return Name;
        }

        public static void PlayerLevel(int Level)
        {
            File.WriteAllText("Stream Files\\Player\\1. Stats\\2. Level.txt", "Level: " + Convert.ToString(Level));
        }

        public static void PlayerHP(int HP, int MaxHP)
        {
            File.WriteAllText("Stream Files\\Player\\1. Stats\\3. HP.txt", "HP: " + Convert.ToString(HP) + "/" + Convert.ToString(MaxHP));
        }

        public static void PlayerAC(int AC)
        {
            File.WriteAllText("Stream Files\\Player\\1. Stats\\5. AC.txt", "AC: " + Convert.ToString(AC));
        }

        public static void PlayerStr(int Str, int StrMod)
        {
            File.WriteAllText("Stream Files\\Player\\1. Stats\\6. Str.txt", "Str: " + Convert.ToString(Str) + " (+" + Convert.ToString(StrMod) + ")");
        }

        public static void PlayerDex(int Dex, int DexMod)
        {
            File.WriteAllText("Stream Files\\Player\\1. Stats\\7. Dex.txt", "Dex: " + Convert.ToString(Dex) + " (+" + Convert.ToString(DexMod) + ")");
        }

        public static void PlayerCon(int Con, int ConMod)
        {
            File.WriteAllText("Stream Files\\Player\\1. Stats\\8. Con.txt", "Con: " + Convert.ToString(Con) + " (+" + Convert.ToString(ConMod) + ")");
        }

        public static void PlayerXP(int XP)
        {
            File.WriteAllText("Stream Files\\Player\\1. Stats\\12. XP.txt", "XP: " + Convert.ToString(XP));
        }

        public static void PlayerLU(int LU)
        {
            File.WriteAllText("Stream Files\\Player\\1. Stats\\13. LevelUp.txt", "Level Up: " + Convert.ToString(LU));
        }

        public static void PlayerStamina(int Stamina, int StaminaMax)
        {
            File.WriteAllText("Stream Files\\Player\\1. Stats\\14. Stamina.txt", "Sta: " + Convert.ToString(Stamina) + "/" + Convert.ToString(StaminaMax));
        }

        #endregion

        #region Equipment

        #region Weapon

        public static void PlayerWeaponName(string Name)
        {
            File.WriteAllText("Stream Files\\Player\\2. Equipment\\1. Weapon\\1. Name.txt", "Weapon: " + Name);
        }

        public static void PlayerWeaponDamage(int Damage)
        {
            File.WriteAllText("Stream Files\\Player\\2. Equipment\\1. Weapon\\2. Damage.txt", "Weapon Damage: " + Convert.ToString(Damage));
        }

        public static void PlayerWeaponTwoHanded(bool TwoHanded)
        {
            if(TwoHanded)
                File.WriteAllText("Stream Files\\Player\\2. Equipment\\1. Weapon\\3. TwoHanded.txt", "TwoHanded");
            else
                File.WriteAllText("Stream Files\\Player\\2. Equipment\\1. Weapon\\3. TwoHanded.txt", "");
        }

        public static void PlayerWeaponVersatile(bool Versatile)
        {
            if(Versatile)
                File.WriteAllText("Stream Files\\Player\\2. Equipment\\1. Weapon\\4. Versatile.txt", "Versatile");
            else
                File.WriteAllText("Stream Files\\Player\\2. Equipment\\1. Weapon\\4. Versatile.txt", "Versatile");
        }

        #endregion

        #region Armour

        public static void PlayerArmourName(string Name)
        {
            File.WriteAllText("Stream Files\\Player\\2. Equipment\\2. Armour\\1. Name.txt", "Name: " + Name);
        }

        public static void PlayerArmourWeight(string Weight)
        {
            File.WriteAllText("Stream Files\\Player\\2. Equipment\\2. Armour\\2. Weight.txt", "Weight: " + Weight);
        }

        public static void PlayerArmourAC(int AC)
        {
            File.WriteAllText("Stream Files\\Player\\2. Equipment\\2. Armour\\3. AC.txt", "Armour AC: " + Convert.ToString(AC));
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
