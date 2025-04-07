using System;
using System.Collections.Generic;

namespace SCI.Annotators
{
    class Pq2Annotator : GameAnnotator
    {
        public override void Run()
        {
            RunEarly();
            GlobalRenamer.Run(Game, globals);
            ExportRenamer.Run(Game, exports);
            InventoryAnnotator.Run(Game, items);
            Sci0InventoryAnnotator.Run(Game, items);
            RunLate();

            // i don't need this anymore, but this is how to detect it
            //bool isJapanese = Game.GetScript(0).Exports.ContainsKey(1);
        }

        static Dictionary<int, string> globals = new Dictionary<int, string>
        {
        };

        static IReadOnlyDictionary<Tuple<int, int>, string> exports = new Dictionary<Tuple<int, int>, string>()
        {
            // SetScore is duplicated because when it's 1, there is no export 22,
            // and when it's 22 there is no export 1.
            { Tuple.Create(0, 1), "SetScore" }, // export 1 in japan, 22 in others
            { Tuple.Create(0, 2), "EgoDead" },
            { Tuple.Create(0, 3), "Notify" },
            { Tuple.Create(0, 4), "HaveMem" },
            { Tuple.Create(0, 5), "RedrawCast" },
            { Tuple.Create(0, 6), "clr" },
            // 7 - 10 are messages
            { Tuple.Create(0, 11), "HandsOff" },
            { Tuple.Create(0, 12), "HandsOn" },
            // 13 message
            { Tuple.Create(0, 14), "IsItemAt" },
            { Tuple.Create(0, 15), "PutItem" },

            { Tuple.Create(0, 16), "SetFlag" },
            { Tuple.Create(0, 17), "ClearFlag" },
            { Tuple.Create(0, 18), "IsFlag" },

            { Tuple.Create(0, 20), "NormalEgo" },
            { Tuple.Create(0, 22), "SetScore" }, // export 1 in japan, 22 in others
        };

        static string[] items =
        {
            "hand_gun",
            "extra_ammo_clips",
            "key_ring",
            "unmarked_car_keys",
            "money_clip",
            "thank_you_letter",
            "death_threat",
            "wallet",
            "handcuffs",
            "wire_clippers",
            "field_kit",
            "potted_plant",
            "new_mug_shot",
            "hit_list",
            "makeshift_knife",
            "ear_protectors",
            "plane_ticket",
            "plaster_cast",
            "lost_badge",
            "thumbprint",
            "bullets",
            "empty_holster",
            "fingerprint",
            "old_mug_shot",
            "envelope_corner",
            "envelope",
            "jail_clothes",
            "motel_key",
            "vial_of_blood",
            "lipstick",
            "walkie_talkie",
            "jailer_s_revolver",
            "gas_mask",
            "bomb_instructions",
            "car_registration",
            "Colby_s_business_card",
            "note_from_Marie_s_door",
            "your_LPD_business_card",
        };
    }
}
