#include "PlayerCharacter.h"

PlayerCharacter::PlayerCharacter()
{
}

PlayerCharacter::PlayerCharacter(std::string _name, Attributes _attributes, Weapon _weapon, unsigned int _gold)
{
	name = _name;
	attributes = _attributes;
	weapon = _weapon;
	level = 1;
	max_xp = 100;
	current_xp = 0;
	gold = _gold;
}

PlayerCharacter::PlayerCharacter(std::string _name, Attributes _attributes, Weapon _weapon, unsigned int _level, unsigned int _max_xp, unsigned int _current_xp, unsigned int _gold)
{
	name = _name;
	attributes = _attributes;
	weapon = _weapon;
	level = _level;
	max_xp = _max_xp;
	current_xp = _current_xp;
	gold = _gold;
}
