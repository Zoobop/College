#pragma once
#include "Entity.h"

class PlayerCharacter : public Entity
{
public:
	PlayerCharacter();
	PlayerCharacter(std::string _name, Attributes _attributes, Weapon _weapon, unsigned int _gold);
	PlayerCharacter(std::string _name, Attributes _attributes, Weapon _weapon, unsigned int _level, unsigned int _max_xp, unsigned int _current_xp, unsigned int _gold);

	unsigned int CalculateLevel();
	void LevelUp();

private:
	unsigned int max_xp;
	unsigned int current_xp;
};

