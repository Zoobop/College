#pragma once
#include "Entity.h"
#include <string>

class Enemy : public Entity
{
public:
	Enemy(std::string _name, Attributes _attributes, Weapon _weapon, unsigned int _level, unsigned int _xp_dropped, unsigned int _gold);

	// Getters
	unsigned int getXPDrop();

private:
	unsigned int xp_dropped;
};