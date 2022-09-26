#pragma once
#include <string> 
#include "Attributes.h"
#include "Weapons_Armor.h"

class Entity
{
public:
	Entity();
	Entity(std::string _name, Attributes _attributes, Weapon _weapon, unsigned int _level, unsigned int _gold);

	// Getters & Setters
	std::string getName();
	Attributes getAttributes();
	void setAttribute(unsigned int index, int value);
	Weapon getWeapon();
	unsigned int getLevel();
	unsigned int getGold();

protected:
	std::string name;
	Attributes attributes;
	Weapon weapon;
	unsigned int level;
	unsigned int gold;
};