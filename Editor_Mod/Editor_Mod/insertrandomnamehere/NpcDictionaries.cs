using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Editor_Mod
{
   public class NpcDictionaries
    {
       public Dstructure MobsFamilies = new Dstructure();
       public Dstructure AtoZ = new Dstructure();
       public NpcDictionaries()
       {
           #region chategories

           this.MobsFamilies.Cathegory = "Mobs Families";
           this.MobsFamilies.Sections = new string[]
            {
            "Slimes",
            "Undead Monsters",
            "Bone Heads",
            "Goblins",
            "Flyers Monsters",
            "Swimming Monsters",
            "Bosses",
            "Friendly",
            "Immortals",
            "Passive",
            "Burrowing Monster"
             };
           #region Sections
            this.MobsFamilies.SectionNpcItems = new string[11][];


           #region Slimes
           int num1 = 0;
           this.MobsFamilies.SectionNpcItems[num1] = new string[] 
           {
             "Blue Slime",
             "Slimeling", 
             "Slimer2", 
             "Pinky",
             "Green Slime",
             "Baby Slime",
             "Black Slime",
             "Purple Slime",
             "Red Slime",
             "Yellow Slime",
             "Jungle Slime",
             "Mother Slime",
             "Lava Slime",
			 "Dungeon Slime",
			 "Corrupt Slime",
             "Slimer",    
             "Illuminant Slime"
           
           
           }; num1++;
           #endregion
           #region Undead Monsters

           this.MobsFamilies.SectionNpcItems[num1] = new string[]
           { 

    "Angry Bones",
    "Doctor Bones",
    "The Groom",
    "Undead Miner",
    "Zombie"
           }; num1++;

           #endregion
           #region Bone Heads
           this.MobsFamilies.SectionNpcItems[num1] = new string[] 
           {  
               "Short Bones",
             "Big Boned",
              "Heavy Skeleton",
                "Doctor Bones",
                 "Skeleton",
                  "Armored Skeleton",
                     "Skeleton Archer"
           }; num1++;
           #endregion
           #region Goblins

           this.MobsFamilies.SectionNpcItems[num1] = new string[] 
           {     
         "Goblin Archer",
	     "Bound Goblin",
         "Goblin Peon",
         "Goblin Thief",
         "Goblin Warrior",
         "Goblin Sorcerer",
         "Goblin Scout"

           }; num1++;
           #endregion
           #region Flyers
           this.MobsFamilies.SectionNpcItems[num1] = new string[] 
           { 
               "Bat",
    "Big Eater",
    "Bird",
    "Birds",
   "Cave Bat",
    "Cursed Skull",
    "Demon",
    "Demon Eye",
    "Dungeon Guardian",
    "Gastropod",
    "Giant Bat",
    "Harpy",
    "Hellbat",
    "Hornet",
    "Jungle Bat",
    "Little Eater",
    "Little Stinger",
    "Meteor Head",
    "Servant of Cthulhu",
    "Voodoo Demon",
    "Vulture",
    "Wraith",
           }; num1++;
           #endregion
           #region Swimming Monsters
           this.MobsFamilies.SectionNpcItems[num1] = new string[]
           {
           
    "Corrupt Goldfish",
    "Goldfish",
    "Piranha",
    "Shark"
           }; num1++;
           #endregion
           #region Bosses
           this.MobsFamilies.SectionNpcItems[num1] = new string[] 
           {
     "Boss Tactics",
    "Eater of Worlds",
    "Eye of Cthulhu",
    "King Slime",
    "Old Man",
    "Skeletron",
    "Skeletron Prime",
    "The Destroyer",
    "The Twins",
    "Wall of Flesh"
           }; num1++;
           #endregion
           #region Friendly
           this.MobsFamilies.SectionNpcItems[num1] = new string[] 
           {
           	     "Guide",
"Merchant",
"Nurse",
"Demolitionist",
"Arms Dealer",
"Dryad",
"Oldman",
"Clothier",
"Mechanic",
"Goblin Tinkerer",
"Wizard"



           }; num1++;
           #endregion
           #region immortals

           this.MobsFamilies.SectionNpcItems[num1] = new string[] 
           { 
               "Blazing Wheel",
               "Spike Ball"
           }; num1++;
           #endregion
           #region passive
           this.MobsFamilies.SectionNpcItems[num1] = new string[]
           {  
      
               "Bird",
               "Bunny",
               "Goldfish" 
           }; num1++;

           #endregion
           #region Burrowing Monster
           this.MobsFamilies.SectionNpcItems[num1] = new string[]
           { 
           
    "Bone Serpent",
   
    "Devourer",
    "Digger",
    "Eater of Worlds",
    "Giant Worm",
    "World Feeder"
           }; num1++;

           #endregion 
           #endregion
           #endregion
           #region A to Z

           this.AtoZ.Cathegory = "A to Z";
           this.AtoZ.Sections = new string[]
            {
"A",
"B",
"C",
"D",
"E",
"F",
"G",
"H",
"I",
"J",
"K",
"L",
"M",
"P",
"S",
"T",
"U",
"V",
"W",
"Z"

            };
           #region sections
           this.AtoZ.SectionNpcItems = new string[20][];
 

           int num = 0;
           this.AtoZ.SectionNpcItems[num] = new string[] 
           {
                   "Angler Fish",
    "Angry Bones",
    "Antlion",
    "Armored Skeleton"
           }; num++;
           this.AtoZ.SectionNpcItems[num] = new string[] 
           {
                   "Bat",
    "Big Boned",
    "Big Eater",
    "Big Stinger",
    "Birds",
    "Blazing Wheel",
    "Bone Serpent",
    "Bones Family",
    "Bunny",
    "Burrowing Monsters"

           }; num++;
           this.AtoZ.SectionNpcItems[num] = new string[] 
           {
                   "Cave Bat",
    "Chaos Elemental",
    "Clinger",
    "Clown",
    "Corrupt Bunny",
    "Corrupt Goldfish",
    "Corrupt Slime",
    "Corruptor",
    "Crab",
    "Cursed Hammer",
    "Cursed Skull"

           }; num++;
           this.AtoZ.SectionNpcItems[num] = new string[] 
           {
    "Dark Caster",
    "Demon",
    "Demon Eye",
    "Devourer",
    "Digger",
    "Doctor Bones",
    "Dungeon Guardian",
    "Dungeon Slime"


           }; num++;
           this.AtoZ.SectionNpcItems[num] = new string[] 
           {
               
    "Eater Family",
    "Eater of Souls",
    "Eater of Worlds",
    "Enchanted Sword",
    "Eye of Cthulhu"

           }; num++;
           this.AtoZ.SectionNpcItems[num] = new string[] 
           {
                   "Family",
    "Fire Imp"
           }; num++;
           this.AtoZ.SectionNpcItems[num] = new string[] 
           {
               
    "Gastropod",
    "Giant Bat",
    "Giant Worm",
    "Goblin Archer",
    "Goblin Army",
    "Goblin Peon",
    "Goblin Scout",
    "Goblin Sorcerer",
    "Goblin Thief",
    "Goblin Warrior",
    "Goldfish",
    "Green Jellyfish"

           }; num++;
           this.AtoZ.SectionNpcItems[num] = new string[] 
           {
                   "Harpy",
    "Hellbat",
    "Hornet",
    "Hornet Family"

           }; num++;
           this.AtoZ.SectionNpcItems[num] = new string[] 
           {
                   "Illuminant Bat",
    "Illuminant Slime"
           }; num++;
           this.AtoZ.SectionNpcItems[num] = new string[] 
           {
               
    "Jellyfish",
    "Jungle Bat"
           }; num++;
           this.AtoZ.SectionNpcItems[num] = new string[] 
           {
                   "King Slime"
           }; num++;
           this.AtoZ.SectionNpcItems[num] = new string[] 
           {
               
    "Lava Slime",
    "Leech",
    "Light Mummy",
    "Little Eater",
    "Little Stinger"
           }; num++;
           this.AtoZ.SectionNpcItems[num] = new string[] 
           {
                   "Mage",

 
    "Man Eater",
    "Meteor Head",
    "Mimic",
 
    "Mummy"
           }; num++;
           this.AtoZ.SectionNpcItems[num] = new string[] 
           {
                   "Pinky",
    "Piranha",
    "Pixie",
    "Possessed Armor"
           }; num++;
           this.AtoZ.SectionNpcItems[num] = new string[] 
           {
                   "Servant of Cthulhu",
    "Shark",
    "Short Bones",
    "Skeleton",
    "Skeleton Archer",
    "Skeletron",
    "Slime Monsters",
    "Slimer",
    "Snatcher",
    "Spike Ball"

           }; num++;
           this.AtoZ.SectionNpcItems[num] = new string[] 
           {
               
    "The Groom",
    "The Hungry",
    "Tim",
    "Toxic Sludge"

           }; num++;
           this.AtoZ.SectionNpcItems[num] = new string[] 
           {
                   "Undead Miner",
    "Unicorn"
           }; num++;
           this.AtoZ.SectionNpcItems[num] = new string[] 
           {
                   "Voodoo Demon",
    "Vulture"
           }; num++;
           this.AtoZ.SectionNpcItems[num] = new string[] 
           {
                   "Wall of Flesh",
    "Werewolf",
    "Wraith",
    "Wyvern"
           }; num++;
           this.AtoZ.SectionNpcItems[num] = new string[] 
           {
                  "Zombie"
           }; num++;




           #endregion
           #endregion
       }
    }
}
