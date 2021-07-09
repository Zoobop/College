#include "Weapon.h"

Weapon::Weapon()
{
	Name = "New Weapon";
	Description = "This is a new weapon";
	MaxDamage = 0;
	MinDamage = 0;
	Value = 0;
}

void Weapon::ToString()
{
	std::cout << Name << " : " << MinDamage << " | " << MaxDamage << std::endl;
}

void Weapon::Sword()
{
	Name = "Sword";
	Description = "This is a new sword";
	MaxDamage = 10;
	MinDamage = 7;
	Value = 40;
}

void Weapon::Hammer()
{
	Name = "Hammer";
	Description = "This is a new hammer";
	MaxDamage = 13;
	MinDamage = 9;
	Value = 40;
}
