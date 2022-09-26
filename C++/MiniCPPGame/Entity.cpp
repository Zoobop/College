#include "Entity.h"

Entity::Entity()
{
}

Entity::Entity(std::string _name, Attributes _attributes, Weapon _weapon, unsigned int _level, unsigned int _gold)
{
	name = _name;
	attributes = _attributes;
	weapon = _weapon;
	level = _level;
	gold = _gold;

}

std::string Entity::getName()
{
	return name;
}

Attributes Entity::getAttributes()
{
	return attributes;
}

void Entity::setAttribute(unsigned int index, int value)
{
	switch (index) {
	case 0:
		attributes.changeHealth(value);
		break;
	case 1:
		attributes.changeStamina(value);
		break;
	case 2:
		attributes.setHealth(value);
		break;
	case 3:
		attributes.setStamina(value);
		break;
	}
}

Weapon Entity::getWeapon()
{
	return weapon;
}

unsigned int Entity::getLevel()
{
	return level;
}

unsigned int Entity::getGold()
{
	return gold;
}
