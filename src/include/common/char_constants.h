#pragma once

static const char* moveIcons[] = { "Fire", "Wind", "Earth", "Lightning", "Blizzard", "Water", "Explosion", "Forest", "Light", "Darkness", "Sword", "Staff", "Crossbow", "Dagger", "Poleaxe", "Chakram", "Red Shoe" };
static const char* targetEffects[] = { "NULL", "Restore HP(MEN)", "Restore MP", "Restore SP", "Ally Buff/Debuff", "Physical Damage(STR)", "Magical Damage(MAG)", "Enemy Buff/Debuff", "Status Change", "Unknown", "Unknown", "Unknown", "Unknown", "Unknown", "Unknown", "Special" };
static const char* targetTypes[] = { "NULL", "One Ally", "Area Allies", "All Allies", "One Enemy", "Area Enemies", "All Enemies", "Enemy Line", "Self", "Unknown", "Area Around Self", "Unknown", "Unknown", "Area Around Self", "Unknown", "Unknown" };
static const char* elements[] = { "Fire", "Wind", "Earth", "Lightning", "Blizzard" };
static const char* slotIDs[] = { "Slot 1", "Slot 2", "Slot 3", "Slot 4", "Slot 5", "Slot 6", "Slot 7", "Slot 8", "Slot 9", "Slot 10", "Slot 11", "Slot 12", "Slot 13", "Slot 14", "Slot 15", "Slot 16", "Slot 17", "Slot 18", "Slot 19", "Slot 20", "Slot 21", "Slot 22", "Slot 23", "Slot 24", "Slot 25", 
						"Slot 26", "Slot 27", "Slot 28", "Slot 29", "Slot 30", "Slot 31", "Slot 32", "Slot 33", "Slot 34", "Slot 35", "Slot 36" , "Slot 37", "Slot 38", "Slot 39", "Slot 40","Slot 41", "Slot 42", "Slot 43", "Slot 44", "Slot 45", "Slot 46", "Slot 47", "Slot 48", "Slot 49", "Slot 50", 
						"Slot 51", "Slot 52", "Slot 53", "Slot 54", "Slot 55", "Slot 56", "Slot 57", "Slot 58", "Slot 59", "Slot 60", "Slot 61" , "Slot 62", "Slot 63", "Slot 64", "Slot 65","Slot 66", "Slot 67", "Slot 68", "Slot 69", "Slot 70", "Slot 71", "Slot 72", "Slot 73", "Slot 74", "Slot 75",
						"Slot 76", "Slot 77", "Slot 78", "Slot 79", "Slot 80", "Slot 81", "Slot 82", "Slot 83", "Slot 84", "Slot 85", "Slot 86" , "Slot 87", "Slot 88", "Slot 89", "Slot 90","Slot 91", "Slot 92", "Slot 93", "Slot 94", "Slot 95", "Slot 96", "Slot 97", "Slot 98", "Slot 99", "Slot 100",	
						"Slot 101", "Slot 102", "Slot 103", "Slot 104", "Slot 105", "Slot 106", "Slot 107", "Slot 108", "Slot 109", "Slot 110", "Slot 111" , "Slot 112", "Slot 113", "Slot 114", "Slot 115","Slot 116", "Slot 117", "Slot 118", "Slot 119", "Slot 120", "Slot 121", "Slot 122", "Slot 123", "Slot 124", "Slot 125",
						"Slot 126" , "Slot 127", "Slot 128", "Slot 129", "Slot 130" };

static const char* entryIDs[] = { "NULL", "Item", "Item", "Perm. Stat Modifier(?)", "Weapon", "Armour", "Headgear", "Footwear", "Accessory", "Mana Egg", "Unknown", "Valuable", "Skillbook", "Special", "Unknown", "Unknown" };
static const char* effectiveOn[] = { "NULL", "Bird", "Bug/Crustacean", "Reptile", "Animal", "Humanoid", "Unknown", "Undead", "Valmar", "Unknown", "Unknown", "Unknown", "Unknown", "Unknown", "Unknown", "Unknown" };

static const char* bookIDs[] = { "NULL", "Adventure Book", "Book of Wizards", "Book of Warriors", "Book of Priests", "Book of Gales", "Book of Swords", "Book of War", "Book of Sages", "Book of Learning" };
static const char* eggIDs[] = { "NULL", "Holy Egg", "Chaos Egg", "Mist Egg", "Gravity Egg", "Soul Egg", "Star Egg", "Tutor Egg", "Change Egg", "Fairy Egg", "Dragon Egg" };
static const char* specialIDs[] = { "NULL", "Ryudo", "Elena", "Millenia", "Roan", "Tio", "Mareg", "Prince Roan", "NULL", "NULL", "NULL", "NULL", "NULL", "NULL" };
static const char* statIDs[] = { "Ryudo", "Elena", "Millenia", "Roan", "Tio", "Mareg", "Prince Roan", "NULL", "NULL", "NULL", "NULL", "NULL", "NULL" };

static const char* animationIDs[] = { "Physical Attack", "Burn!", "Burnflame", "Burnstrike", "Hellburner", "Howl", "Howlslash", "Howlnado", "Runner", "Shhh!", "Snooze", "Heal", "Healer", "Healer+", "Alhealer", "Resurrect", "Tremor", "Quake", "Diggin'", "Def-Loss", "Gravity", "BOOM!", "BA-BOOM!",
						"Meteor Strike", "WOW!", "Zap!", "Zap All", "GadZap", "DragonZap", "Stram", "Poizn", "Speedy", "Craze", "Cure", "Refresh", "Halvah", "Crackle", "Crackling", "Cold", "Freeze!", "Fiora", "unknown 1", "unknown 2", "unknown 3", "unknown 4", "unknown 5", "Alhealer",
						"unknown 7", "unknown 8", "Glitter/Confetti", "Alheal", "Healer(Teal)", "Healer+(Teal)", "Healer(Green)", "Alhealer(Green)", "unknown 15", "Bomb", "Vanish", "unknown17", "unknown 18", "unknown 19", "Tenseiken Slash", "Flying Tenseiken", "Purple Lightning", "Sky Dragon Slash",
						"unknown 20", "unknown 21", "Impact Bomb", "Nightmare Ball", "Droplets of Life", "White Apocalypse", "unknown 22", "unknown 23", "Arrow Shot", "Heel Crush", "Fallen Wings", "Starving Tongue", "Spellbinding Eye", "Grudging Claws", "Golden Hammer", "Snowball Fight!",
						"Dragon Rise", "Vitality March", "unknown 24", "unknown 25", "Lotus Flower", "Tornado", "Fast Dance-Whirl", "Whisper to Stars", "unknown 26", "unknown 27", "Beast-Fang Cut", "Beat-King Blast", "Beast-King Smash", "Lion's Roar", "unknown 28", "unknown 29", "Golden Hammer",
						"Ice Prison", "True Dragon Rise", "Vitality March", "Taint of Wind", "unknown 30", "unknown 31", "unknown 32", "unknown 33", "unknown 34", "unknown 35", "unknown 36", "unknown 37", "unknown 38", "unknown 39", "unknown 40", "unknown 41", "unknown 42", "unknown 43",
						"unknown 44", "unknown 45", "unknown 46", "unknown 47", "unknown 48", "unknown 49", "unknown 50", "unknown 51", "unknown 52", "unknown 53", "unknown 54", "unknown 55" };
