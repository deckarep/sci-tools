using System.Collections.Generic;
using System.Linq;
using SCI.Language;

// There's a file floating around with the original names of all the sounds.
//
// Annotates the Sound:number property and WrapMusic:init parameters.

namespace SCI.Annotators
{
    static class DaggerSoundAnnotator
    {
        public static void Run(Game game)
        {
            string gameMusic1 = game.GetGlobal(102).Name;
            string gameMusic2 = game.GetGlobal(103).Name;

            var soundObjectNames = new HashSet<string>();
            var wrapMusicNames = new HashSet<string>();
            foreach (var script in game.Scripts)
            {
                soundObjectNames.Clear();
                soundObjectNames.Add(gameMusic1);
                soundObjectNames.Add(gameMusic2);

                wrapMusicNames.Clear();
                wrapMusicNames.Add("WrapMusic");

                foreach (var instance in script.Instances)
                {
                    if (instance.Super == "Sound")
                    {
                        soundObjectNames.Add(instance.Name);

                        // annotate "number" property of Sound instances
                        var property = instance.Properties.FirstOrDefault(p => p.Name == "number");
                        if (property != null)
                        {
                            Annotate(property.ValueNode);
                        }
                    }
                    else if (instance.Super == "WrapMusic")
                    {
                        wrapMusicNames.Add(instance.Name);
                    }
                }

                // annotate "number:" parameter
                foreach (var function in script.GetFunctions())
                {
                    foreach (var node in function.Node)
                    {
                        if (node.Text == "number:" &&
                            soundObjectNames.Contains(node.Parent.At(0).Text))
                        {
                            Annotate(node.Next());
                            continue;
                        }
                        if (node.Text == "init:" &&
                            wrapMusicNames.Contains(node.Parent.At(0).Text))
                        {
                            // first param is a loop flag, the rest are sound numbers
                            foreach (Node initParam in node.GetSelectorParameters().Skip(1))
                            {
                                Annotate(initParam);
                            }
                        }
                    }
                }
            }
        }

        static void Annotate(Node node)
        {
            if (node is Integer)
            {
                int number = node.Number;
                if (number >= 1000)
                {
                    number -= 1000;
                }
                string sound;
                if (sounds.TryGetValue(number, out sound))
                {
                    node.Annotate(sound);
                }
            }
            else if (node.At(0).Text == "switch")
            {
                // clockSound:number is assigned a switch
                for (int c = 2; c < node.Children.Count; c++)
                {
                    if (node.At(c).Children.Any())
                    {
                        Annotate(node.At(c).Children.Last());
                    }
                }
            }
            else if (node.At(0).Text == "if")
            {
                // wrapMusic:init takes a ternary
                Annotate(node.At(2));
                Annotate(node.At(4));
            }
        }

        static Dictionary<int, string> sounds = new Dictionary<int, string>
        {
            { 1, "mDeadBody1" },
            { 2, "mDeadBody2" },
            { 3, "mDeadBody3" },
            { 4, "mDeadBody4" },
            { 5, "mHide&Listen" },
            { 6, "mExamine" },
            { 16, "mChase1" },
            { 17, "mChase2" },
            { 19, "mHimlerShoos" },
            { 20, "sClock1/4Hr" },
            { 21, "sClock1/2Hr" },
            { 22, "sClock3/4Hr" },
            { 23, "sClockChime" },
            { 30, "mBetweenActs" },
            { 40, "sCarDoorOpen" },
            { 41, "sCarDoorClose" },
            { 42, "sDeskDrawerOpen" },
            { 44, "sSqueakyDoorOpen" },
            { 45, "sSqueakyDoorClose" },
            { 46, "sHeavyWoodDoorOpen" },
            { 47, "sHeavyWoodDoorClose" },
            { 48, "sTryingALockedDoor" },
            { 49, "sDoorUnlatch" },
            { 50, "sOpenBook" },
            { 51, "sRubCharcoalOnPaper" },
            { 52, "sGunShot" },
            { 53, "sBats" },
            { 54, "sDiggingInGarbage" },
            { 55, "sPanelFlip" },
            { 56, "music" },
            { 80, "sThud" },
            { 81, "sRunOver2" },
            { 82, "scream" },
            { 83, "scream" },
            { 84, "gasp" },
            { 85, "Oh" },
            { 90, "mMuseum1" },
            { 91, "mMuseum2" },
            { 92, "mMuseum3" },
            { 93, "mMuseum4" },
            { 94, "sStreetAmbience" },
            { 96, "sLauraRunOverByCar" },
            { 97, "yoTaxi" },
            { 99, "mDeathMusic" },
            { 100, "mOcean" },
            { 105, "mSierraLogo" },
            { 110, "mStateRoom" },
            { 112, "mStateMurder" },
            { 120, "mDocks" },
            { 121, "sDistantFoghorn" },
            { 140, "mLauraTheme1" },
            { 150, "mNightTrain" },
            { 151, "sTrainInterior" },
            { 160, "sStation" },
            { 161, "mRippedOff1" },
            { 162, "mRippedOff2" },
            { 163, "mRippedOff3" },
            { 164, "mRippedOff4" },
            { 180, "mNewYork" },
            { 190, "mLuigi" },
            { 210, "sNewsFX" },
            { 220, "mNews" },
            { 250, "mTaxiClean" },
            { 252, "sTaxiCabSFX" },
            { 260, "mLoFat1" },
            { 261, "mBaseBall" },
            { 270, "mLoFat2" },
            { 280, "mPolice" },
            { 281, "sSnoringDrunk" },
            { 292, "sPaperShuffle" },
            { 295, "mRiley1" },
            { 297, "sDoorKnock" },
            { 300, "mAct1End" },
            { 310, "mBar1" },
            { 311, "mBar2" },
            { 312, "mBar3" },
            { 314, "mBar1notMT32" },
            { 321, "sRunningSinkWater" },
            { 330, "mMuseumEstablishment" },
            { 332, "mSteve&Laura" },
            { 333, "sFountain" },
            { 334, "mSteveKissLaura" },
            { 335, "mPartyWaltz" },
            { 350, "mExitMuseum" },
            { 430, "sCutCable" },
            { 440, "sHelmetOpen" },
            { 441, "sHelmetClose" },
            { 442, "sMoveTapestry" },
            { 443, "sScootChair" },
            { 444, "sBreakDownDoor" },
            { 445, "sGlassOverDoor" },
            { 446, "sBoltLock" },
            { 450, "mEgyptionTheme" },
            { 451, "mOpenCoffin" },
            { 452, "sCreakingMummyOpen" },
            { 453, "sCreakingMummyClose" },
            { 454, "mAct2End" },
            { 455, "sMummyShut" },
            { 460, "sCutRope" },
            { 462, "sCrateHit" },
            { 480, "sTalkingDinosaur" },
            { 481, "sCaughtInMouth" },
            { 482, "mBuildToEnding" },
            { 483, "mEndAct5" },
            { 490, "sHeadHitsFloor" },
            { 500, "sBreakPlaster" },
            { 501, "sPryingOffKey" },
            { 502, "mEndAct3Alternate" },
            { 520, "mOlympiasOffice" },
            { 521, "mCobraArcade" },
            { 522, "sCobraHiss1" },
            { 523, "sCobraHiss2" },
            { 524, "mEndAct4" },
            { 525, "sHyroglphPage" },
            { 531, "sHingeSqueak" },
            { 540, "mUpTheStairs" },
            { 541, "mDownTheStairs" },
            { 542, "need" },
            { 543, "sImpact" },
            { 550, "mYvettesOffice" },
            { 551, "mYvettesHot" },
            { 553, "sScrewLightBulb" },
            { 556, "sYvetteFallsBack" },
            { 557, "mOh" },
            { 558, "sSwitch" },
            { 560, "sOpenSafe" },
            { 561, "sCloseSafe" },
            { 562, "sTurnTumbler" },
            { 563, "sMoveBlotterOnDesk" },
            { 564, "sPickUpCharcoal" },
            { 565, "mCarringtonsOffice" },
            { 566, "sPulBookFromShelf" },
            { 567, "static" },
            { 600, "sBreakGlass" },
            { 610, "mVatLab" },
            { 611, "mSwishingAroundInVat" },
            { 612, "sSplash" },
            { 613, "sFootFallOnLadder1" },
            { 615, "mFindDagger" },
            { 616, "mFindDagger0" },
            { 631, "sRefridgerator" },
            { 632, "sRefridgerator" },
            { 633, "sTrunkOpen" },
            { 634, "sTrunkClose" },
            { 635, "need" },
            { 636, "sBeetlesMeat" },
            { 637, "sFerretChuckles" },
            { 640, "sToolBoxOpen" },
            { 641, "sToolBoxClose" },
            { 642, "mErniesOffice" },
            { 643, "sTalkingBear" },
            { 650, "mHimlersOffice" },
            { 651, "sLauraElectrocuted" },
            { 652, "sImpaleLaura" },
            { 653, "sMachineGunned" },
            { 654, "sRatTrapSprung" },
            { 655, "sSwordSlide" },
            { 660, "mEscape" },
            { 661, "sHeavyMetalLever" },
            { 662, "sSqueakyElevatorMoving" },
            { 700, "sPushMummyAgainstDoor" },
            { 701, "sEgyptianChantSoft" },
            { 710, "sEgyptianChanting" },
            { 711, "mEgyptianTheme2" },
            { 712, "sChant2" },
            { 713, "sChant3" },
            { 714, "mChant1" },
            { 715, "mChant2" },
            { 716, "mChant3" },
            { 720, "sFurnaceRm" },
            { 721, "sHeavyStoneMoved" },
            { 722, "sNailSteve" },
            { 723, "sRustlingOfCoal" },
            { 732, "sGroupCobrasHissing" },
            { 733, "sMadRats" },
            { 736, "sBurn" },
            { 750, "sCrowdCheer" },
            { 751, "sCrowdSmallClaps" },
            { 752, "sCrowdMutter" },
            { 753, "mInquest" },
            { 760, "mPaperSpin" },
            { 770, "sBreakingRocks" },
            { 771, "sSeaGulls" },
            { 772, "mJailEnd" },
            { 795, "mClosingMedley" },
        };
    }
}
