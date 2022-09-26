#pragma once
#include "DuelManager.h"
#include "CharacterCreator.h"
#include "PlayerCharacter.h"
#include "Enemy.h"
#include "Weapons_Armor.h"
#include <iostream>

class GameManager
{
public:
	GameManager();

	PlayerCharacter getPlayer();
	void setPlayer(PlayerCharacter _player);

private:
	Weapons_Armor weapons_armor;
	PlayerCharacter player;
};

