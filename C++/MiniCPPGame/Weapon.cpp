#include "Weapon.h"

Weapon::Weapon()
{
}

Weapon::Weapon(std::string _name, unsigned int _max_damage, unsigned int _min_damage, unsigned int _crit_chance)
{
	name = _name;
	max_damage = _max_damage;
	min_damage = _min_damage;
	crit_chance = _crit_chance;
}

std::string Weapon::getName()
{
	return name;
}

unsigned int Weapon::getDamage()
{
	return rand() % max_damage + min_damage;
}

unsigned int Weapon::getCrit()
{
	return crit_chance;
}
