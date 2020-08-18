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
        }

        static void LoadAdventureType(XmlNode Node)
        {
            int Count = 0;
            int ChildCount = Convert.ToInt32(Node.Attributes[0].Value);
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

        #region Stores

        static void LoadStoreTypes(XmlNode Node)
        {
            int Count = 0;
            int ChildCount = Convert.ToInt32(Node.Attributes[0].Value);
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

        static void LoadStoreStock(XmlNode Stock, Stores Store,string ErMsg)
        {
            foreach(XmlNode StockType in Stock)
            {
                int Count = 0;
                int ChildCount = Convert.ToInt32(StockType.Attributes[0].Value);
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
                            WeapTemp[Count].Cost = Convert.ToInt32(Weapon.Attributes[0].Value);
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
                            ArmTemp[Count].Cost = Convert.ToInt32(Armour.Attributes[0].Value);
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
                            PotTemp[Count].Cost = Convert.ToInt32(Potion.Attributes[0].Value);
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

        #endregion

        #region Generation Tools

        public static string NameGenerator(string Arg)
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
                case "City":
                    return Name;
                case "":
                    break;
                default:
                    Debug.Log("Name Generation Error -> Arg: " + Arg);
                    return Name;
            }
            return Name;
        }

        #endregion
    }
}
