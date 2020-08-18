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
        List<Stores> Stores = new List<Stores>();

        public Cities()
        {
            Name = ProGen.NameGenerator("City");
            Stores = ProGen.GenerateStores();
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

    class Stores
    {
        string Name, SType;
        List<Weapon> WeaponStock = new List<Weapon>();
        List<Armour> ArmourStock = new List<Armour>();
        List<Potion> PotionStock = new List<Potion>();

        public Stores()
        {
            Name = ProGen.NameGenerator("");
            SType = ProGen.GenerateStoreType();
            foreach(Stores Store in ProGen.GetStoreTypes())
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
