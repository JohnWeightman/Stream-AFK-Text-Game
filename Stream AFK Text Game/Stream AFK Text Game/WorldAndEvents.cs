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
