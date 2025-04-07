using System.Collections.Generic;
using SCI.Annotators.Original;

namespace SCI.Annotators
{
    class RealmAnnotator : GameAnnotator
    {
        public override void Run()
        {
            RunEarly();
            GlobalRenamer.Run(Game, globals);
            RunLate();
        }

        protected override Dictionary<int, Original.Script[]> GameHeaders { get { return RealmSymbols.Headers; } }

        static IReadOnlyDictionary<int, string> globals = new Dictionary<int, string>
        {
            { 120, "gPlayerSlotNum" },
            { 121, "gMyTextFore" },
            { 122, "gMyTextBack" },
            { 156, "gChatDisplay" },
            { 158, "gNBody" },
            { 159, "gNFace" },
            { 160, "gNAttrib" },
            { 161, "gNPants" },
            { 162, "gNShirt" },
            { 163, "gNShoes" },
            { 179, "gAutoSell" },
            { 180, "gPlayerGive" },
            { 181, "gPlayerFont" },
            { 182, "gPlayerTextColor" },
            { 183, "gPlayerVolume" },
            { 200, "gCreateFlag" },
            { 201, "gFancyWindows" },
            { 203, "gNoise" },
            { 204, "gWorldEditor" },
            { 205, "gCursRegController" },
            { 207, "gLoginName" },
            { 208, "gPassword" },
            { 209, "gUserID" },
            { 210, "gConnected" },
            { 211, "gLag" },
            { 212, "gGroupDialog" },
            { 213, "gFastForward" },
            { 214, "gSystemSpeed" },
            { 215, "gStartingRoom" },
            { 216, "gStartingX" },
            { 217, "gStartingY" },
            { 218, "gStartingLoop" },
            { 219, "gAttacking" },
            { 220, "gSeed" },
            { 221, "gGotoGrid" },
            { 222, "gEndingRoom" },
            { 223, "gEndingX" },
            { 224, "gEndingY" },
            { 225, "gExitLoop" },
            { 226, "gPackInfo" },
            { 227, "gEgoStepX" },
            { 228, "gEgoStepY" },
            { 229, "gDirection" },
            { 230, "gCombatAllowed" },
            { 231, "gMIDIFile" },
            { 232, "gHandsTimer" },
            { 233, "gRouteIP" },
            { 234, "gRoutePort" },
            { 235, "gCombatStarted" },
            { 236, "gPvPCombat" },
            { 237, "gArena" },
            { 238, "gTurnTime" },
            { 239, "gMovementRate" },
            { 240, "gMovieCmd" },
            { 241, "gChooseSpellTarget" },
            { 242, "gChooseSpellArea" },
            { 243, "gWObjectLite" },
            { 244, "gIsTeleporting" },
            { 245, "gTreasureObj" },
            { 246, "gTreasureSelector" },
            { 247, "gCharRemake" },
            { 248, "gMirrorStartingRoomLo" },
            { 249, "gMirrorStartingRoomHi" },
            { 250, "gMirrorEndingRoomLo" },
            { 251, "gMirrorEndingRoomHi" },
            { 252, "gRegistered" },
            { 253, "gServerNum" },
            { 254, "gNewSpellDialog" },
            { 255, "gRights" },
        };
    }
}
