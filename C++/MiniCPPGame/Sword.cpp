#include "Sword.h"

Sword::Sword()
{
}

Sword::Sword(std::string _name, unsigned int _max_damage, unsigned int _min_damage, unsigned int _crit_chance)
{
	name = _name;
	max_damage = _max_damage;
	min_damage = _min_damage;
	crit_chance = _crit_chance;
}
