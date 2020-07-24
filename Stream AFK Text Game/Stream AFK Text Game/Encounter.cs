using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Stream_AFK_Text_Game
{
    static class Encounter
    {
        static List<EnemyNPC> EncounterNPCs = new List<EnemyNPC>();
        static List<EnemyNPC> FightOrder = new List<EnemyNPC>();
        static List<string> FightOptions = new List<string>() { "Heavy Attack (10s)", "Light Attack (7s)", "Drink Potion (3s)", "End Turn (0s)" };
        static List<int> FightOptionCosts = new List<int>() { 10, 7, 3, 0 };
        static List<int> Initiatives = new List<int>();
        public static int EncounterXP = 0;
        static Player Player;

        #region Fight Setup

        public static void SortFightOrder()
        {
            if(EncounterNPCs.Count > 0)
            {
                Player.SetInitiative(RollInitiative(Player.GetDexMod()));
                foreach (EnemyNPC Enemy in EncounterNPCs)
                {
                    Enemy.Initiative = RollInitiative(Enemy.DexMod);
                }
                int Ini = Initiatives.Max();
                while (Ini != 0)
                {
                    if (Player.GetInitiative() == Ini)
                    {
                        EnemyNPC PlayerIni = new EnemyNPC();
                        PlayerIni.Name = Player.GetName(); ;
                        FightOrder.Add(PlayerIni);
                    }
                    foreach (EnemyNPC Enemy in EncounterNPCs)
                    {
                        if (Enemy.Initiative == Ini)
                        {
                            FightOrder.Add(Enemy);
                        }
                    }
                    Ini--;
                }
            }
            else
            {
                EnemyNPC DefaultNPC = new EnemyNPC();
                DefaultNPC = GameObjects.NPCs[0];
                EncounterNPCs.Add(DefaultNPC);
                SortFightOrder();
            }
            IO.NPCs(EncounterNPCs);
        }

        static int RollInitiative(int DexMod)
        {
            int Initiative = DiceRoller.RollDice(12) + DexMod;
            Initiatives.Add(Initiative);
            return Initiative;
        }

        #endregion

        #region FightMechanics

        public static void StartEncounter(List<EnemyNPC> EncounterData, Player ChatPlayer)
        {
            Player = ChatPlayer;
            Debug.Stats.Encounters.SetFightNumber(Debug.Stats.Encounters.GetFightNumber() + 1);
            Debug.Log("Starting Encounter " + Debug.Stats.Encounters.GetFightNumber());
            foreach (EnemyNPC NPC in EncounterData)
                EncounterNPCs.Add(NPC);
            SortFightOrder();
            string Update = "Your under attack!\n";
            foreach (EnemyNPC Enemy in EncounterNPCs)
                Update += "\n" + Enemy.Name;
            IO.GameUpdate(Update);
            Thread.Sleep(Settings.GetPauseTime());
            EncounterLoop();
        }

        static void EncounterLoop()
        {
            bool Finished = false;
            sbyte CharTurn = 0;
            while (!Finished)
            {
                if (FightOrder[CharTurn].Name == Player.GetName())
                    PlayerTurn();
                else
                    NPCTurn(FightOrder[CharTurn]);
                CharTurn++;
                if (CharTurn == FightOrder.Count)
                    CharTurn = 0;
                Finished = CheckFightStatus();
            }
            if (Player.GetHP() <= 0)
            {
                FightOrder.Clear();
                EncounterNPCs.Clear();
                Player.SetDead(true);
            }
            else
                EncounterEnd();
        }

        static void EncounterEnd()
        {
            string Update = "You won the fight!";
            Events.NewEvent("EncounterWon", ES1: Player.GetName());
            IO.GameUpdate(Update);
            Thread.Sleep(Settings.GetPauseTime());
            Update = "You won the fight!\n\nXP Earned: " + EncounterXP;
            Player.AddXP(EncounterXP);
            EncounterXP = 0;
            Player.SetStamina(Player.GetStaminaMax());
            IO.GameUpdate(Update);
            IO.PlayerStamina(Player.GetStamina(), Player.GetStaminaMax());
            IO.PlayerXP(Player.GetXP());
            IO.PlayerLU(Player.GetLU());
            IO.NPCs(EncounterNPCs);
            Thread.Sleep(Settings.GetPauseTime());
            FightOrder.Clear();
        }

        static bool CheckFightStatus()
        {
            bool Finished = false;
            if (Player.GetHP() <= 0 || EncounterNPCs.Count == 0)
                Finished = true;
            return Finished;
        }

        #endregion

        #region Players Turn

        static void PlayerTurn()
        {
            bool TurnDone = false;
            while (!TurnDone)
            {
                IO.Options(FightOptions);
                int Input = MainClass.ChatInput(FightOptions.Count);
                int TargetEnemy = 0;
                switch (Input)
                {
                    case 1:
                        if(Player.GetStamina() >= FightOptionCosts[Input - 1])
                        {
                            Player.SetStamina(Player.GetStamina() - FightOptionCosts[Input - 1]);
                            TargetEnemy = WhichEnemy();
                            AttackEnemy(TargetEnemy, "Heavy");
                        }
                        else
                        {
                            string Update = "Not Enough Stamina!";
                            IO.GameUpdate(Update);
                        }
                        break;
                    case 2:
                        if(Player.GetStamina() >= FightOptionCosts[Input - 1])
                        {
                            Player.SetStamina(Player.GetStamina() - FightOptionCosts[Input - 1]);
                            TargetEnemy = WhichEnemy();
                            AttackEnemy(TargetEnemy, "Light");
                        }
                        else
                        {
                            string Update = "Not Enough Stamina!";
                            IO.GameUpdate(Update);
                        }
                        break;
                    case 3:
                        if(Player.GetStamina() >= FightOptionCosts[Input - 1])
                        {
                            Player.SetStamina(Player.GetStamina() - FightOptionCosts[Input - 1]);
                            HealthPotion();
                        }
                        else
                        {
                            string Update = "Not Enough Stamina!";
                            IO.GameUpdate(Update);
                        }
                        break;
                    case 4:
                        TurnDone = true;
                        break;
                    default:
                        break;
                }
                IO.PlayerStamina(Player.GetStamina(), Player.GetStaminaMax());
                if (!TurnDone)
                    TurnDone = CheckFightStatus();
            }
            Player.SetStamina(Player.GetStaminaMax());
            IO.PlayerStamina(Player.GetStamina(), Player.GetStaminaMax());
        }

        static int WhichEnemy()
        {
            List<string> EnemyList = new List<string>();
            foreach(EnemyNPC Enemy in EncounterNPCs)
            {
                EnemyList.Add(Enemy.Name);
            }
            IO.Options(EnemyList);
            int Input = MainClass.ChatInput(EnemyList.Count) - 1;
            return Input;
        }

        static void AttackEnemy(int TargetEnemy, string AttackType)
        {
            int Attack = DiceRoller.RollDice(12) + Player.GetAtkBonus();
            if (Attack >= EncounterNPCs[TargetEnemy].AC)
            {
                Events.NewEvent("AttackRoll", Attack - Player.GetStrMod() - (Player.GetLevel() / 3), Player.GetStrMod(), Player.GetLevel() / 3, Attack,
                    EncounterNPCs[TargetEnemy].AC, Player.GetName(), EncounterNPCs[TargetEnemy].Name, "HIT");Attack = DamageEnemy(AttackType, 
                    EncounterNPCs[TargetEnemy]);
                bool Dead = EncounterNPCs[TargetEnemy].TakeDamage(Attack);
                if (Dead)
                {
                    Events.NewEvent("NPCDeath", ES1: Player.GetName(), ES2: EncounterNPCs[TargetEnemy].Name);
                    string Update = "You strike down " + EncounterNPCs[TargetEnemy].Name + "!";
                    IO.GameUpdate(Update);
                    FightOrder.Remove(EncounterNPCs[TargetEnemy]);
                    EncounterNPCs.Remove(EncounterNPCs[TargetEnemy]);
                }
                else
                {
                    string Update = "You strike " + EncounterNPCs[TargetEnemy].Name + " for " + Attack + " Damage!";
                    IO.GameUpdate(Update);
                }
                IO.NPCs(EncounterNPCs);
            }
            else
            {
                Events.NewEvent("AttackRoll", Attack - Player.GetStrMod() - (Player.GetLevel() / 3), Player.GetStrMod(), Player.GetLevel() / 3, Attack,
                    EncounterNPCs[TargetEnemy].AC, Player.GetName(), EncounterNPCs[TargetEnemy].Name, "MISS");
                string Update = "Your attack missed!";
                IO.GameUpdate(Update);
            }
        }

        static int DamageEnemy(string AttackType, EnemyNPC NPC)
        {
            int Damage = DiceRoller.RollDice(Player.Weapon.Damage) + Player.GetStrMod();
            int Damage2 = Damage;
            if (AttackType == "Light")
            {
                Damage2 = (Damage / 3) * 2;
                Events.NewEvent("LightDamageRoll", EN1: Damage - Player.GetStrMod(), EN2: Player.GetStrMod(), EN3: Damage2, ES1: Player.GetName(),
                    ES2: NPC.Name);
            }
            else
            {
                Events.NewEvent("HeavyDamageRoll", EN1: Damage - Player.GetStrMod(), EN2: Player.GetStrMod(), EN3: Damage2, ES1: Player.GetName(),
                    ES2: NPC.Name);
            }

            return Damage2;
        }

        static void HealthPotion()
        {
            if(Player.Inventory.Potions.Count > 0)
            {
                List<string> Potions = new List<string>();
                foreach (Potions Pot in Player.Inventory.Potions)
                    Potions.Add(Pot.Name);
                IO.Options(Potions);
                int Input = MainClass.ChatInput(4);
                if(Input == -1)
                {
                    Player.SetStamina(Player.GetStamina() + FightOptionCosts[2]);
                    return;
                }
                else
                {
                    int Regen = Player.Inventory.Potions[Input].HealthRegen();
                    if (Player.GetHP() + Regen > Player.GetMaxHP())
                        Regen = Player.GetMaxHP() - Player.GetHP();
                    Player.SetHP(Player.GetHP() + Regen);
                    string Update = "You recovered " + Regen + "HP!";
                    IO.PlayerStamina(Player.GetStamina(), Player.GetStaminaMax());
                    IO.PlayerHP(Player.GetHP(), Player.GetMaxHP());
                    IO.GameUpdate(Update);
                    IO.PlayerInventory(Player.Inventory);
                }
            }
            else
            {
                Player.SetStamina(Player.GetStamina() + FightOptionCosts[2]);
                string Update = "You don't have any Health Potions!";
                IO.GameUpdate(Update);
            }
        }

        #endregion

        #region NPCs Turn

        static void NPCTurn(EnemyNPC NPC)
        {
            bool TurnDone = false;
            while (!TurnDone)
            {
                byte Decision = NPC.CombatDecision(FightOptionCosts);
                switch (Decision)
                {
                    case 0:
                        NPC.Stamina -= FightOptionCosts[0];
                        AttackPlayer(NPC, Decision);
                        break;
                    case 1:
                        NPC.Stamina -= FightOptionCosts[1];
                        AttackPlayer(NPC, Decision);
                        break;
                    case 2:
                        TurnDone = !TurnDone;
                        break;
                    default:
                        break;
                }
            }
            NPC.Stamina = NPC.StaminaMax;
        }

        static void AttackPlayer(EnemyNPC NPC, byte AttackType)
        {
            int Attack = DiceRoller.RollDice(12) + NPC.StrMod + NPC.DifBonus;
            if(Attack >= Player.GetAC())
            {
                Events.NewEvent("AttackRoll", Attack - (NPC.StrMod + NPC.DifBonus), NPC.StrMod, NPC.DifBonus, Attack, Player.GetAC(), NPC.Name, Player.GetName(), 
                    "HIT");
                DamagePlayer(NPC, AttackType);
            }
            else
            {
                Events.NewEvent("AttackRoll", Attack - (NPC.StrMod + NPC.DifBonus), NPC.StrMod, NPC.DifBonus, Attack, Player.GetAC(), NPC.Name, Player.GetName(), 
                    "MISS");
                string Update = NPC.Name + " attacked you and missed!";
                IO.GameUpdate(Update);
            }
            //List<string> Options = new List<string>() { "Continue" };
            //IO.Options(Options);
            //int Input = Player.PlayerInputs(Options.Count);
            Thread.Sleep(5000);
        }

        static void DamagePlayer(EnemyNPC NPC, byte AttackType)
        {
            int Damage = DiceRoller.RollDice(NPC.Weapon.Damage) + NPC.StrMod;
            int Damage2 = Damage;
            if (AttackType == 1)
            {
                Damage2 = (Damage / 3) * 2;
                Events.NewEvent("LightDamageRoll", EN1: Damage - NPC.StrMod, EN2: NPC.StrMod, EN3: Damage2, ES1: NPC.Name, ES2: Player.GetName());
            }
            else
            {
                Events.NewEvent("HeavyDamageRoll", EN1: Damage - NPC.StrMod, EN2: NPC.StrMod, EN3: Damage, ES1: NPC.Name, ES2: Player.GetName());
            }
            Player.SetHP(Player.GetHP() - Damage2);
            string Update = NPC.Name + " attacked you for " + Damage2 + " Damage!";
            IO.PlayerHP(Player.GetHP(), Player.GetMaxHP());
            IO.GameUpdate(Update);
        }

        #endregion
    }
}
