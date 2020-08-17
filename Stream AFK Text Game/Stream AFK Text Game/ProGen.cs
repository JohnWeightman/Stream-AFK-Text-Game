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

        public static Campaign GenerateNewAdventure()
        {
            Debug.WG("Loading Generation Objects...");
            LoadGenerationData();
            Debug.WG("Loading Game Objects...");
            GameObjects.LoadGameObjects();
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

        static void LoadStoreTypes(XmlNode Node)
        {
            int Count = 0;
            int ChildCount = Convert.ToInt32(Node.Attributes[0].Value);
            Stores[] StTypes = new Stores[ChildCount];
            foreach(XmlNode Child in Node.ChildNodes)
            {
                StTypes[Count] = new Stores();
                StTypes[Count].SetType(Child.Name);
                Count++;
            }
        }

        #endregion

        #region Generate Adventure

        public static List<Cities> GenerateCities()
        {
            List<Cities> CityList = new List<Cities>();
            int CityCount = DiceRoller.RandomRange(3, 5);
            Cities[] Temp = new Cities[CityCount];
            for (int Count = 0; Count < CityCount; Count++)
            {
                Temp[Count] = new Cities();
            }
            foreach (Cities City in Temp)
                CityList.Add(City);
            return CityList;
        }

        public static List<Stores> GenerateShops()
        {
            List<Stores> StoreList = new List<Stores>();
            return StoreList;
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
