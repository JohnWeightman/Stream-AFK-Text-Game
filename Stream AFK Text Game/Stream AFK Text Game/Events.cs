using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stream_AFK_Text_Game
{
    static class Events
    {
        public static List<string> EventsList = new List<string>();
        static EventObj EventObj = new EventObj();

        public static void NewEvent(string EventType, int EN1 = 0, int EN2 = 0, int EN3 = 0, int EN4 = 0, int EN5 = 0, string ES1 = "", string ES2 = "", string ES3 = "")
        {
            EventObj.UpdateEventVariables(EN1, EN2, EN3, EN4, EN5, ES1, ES2, ES3);
            string Event = "";
            switch (EventType)
            {
                case "AttackRoll":
                    Event = AttackRollEvent();
                    break;
                case "HeavyDamageRoll":
                    Event = HeavyDamageRollEvent();
                    break;
                case "LightDamageRoll":
                    Event = LightDamageRollEvent();
                    break;
                case "NPCDeath":
                    Event = NPCDeathEvent();
                    break;
                case "EncounterWon":
                    Event = EncounterWonEvent();
                    break;
                case "BoughtWeapon":
                    Event = BoughtWeaponEvent();
                    break;
                case "BoughtArmour":
                    Event = BoughtArmourEvent();
                    break;
                default:
                    return;
            }
            AddEventToList(Event);
            IO.Events(EventsList);
        }

        static void AddEventToList(string Event)
        {
            EventsList.Add(Event);
            if (EventsList.Count == 10)
                EventsList.RemoveAt(0);
        }

        static string AttackRollEvent()
        {
            string Event = "ATKROLL:" + EventObj.ES1 + "->" + EventObj.ES2 + "[" + EventObj.EN1 + "+" + EventObj.EN2 + "+" + EventObj.EN3 + "=" + 
                EventObj.EN4 + "],AC: " + EventObj.EN5 + ",Strike: " + EventObj.ES3;
            return Event;
        }

        static string HeavyDamageRollEvent()
        {
            string Event = "DMGROLL:" + EventObj.ES1 + "->" + EventObj.ES2 + "[" + EventObj.EN1 + "+" + EventObj.EN2 + "=" + EventObj.EN3 + "],DMG:" + 
                EventObj.EN3;
            return Event;
        }

        static string LightDamageRollEvent()
        {
            string Event = "DMGROLL:" + EventObj.ES1 + "->" + EventObj.ES2 + "[(" + EventObj.EN1 + "+" + EventObj.EN2 + ")*0.666=" + EventObj.EN3 + "],DMG:" +
                EventObj.EN3;
            return Event;
        }

        static string NPCDeathEvent()
        {
            string Event = EventObj.ES1 + " KILLED " + EventObj.ES2;
            return Event;
        }

        static string EncounterWonEvent()
        {
            string Event = EventObj.ES1 + " WON THE FIGHT";
            return Event;
        }

        static string BoughtWeaponEvent()
        {
            string Event = "NEW WEP: " + EventObj.ES1 + ", MAX DMG INC: " + EventObj.EN1;
            return Event;
        }

        static string BoughtArmourEvent()
        {
            string Event = "NEW ARM: " + EventObj.ES1 + ", AC INC: " + EventObj.EN1;
            return Event;
        }
    }

    class EventObj
    {
        public int EN1 = 0;
        public int EN2 = 0;
        public int EN3 = 0;
        public int EN4 = 0;
        public int EN5 = 0;
        public string ES1 = "";
        public string ES2 = "";
        public string ES3 = "";

        public void UpdateEventVariables(int N1, int N2, int N3, int N4, int N5, string S1, string S2, string S3)
        {
            EN1 = N1;
            EN2 = N2;
            EN3 = N3;
            EN4 = N4;
            EN5 = N5;
            ES1 = S1;
            ES2 = S2;
            ES3 = S3;
        }
    }
}