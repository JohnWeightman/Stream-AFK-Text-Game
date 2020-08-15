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

        public static Campaign GenerateNewAdventure()
        {
            Campaign Campaign = new Campaign();
            Debug.WG("Loading Generation Objects...");
            LoadGenerationData();
            Debug.WG("Loading Game Objects...");
            GameObjects.LoadGameObjects();
            Debug.WG("Generating World...");
            Campaign = GenerateCampaignWorld(Campaign);
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
                    default:
                        Debug.Log("XML ERROR -> 'GenerationObjects.xml, Node: " + Node.Name);
                        break;
                }
            }
        }

        static void ClearGenerationData()
        {
            AdventureTypes.Clear();
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

        #endregion

        #region Generate Adventure

        static Campaign GenerateCampaignWorld(Campaign Campaign)
        {
            Debug.WG("Generating World -> Cities...");
            Campaign = GenerateCities(Campaign);
            return Campaign;
        }

        static Campaign GenerateCities(Campaign Campaign)
        {
            int CityCount = DiceRoller.RandomRange(3, 5);
            return Campaign;
        }

        #endregion
    }
}
