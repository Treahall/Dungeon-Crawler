using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFGame.Data
{
    public class Animations
    {
        public Animations() { }
        public readonly List<string> CharacterRun = new List<string> { "../../VisualAssets/Actors/Player_Sprites/Run/PlayerRun_0.png",
                                                              "../../VisualAssets/Actors/Player_Sprites/Run/PlayerRun_1.png",
                                                              "../../VisualAssets/Actors/Player_Sprites/Run/PlayerRun_2.png",
                                                              "../../VisualAssets/Actors/Player_Sprites/Run/PlayerRun_3.png",
                                                              "../../VisualAssets/Actors/Player_Sprites/Run/PlayerRun_4.png",
                                                              "../../VisualAssets/Actors/Player_Sprites/Run/PlayerRun_5.png" };

        public readonly List<string> CharacterIdol = new List<string> { "../../VisualAssets/Actors/Player_Sprites/Idle/PlayerIdle_0.png",
                                                               "../../VisualAssets/Actors/Player_Sprites/Idle/PlayerIdle_1.png",
                                                               "../../VisualAssets/Actors/Player_Sprites/Idle/PlayerIdle_2.png",
                                                               "../../VisualAssets/Actors/Player_Sprites/Idle/PlayerIdle_3.png" };

        public readonly List<string> CharacterJump = new List<string> { "../../VisualAssets/Actors/Player_Sprites/Jump/Jump_0.png",
                                                               "../../VisualAssets/Actors/Player_Sprites/Jump/Jump_1.png",
                                                               "../../VisualAssets/Actors/Player_Sprites/Jump/Jump_2.png",
                                                               "../../VisualAssets/Actors/Player_Sprites/Jump/Jump_3.png" };

        public readonly List<string> WereWRun = new List<string> {"../../VisualAssets/Actors/Enemies/Werewolf/Run/werewolf_run_0.png",
                                                         "../../VisualAssets/Actors/Enemies/Werewolf/Run/werewolf_run_1.png",
                                                         "../../VisualAssets/Actors/Enemies/Werewolf/Run/werewolf_run_2.png",
                                                         "../../VisualAssets/Actors/Enemies/Werewolf/Run/werewolf_run_3.png",
                                                         "../../VisualAssets/Actors/Enemies/Werewolf/Run/werewolf_run_4.png",
                                                         "../../VisualAssets/Actors/Enemies/Werewolf/Run/werewolf_run_5.png" };

        //List<string> CharacterLeft = new List<string> {"", "", "", "", "", "" };
        //List<string> CharacterLeft = new List<string> {"", "", "", "", "", "" };
        //List<string> CharacterLeft = new List<string> {"", "", "", "", "", "" };
        //List<string> CharacterLeft = new List<string> {"", "", "", "", "", "" };
    }
}
