#pragma once
#include <string>

class Weapon
{
public:
	Weapon();
	Weapon(std::string _name, unsigned int _max_damage, unsigned int _min_damage, unsigned int _crit_chance);

	// Getters
	std::string getName();
	unsigned int getDamage();
	unsigned int getCrit();

protected:
	std::string name;
	unsigned int max_damage;
	unsigned int min_damage;
	unsigned int crit_chance;
};

