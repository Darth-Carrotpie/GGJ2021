﻿using System.Collections.Generic;
using System.Linq;

public class EventName {
    public class UI {
        public static string ExampleEvent() { return "UI_ExampleEvent"; }
        public static List<string> Get() { return new List<string> { ExampleEvent() }; }
    }
    public class Editor {
        public static string None() { return null; }
        public static List<string> Get() { return new List<string> { None() }; }
    }
    public class Input {
        public class Menus {
            public static string None() { return null; }
            public static List<string> Get() { return new List<string> { None() }; }
        }
        public class Player {
            public static string Move() { return "Player_Move"; }
            public static string MovementStopped() { return "Player_MovementStopped"; }
            public static string ThrowLoot() { return "Player_ThrowLoot"; }
            public static string StartChannelingPortal() { return "Player_StartChannelingPortal"; }
            public static string StopChannelingPortal() { return "Player_StopChannelingPortal"; }
            public static string OpenChest() { return "Player_OpenChest"; }
            public static List<string> Get() { return new List<string> { Move(), ThrowLoot(), MovementStopped(), StartChannelingPortal(), StopChannelingPortal(), OpenChest() }; }
        }
        public static string Tap() { return "Input_Tap"; }
        public static string StartGame() { return "Input_StartGame"; }

        public static List<string> Get() {
            return new List<string> {
                    Tap(),
                    StartGame()
                }
                .Concat(Menus.Get())
                .Concat(Player.Get())
                .ToList();
        }
    }
    public class System {
        public class Economy {
            public static string ChestWasOpened() { return "Economy_ChestWasOpened"; }
            public static string AmountLootedChanged() { return "Economy_AmountLootedChanged"; }
            public static string PortalProgress() { return "Economy_PortalProgress"; } // should be under Playfield
            public static string GoldChanged() { return "Economy_GoldChanged"; }
            public static List<string> Get() { return new List<string> { PortalProgress(), GoldChanged(), ChestWasOpened(), AmountLootedChanged() }; }
        }
        public class Environment {
            public static string Initialized() { return "System_Environment_Initialized"; }
            public static string EndMatch() { return "System_Environment_EndMatch"; }
            public static string CleanScene() { return "System_Environment_CleanScene"; }
            public static string Damage() { return "System_Environment_Damage"; }
            public static string CreateLoot() { return "System_Environment_CreateLoot"; }
            public static string CreateMob() { return "System_Environment_CreateMob"; }
            public static string PickUpLoot() { return "System_Environment_PickUpLoot"; }
            public static string MobKilled() { return "System_Environment_MobKilled"; }
            public static string GoblinDied() { return "System_Environment_GoblinDied"; }
            public static List<string> Get() { return new List<string> { Initialized(), EndMatch(), CleanScene(), Damage(), CreateLoot(), CreateMob(), PickUpLoot(), MobKilled(), GoblinDied() }; }
        }
        public class Player {
            public static string ProfileCreated() { return "System_ProfileCreated"; }
            public static string ProfileUpdate() { return "System_ProfileUpdate"; }
            public static List<string> Get() { return new List<string> { ProfileCreated(), ProfileUpdate() }; }
        }
        //public static string NextScene() { return "NextScene"; }
        public static string Victory() { return "Victory"; }
        public static string SceneLoaded() { return "SceneLoaded"; }
        public static List<string> Get() {
            return new List<string> {
                //MapLayoutChanged(),
                SceneLoaded(),
                Victory()
            }.Concat(Economy.Get()).Concat(Environment.Get()).ToList();
        }
    }

    public static List<string> Get() {
        return new List<string> {}.Concat(UI.Get())
            .Concat(Editor.Get())
            .Concat(Input.Get())
            .Concat(System.Get())
            .Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
    }
}