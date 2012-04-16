using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Editor_Mod
{
    public  class ItemDictionaries
    {

        public Dstructure Tools = new Dstructure();
        public Dstructure Weapons = new Dstructure();
        public Dstructure Furniture = new Dstructure();
        public Dstructure Miscellaneous = new Dstructure();
        public Dstructure Accessories = new Dstructure();
        public Dstructure Blocks = new Dstructure();
        public Dstructure Potions = new Dstructure();

        public Dstructure Defenseitems = new Dstructure();
        public Dstructure ArmorSets = new Dstructure();

        public Dstructure VanityItems = new Dstructure();
        public Dstructure VanitySets = new Dstructure();

        public ItemDictionaries()
        {   
            
            #region Tools
            this.Tools.Cathegory = "Tools";
            this.Tools.Sections = new string[]
            {
             "All in Ones",
             "Pickaxes and Drills",
             "Axes and Chainsaws",
             "Hammers",
             "Hooks",
             "Misc"
             };
            #region Sections
             this.Tools.SectionNpcItems = new string[6][];




            this.Tools.SectionNpcItems[0] = new string[]
            { 
            "Hamdrax"
            };
            this.Tools.SectionNpcItems[1] = new string[]
            { 
         "Copper Pickaxe",
         "Silver Pickaxe",
         "Iron Pickaxe", 

         "Gold Pickaxe", 
         "Nightmare Pickaxe",
         "Molten Pickaxe",

         "Cobalt Drill",
         "Mythril Drill",
         "Adamantite Drill"
           };
            this.Tools.SectionNpcItems[2] = new string[]
           {
         "Copper Axe",
         "Silver Axe",
         "Iron Axe",
         "Gold Axe",

         "War Axe of the Night",
         "Meteor Hamaxe",
         "Molten Hamaxe",

         "Cobalt Chainsaw",
         "Mythril Chainsaw",
         "Adamantite Chainsaw"
         };
            this.Tools.SectionNpcItems[3] = new string[]
        {
        "Wooden Hammer",
        "Copper Hammer",
        "Iron Hammer",

        "Silver Hammer",
        "Gold Hammer",
        "The Breaker",

        "Meteor Hamaxe", 
        "Molten Hamaxe",
        "Pwnhammer"

        };
            this.Tools.SectionNpcItems[4] = new string[]
        {
            "Grappling Hook",
            "Dual Hook",
            "Ivy Whip"

        };
            this.Tools.SectionNpcItems[5] = new string[]
        {
 
"Dirt Rod",
"Purification Powder",
"Orb of Light",
"Magic Mirror",
"Breathing Reed",
"Whoopie Cushion",
"Golden Key",
"Shadow Key",
"Fairy Bell"
        };
            
            #endregion
            #endregion
            #region Weapons

            this.Weapons.Cathegory = "Weapons";
            this.Weapons.Sections = new string[]
            {
             "Consumable items",
             "Explosives",
             "Flails",
             "Spears",
             "Bows",
             "Boomerangs",
             "Guns",
             "Magic items",
             "Melee items"
 
             };
            #region Sections

            this.Weapons.SectionNpcItems = new string[9][];


            this.Weapons.SectionNpcItems[0] = new string[]
                 {

                     "Shuriken",
"Throwing Knife",
"Bone",
"Spiky Ball",
"Poisoned Knife"


                 };
            this.Weapons.SectionNpcItems[1] = new string[]
                 {
                     "Grenade",
"Bomb",
"Sticky Bomb",
"Dynamite",
"Explosives"



                 };
            this.Weapons.SectionNpcItems[2] = new string[]
                 {
                     @"Ball 'O Hurt",
"Blue Moon",
"Sunfury",
"Dao of Pow",
"Harpoon"



                 };
            this.Weapons.SectionNpcItems[3] = new string[]
                 {
                     "Spear",
"Trident",
"Dark Lance",
"Cobalt Naginata",
"Mythril Halberd",
"Adamantite Glaive",
"Gungnir"



                 };
            this.Weapons.SectionNpcItems[4] = new string[]
                 {
                     "Copper Bow",
                     "Wooden Bow",
                     "Silver Bow",
"Iron Bow",
                   
 "Gold Bow",
"Demon Bow",
"Molten Fury",

"Cobalt Repeater",
"Mythril Repeater",
"Adamantite Repeater",
"Hallowed Repeater"



                 };
            this.Weapons.SectionNpcItems[5] = new string[]
                 {
                     "Wooden Boomerang",
"Enchanted Boomerang",
"Flamarang",
"Thorn Chakram",
"Light Disc"



                 };
            this.Weapons.SectionNpcItems[6] = new string[]
                 {
"Blowpipe",
"Flintlock Pistol",
"Sandgun",
"Musket",
"Handgun",
"Minishark",
"Space Gun",
"Phoenix Blaster",
"Star Cannon",
"Flamethrower",
"Megashark",
"Clockwork Assault Rifle",
"Shotgun"
};
            this.Weapons.SectionNpcItems[7] = new string[]
                 {
"Flower of Fire",
"Starfury",
"Space Gun",
"Magic Missile",
"Vilethorn",
"Water Bolt",
"Flamelash",
"Aqua Scepter",
"Demon Scythe",
"Crystal Storm",
"Cursed Flames",
"Laser Rifle",
"Magic Dagger",
"Magical Harp",
"Rainbow Rod",
"Ice Rod"





                 };
            this.Weapons.SectionNpcItems[8] = new string[]
                 {

"Copper Shortsword",
"Copper Broadsword",
"Wooden Sword",
"Iron Shortsword",
"Iron Broadsword",
"Muramasa",
"Silver Shortsword",
"Silver Broadsword",
"Blade of Grass",
"Gold Shortsword",
"old Broadsword",
"Fiery Greatsword",
"Purple Phaseblade",
"Yellow Phaseblade",
"Blue Phaseblade",
"Green Phaseblade",
"Red Phaseblade",
"White Phaseblade",
"War Axe of the Night",
"The Breaker",
"Light's Bane",
"Staff of Regrowth",
"Starfury",
"Night's Edge",
"Cobalt Sword",
"Mythril Sword",
"Adamantite Sword",
"Breaker Blade",
"Excalibur"
};



            
            #endregion
            #endregion
            #region Furniture


            this.Furniture.Cathegory = "Furniture";
            this.Furniture.Sections = new string[]
            {
                "Crafting stations",
                    "Light sources",
                        "Storage",
                            "Decorative",
                                "Functional",
                                    "Statues"
                                  

            };
            #region Sections
            this.Furniture.SectionNpcItems = new string[6][];
            this.Furniture.SectionNpcItems[0] = new string[]
                 {
                     "Work Bench",
 
"Loom",
"Iron Anvil",
"Mythril Anvil",
"Bookcase",
"Sawmill",
"Furnace",
"Hellforge",
"Adamantite Forge",
"Wooden Chair",
"Wooden Table",
"Keg",
"Cooking Pot",
@"Tinkerer's Workshop"
                 };

            this.Furniture.SectionNpcItems[1] = new string[]
                 {

                     "Torch",
"Blue Torch",
"Purple Torch",
"White Torch",
"Green Torch",
"Red Torch",
"Yellow Torch",
"Demon Torch",
"Cursed Torch",
"Candle",
"Water Candle",
"Candelabra",
"Copper Chandelier",
"Silver Chandelier",
"Gold Chandelier",
"Tiki Torch",
"Lamp Post",
"Skull Lantern",
"Chain Lantern",
"Chinese Lantern",
"Disco Ball",
 "Blue Light",
 "Red Light",
 "Green Light"
   
                 };



            this.Furniture.SectionNpcItems[2] = new string[]
                 {
                     "Piano",
"Dresser",
"Bathtub",
"Bench",
"Book",
"Wooden Beam",
"Toilet",
"Mannequin",
"Coral",
"Throne",
"Crystal Shard",
"Green Banner",
"Red Banner",
"Yellow Banner",
"Blue Banner"
                 };
            this.Furniture.SectionNpcItems[3] = new string[]
                 {
                     "Chest",
"Gold Chest",
"Shadow Chest",
"Barrel",
"Trash Can",
"Piggy Bank",
"Safe"

                 };

            this.Furniture.SectionNpcItems[4] = new string[]
                 {

                     "Bed",
"Grandfather Clock",
"Sign",
"Tombstone",
"Bottle",
"Mug",
"Pink Vase",
"Wooden Door",
"Clay Pot",
"Wood Platform",
"Crystal Ball",
 "Music Box",
 @"Music Box (Overworld Day)",
 @"Music Box (Eerie)",
 @"Music Box (Night)",
 @"Music Box (Title)",
 @"Music Box (Underground)",
 @"Music Box (Jungle)",
 @"Music Box (Corruption)",
 @"Music Box (Underground Corruption)",
 @"Music Box (The Hallow)",
 @"Music Box (Underground Hallow)",
 @"Music Box (Boss 1)",
 @"Music Box (Boss 2)",
 @"Music Box (Boss 3)"

                 };

            this.Furniture.SectionNpcItems[5] = new string[]
                 {

"Angel Statue",
"Anvil Statue",
"Armor Statue",
"Axe Statue",
"Bat Statue",
"Bird Statue",
"Bomb Statue",
"Boomerang Statue",
"Boot Statue",
"Bow Statue",
"Bunny Statue",
"Chest Statue",
"Corrupt Statue",
"Spear Statue",
"Star Statue",
"Cross Statue",
"Eyeball Statue",
"Fish Statue",
"Gargoyle Statue",
"Gloom Statue",
"Goblin Statue",
"Hammer Statue",
"Heart Statue",
"Hornet Statue",
"Imp Statue",
"Jellyfish Statue",
"Sword Statue",
"Tree Statue",
"Pillar Statue",
"Crab Statue",
"King Statue",
"Pickaxe Statue",
"Pirahna Statue",
"Pot Statue",
"Potion Statue",
"Queen Statue",
"Reaper Statue",
"Shield Statue",
"Skeleton Statue",
"Slime Statue",
"Sunflower Statue",
"Woman Statue",
"Mushroom Statue"
                 };

 
            #endregion
            #endregion    
            #region Blocks

            this.Blocks.Cathegory = "Blocks and Walls";
            this.Blocks.Sections = new string[] 
            { 
                
                "Blocks",
                "Bricks",
                "Ores",
                "Gems",
                "Walls"
            };
            this.Blocks.SectionNpcItems = new string[6][];
            this.Blocks.SectionNpcItems[0] = new string[] 
            {
 
"Dirt Block",
"Stone Block",
"Mud Block",
"Clay Block",
"Ash Block",
"Sand Block",
"Silt Block",
"Pearlsand Block",
"Obsidian",
"Ebonstone Block",
"Cobweb",
"Spike",
 "Snow Block",
"Mudstone Block",
 "Candy Cane Block",
  "Green Candy Cane Block" 
            
            };
            this.Blocks.SectionNpcItems[1] = new string[] 
            {    
                "Wood",
            "Wood Platform",
"Glass",
"Gray Brick",
"Red Brick",
"Obsidian Brick",
"Iridescent Brick",
"Copper Brick",
"Silver Brick",
"Gold Brick",
"Demonite Brick",
"Hellstone Brick",
"Cobalt Brick",
"Mythril Brick",
"Pearlstone Brick",
"Blue Brick",
"Green Brick",
"Pink Brick",
 "Snow Brick"

            };
 
            this.Blocks.SectionNpcItems[2] = new string[] 
            { 
"Copper Ore",
"Iron Ore",
"Silver Ore",
"Gold Ore",
"Demonite Ore",
"Meteorite",
"Hellstone",
"Cobalt Ore",
"Mythril Ore",
"Adamantite Ore"
            };

            this.Blocks.SectionNpcItems[3] = new string[] 
            { 
"Diamond",
"Ruby",
"Emerald",
"Sapphire",
"Topaz",
"Amethyst"
            };
            this.Blocks.SectionNpcItems[4] = new string[] 
            { 
"Wood Wall",
"Dirt Wall",
"Stone Wall",
"Gray Brick Wall",
"Red Brick Wall",
"Copper Brick Wall",
"Silver Brick Wall",
"Gold Brick Wall",
"Obsidian Brick Wall",
"Pink Brick Wall",
"Blue Brick Wall",
"Green Brick Wall",
"Glass Wall",
"Planked Wall",
"Mudstone Brick Wall",
"Mythril Brick Wall",
"Cobalt Brick Wall",
"Iridescent Brick Wall",
"Pearlstone Brick Wall",
"Green Candy Cane Wall",
"Snow Brick Wall",
"Candy Cane Wall",
"Green Candy Cane Wall"

            };
            #endregion
            #region Accessories

            this.Accessories.Cathegory = "Accessories";
            this.Accessories.Sections = new string[] 
            { 

                "Movement",
                "Informational",
                @"Health/Mana",
                "Combat",
                "Misc",


            };

            #region sections
            this.Accessories.SectionNpcItems = new string[5][];



            this.Accessories.SectionNpcItems[0] = new string[]
                 {

                     "Aglet",
"Angel Wings",
"Anklet of the Wind",
"Cloud in a Balloon",
"Cloud in a Bottle",
"Demon Wings",
"Diving Gear",
"Flipper",
"Hermes Boots",
"Lucky Horseshoe",
"Obsidian Horseshoe",
"Rocket Boots",
"Shiny Red Balloon",
"Spectre Boots"


                 };
            this.Accessories.SectionNpcItems[1] = new string[]
                 {
                     "Depth Meter",
"Copper Watch",
"Silver Watch",
"Gold Watch",
"Compass",
"GPS",
"Ruler"



                 };
            this.Accessories.SectionNpcItems[2] = new string[]
                 {

                     "Band of Regeneration",
"Band of Starpower",
"Mana Flower",
@"Nature's Gift",
@"Philosopher's Stone"



                 };
            this.Accessories.SectionNpcItems[3] = new string[]
                 {

                     "Cobalt Shield",
"Feral Claws",
"Obsidian Skull",
"Shackle",
"Obsidian Shield",
"Ranger Emblem",
"Sorcerer Emblem",
"Warrior Emblem",
"Star Cloak",
"Titan Glove",
"Cross Necklace"


                 };

            this.Accessories.SectionNpcItems[4] = new string[]
                 {
                     "Guide Voodoo Doll",
@"Neptune's Shell",
"Moon Charm",
"Toolbelt",
"Music Box"



                 };

            #endregion
                  #endregion
            #region Miscellaneous

            this.Miscellaneous.Cathegory = "Miscellaneous";
            this.Miscellaneous.Sections = new string[] 
            { 
            "Coins",
            "Seeds",
            "Materials",
            "Objects",
            @"Ammunition & Arrows",
            "Potions",
            "Boss Summon",
            "Other"
 
            
            };
            #region Sections
            this.Miscellaneous.SectionNpcItems = new string[9][];
            this.Miscellaneous.SectionNpcItems[0] = new string[]
                 {
                     "Copper Coin",
"Silver Coin",
"Gold Coin",
"Platinum Coin"




                 };

            this.Miscellaneous.SectionNpcItems[1] = new string[]
                 {

                     "Acorn",
"Sunflower",
"Blinkroot Seeds",
"Daybloom Seeds",
"Hallowed Seeds",
"Grass Seeds",
"Mushroom Grass Seeds",
"Deathweed Seeds",
"Fireblossom Seeds",
"Corrupt Seeds",
"Jungle Grass Seeds",
"Moonglow Seeds",
"Waterleaf Seeds"

                 };

            this.Miscellaneous.SectionNpcItems[2] = new string[]
                 {

                     "Cobweb",
"Gel",
"Lens",
"Black Lens",
"Iron Chain",
"Hook",
"Silk",
"Vile Mushroom",
"Glowstick",
"Feather",
"Waterleaf",
"Moonglow",
"Daybloom",
"Amethyst",
"Topaz",
"Ruby",
"Tattered Cloth",
"Rotten Chunk",
"Worm Tooth",
"Stinger",
"Jungle Spores",
"Vine",
"Leather",
"Shark Fin",
"Antlion Mandible",
"Illegal Gun Parts",
"Deathweed",
"Blinkroot",
"Fireblossom",
"Sapphire",
"Emerald",
"Diamond",
"Shadow Scale",
"Coral",
"Cactus",
"Green Dye",
"Black Dye",
"Copper Bar",
"Iron Bar",
"Silver Bar",
"Gold Bar",
"Demonite Bar",
"Meteorite Bar",
"Hellstone Bar",
"Cobalt Bar",
"Mythril Bar",
"Adamantite Bar"

                 };

            this.Miscellaneous.SectionNpcItems[3] = new string[]
                 {
"Demon Altar",
"Crystal Heart",
"Shadow Orb",
"Pot",
"Tree",
"Shroom"


                 };

            this.Miscellaneous.SectionNpcItems[4] = new string[]
                 {
                     "Wooden Arrow",
"Flaming Arrow",
"Unholy Arrow",
"Jester's Arrow",
"Hellfire Arrow",
"Cursed Arrow",
"Musket Ball",
"Silver Bullet",
"Meteor Shot",
"Crystal Bullet",
"Cursed Bullet",
"Seed"


                 };

            this.Miscellaneous.SectionNpcItems[5] = new string[]
                 {
"Mushroom",
"Glowing Mushroom",
"Lesser Healing Potion",
"Healing Potion",
"Greater Healing Potion",
"Life Crystal",
"Fallen Star",
"Lesser Mana Potion",
"Mana Potion",
"Greater Mana Potion",
"Mana Crystal",
"Ale",
"Lesser Restoration Potion",
"Restoration Potion",
"Goldfish",
"Bottled Water",
"Archery Potion",
"Battle Potion",
"Featherfall Potion",
"Gills Potion",
"Gravitation Potion",
"Hunter Potion",
"Invisibility Potion",
"Ironskin Potion",
"Magic Power Potion",
"Mana Regeneration Potion",
"Night Owl Potion",
"Obsidian Skin Potion",
"Regeneration Potion",
"Shine Potion",
"Spelunker Potion",
"Swiftness Potion",
"Thorns Potion",
"Water Walking Potion",
"Bowl of Soup"


                 };

            this.Miscellaneous.SectionNpcItems[6] = new string[]
                 {

                     "Suspicious Looking Eye",
"Worm Food",
"Mechanical Eye",
"Mechanical Worm",
"Mechanical Skull",
"Goblin Battle Standard",
 "Snow Globe",
"Slime Crown"

                 };

            this.Miscellaneous.SectionNpcItems[7] = new string[]
                 {

                     "Angel Statue",
"Empty Bucket",
"Water Bucket",
"Lava Bucket",
"Ivy Whip",
"Dual Hook",
"Grappling Hook",
"Golden Key",
"Shadow Key",
"Dirt Rod",
"Purification Powder",
"Whoopie Cushion",
"Orb of Light",
"Magic Mirror",
"Breathing Reed",
"Dart Trap",
"Active Stone Block",
"Inactive Stone Block",
"Boulder",
 "Inlet Pump",
 "Outlet Pump",
 "1 Second Timer",
 "3 Second Timer",
 "5 Second Timer",
 "Wrench",
"Wire Cutter", 	 
"Wire", 	 
"Lever",  
"Switch", 	 
"Red Pressure Plate",  
"Green Pressure Plate", 	 
"Gray Pressure Plate", 	 
"Brown Pressure Plate"
                 };


            this.Miscellaneous.SectionNpcItems[8] = new string[]
                 {
            
             "Blue Present",
             "Green Present",
             "Yellow Present"

                 };

            #endregion
            #endregion
            #region Defense items


            this.Defenseitems.Cathegory = "Defense items";
            this.Defenseitems.Sections = new string[] 
            { 
            "Armor Pieces",
             "Accessories",
              "Consumables",
               "Armor Sets",
            
            };
            #region Sections
            this.Defenseitems.SectionNpcItems = new string[4][];
            this.Defenseitems.SectionNpcItems[0] = new string[] 
            { 
                "Goggles",
                "Empty Bucket",
                "Diving Helmet",
                "Wizard Hat"

            };
            this.Defenseitems.SectionNpcItems[1] = new string[] 
            { 
"Shackle",
"Cobalt Shield",
"Obsidian Skull",
"Obsidian Shield",
"Cross Necklace",
"Star Cloak"

            };
            this.Defenseitems.SectionNpcItems[2] = new string[] 
            { 
                "Ironskin Potion",
                "Bowl of Soup",
                "Obsidian Skin Potion",
                "Soul of Flight",
"Soul of Light",
"Soul of Night",
"Soul of Sight",
"Soul of Might",
"Soul of Fright"
            }; 
            #endregion
            #region Armor Sets


            this.ArmorSets.Cathegory = "Armor Sets";
            this.ArmorSets.Sections = new string[] 
            { 
"Mining armor",
"Copper armor",
"Iron armor",
"Silver armor",
"Gold armor",
"Meteor armor",
"Shadow armor",
"Jungle armor",
"Necro armor",
"Molten armor",
"Cobalt armor",
"Mithril armor",
"Adamantite armor",
"Hallowed armor"
            
            };
            #region Sections

            this.ArmorSets.SectionNpcItems = new string[14][]; 
            this.ArmorSets.SectionNpcItems[0] = new string[] 
            { 
                "Mining Helmet",
                "Mining Shirt",
                "Mining Pants"

            };
            this.ArmorSets.SectionNpcItems[1] = new string[] 
            { 
                "Copper Helmet",
                 "Copper Chainmail",
                  "Copper Greaves",
                  

            };
            this.ArmorSets.SectionNpcItems[2] = new string[] 
            { 
                "Iron Helmet",
                 "Iron Chainmail",
                  "Iron Greaves"

            };
            this.ArmorSets.SectionNpcItems[3] = new string[] 
            { 
                "Silver Helmet",
                 "Silver Chainmail",
                  "Silver Greaves"

            };
            this.ArmorSets.SectionNpcItems[4] = new string[] 
            { 
             "Gold Helmet",
                 "Gold Chainmail",
                  "Gold Greaves"

            };
            this.ArmorSets.SectionNpcItems[5] = new string[] 
            { 
                "Meteor Helmet",
                 "Meteor Suit",
                  "Meteor Leggings" 

            };
            this.ArmorSets.SectionNpcItems[6] = new string[] 
            { 
                     "Shadow Helmet",
                 "Shadow Chainmail",
                  "Shadow Greaves"

            };
            this.ArmorSets.SectionNpcItems[7] = new string[] 
            { 
                "Jungle Hat",
                 "Jungle Shirt",
                  "Jungle Pants"

            };
            this.ArmorSets.SectionNpcItems[8] = new string[] 
            { 
                "Necro Helmet",
                 "Necro Breastplate",
                  "Necro Greaves",
                

            };
            this.ArmorSets.SectionNpcItems[9] = new string[] 
            { 
                    "Molten Helmet",
                 "Molten Breastplate",
                  "Molten Greaves",
                

            };
            this.ArmorSets.SectionNpcItems[10] = new string[] 
            { 
                "Cobalt Breastplate",
                 "Cobalt Leggings",
                  "Cobalt Hat",
                   "Cobalt Helmet",
                   "Cobalt Mask"

            };
            this.ArmorSets.SectionNpcItems[11] = new string[] 
            { 
                "Mythril Chainmail",
                 "Mythril Greaves",
                  "Mythril Hood",
                   "Mythril Helmet",
                   "Mythril Mask"

            };
            this.ArmorSets.SectionNpcItems[12] = new string[] 
            { 
                   "Adamantite Breastplate",
                 "Adamantite Leggings",
                  "Adamantite Headgear",
                   "Adamantite Helmet",
                    "Adamantite Mask"

            };
            this.ArmorSets.SectionNpcItems[13] = new string[] 
            { 
                "Hallowed Plate Mail",
                 "Hallowed Greaves",
                  "Hallowed Headgear",
                   "Hallowed Helmet",
                    "Hallowed Mask"

            };
 
  

            #endregion



            
            #endregion
            #endregion
            #region Vanity items


            this.VanityItems.Cathegory = "Vanity Items";
            this.VanityItems.Sections = new string[] 
            { 
                "Vanity pieces",
                 "Vanity Sets",

            };
            this.VanityItems.SectionNpcItems = new string[1][];
            this.VanityItems.SectionNpcItems[0] = new string[] 
            { 
"Jungle Rose",
"Fish Bowl",
"Robe",
"Summer Hat",
"Red Hat",
"Bunny Hood",
"Gold Crown",
"Robot Hat",
"Mime Mask",
"Sunglasses"
            };

            #region Vanitysets


            this.VanitySets.Cathegory = "Vanity Sets";

            this.VanitySets.Sections = new string[] 
            { 
            "Archaeologist's suit",
"Plumber's clothes",
"Tuxedo",
"Familiar clothes",
"The Doctor's clothes",
"Ninja clothes",
"Hero's clothes",
"Clown costume",
"Santa's clothes"
            };
            this.VanitySets.SectionNpcItems = new string[9][];
            this.VanitySets.SectionNpcItems[0] = new string[] 
            { 
                "Archaeologist's Hat",
                "Archaeologist's Jacket",
                "Archaeologist's Pants"

            };
            this.VanitySets.SectionNpcItems[1] = new string[] 
            { 
                "Plumber's Hat",
                "Plumber's Shirt",
                "Plumber's Pants"

            };
            this.VanitySets.SectionNpcItems[2] = new string[] 
            { 
                "Top Hat",
                "Tuxedo Shirt",
                "Tuxedo Pants"

            };
            this.VanitySets.SectionNpcItems[3] = new string[] 
            { 
                "Familiar Wig",
                "Familiar Shirt",
                "Familiar Pants"

            };
            this.VanitySets.SectionNpcItems[4] = new string[] 
            { 
                "The Doctor's Shirt",
                "The Doctor's Pants"
            

            };
            this.VanitySets.SectionNpcItems[5] = new string[] 
            { 
                "Ninja Hood",
                "Ninja Shirt",
                "Ninja Pants"

            };
            this.VanitySets.SectionNpcItems[6] = new string[] 
            { 
                "Hero's Hat",
                "Hero's Shirt",
                "Hero's Pants"

            };
            this.VanitySets.SectionNpcItems[7] = new string[] 
            { 
                "Clown Hat",
                "Clown Shirt",
                "Clown Pants"

            };
            this.VanitySets.SectionNpcItems[8] = new string[] 
            { 
            "Santa Hat",
"Santa Shirt",
"Santa Pants"

            };


            #endregion




            #endregion
            #region Potions and ingredients


            this.Potions.Cathegory = "Potions";
            this.Potions.Sections = new string[] 
            { 

"Standard potions",
"Food and drink",
"Buff potions",
"Ingredients"

            };
            #region sections
            this.Potions.SectionNpcItems = new string[4][];
            this.Potions.SectionNpcItems[0] = new string[] 
            {
  "Healing Potion",
"Lesser Healing Potion",
"Greater Healing Potion",
"Mana Potion",
"Lesser Mana Potion",
"Restoration Potion",	
"Lesser Restoration Potion",
"Bottled Water",
"Fallen Star",
"Bottle",
"Glowing Mushroom",
"Mushroom",
"Pixie Dust",
"Crystal Shard",
"Gel"


            };
            this.Potions.SectionNpcItems[1] = new string[] 
            {

"Ale",
"Bowl of Soup",
"Goldfish",
"Mushroom"
            };
            this.Potions.SectionNpcItems[2] = new string[] 
            { 
"Archery Potion",
"Battle Potion",
"Featherfall Potion",
"Gills Potion",
"Gravitation Potion",
"Hunter Potion",
"Invisibility Potion",
"Ironskin Potion",
"Magic Power Potion",
"Mana Regeneration Potion",
"Night Owl Potion",
"Obsidian Skin Potion",
"Regeneration Potion",
"Shine Potion",
"Spelunker Potion",
"Swiftness Potion",
"Water Walking Potion",
             };

            this.Potions.SectionNpcItems[3] = new string[] 
            { 
"Daybloom",
"Lens",
"Deathweed",
"Rotten Chunk",
"Blinkroot",
"Feather",
"Waterleaf",
"Coral",
"Fireblossom",
"Deathweed",
"Shark Fin",
"Moonglow",
"Fallen Star",
"Glowing Mushroom",
"Cactus",
"Thorns Potion",
"Deathweed"
            };
            #endregion










                 #endregion



        }






 




















    }
}
