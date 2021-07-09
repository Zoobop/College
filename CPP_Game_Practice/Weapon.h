#pragma once

#include <string>
#include <iostream>

class Weapon
{
public:
	Weapon();

	void ToString();

	void Sword();
	void Hammer();

protected:
	std::string Name;
	std::string Description;
	int MaxDamage;
	int MinDamage;
	int Value;
};

