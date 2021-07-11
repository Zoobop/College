#include "Entity.h"

Entity::Entity()
{
	Name = "New Entity";
	Species = ESpecies::Human;
	Faction = EFaction::None;
	Equipment = EEquipment();

	number = 0;
}

void Entity::SetName(const char* newName)
{
	Name = newName;
}
