#pragma once

#include "Weapon.h"
#include <array>

class EEquipment
{
public:
	EEquipment();

	bool AddWeapon(Weapon* weapon);
	bool RemoveWeapon(Weapon* weapon);

	void NextWeapon();
	void PreviousWeapon();

	void DisplayWeapon();

private:
	std::array<Weapon*, 2> weapons;
	size_t currentSize;
	size_t activeIndex;

	bool IsFull();
	bool IsEmpty();
};

