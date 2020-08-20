using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Stream_AFK_Text_Game
{
    static class ProGen
    {
        static Names Names;
        static List<Adventures> AdventureTypes = new List<Adventures>();
        static List<Stores> StoreTypes = new List<Stores>();

        #region Get/Set Functions

        public static List<Stores> GetStoreTypes()
        {
            return StoreTypes;
        }

        #endregion

        public static Campaign GenerateNewAdventure()
        {
            Names = new Names();
            Debug.WG("Loading Game Objects...");
            GameObjects.LoadGameObjects();
            Debug.WG("Loading Generation Objects...");
            LoadGenerationData();
            Debug.WG("Generating World...");
            Campaign Campaign = new Campaign();
            return Campaign;
        }

        #region Load Generation Data

        static void LoadGenerationData()
        {
            ClearGenerationData();
            XmlDocument Doc = new XmlDocument();
            Doc.Load("GenerationObjects.xml");
            foreach (XmlNode Node in Doc.DocumentElement)
            {
                switch (Node.Name)
                {
                    case "AdventureType":
                        LoadAdventureType(Node);
                        break;
                    case "StoreTypes":
                        LoadStoreTypes(Node);
                        break;
                    case "NameTypes":
                        LoadNameTypes(Node);
                        break;
                    default:
                        Debug.Log("XML ERROR -> 'GenerationObjects.xml, Node: " + Node.Name);
                        break;
                }
            }
        }

        static void ClearGenerationData()
        {
            AdventureTypes.Clear();
            StoreTypes.Clear();
            Names.ClearNames();
        }

        #region Adventures

        static void LoadAdventureType(XmlNode Node)
        {
            int Count = 0;
            int ChildCount = Node.ChildNodes.Count;
            Adventures[] AdTypes = new Adventures[ChildCount];
            foreach (XmlNode Child in Node.ChildNodes)
            {
                AdTypes[Count] = new Adventures();
                AdTypes[Count].SetAdventureType(Child.Name);
                Count++;
            }
            foreach (Adventures AdType in AdTypes)
                AdventureTypes.Add(AdType);
        }

        #endregion

        #region Stores

        static void LoadStoreTypes(XmlNode Node)
        {
            int Count = 0;
            int ChildCount = Node.ChildNodes.Count;
            Stores[] StTypes = new Stores[ChildCount];
            foreach(XmlNode StoreType in Node.ChildNodes)
            {
                StTypes[Count] = new Stores();
                StTypes[Count].SetType(StoreType.Name);
                foreach(XmlNode StoreProp in StoreType)
                {
                    switch (StoreProp.Name)
                    {
                        case "Stock":
                            LoadStoreStock(StoreProp, StTypes[Count], "XML ERROR -> 'GenerationObjects.xml, Node: " + Node.Name + " - " + StoreType.Name + " - " + StoreProp.Name);
                            break;
                        default:
                            Debug.Log("XML ERROR -> 'GenerationObjects.xml, Node: " + Node.Name + " - " + StoreType.Name + " - " + StoreProp.Name);
                            break;
                    }
                }
                Count++;
            }
            foreach (Stores StType in StTypes)
                StoreTypes.Add(StType);
        }

        static void LoadStoreStock(XmlNode Stock, Stores Store, string ErMsg)
        {
            foreach(XmlNode StockType in Stock)
            {
                int Count = 0;
                int ChildCount = StockType.ChildNodes.Count;
                switch (StockType.Name)
                {
                    case "Weapons":
                        Weapon[] WeapTemp = new Weapon[ChildCount];
                        foreach(XmlNode Weapon in StockType)
                        {
                            WeapTemp[Count] = new Weapon();
                            foreach(Weapon GOWeapon in GameObjects.Weapons)
                                if(GOWeapon.Name == Weapon.Name)
                                {
                                    WeapTemp[Count] = GOWeapon;
                                    break;
                                }
                            WeapTemp[Count].Cost = Weapon.ChildNodes.Count;
                            Count++;
                        }
                        foreach (Weapon Weapon in WeapTemp)
                            Store.AddToWeaponStock(Weapon);
                        break;
                    case "Armour":
                        Armour[] ArmTemp = new Armour[ChildCount];
                        foreach(XmlNode Armour in StockType)
                        {
                            ArmTemp[Count] = new Armour();
                            foreach(Armour GOArmour in GameObjects.Armour)
                                if(GOArmour.Name == Armour.Name)
                                {
                                    ArmTemp[Count] = GOArmour;
                                    break;
                                }
                            ArmTemp[Count].Cost = Armour.ChildNodes.Count;
                            Count++;
                        }
                        foreach (Armour Armour in ArmTemp)
                            Store.AddToArmourStock(Armour);
                        break;
                    case "Potions":
                        Potion[] PotTemp = new Potion[ChildCount];
                        foreach(XmlNode Potion in StockType)
                        {
                            PotTemp[Count] = new Potion();
                            foreach(Potion GOPotions in GameObjects.Potions)
                                if(GOPotions.Name == Potion.Name)
                                {
                                    PotTemp[Count] = GOPotions;
                                }
                            PotTemp[Count].Cost = Potion.ChildNodes.Count;
                            Count++;
                        }
                        foreach (Potion Potion in PotTemp)
                            Store.AddToPotionStock(Potion);
                        break;
                    default:
                        Debug.Log(ErMsg + " - " + StockType.Name);
                        break;
                }
            }
        }

        #endregion

        #region Names

        static void LoadNameTypes(XmlNode Node)
        {
            foreach(XmlNode NameType in Node)
            {
                switch (NameType.Name)
                {
                    case "Stores":
                        LoadStoreNames(NameType, "XML ERROR -> 'GenerationObjects.xml, Node: " + Node.Name + " - " + NameType.Name);
                        break;
                    default:
                        Debug.Log("XML ERROR -> 'GenerationObjects.xml, Node: " + Node.Name + " - " + NameType.Name);
                        break;
                }
            }
        }

        static void LoadStoreNames(XmlNode Stores, string ErMsg)
        {
            foreach(XmlNode Store in Stores)
            {
                switch (Store.Name)
                {
                    case "BlackSmith":
                        for (int x = 0; x < Store.Attributes.Count; x++)
                            Names.AddToBlackSmith(Store.Attributes[x].Value);
                        break;
                    case "Armourer":
                        for (int x = 0; x < Store.Attributes.Count; x++)
                            Names.AddToArmourer(Store.Attributes[x].Value);
                        break;
                    case "PotionBrewer":
                        for (int x = 0; x < Store.Attributes.Count; x++)
                            Names.AddToPotionBrewer(Store.Attributes[x].Value);
                        break;
                    default:
                        Debug.Log(ErMsg + " - " + Store.Name);
                        break;
                }
            }
        }

        #endregion

        #endregion

        #region Generate Adventure

        public static List<Cities> GenerateCities()
        {
            List<Cities> CityList = new List<Cities>();
            int CityCount = DiceRoller.RandomRange(3, 5);
            Cities[] Temp = new Cities[CityCount];
            for (int Count = 0; Count < CityCount; Count++)
                Temp[Count] = new Cities();
            foreach (Cities City in Temp)
                CityList.Add(City);
            return CityList;
        }

        public static List<Stores> GenerateStores()
        {
            List<Stores> StoreList = new List<Stores>();
            int StoreCount = DiceRoller.RollDice(3);
            Stores[] Temp = new Stores[StoreCount];
            for (int Count = 0; Count < StoreCount; Count++)
                Temp[Count] = new Stores();
            foreach (Stores Store in Temp)
            {
                if (StoreList.Count > 0)
                {
                    bool Found = false;
                    foreach(Stores Store2 in StoreList)
                        if(Store2.GetSType() == Store.GetSType())
                        {
                            Found = true;
                            break;
                        }
                    if (!Found)
                        StoreList.Add(Store);
                }
                else
                    StoreList.Add(Store);
            }
            return StoreList;
        }

        public static string GenerateStoreType()
        {
            string SType;
            int Ran = DiceRoller.RollDice(StoreTypes.Count);
            switch (Ran)
            {
                case 1:
                    SType = "BlackSmith";
                    break;
                case 2:
                    SType = "Armourer";
                    break;
                case 3:
                    SType = "PotionBrewer";
                    break;
                default:
                    SType = "BlackSmith";
                    break;
            }
            return SType;
        }

        public static List<NPCS> GenerateNPCS(int NPCNumber)
        {
            List<NPCS> NPCS = new List<NPCS>();
            NPCS[] Temp = new NPCS[NPCNumber];
            for(int x = 0; x < NPCNumber; x++)
            {
                Temp[x] = new NPCS();
                NPCS.Add(Temp[x]);
            }
            return NPCS;
        }

        #endregion

        #region Generation Tools

        public static string NameGenerator(string Arg, string NPCName)
        {
            string Name = "";
            string[] consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "l", "n", "p", "q", "r", "s", "sh", "zh", "t", "v", "w", "x" };
            string[] vowels = { "a", "e", "i", "o", "u", "ae", "y" };
            Name += consonants[DiceRoller.RollDice(consonants.Length - 1)].ToUpper();
            Name += vowels[DiceRoller.RollDice(vowels.Length - 1)];
            int Length = DiceRoller.RollDice(6) + 2;
            int LetterCount = 2;
            while (LetterCount < Length)
            {
                Name += consonants[DiceRoller.RollDice(consonants.Length - 1)];
                LetterCount++;
                Name += vowels[DiceRoller.RollDice(vowels.Length - 1)];
                LetterCount++;
            }
            switch (Arg)
            {
                case null:
                    return Name;
                case "City":
                    return "City of " + Name;
                case "BlackSmith":
                    Name = EditName(Name, Names.GetFromBlackSmith(DiceRoller.RandomRange(0, Names.GetCountBlackSmith() - 1)));
                    break;
                case "Armourer":
                    Name = EditName(Name, Names.GetFromArmourer(DiceRoller.RandomRange(0, Names.GetCountArmourer() - 1)));
                    break;
                case "PotionBrewer":
                    Name = EditName(Name, Names.GetFromPotionBrewer(DiceRoller.RandomRange(0, Names.GetCountPotionBrewer() - 1)));
                    break;
                default:
                    Debug.Log("Name Generation Error -> Arg: " + Arg);
                    break;
            }
            return Name;
        }

        static string EditName(string Name, string Extra)
        {
            if(Extra.Contains("*****"))
            {
                Extra = Extra.Replace("*****", "'s");
                Name = Name + Extra;
            }
            else
            {
                Name = Extra;
            }
            return Name;
        }

        #endregion
    }

    class Names
    {
        List<string> BlackSmith = new List<string>();
        List<string> Armourer = new List<string>();
        List<string> PotionBrewer = new List<string>();

        #region Get/Set Functions

        public void AddToBlackSmith(string Name)
        {
            BlackSmith.Add(Name);
        }

        public string GetFromBlackSmith(int Arg)
        {
            if (Arg >= 0 && Arg < BlackSmith.Count)
                return BlackSmith[Arg];
            else
            {
                Debug.Log("Blacksmith Name Generation Error -> Arg: " + Arg);
                return "Brammer's Hammers";
            }
        }

        public int GetCountBlackSmith()
        {
            return BlackSmith.Count;
        }

        public void AddToArmourer(string Name)
        {
            Armourer.Add(Name);
        }

        public string GetFromArmourer(int Arg)
        {
            if (Arg >= 0 && Arg < Armourer.Count)
                return Armourer[Arg];
            else
            {
                Debug.Log("Armourer Name Generation Error -> Arg: " + Arg);
                return "Kilder's Armoury";
            }
        }

        public int GetCountArmourer()
        {
            return Armourer.Count;
        }

        public void AddToPotionBrewer(string Name)
        {
            PotionBrewer.Add(Name);
        }

        public string GetFromPotionBrewer(int Arg)
        {
            if (Arg >= 0 && Arg < PotionBrewer.Count)
                return PotionBrewer[Arg];
            else
            {
                Debug.Log("Armourer Name Generation Error -> Arg: " + Arg);
                return "Ruby's Potions";
            }
        }

        public int GetCountPotionBrewer()
        {
            return PotionBrewer.Count;
        }

        public void ClearNames()
        {
            BlackSmith.Clear();
            Armourer.Clear();
            PotionBrewer.Clear();
        }

        #endregion
    }
}
