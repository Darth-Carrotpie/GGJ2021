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
            public static string StartChannelingPortal() { return "Player_CStartChannelingPortal"; }
            public static string StopChannelingPortal() { return "Player_CStartChannelingPortal"; }
            public static string OpenChest() { return "Player_OpenChest"; }
            public static List<string> Get() { return new List<string> { Move(), StartChannelingPortal(), StopChannelingPortal(), OpenChest() }; }
        }
        public static string Tap() { return "Input_Tap"; }
        public static string StartGame() { return "Input_StartGame"; }

        public static List<string> Get() {
            return new List<string> {
                    Tap(),
                    StartGame(),
                }
                .Concat(Menus.Get())
                .Concat(Player.Get())
                .ToList();
        }
    }
    public class System {
        public class Economy {
            public static string EatGrass() { return "Economy_EatGrass"; }
            public static string GrassChanged() { return "Economy_GrassChanged"; } // should be under Playfield
            public static string GoldChanged() { return "Economy_GoldChanged"; }
            public static string ComboChanged() { return "Economy_ComboChanged"; }
            public static List<string> Get() { return new List<string> { GrassChanged(), GoldChanged(), EatGrass(), ComboChanged() }; }
        }
        public class Environment {
            public static string Initialized() { return "System_Environment_Initialized"; }
            public static string EndMatch() { return "System_Environment_EndMatch"; }
            public static string DestroyArena() { return "System_Environment_DestroyArena"; }
            public static string ArenaDestroyed() { return "System_Environment_ArenaDestroyed"; }
            public static string ArenaAnimating() { return "System_Environment_ArenaAnimating"; }
            public static string PlayfieldAnimated() { return "System_Environment_PlayfieldAnimated"; }
            public static string ArenaAnimated() { return "System_Environment_ArenaAnimated"; }
            public static string ScrollScoresOut() { return "System_Environment_ScrollScoresOut"; }
            public static string CleanScene() { return "System_Environment_CleanScene"; }
            public static List<string> Get() { return new List<string> { Initialized(), EndMatch(), DestroyArena(), ArenaDestroyed(), ArenaAnimating(), PlayfieldAnimated(), ArenaAnimated(), CleanScene() }; }
        }
        public class Player {
            public static string ProfileCreated() { return "System_ProfileCreated"; }
            public static string ProfileUpdate() { return "System_ProfileUpdate"; }
            public static List<string> Get() { return new List<string> { ProfileCreated(), ProfileUpdate() }; }
        }
        //public static string NextScene() { return "NextScene"; }
        public static string SceneLoaded() { return "SceneLoaded"; }
        public static List<string> Get() {
            return new List<string> {
                //MapLayoutChanged(),
                SceneLoaded()
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