using System;
using System.Collections.Generic;
using System.Threading;

namespace Stream_AFK_Text_Game
{
    class Player
    {
        string Name;
        int Level, HP, MaxHP, AC, Str, Dex, Con, StrMod, DexMod, ConMod, XP, LU, Stamina, StaminaMax, Initiative, AtkBonus;
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

        public void SetStrMod()
        {
            StrMod = Str / 3;
        }

        public int GetStrMod()
        {
            return StrMod;
        }

        public void SetDexMod()
        {
            DexMod = Dex / 3;
        }

        public int GetDexMod()
        {
            return DexMod;
        }

        public void SetConMod()
        {
            ConMod = Con / 3;
        }

        public int GetConMod()
        {
            return ConMod;
        }

        public void AddXP(int AXP)
        {
            XP += AXP;
            if (XP >= LU)
                LevelUp();
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

        public void SetAtkBonus()
        {
            AtkBonus = StrMod + (Level / 3);
        }

        public int GetAtkBonus()
        {
            return AtkBonus;
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
            SetStrMod();
            SetDexMod();
            SetConMod();
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
            SetAtkBonus();
            AC = DexMod;
            XP = 0;
            LU = 50;
            Armour.UpdateArmourString("");
            Weapon.UpdateWeaponString("Shortsword");
            UpdatePlayerAC();
        }

        #endregion

        #region Level Up

        void LevelUp()
        {
            Thread.Sleep(Settings.GetPauseTime());
            IO.GameUpdate("You Leveled up!");
            Thread.Sleep(Settings.GetPauseTime());
            List<string> Options = new List<string>() { "Str", "Dex", "Con" };
            string Extra = "";
            int ABPoints = 3;
            while(ABPoints > 0)
            {
                IO.GameUpdate(Extra + "You have 3 ability points to spend!\n\nStr: " + Str + " (+" + StrMod + ")\nDex: " + Dex + " (+" + DexMod + ")\nCon: " + Con +
                    " (+" + ConMod + ")");
                IO.Options(Options);
                int Input = MainClass.ChatInput(Options.Count);
                switch (Input)
                {
                    case 1:
                        Str += 1;
                        Extra = "Strength increased by 1!\n\n";
                        break;
                    case 2:
                        Dex += 1;
                        Extra = "Dexterity increased by 1!\n\n";
                        break;
                    case 3:
                        Con += 1;
                        Extra = "Constitution increased by 1!\n\n";
                        break;
                }
                UpdateAbilityModifiers();
                SetAtkBonus();
                ABPoints -= 1;
            }
        }

        #endregion

        #region Change Stats from Console

        public void ConsoleInput(string Stat, int Arg)
        {
            switch (Stat)
            {
                case "str":
                    ConsoleInputStr(Arg);
                    break;
                case "dex":
                    ConsoleInputDex(Arg);
                    break;
                case "con":
                    ConsoleInputCon(Arg);
                    break;
                case "sta":
                    ConsoleInputStamina(Arg);
                    break;
                case "stamax":
                    ConsoleInputStaminaMax(Arg);
                    break;
                case "hp":
                    ConsoleInputHP(Arg);
                    break;
                case "hpmax":
                    ConsoleInputMaxHP(Arg);
                    break;
                default:
                    Debug.Log("Player -> Invalid Stat - " + Stat);
                    break;
            }
        }

        void ConsoleInputStr(int Arg)
        {
            if(Arg >= -30 && Arg <= 30)
            {
                Str = Arg;
                UpdateAbilityModifiers();
                SetAtkBonus();
                Debug.Log("Player 'Str' set to " + Str);
                IO.PlayerStr(Str, StrMod);
            }
            else
            {
                Debug.Log("Unable to set Player 'Str' to " + Arg);
                Debug.Log("Player 'Str' Range -> -30-30");
            }
        }

        void ConsoleInputDex(int Arg)
        {
            if(Arg >= -30 && Arg <= 30)
            {
                Dex = Arg;
                UpdateAbilityModifiers();
                UpdatePlayerAC();
                Debug.Log("Player 'Dex' set to " + Dex);
                IO.PlayerDex(Dex, DexMod);
            }
            else
            {
                Debug.Log("Unable to set Player 'Dex' to " + Arg);
                Debug.Log("Player 'Dex' Range -> -30-30");
            }
        }

        void ConsoleInputCon(int Arg)
        {
            if(Arg >= -30 && Arg <= 30)
            {
                Con = Arg;
                UpdateAbilityModifiers();
                Debug.Log("Player 'Con' set to " + Con);
                IO.PlayerCon(Con, ConMod);
            }
            else
            {
                Debug.Log("Unable to set Player 'Con' to " + Arg);
                Debug.Log("Player 'Con' Range -> -30-30");
            }
        }

        void ConsoleInputStamina(int Arg)
        {
            if (Arg >= 0 && Arg <= StaminaMax)
            {
                Stamina = Arg;
                Debug.Log("Player 'Stamina' set to " + Stamina);
                IO.PlayerStamina(Stamina, StaminaMax);
            }
            else
            {
                Debug.Log("Unable to set Player 'Stamina' to " + Arg);
                Debug.Log("Player 'Stamina' Range -> 0-" + StaminaMax);
            }
        }

        void ConsoleInputStaminaMax(int Arg)
        {
            if (Arg >= 0 && Arg <= 9999)
            {
                StaminaMax = Arg;
                Debug.Log("Player 'StaminaMax' set to " + StaminaMax);
                IO.PlayerStamina(Stamina, StaminaMax);
            }
            else
            {
                Debug.Log("Unable to set Player 'StaminaMax' to " + Arg);
                Debug.Log("Player 'StaminaMax' Range -> 0-9999");
            }
        }

        void ConsoleInputHP(int Arg)
        {
            if (Arg >= 0 && Arg <= 9999)
            {
                HP = Arg;
                Debug.Log("Player 'HP' set to " + HP);
                IO.PlayerHP(HP, MaxHP);
            }
            else
            {
                Debug.Log("Unable to set Player 'HP' to " + Arg);
                Debug.Log("Player 'HP' Range -> 0-9999");
            }
        }

        void ConsoleInputMaxHP(int Arg)
        {
            if (Arg >= 0 && Arg <= 9999)
            {
                MaxHP = Arg;
                Debug.Log("Player 'HPMax' set to " + MaxHP);
                IO.PlayerHP(HP, MaxHP);
            }
            else
            {
                Debug.Log("Unable to set Player 'HPMax' to " + Arg);
                Debug.Log("Player 'HPMax' Range -> 0-9999");
            }
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
