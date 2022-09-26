#pragma once
#include <iostream>
#include "PlayerCharacter.h"

class CharacterCreator
{
public:
	CharacterCreator();

	PlayerCharacter TransferPlayer();

private:
	void setCreatedPlayer(PlayerCharacter _player);

	PlayerCharacter created_player;
};

