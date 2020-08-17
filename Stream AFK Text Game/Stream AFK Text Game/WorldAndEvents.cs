using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stream_AFK_Text_Game
{
    class Campaign
    {
        string AdventureType;

        List<Cities> Cities = new List<Cities>();

        public Campaign()
        {
            Debug.WG("Generating World -> Cities...");
            Cities = ProGen.GenerateCities();
        }

        #region Get/Set Functions

        public void SetAdventureType(string NewAdventureType)
        {
            AdventureType = NewAdventureType;
        }

        public string GetAdventureType()
        {
            return AdventureType;
        }

        #endregion
    }

    class Cities
    {
        string Name;

        public Cities()
        {
            Name = ProGen.NameGenerator("City");
        }

        #region Get/Set Functions

        public void SetName(string NewName)
        {
            Name = NewName;
        }

        public string GetName()
        {
            return Name;
        }

        #endregion
    }

    class Stores
    {
        string Name, SType;
        List<Weapon> WeaponStock = new List<Weapon>();
        List<Armour> ArmourStock = new List<Armour>();
        List<Potions> PotionStock = new List<Potions>();

        public Stores()
        {
            Name = ProGen.NameGenerator("");
        }

        #region Get/Set Functions

        public void SetName(string NewName)
        {
            Name = NewName;
        }

        public string GetName()
        {
            return Name;
        }

        public void SetType(string NewSType)
        {
            SType = NewSType;
        }

        public string GetSType()
        {
            return SType;
        }

        #endregion
    }

    class Adventures
    {
        string AdventureType;

        #region Get/Set Functions

        public void SetAdventureType(string NewAdventureType)
        {
            AdventureType = NewAdventureType;
        }

        public string GetAdventureType()
        {
            return AdventureType;
        }

        #endregion
    }
}
