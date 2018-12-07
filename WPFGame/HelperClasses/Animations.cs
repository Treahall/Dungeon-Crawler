using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFGame.Data
{
    public static class Animations
    {
        public static readonly List<string> CharacterRun = new List<string>
        {
            "../../VisualAssets/Actors/Player_Sprites/Run/PlayerRun_0.png",
            "../../VisualAssets/Actors/Player_Sprites/Run/PlayerRun_1.png",
            "../../VisualAssets/Actors/Player_Sprites/Run/PlayerRun_2.png",
            "../../VisualAssets/Actors/Player_Sprites/Run/PlayerRun_3.png",
            "../../VisualAssets/Actors/Player_Sprites/Run/PlayerRun_4.png",
            "../../VisualAssets/Actors/Player_Sprites/Run/PlayerRun_5.png"
        };

        public static readonly List<string> CharacterIdle = new List<string>
        {
            "../../VisualAssets/Actors/Player_Sprites/Idle/PlayerIdle_0.png",
            "../../VisualAssets/Actors/Player_Sprites/Idle/PlayerIdle_1.png",
            "../../VisualAssets/Actors/Player_Sprites/Idle/PlayerIdle_2.png",
            "../../VisualAssets/Actors/Player_Sprites/Idle/PlayerIdle_3.png"
        };

        public static readonly List<string> CharacterJump = new List<string>
        {
            "../../VisualAssets/Actors/Player_Sprites/Jump/Jump_0.png",
            "../../VisualAssets/Actors/Player_Sprites/Jump/Jump_1.png",
            "../../VisualAssets/Actors/Player_Sprites/Jump/Jump_2.png",
            "../../VisualAssets/Actors/Player_Sprites/Jump/Jump_3.png"
        };

        public static readonly List<string> CharacterAtk = new List<string>
        {
            "../../VisualAssets/Actors/Player_Sprites/Attack/player_atk_0.png",
            "../../VisualAssets/Actors/Player_Sprites/Attack/player_atk_1.png",
            "../../VisualAssets/Actors/Player_Sprites/Attack/player_atk_2.png",
            "../../VisualAssets/Actors/Player_Sprites/Attack/player_atk_3.png",
            "../../VisualAssets/Actors/Player_Sprites/Attack/player_atk_4.png",
            "../../VisualAssets/Actors/Player_Sprites/Attack/player_atk_5.png"
        };

        public static readonly List<string> WereWRun = new List<string>
        {
            "../../VisualAssets/Actors/Enemies/Werewolf/Run/werewolf_run_0.png",
            "../../VisualAssets/Actors/Enemies/Werewolf/Run/werewolf_run_1.png",
            "../../VisualAssets/Actors/Enemies/Werewolf/Run/werewolf_run_2.png",
            "../../VisualAssets/Actors/Enemies/Werewolf/Run/werewolf_run_3.png",
            "../../VisualAssets/Actors/Enemies/Werewolf/Run/werewolf_run_4.png",
            "../../VisualAssets/Actors/Enemies/Werewolf/Run/werewolf_run_5.png"
        };

        public static readonly List<string> WereWIdle = new List<string>
        {
            "../../VisualAssets/Actors/Enemies/Werewolf/Idle/werewolf_idle_0.png",
            "../../VisualAssets/Actors/Enemies/Werewolf/Idle/werewolf_idle_1.png",
            "../../VisualAssets/Actors/Enemies/Werewolf/Idle/werewolf_idle_2.png",
            "../../VisualAssets/Actors/Enemies/Werewolf/Idle/werewolf_idle_3.png"
        };

        public static readonly List<string> WereWAtk = new List<string>
        {
            "../../VisualAssets/Actors/Enemies/Werewolf/Attack/werewolf_atk_0.png",
            "../../VisualAssets/Actors/Enemies/Werewolf/Attack/werewolf_atk_1.png",
            "../../VisualAssets/Actors/Enemies/Werewolf/Attack/werewolf_atk_2.png",
            "../../VisualAssets/Actors/Enemies/Werewolf/Attack/werewolf_atk_3.png",
            "../../VisualAssets/Actors/Enemies/Werewolf/Attack/werewolf_atk_4.png",
            "../../VisualAssets/Actors/Enemies/Werewolf/Attack/werewolf_atk_5.png",
            "../../VisualAssets/Actors/Enemies/Werewolf/Attack/werewolf_atk_6.png"

        };

        public static readonly List<string> HoundRun = new List<string>
        {
            "../../VisualAssets/Actors/Enemies/HellHound/Run/HellHoundRun_0.png",
            "../../VisualAssets/Actors/Enemies/HellHound/Run/HellHoundRun_1.png",
            "../../VisualAssets/Actors/Enemies/HellHound/Run/HellHoundRun_2.png",
            "../../VisualAssets/Actors/Enemies/HellHound/Run/HellHoundRun_3.png",
            "../../VisualAssets/Actors/Enemies/HellHound/Run/HellHoundRun_4.png"
        };

        public static readonly List<string> HoundIdle = new List<string>
        {
            "../../VisualAssets/Actors/Enemies/HellHound/Idle/HellHoundIdle_0.png",
            "../../VisualAssets/Actors/Enemies/HellHound/Idle/HellHoundIdle_1.png",
            "../../VisualAssets/Actors/Enemies/HellHound/Idle/HellHoundIdle_2.png",
            "../../VisualAssets/Actors/Enemies/HellHound/Idle/HellHoundIdle_3.png",
            "../../VisualAssets/Actors/Enemies/HellHound/Idle/HellHoundIdle_4.png",
            "../../VisualAssets/Actors/Enemies/HellHound/Idle/HellHoundIdle_5.png"
        };

        public static readonly List<string> HoundAtk = new List<string>
        {
            "../../VisualAssets/Actors/Enemies/HellHound/Attack/HellHoundAttack_0.png",
            "../../VisualAssets/Actors/Enemies/HellHound/Attack/HellHoundAttack_1.png",
            "../../VisualAssets/Actors/Enemies/HellHound/Attack/HellHoundAttack_2.png",
            "../../VisualAssets/Actors/Enemies/HellHound/Attack/HellHoundAttack_3.png",
            "../../VisualAssets/Actors/Enemies/HellHound/Attack/HellHoundAttack_4.png",
            "../../VisualAssets/Actors/Enemies/HellHound/Attack/HellHoundAttack_5.png"
        };

        public static readonly List<string> MandrakeAtk = new List<string>
        {
            "../../VisualAssets/Actors/Enemies/Mandrake/attack/mandrake-attack-00.png",
            "../../VisualAssets/Actors/Enemies/Mandrake/attack/mandrake-attack-01.png",
            "../../VisualAssets/Actors/Enemies/Mandrake/attack/mandrake-attack-02.png",
            "../../VisualAssets/Actors/Enemies/Mandrake/attack/mandrake-attack-03.png",
            "../../VisualAssets/Actors/Enemies/Mandrake/attack/mandrake-attack-04.png",
            "../../VisualAssets/Actors/Enemies/Mandrake/attack/mandrake-attack-05.png",
            "../../VisualAssets/Actors/Enemies/Mandrake/attack/mandrake-attack-06.png"
        };

        public static readonly List<string> MandrakeRun = new List<string>
        {
            "../../VisualAssets/Actors/Enemies/Mandrake/run/mandrake-run-00.png",
            "../../VisualAssets/Actors/Enemies/Mandrake/run/mandrake-run-01.png",
            "../../VisualAssets/Actors/Enemies/Mandrake/run/mandrake-run-02.png",
            "../../VisualAssets/Actors/Enemies/Mandrake/run/mandrake-run-03.png",
            "../../VisualAssets/Actors/Enemies/Mandrake/run/mandrake-run-04.png",
            "../../VisualAssets/Actors/Enemies/Mandrake/run/mandrake-run-05.png"
        };

        public static readonly List<string> MandrakeSpawn = new List<string>
        {
            "../../VisualAssets/Actors/Enemies/Mandrake/spawn/mandrake-show-00.png",
            "../../VisualAssets/Actors/Enemies/Mandrake/spawn/mandrake-show-01.png",
            "../../VisualAssets/Actors/Enemies/Mandrake/spawn/mandrake-show-02.png",
            "../../VisualAssets/Actors/Enemies/Mandrake/spawn/mandrake-show-03.png",
            "../../VisualAssets/Actors/Enemies/Mandrake/spawn/mandrake-show-04.png"
        };
    }
}
