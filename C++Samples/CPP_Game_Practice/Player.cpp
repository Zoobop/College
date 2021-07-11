#include "Player.h"

Player::Player()
{
	Name = "New Player";
	Species = ESpecies::Human;
	Faction = EFaction::Survivors;
	Equipment = EEquipment();
}

void Player::ToString()
{
	std::cout << Name << " : " << number << std::endl;
}
