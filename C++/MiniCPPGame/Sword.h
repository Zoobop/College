#pragma once
#include "Weapon.h"
class Sword : public Weapon
{
public:
	Sword();
	Sword(std::string _name, unsigned int _max_damage, unsigned int _min_damage, unsigned int _crit_chance);

private:

};

