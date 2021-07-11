#include "Equipment.h"

EEquipment::EEquipment()
{
	weapons = std::array<Weapon*, 2>();
	currentSize = 0;
	activeIndex = 0;
}

bool EEquipment::AddWeapon(Weapon* weapon)
{
	// Check if array is full
	if (IsFull())
		return false;

	// Check if weapon is valid
	if (!weapon)
		return false;

	// Add weapon to weapon array
	for (size_t i = 0; i < weapons.size(); i++) {
		if (weapons[i] == nullptr) {
			weapons[i] = weapon;
			std::cout << "Weapon successfully added!" << std::endl;
			currentSize++;
			return true;
		}
	}

	return false;
}

bool EEquipment::RemoveWeapon(Weapon* weapon)
{
	// Check if array is empty
	if (IsEmpty())
		return false;

	// Check if weapon is valid
	if (!weapon)
		return false;

	// Add weapon to weapon array
	for (size_t i = 0; i < currentSize; i++) {
		if (weapons[i] == weapon) {
			weapons[i] = nullptr;
			std::cout << "Weapon successfully removed!" << std::endl;
			currentSize--;
			return true;
		}
	}

	return false;
}

void EEquipment::NextWeapon()
{
	if (activeIndex == weapons.size() - 1)
		activeIndex = 0;
	else
		activeIndex++;

	DisplayWeapon();
}

void EEquipment::PreviousWeapon()
{
	if (activeIndex == 0)
		activeIndex = weapons.size() - 1;
	else
		activeIndex--;

	DisplayWeapon();
}

void EEquipment::DisplayWeapon()
{
	if (IsEmpty())
		return;

	weapons[activeIndex]->ToString();
}

bool EEquipment::IsFull()
{
	if (currentSize == weapons.size()) {
		std::cout << "You are already holding " << weapons.size() << " weapons!" << std::endl;
		return true;
	}
	return false;
}

bool EEquipment::IsEmpty()
{
	if (currentSize == 0) {
		std::cout << "You have no weapons!" << std::endl;
		return true;
	}
	return false;
}
