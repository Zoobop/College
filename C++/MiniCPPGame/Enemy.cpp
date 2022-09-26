#include "Enemy.h"

Enemy::Enemy(std::string _name, Attributes _attributes, Weapon _weapon, unsigned int _level, unsigned int _xp_dropped, unsigned int _gold)
{
	name = _name;
	attributes = _attributes;
	weapon = _weapon;
	level = _level;
	xp_dropped = _xp_dropped;
	gold = _gold;
}

unsigned int Enemy::getXPDrop()
{
	return xp_dropped;
}
