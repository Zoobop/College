#pragma once

#include "Equipment.h"

enum struct ESpecies : int
{
	Human,
	Vhas,
	Maziini
};

enum struct EFaction : int
{
	None,
	Survivors,
	Overlords
};

class Entity
{
public:
	Entity();

	virtual const inline std::string& GetName() { return Name; }
	virtual void SetName(const char* newName);

	virtual inline EEquipment& GetEquipment() { return Equipment; }

	virtual void ToString() = 0;

	int number;

protected:
	std::string Name;
	ESpecies Species;
	EFaction Faction;
	EEquipment Equipment;
};

