using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stream_AFK_Text_Game
{
    class Campaign
    {
        Adventures Adventure;
        List<Cities> Cities = new List<Cities>();

        public Campaign()
        {
            Debug.WG("Generating World -> Cities...");
            Cities = ProGen.GenerateCities();
            Debug.WG("Generating World -> Adventure...");
            Adventure = ProGen.GenerateAdventure();
        }
    }

    class Cities
    {
        string Name;
        Tavern Tavern;
        List<Stores> Stores = new List<Stores>();

        public Cities()
        {
            Name = ProGen.NameGenerator("City", null);
            Stores = ProGen.GenerateStores();
            Tavern = new Tavern();
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

        public void SetStores(List<Stores> NewStores)
        {
            Stores = NewStores;
        }

        public void ClearStores()
        {
            Stores.Clear();
        }

        public void AddToStores(Stores Store)
        {
            Stores.Add(Store);
        }

        public void RemoveFromStores(int Arg)
        {
            if (Arg >= 0 && Arg < Stores.Count)
                Stores.RemoveAt(Arg);
        }

        #endregion//set, clear, add, remove
    }

    class Tavern
    {
        string Name;
        NPCS BarKeep;
        List<NPCS> Patrons = new List<NPCS>();

        public Tavern()
        {
            Name = ProGen.NameGenerator(null, null);
            BarKeep = new NPCS();
            Patrons = ProGen.GenerateNPCS(DiceRoller.RandomRange(2, 5));
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

        public void SetBarKeep(NPCS NewBarKeep)
        {
            BarKeep = NewBarKeep;
        }

        public NPCS GetBarKeep()
        {
            return BarKeep;
        }

        #endregion
    }

    class Stores
    {
        string Name, SType;
        NPCS ShopKeep;
        List<Weapon> WeaponStock = new List<Weapon>();
        List<Armour> ArmourStock = new List<Armour>();
        List<Potion> PotionStock = new List<Potion>();

        public Stores()
        {
            SType = ProGen.GenerateStoreType();
            ShopKeep = new NPCS();
            Name = ProGen.NameGenerator(SType, ShopKeep.GetName());
            foreach (Stores Store in ProGen.GetStoreTypes())
                if(Store.SType == SType)
                {
                    WeaponStock = Store.GetWeaponStock();
                    ArmourStock = Store.GetArmourStock();
                    PotionStock = Store.GetPotionStock();
                }
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

        public void SetWeaponStock(List<Weapon> NewWeaponStock)
        {
            WeaponStock = NewWeaponStock;
        }

        public void ClearWeaponStock()
        {
            WeaponStock.Clear();
        }

        public void AddToWeaponStock(Weapon Weapon)
        {
            WeaponStock.Add(Weapon);
        }

        public void RemoveFromWeaponStock(int Arg)
        {
            if (Arg >= 0 && Arg < WeaponStock.Count)
                WeaponStock.RemoveAt(Arg);
        }

        public List<Weapon> GetWeaponStock()
        {
            return WeaponStock;
        }

        public void SetArmourStock(List<Armour> NewArmourStock)
        {
            ArmourStock = NewArmourStock;
        }

        public void ClearArmourStock()
        {
            ArmourStock.Clear();
        }

        public void AddToArmourStock(Armour Armour)
        {
            ArmourStock.Add(Armour);
        }

        public void RemoveFromArmourStock(int Arg)
        {
            if (Arg >= 0 && Arg < ArmourStock.Count)
                ArmourStock.RemoveAt(Arg);
        }
        
        public List<Armour> GetArmourStock()
        {
            return ArmourStock;
        }

        public void SetPotionsStock(List<Potion> NewPotionStock)
        {
            PotionStock = NewPotionStock;
        }

        public void ClearPotionsStock()
        {
            PotionStock.Clear();
        }

        public void AddToPotionStock(Potion Potion)
        {
            PotionStock.Add(Potion);
        }

        public void RemoveFromPotionStock(int Arg)
        {
            if (Arg >= 0 && Arg < PotionStock.Count)
                PotionStock.RemoveAt(Arg);
        }

        public List<Potion> GetPotionStock()
        {
            return PotionStock;
        }

        public void SetNPC(NPCS NewNPC)
        {
            ShopKeep = NewNPC;
        }

        public NPCS GetNPC()
        {
            return ShopKeep;
        }

        #endregion
    }

    class NPCS
    {
        string Name;

        public NPCS()
        {
            Name = ProGen.NameGenerator(null, null);
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

    class Adventures
    {
        string AdventureType;

        public Adventures()
        {
            AdventureType = ProGen.GenerateAdventureType();
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
}
