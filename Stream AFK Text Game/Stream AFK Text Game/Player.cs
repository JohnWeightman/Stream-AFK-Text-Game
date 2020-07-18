using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stream_AFK_Text_Game
{
    class Player
    {
        string Name;
        int Level, HP, MaxHP, AC, Str, Dex, Con, StrMod, DexMod, ConMod, XP, LU, Stamina, StaminaMax, Initiative;
        bool Dead;
        public Weapon Weapon = new Weapon();
        public Armour Armour = new Armour();
        public Inventorys Inventory = new Inventorys();

        #region Get/Set Variables

        public void SetName(string NewName)
        {
            Name = NewName;
        }

        public string GetName()
        {
            return Name;
        }

        public void SetLevel(int NewLevel)
        {
            Level = NewLevel;
        }

        public int GetLevel()
        {
            return Level;
        }

        public void SetHP(int NewHP)
        {
            HP = NewHP;
        }

        public int GetHP()
        {
            return HP;
        }

        public void SetMaxHP(int NewMaxHP)
        {
            MaxHP = NewMaxHP;
        }

        public int GetMaxHP()
        {
            return MaxHP;
        }

        public void SetAC(int NewAC)
        {
            AC = NewAC;
        }

        public int GetAC()
        {
            return AC;
        }

        public void SetStr(int NewStr)
        {
            Str = NewStr;
        }

        public int GetStr()
        {
            return Str;
        }

        public void SetDex(int NewDex)
        {
            Dex = NewDex;
        }

        public int GetDex()
        {
            return Dex;
        }

        public void SetCon(int NewCon)
        {
            Con = NewCon;
        }

        public int GetCon()
        {
            return Con;
        }

        public void SetStrMod(int NewStrMod)
        {
            StrMod = NewStrMod;
        }

        public int GetStrMod()
        {
            return StrMod;
        }

        public void SetDexMod(int NewDexMod)
        {
            DexMod = NewDexMod;
        }

        public int GetDexMod()
        {
            return DexMod;
        }

        public void SetConMod(int NewConMod)
        {
            ConMod = NewConMod;
        }

        public int GetConMod()
        {
            return ConMod;
        }

        public void SetXP(int NewXP)
        {
            XP = NewXP;
        }

        public int GetXP()
        {
            return XP;
        }

        public void SetLU(int NewLU)
        {
            LU = NewLU;
        }

        public int GetLU()
        {
            return LU;
        }

        public void SetStamina(int NewStamina)
        {
            Stamina = NewStamina;
        }

        public int GetStamina()
        {
            return Stamina;
        }

        public void SetStaminaMax(int NewStaminaMax)
        {
            StaminaMax = NewStaminaMax;
        }

        public int GetStaminaMax()
        {
            return StaminaMax;
        }

        public void SetDead(bool NewDead)
        {
            Dead = NewDead;
        }

        public bool GetDead()
        {
            return Dead;
        }

        public void SetInitiative(int NewIni)
        {
            Initiative = NewIni;
        }

        public int GetInitiative()
        {
            return Initiative;
        }

        #endregion

        #region Update Stats

        public void UpdateAbilityModifiers()
        {
            StrMod = Str / 3;
            DexMod = Dex / 3;
            ConMod = Con / 3;
        }

        public int UpdatePlayerAC()
        {
            int AC = 0;
            if (Armour.Name != "N/A")
            {
                if (Armour.Weight == "Light")
                {
                    if (DexMod > 5)
                        AC = Armour.AC + 5;
                    else
                        AC = Armour.AC + DexMod;
                }
                else
                {
                    AC = Armour.AC;
                }
            }
            else
            {
                if (DexMod > 5)
                    AC = 11;
                else
                    AC = 6 + DexMod;
            }
            return AC;
        }

        #endregion

        #region Character Creation

        public void CreateCharacter()
        {
            Dead = false;
            Name = IO.GetPlayerName();
            Level = 1;
            Str = 1;
            Dex = 1;
            Con = 1;
            int Count = 27;
            while (Count > 0)
            {
                int Stat = DiceRoller.RollDice(3);
                switch (Stat)
                {
                    case 1:
                        Str += 1;
                        if (Str > 15)
                        {
                            Str = 15;
                            Count++;
                        }
                        break;
                    case 2:
                        Dex += 1;
                        if (Dex > 15)
                        {
                            Dex = 15;
                            Count++;
                        }
                        break;
                    case 3:
                        Con += 1;
                        if (Con > 15)
                        {
                            Con = 15;
                            Count++;
                        }
                        break;
                    default:
                        Str += 1;
                        break;
                }
                Count--;
            }
            UpdateAbilityModifiers();
            MaxHP = 10 + ConMod;
            HP = MaxHP;
            StaminaMax = 4 + (2 * DexMod);
            if (StaminaMax < 7)
                StaminaMax = 7;
            Stamina = StaminaMax;
            AC = DexMod;
            XP = 0;
            LU = 50;
            Armour.UpdateArmourString("");
            Weapon.UpdateWeaponString("Shortsword");
            UpdatePlayerAC();
            IO.Player(this);
        }

        #endregion
    }

    class Inventorys
    {
        public List<Potions> Potions = new List<Potions>();
        int MaxItems = 10;
        int CurrentItems = 0;

        public void Clear()
        {
            Potions.Clear();
            CurrentItems = 0;
        }
    }
}
